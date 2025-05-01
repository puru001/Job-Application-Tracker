using System.ComponentModel.DataAnnotations;

namespace JobApplicationTracker.DTO
{
    public class JobApplicationUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public string Status { get; set; }
        public DateTime DateApplied { get; set; }
    }
}
