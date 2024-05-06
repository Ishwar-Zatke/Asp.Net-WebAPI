using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO
{
    public class UpdateWalkRequestDTO
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be Max of 100 characters")]
        public string Name { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Description has to be Max of 100 characters")]
        public string Description { get; set; }

        [Required]
        [Range(0, 50)]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        [Required]
        public Guid DifficultyId { get; set; }

        [Required]
        public Guid RegionId { get; set; }
    }
}
