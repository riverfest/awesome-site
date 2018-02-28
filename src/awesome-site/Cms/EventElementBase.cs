using System;

namespace awesome_site.Cms
{
  public abstract class EventElementBase : SiteElementBase
  {
    public bool PublicStartTime { get; set; }
    public DateTime EventStartTime { get; set; }
    public DateTime EventEndTime { get; set; }

  }
}