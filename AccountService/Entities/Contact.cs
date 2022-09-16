using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountService.Entities
{
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactId { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [MaxLength(50)]
        public string UserName { get; set; } = null!;

        [Required]
        [Column(TypeName = "VARCHAR")]
        [MaxLength(250)]
        public string Address { get; set; } = null!;

        [Required]
        [Column(TypeName = "VARCHAR")]
        [MaxLength(50)]
        public string City { get; set; } = null!;

        [Required]
        [Column(TypeName = "VARCHAR")]
        [MaxLength(50)]
        public string State { get; set; } = null!;

        [Required]
        [Column(TypeName = "VARCHAR")]
        [MaxLength(50)]
        public string Country { get; set; } = null!;

        [Required]
        [Column(TypeName = "VARCHAR")]
        [MaxLength(50)]
        public string ContactPreference { get; set; } = null!;

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(10)]
        public string PhoneNo { get; set; } = null!;

        [ForeignKey("ApplicationUser")]
        public string UserRefId { get; set; } = null!;

        public virtual ApplicationUser ApplicationUser { get; set; } = null!;
    }
}
