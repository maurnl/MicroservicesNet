using CommandsService.Models;

namespace CommandsService.Data
{
    public class CommandRepository : ICommandRepository
    {
        private readonly AppDbContext _context;
        public CommandRepository(AppDbContext context)
        {
            _context = context;
        }
        public void CreateCommand(int platformId, Command command)
        {
            if(!PlatformExists(platformId))
            {
                throw new ArgumentException(nameof(platformId));
            }

            if(command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            command.PlatformId = platformId;

            _context.Commands.Add(command);
        }

        public void CreatePlatform(Platform platform)
        {
            if(platform is null)
            {
                throw new ArgumentNullException(nameof(platform));
            }

            _context.Platforms.Add(platform);
        }

        public IEnumerable<Platform> GetPlatforms()
        {
            return _context.Platforms.ToList();
        }
        public Command GetCommand(int platformId, int commandId)
        {
            return _context.Commands
                .Where(c => c.Id == commandId && c.PlatformId == platformId)
                .FirstOrDefault();
        }
        public IEnumerable<Command> GetCommandsByPlatform(int platformId)
        {
            return _context.Commands
                .Where(c => c.PlatformId == platformId)
                .OrderBy(c => c.Platform.Name);
        }

        public bool PlatformExists(int platformId)
        {
            return _context.Platforms.Any(p => p.Id == platformId);
        }

        public bool ExternalPlatformExists(int externalPlatformId)
        {
            return _context.Platforms.Any(p => p.ExternalId == externalPlatformId);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
