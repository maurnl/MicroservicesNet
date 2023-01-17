using System.ComponentModel.DataAnnotations;

namespace CommandsService.Dtos
{
    public class CommandReadDto
    {
        public int Id { get; set; }
        public string HowTo { get; set; }
        public string CommandLine { get; set; } = string.Empty;
        public int PlatformId { get; set; }
    }
}
