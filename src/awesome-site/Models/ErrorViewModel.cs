namespace awesome_site.Models
{
  public class ErrorViewModel
  {
    public string RequestId { get; set; }

    public bool ShowRequestId => !System.String.IsNullOrEmpty(RequestId);
  }
}