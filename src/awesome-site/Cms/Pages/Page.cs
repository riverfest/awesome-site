namespace awesome_site.Cms.Pages
{
  public class Page : SiteElementBase
  {
    public string Title { get; set; }
    public string Header { get; set; }
    public string Section { get; set; }
    public string Content { get; set; }

    public string FullPath => $"{Section}/{FileBaseName}";
  }
}