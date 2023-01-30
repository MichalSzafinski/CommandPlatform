using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace PlatformService.DTOs
{
    [DataContract]
    public class PlatformReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Cost { get; set; }
    }
}
