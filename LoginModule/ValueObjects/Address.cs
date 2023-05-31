using LoginModule.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LoginModule.ValueObjects {
    public class Address {
        [Key]
        [JsonIgnore]
        
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Number { get; set; }
        [StringLength(100)]
        public string? Street { get; set; }
        [StringLength(25)]
        public string? City { get; set; }
        [StringLength(2)]
        public string? State { get; set; }
        [StringLength(2)]
        public string? Country { get; set; }
        
        public Guid UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }

    }
}
