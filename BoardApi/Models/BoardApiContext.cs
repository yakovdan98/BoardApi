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


    
    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Group>()
        .HasData(
          new Group { GroupId = 1, Title = "Travel"},
          new Group { GroupId = 2, Title = "Gardening"}
        );
      builder.Entity<Message>()
        .HasData(
          new Message { MessageId = 1, Title = "Mexico", Body = "Test body Travel", Time = new DateTime (2023, 1, 17), GroupId = 1, RealName = "Johnny", PenName = "Jon"},
          new Message { MessageId = 2, Title = "Flowers", Body = "Test body Gardening", Time = new DateTime (2023, 1, 15), GroupId = 2, RealName = "Christopher", PenName = "Chris"}
        );
    }
  }
}