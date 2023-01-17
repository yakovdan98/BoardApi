using System.Collections.Generic;
namespace BoardApi.Models;

public class Message 
{
  public int MessageId { get; set; }
  public string Title { get; set; }
  public string Body { get; set; }
  public DateTime Time { get; set; }
  public int GroupId { get; set; }
  public Group Group { get; set; }
  // public List<TagMessage> JoinEntities { get; set; }
  public string RealName { get; set; }
  public string PenName { get; set; }

}