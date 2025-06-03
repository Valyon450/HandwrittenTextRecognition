using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public static class HTRDbInitializer
    {
        public static void Initialize(HTRDbContext context)
        {
            if (context.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
            {
                context.Database.Migrate();
            }

            // SeedDatabase(context);
        }

        public static void SeedDatabase(HTRDbContext context)
        {

        }
    }
}
