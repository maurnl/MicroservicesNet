
using CommandsService.Models;
using CommandsService.SyncDataServices.Grpc;

namespace CommandsService.Data
{
    public static class DbInitializer
    {
        public static void Populate(IApplicationBuilder app)
        {
            using(var scope = app.ApplicationServices.CreateScope())
            {
                var grpcClient = scope.ServiceProvider.GetRequiredService<IPlatformDataClient>();

                var platforms = grpcClient.GetAllPlatforms();

                SeedData(scope.ServiceProvider.GetRequiredService<ICommandRepository>(), platforms);
            }
        }

        private static void SeedData(ICommandRepository repo, IEnumerable<Platform> platforms)
        {
            Console.WriteLine("-> Saving platforms to commands service...");
            foreach (var platform in platforms)
            {
                if(!repo.ExternalPlatformExists(platform.ExternalId))
                {
                    repo.CreatePlatform(platform);
                    repo.SaveChanges();
                }
            }
        }
    }
}
