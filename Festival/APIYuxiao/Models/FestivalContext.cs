using System.Data.Entity;

namespace APIYuxiao.Models
{
    public class FestivalContext : DbContext
    {

        public FestivalContext() : base("name=FestivalContext")
        {
        }

        public DbSet<Artist> Artist { get; set; }

        public DbSet<Place> Places { get; set; }
    }
}