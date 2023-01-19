using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoardApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace BoardApi.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
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
        query = query.Where(entry => entry.Title.Contains(title));
      }

      if (body != null)
      {
        query = query.Where(entry => entry.Body.Contains(body));
      }

      // if (startDate != null && endDate != null)
      // { 
      //   query = query.Where(entry => (entry.Time >= startDate && entry.Time <= endDate));
      // }
      // if (startDate != null && endDate == null)
      // {
      //   query = query.Where(entry => (entry.Time >= startDate));
      // }
      if (startDate == null && endDate != null)
      {
        query = query.Where(entry => (entry.Time <= endDate));
      }
      if (penName != null)
      {
        query = query.Where(entry => entry.PenName.Contains(penName));
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

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Message message)
    {
      Message originalMessage = await _db.Messages.FindAsync(id);
      
      if (id != message.MessageId)
      {
        return BadRequest();
      }
      
      if (originalMessage.RealName != message.RealName)
      {
        return Unauthorized();
      }      

      _db.Messages.Update(message);

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!MessageExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    private bool MessageExists(int id)
    {
      return _db.Messages.Any(e => e.MessageId == id);
    }

    [HttpDelete ("{id}")]
    public async Task<IActionResult> DeleteMessage(int id)
    {
      Message message = await _db.Messages.FindAsync(id);
      if (message == null) 
      {
        return NotFound();
      }

      _db.Messages.Remove(message);
      await _db.SaveChangesAsync();

      return NoContent();
    }    
    
  }
}