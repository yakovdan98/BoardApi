using Microsoft.EntityFrameworkCore;

namespace BoardApi.Models
{
  public class BoardApiContext : DbContext
  {
    public DbSet<Group> Groups { get; set; }
    public DbSet<Message> Messages { get; set; }
    // public DbSet<Tag> Tags { get; set;}
    // public DbSet<TagMessage> TagMessages { get; set;}
    

    public BoardApiContext(DbContextOptions<BoardApiContext> options) : base(options)
    {
    }


    //SEEDING THE DATABASE
    // protected override void OnModelCreating(ModelBuilder builder)
    // {
    //   builder.Entity<Group>()
    //     .HasData(
    //       new Group { GroupId = 1, Title = "Travel"}
    //       new Group { GroupId = 2, Title = "Gardening"}
    //     );
    // }
  }
}