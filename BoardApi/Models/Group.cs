
namespace BoardApi.Models
{
  public class Group
  {
    public string Title { get; set; }
    public int GroupId { get; set; }
    public List<Message> Messages { get; set; }
  }
}