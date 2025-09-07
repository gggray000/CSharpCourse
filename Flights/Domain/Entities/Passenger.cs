using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace Flights.Domain.Entities
{
    public record class Passenger
    {
        [Key]
        public string Email { get; init; }
        public string HashedPassword { get; set; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public bool Gender { get; init; }

        public Passenger(string email, string hashedPassword, string firstName, string lastName, bool gender)
        {
            this.Email = email;
            this.HashedPassword = hashedPassword;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Gender = gender;
        }
    };
    
    
}