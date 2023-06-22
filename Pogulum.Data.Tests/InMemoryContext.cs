namespace Pogulum.Data.Tests;

internal static class InMemoryContext
{
    internal static PogulumDbContext GetDbContext()
    {
        return new PogulumDbContextFactory()
                .CreateDbContext(PogulumDbContextFactory.TEST_ARGS);
    }
}