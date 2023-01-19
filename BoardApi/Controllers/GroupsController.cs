using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoardApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace BoardApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class GroupsController : ControllerBase
  {
    private readonly BoardApiContext _db;


    public GroupsController(BoardApiContext db)
    {
      _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Group>>> Get()
    {
      return await _db.Groups.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Group>> GetGroup(int id)
    {      
      Group group = await _db.Groups.Include(group => group.Messages).FirstOrDefaultAsync(group => group.GroupId == id);

      if (group == null)
      {
        return NotFound();
      }
      return group;
    }

    [HttpPost]
    public async Task<ActionResult<Group>> Post(Group group)
    {
      _db.Groups.Add(group);
      await _db.SaveChangesAsync();
      return CreatedAtAction(nameof(GetGroup), new { id = group.GroupId }, group);
    }

  }
}

