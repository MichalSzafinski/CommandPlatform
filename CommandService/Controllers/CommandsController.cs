using AutoMapper;
using CommandService.Data;
using CommandService.DTOs;
using CommandService.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace CommandService.Controllers
{
    [ApiController]
    [Route("api/c/platforms/{platformId}/[controller]")]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepository commandRepository;
        private readonly IMapper mapper;

        public CommandsController(ICommandRepository commandRepository, IMapper mapper)
        {
            this.commandRepository = commandRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int platformId)
        {
            Console.WriteLine($"--> Hit GetCommandsForPlatform: {platformId}");

            if (!this.commandRepository.PlatformExists(platformId))
            {
                return NotFound($"Platform {platformId} was not found");
            }

            var commands = this.commandRepository.GetCommandsForPlatform(platformId);

            return Ok(mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
        public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId, int commandId)
        {
            Console.WriteLine($"--> Hit GetCommandForPlatform: {platformId}, commandId: {commandId}");

            if (!this.commandRepository.PlatformExists(platformId))
            {
                return NotFound($"Platform {platformId} was not found");
            }

            var command = this.commandRepository.GetCommand(platformId, commandId);

            if (command == null)
            {
                return NotFound($"Command {commandId} was not found");
            }

            return Ok(mapper.Map<CommandReadDto>(command));
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommandForPlatform(int platformId, CommandCreateDto commandDto)
        {
            Console.WriteLine($"--> Hit CreateCommandForPlatform: {platformId}");

            if (commandDto == null)
            {
                return BadRequest();
            }

            if (!this.commandRepository.PlatformExists(platformId))
            {
                return NotFound($"Platform {platformId} was not found");
            }

            var command = mapper.Map<Command>(commandDto);

            commandRepository.CreateCommand(platformId, command);
            commandRepository.SaveChanges();

            var commandReadDto = mapper.Map<CommandReadDto>(command);

            return CreatedAtRoute(nameof(GetCommandForPlatform),
                new { platformId = platformId, commandId = commandReadDto.Id },
                commandReadDto);
        }
    }
}
