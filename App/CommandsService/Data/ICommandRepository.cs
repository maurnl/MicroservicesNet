using CommandsService.Models;
using System.Collections.Generic;

namespace CommandsService.Data
{
    public interface ICommandRepository
    {
        bool PlatformExists(int platformId);

        void CreatePlatform(Platform platform);
        IEnumerable<PlatformID> GetPlatforms();
        bool SaveChanges();

        IEnumerable<Command> GetCommandsByPlatform(int platformId);
        Command GetCommand(int platform, int commandId);
        void CreateCommand(int platformId, Command command);
    }
}
