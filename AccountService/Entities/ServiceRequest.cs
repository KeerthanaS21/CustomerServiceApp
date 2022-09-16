using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountService.Entities
{
    public class ServiceRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequestId { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [MaxLength(20)]
        public string RequestType { get; set; } = null!;

        [Required]
        [Column(TypeName = "VARCHAR")]
        [MaxLength(500)]
        public string RequestDetail { get; set; } = null!;

        [Required]
        [Column(TypeName = "VARCHAR")]
        [MaxLength(20)]
        public string RequestStatus { get; set; } = null!;

        [ForeignKey("ApplicationUser")]
        public string? RequestAssignedTo { get; set; }

        [Column(TypeName = "VARCHAR")]
        [MaxLength(200)]
        public string? RequestAssigneeComments { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [MaxLength(20)]
        public string RequestCreatedBy { get; set; } = null!;

        [Required]
        [Column(TypeName = "DATETIME")]
        public DateTime RequestCreatedDate  { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime? RequestClosedDate { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; } = null!;

    }
}
