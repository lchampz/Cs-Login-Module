using LoginModule.Enums;
using LoginModule.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LoginModule.Entities {
    [Index(nameof(Email), IsUnique = true)]
    public class User {

        [JsonIgnore]
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(20)]
        public string? FirstName { get; set; }
        [Required]
        [StringLength(80)]
        public string? LastName { get; set; }
        [Required]
        [StringLength(35)]

        public string? Email { get; set; }
        [Required]
        [StringLength(90)]
        public string? Password { get; set; }
        
        public EnumUserStatus? Status { get; set; } = EnumUserStatus.Inativo;
        public DateTime? RegisterDate { get; set; } = DateTime.Now;
        [JsonIgnore]
        public DateTime? ModifiedDate { get; set; } = null;
        [JsonIgnore]
        public ICollection<Address>? Addresses { get; set; }

    }
}
