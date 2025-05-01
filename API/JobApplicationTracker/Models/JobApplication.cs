using System.ComponentModel.DataAnnotations;

namespace JobApplicationTracker.Models
{    
    public class JobApplication
    {
        [Key]
        public int Id { get; set; }
        
        public string CompanyName { get; set; }
        
        public string Position { get; set; }
        
        public string Status { get; set; }
        
        public DateTime DateApplied { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
