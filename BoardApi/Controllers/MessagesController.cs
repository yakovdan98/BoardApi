using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoardApi.Models;

namespace BoardApi.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  public class MessagesController : ControllerBase
  {
    private readonly BoardApiContext _db;
    public MessagesController(BoardApiContext db)
    {
      _db = db;
    }

    [HttpGet]
    public async Task<List<Message>> Get(string title, string body, DateTime startDate, DateTime endDate, string penName)
    {
      IQueryable<Message> query = _db.Messages.AsQueryable();

      if (title != null)
      {
        query = query.Where(entry => entry.Title == title);
      }

      if (body != null)
      {
        query = query.Where(entry => entry.Body == body);
      }

      if (startDate != null && endDate != null)
      {
        query = query.Where(entry => (entry.Time >= startDate && entry.Time <= endDate));
      }
      if (startDate != null && endDate == null)
      {
        query = query.Where(entry => (entry.Time >= startDate));
      }
      if (startDate == null && endDate != null)
      {
        query = query.Where(entry => (entry.Time <= endDate));
      }
      if (penName != null)
      {
        query = query.Where(entry => entry.PenName == penName);
      }
      return await query.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Message>> GetMessage(int id)
    {
      Message message = await _db.Messages.FindAsync(id);
      if (message == null)
      {
        return NotFound();
      }
      return message;
    }

    [HttpPost]
    public async Task<ActionResult<Message>> Post([FromBody] Message message)
    {
      _db.Messages.Add(message);
      await _db.SaveChangesAsync();
      return CreatedAtAction(nameof(GetMessage), new { id = message.MessageId }, message);
    }
  }
}