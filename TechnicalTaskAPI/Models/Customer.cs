using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TechnicalTaskAPI.Models
{
    public class Customer
    {
        public string Name { get; set; } = "";

        public Guid Id { get; set; }

        public string? Address { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        public string? TelephoneNumber { get; set; }

        public DateTime Created { get; set; }

        public bool Active { get; set; }

    }
}
