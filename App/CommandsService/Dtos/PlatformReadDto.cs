using CommandsService.Models;
using System.ComponentModel.DataAnnotations;

namespace CommandsService.Dtos
{
    public class PlatformReadDto
    {
        public int Id { get; set; }
        public int ExternalId { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<CommandReadDto> Commands { get; set; } = new List<CommandReadDto>();
    }
}
