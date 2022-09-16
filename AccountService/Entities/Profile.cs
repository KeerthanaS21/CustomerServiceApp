using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountService.Entities
{
    public class Profile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProfileId { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [Column(TypeName = "VARCHAR")]
        [MaxLength(250)]
        public string? LastName { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Dob { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [MaxLength(50)]
        public string IdType { get; set; } = null!;

        [Required]
        [Column(TypeName = "VARCHAR")]
        [MaxLength(50)]
        public string IdValue { get; set; } = null!;

        [ForeignKey("ApplicationUser")]
        public string UserRefId { get; set; } = null!;

        public virtual ApplicationUser ApplicationUser { get; set; } = null!;
    }
}
