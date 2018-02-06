using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using awesome_site.Models;
using awesome_site.Cms.Pages;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace awesome_site.Controllers
{
  public class PageConroller : Controller
  {
    private const string PagesPath = @"./Cms/Pages/";
    private static Lazy<List<Page>> _pages = new Lazy<List<Page>>(() => GetAllPages().Result.ToList());

    private async static Task<Page[]> GetAllPages()
    {
      return await Task.WhenAll(
        Directory
          .EnumerateFiles(PagesPath, "*.yaml", SearchOption.AllDirectories)
          .Select(GetPageFromFilePath));
    }

    private async static Task<Page> GetPageFromFilePath(string filePath)
    {
      string fileContents = await System.IO.File.ReadAllTextAsync(filePath, System.Text.UTF8Encoding.UTF8));
      
      var page = new DeserializerBuilder()
        .WithNamingConvention(new CamelCaseNamingConvention())
        .Build()
        .Deserialize<Page>(fileContents);

      page.Content = await System.IO.File.ReadAllTextAsync(filePath.Substring(0, filePath.Length - ".yaml".Length) + ".html", System.Text.UTF8Encoding.UTF8);

      return page;
    }

    
    [Route("{*url}")]
    public IActionResult Index(string url)
    {
      var pages = _pages.Value;
      return View();
    }
  }
}
