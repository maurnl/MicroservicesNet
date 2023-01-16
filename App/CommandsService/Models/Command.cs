﻿using System.ComponentModel.DataAnnotations;

namespace CommandsService.Models
{
    public class Command
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int HowTo { get; set; }
        [Required]
        public string CommandLine { get; set; } = string.Empty;
        [Required]
        public int PlatformId { get; set; }
        public Platform Platform { get; set; }
    }
}
