using CommandService.Models;

namespace CommandService.Data
{
    public interface ICommandRepository
    {
        bool SaveChanges();

        //Platforms
        IEnumerable<Platform> GetAllPlatforms();
        void CreatePlatform(Platform platformID);
        bool PlatformExists(int platformID);
        bool ExternalPlatformExists(int platformExternalId);

        //Commands
        IEnumerable<Command> GetCommandsForPlatform(int platformID);
        Command? GetCommand(int platformID, int commandID);
        void CreateCommand(int platformID, Command command);

    }
}
