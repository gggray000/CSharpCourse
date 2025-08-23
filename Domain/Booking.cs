namespace Domain
{
    public class Booking
    {
        public Booking(string email, int numberOfSeats)
        {
            this.Email = email;
            this.NumberOfSeats = numberOfSeats;
        }
        public string Email { get; set; }
        public int NumberOfSeats { get; set; }

    }
}