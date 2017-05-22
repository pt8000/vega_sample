using System.ComponentModel.DataAnnotations;

namespace VegaApp.Resources
{
    public class ContactResource
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}