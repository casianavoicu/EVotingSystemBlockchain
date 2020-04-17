using System.ComponentModel.DataAnnotations;

namespace EVotingSystem.Application.Model
{
    public class CreatePartyModel
    {
        [Required]
        [MaxLength(12)]
        public string Name { get; set; }
    }
}
