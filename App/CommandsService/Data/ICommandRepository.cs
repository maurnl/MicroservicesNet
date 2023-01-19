using CommandsService.Models;
using System.Collections.Generic;

namespace CommandsService.Data
{
    public interface ICommandRepository
    {
        bool PlatformExists(int platformId);

        void CreatePlatform(Platform platform);
        IEnumerable<Platform> GetPlatforms();
        bool ExternalPlatformExists(int externalPlatformId);
        bool SaveChanges();

        IEnumerable<Command> GetCommandsByPlatform(int platformId);
        Command GetCommand(int platformId, int commandId);
        void CreateCommand(int platformId, Command command);

    }
}
