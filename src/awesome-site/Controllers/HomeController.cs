using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using awesome_site.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using awesome_site.Cms;
using awesome_site.Cms.Pages;
using awesome_site.Cms.Events;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System;
using System.Collections.Generic;

namespace awesome_site.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }

    [Route("page/{*suburl}")]
    public IActionResult Index(string suburl)
    {
      var page = _pages.Value.FirstOrDefault(p => p.FullPath == suburl.ToLowerInvariant());
      if (page == null) return NotFound();
      ViewBag.Title = page.Title;
      return View("Content", page);
    }

    [Route("events")]
    public IActionResult Events()
    {
      ViewBag.Title = "Events";
      return View("Events", _events.Value);
    }

    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

#region Markdown/Yaml parsing section
    private const string PagesPath = @"./Cms/Pages/";
    private const string EventsPath = @"./Cms/Events/";
    private static Lazy<List<Page>> _pages = new Lazy<List<Page>>(() => GetAllPages().Result.ToList());
    private static Lazy<List<Event>> _events = new Lazy<List<Event>>(() => GetAllEvents().Result.ToList());

    
    private async static Task<Page[]> GetAllPages()
    {
      var pages = GetElementsFromFilePath<Page>(PagesPath);
      return await Task.WhenAll(
        pages.Select(async p => {
          var page = await p;
          page.ContentAsHtml = CommonMark.CommonMarkConverter.Convert(page.Content);
          page.Section = System.IO.Path.GetDirectoryName(page.FilePath).Substring(PagesPath.Length);
          return page;
        }));
    }

    private async static Task<Event[]> GetAllEvents()
    {
      var events = GetElementsFromFilePath<Event>(EventsPath);
      return await Task.WhenAll(
        events.Select(async e => {
          var eventResult = await e;
          eventResult.ProgramContentAsHtml = CommonMark.CommonMarkConverter.Convert(eventResult.ProgramContent);
          return eventResult;
        }));
    }

    private static IEnumerable<Task<T>> GetElementsFromFilePath<T>(string rootSearchPath) where T : SiteElementBase, new()
    {
      return Directory
          .EnumerateFiles(rootSearchPath, "*.yaml", SearchOption.AllDirectories)
          .Select(async path => await GetElementFromFilePath<T>(path, rootSearchPath));
    }

    private async static Task<T> GetElementFromFilePath<T>(string filePath, string rootSearchPath) where T : SiteElementBase, new()
    {
      string fileContents = await System.IO.File.ReadAllTextAsync(filePath, System.Text.UTF8Encoding.UTF8);
      
      var element = new DeserializerBuilder()
        .WithNamingConvention(new CamelCaseNamingConvention())
        .Build()
        .Deserialize<T>(fileContents);
      
      element.FilePath = filePath;

      return element;
    }
  }
  #endregion
}
