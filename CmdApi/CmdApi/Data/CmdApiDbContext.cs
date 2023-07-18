namespace CmdApi.Data
{
    using CmdApi.Models;
    using Microsoft.EntityFrameworkCore;

    public class CmdApiDbContext : DbContext
    {
        public CmdApiDbContext(DbContextOptions<CmdApiDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Command> Commands { get; set; }
    }
}
