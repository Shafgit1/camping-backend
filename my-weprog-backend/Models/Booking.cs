using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace my_weprog_backend.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("users")]
        public int UserId { get; set; }

        [Required]
        [ForeignKey("camping")]
        public int CampingId { get; set; }

        [Required]
        public DateTime DateFrom { get; set; }

        [Required]
        public DateTime DateTo { get; set; }

        public string CampingType { get; set; }
        public decimal CampingPrice { get; set; }
        public string? CampingName { get; set; }
    }
}
