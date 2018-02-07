using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using awesome_site.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using awesome_site.Cms.Pages;
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

    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

#region Markdown/Yaml parsing section
    private const string PagesPath = @"./Cms/Pages/";
    private static Lazy<List<Page>> _pages = new Lazy<List<Page>>(() => GetAllPages().Result.ToList());

    private async static Task<Page[]> GetAllPages()
    {
      return await Task.WhenAll(
        Directory
          .EnumerateFiles(PagesPath, "*.yaml", SearchOption.AllDirectories)
          .Select(path => GetPageFromFilePath(path, PagesPath)));
    }

    private async static Task<Page> GetPageFromFilePath(string filePath, string rootSearchPath)
    {
      string fileContents = await System.IO.File.ReadAllTextAsync(filePath, System.Text.UTF8Encoding.UTF8);
      
      var page = new DeserializerBuilder()
        .WithNamingConvention(new CamelCaseNamingConvention())
        .Build()
        .Deserialize<Page>(fileContents);

      page.FileBaseName = Path.GetFileNameWithoutExtension(filePath);
      page.Content = await System.IO.File.ReadAllTextAsync(filePath.Substring(0, filePath.Length - ".yaml".Length) + ".html", System.Text.UTF8Encoding.UTF8);

      page.Section = System.IO.Path.GetDirectoryName(filePath).Substring(rootSearchPath.Length);

      return page;
    }
  }
  #endregion
}
