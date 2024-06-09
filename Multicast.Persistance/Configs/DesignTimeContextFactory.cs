using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Multicast.Persistance;

public class DesignTimeContextFactory : IDesignTimeDbContextFactory<Context>
{
    public Context CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<Context>();
        optionsBuilder.UseSqlite("Data Source=../multicast.db");

        return new Context(optionsBuilder.Options);
    }
}