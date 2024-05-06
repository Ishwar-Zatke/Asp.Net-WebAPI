using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO
{
    public class AddRegionRequestDTO
    {
        [Required]
        [MinLength(2, ErrorMessage = "Code has to be minimum of 2 characters")]
        [MaxLength(4, ErrorMessage = "Code has to be maximum of 4 characters")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be Max of 100 characters")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
