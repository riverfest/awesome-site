using System.IO;

namespace awesome_site.Cms
{
  public abstract class SiteElementBase
  {
    public string Version { get; set; }
    public bool Active { get; set; }
    public string FileBaseName { get; private set; }
    private string _filePath;
    public string FilePath
    { 
      get => _filePath;
      set
      {
        _filePath = value;
        FileBaseName = Path.GetFileNameWithoutExtension(_filePath);
      }
    }
  }
}