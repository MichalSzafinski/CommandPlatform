using AutoMapper.Internal.Mappers;
using CommandService.Models;
using System;

namespace CommandService.Data
{
    public class CommandRepository : ICommandRepository
    {
        private readonly AppDbContext appDbContext;

        public CommandRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public void CreateCommand(int platformID, Command command)
        {
            if (command == null)
            { 
                throw new ArgumentNullException(nameof(command));
            }

            var platform = this.appDbContext.Platforms.FirstOrDefault(p => p.Id == platformID);

            if (platform == null)
            {
                throw new ArgumentException("--> Platform not found");
            }

            command.PlatformId = platformID;
            this.appDbContext.Commands.Add(command);
        }

        public void CreatePlatform(Platform platform)
        {
            if (platform == null)
            {
                throw new ArgumentNullException(nameof(platform));
            }

            this.appDbContext.Platforms.Add(platform);
        }

        public bool ExternalPlatformExists(int platformExternalId)
        {
            return this.appDbContext.Platforms.Any(p => p.ExternalId == platformExternalId);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return this.appDbContext.Platforms;
        }

        public Command? GetCommand(int platformID, int commandID)
        {
            return this.appDbContext.Commands.FirstOrDefault(c => c.PlatformId == platformID && c.Id == commandID);
        }

        public IEnumerable<Command> GetCommandsForPlatform(int platformID)
        {
            return this.appDbContext.Commands.Where(c => c.PlatformId == platformID).OrderBy(c => c.CommandLine);
        }

        public bool PlatformExists(int platformID)
        {
            return this.appDbContext.Platforms.Any(p => p.Id == platformID);
        }

        public bool SaveChanges()
        {
            return this.appDbContext.SaveChanges() >= 0;
        }
    }
}
