using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class PogulumDbContextFactory : IDesignTimeDbContextFactory<PogulumDbContext>
{
    public static readonly string[] DEV_ARGS = new string[] { "--environment", "development" };

    public static readonly string[] PROD_ARGS = new string[] { "--environment", "production" };

    public static readonly string[] TEST_ARGS = new string[] { "--environment", "testing" };

    public PogulumDbContext CreateDbContext(string[] args)
    {
        int environmentIndex = Array.IndexOf(args, "--environment") + 1;
        string? environment = args.ElementAtOrDefault(environmentIndex);

        var optionsBuilder = new DbContextOptionsBuilder<PogulumDbContext>();

        switch (environment)
        {
            case "testing":
                optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
                break;
            case "development":
                optionsBuilder.UseSqlServer("Server=.; Database=Pogulum; Trusted_Connection=True; Trust Server Certificate=True;");
                break;
            case "production":
                optionsBuilder.UseSqlServer("something else");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(args), "The --environment option must be either \"testing\", \"development\", or \"production\"!");
        }

        return new PogulumDbContext(optionsBuilder.Options);
    }

}