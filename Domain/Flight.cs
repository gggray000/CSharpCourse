using System;

namespace Domain;

public class Flight
{
    public int RemainingSeats { get; set; }

    List<Booking> bookingList { get; set; } = new();
    public IEnumerable<Booking> BookingList => bookingList;

    public Flight(int seatCapacity)
    {
        this.RemainingSeats = seatCapacity;
    }

    public object? Book(string email, int numberOfSeats)
    {
        if (numberOfSeats > this.RemainingSeats)
        {
            return new OverBookingError();
        }
        this.RemainingSeats -= numberOfSeats;
        this.bookingList.Add(new Booking(email, numberOfSeats));
        return null;
    }

    public object? CancelBooking(string email, int numberOfSeats)
    {
        var bookingToRemove = bookingList
            .FirstOrDefault(b => b.Email == email);

        if (bookingToRemove == null)
        {
            return new BookingNotFoundError();
        }

        bookingList.Remove(bookingToRemove);
        this.RemainingSeats += numberOfSeats;
        return null;
    }

    public object? ChangeBooking(string email, int newNumberOfSeats)
    {
         var bookingToChange = bookingList
            .FirstOrDefault(b => b.Email == email);

        if (bookingToChange == null)
        {
            return new BookingNotFoundError();
        }

        var oldNumberOfSeats = bookingToChange.NumberOfSeats;

        if (this.RemainingSeats < newNumberOfSeats - oldNumberOfSeats)
        {
            return new OverBookingError();
        }

        this.RemainingSeats += oldNumberOfSeats;
        bookingList.Remove(bookingToChange);
        
        bookingList.Add(new Booking(email, newNumberOfSeats));
        this.RemainingSeats -= newNumberOfSeats;
        return null;
    }

}
