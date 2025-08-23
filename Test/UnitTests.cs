namespace Test;

using Xunit;
using Domain;
using FluentAssertions;

public class UnitTests
{
    [Fact]
    public void ResultOf2Plus2ShouleBe4()
    => new Calculator()
        .Sum(2, 2)
        .Should()
        .Be(4);

    [Fact]
    public void BookingReducesNumberOfSeats()
    {
        // Given
        var flight = new Flight(seatCapacity: 3);
        // When
        flight.Book("ggg@g.com", 1);
        // Then
        flight.RemainingSeats.Should().Be(2);
    }

    [Theory]
    [InlineData(5, 2, 3)]
    [InlineData(10, 10, 0)]
    [InlineData(100, 1, 99)]
    public void BookingReducesNumberOfSeats2(int seatCapacity, int numberOfSeats, int RemainingSeats)
    {
        var flight = new Flight(seatCapacity: seatCapacity);
        flight.Book("ggg@g.com", numberOfSeats);
        flight.RemainingSeats.Should().Be(RemainingSeats);
    }

    [Fact]
    public void AvoidOverBooking()
    {
        // Given
        var flight = new Flight(seatCapacity: 3);
        // When
        var error = flight.Book("ggg@g.com", 4);
        // Then
        error.Should().BeOfType<OverBookingError>();
    }

    [Fact]
    public void BookFlightSuccessfully()
    {
        var flight = new Flight(seatCapacity: 3);
        var error = flight.Book("ggg@g.com", 2);
        error.Should().BeNull();
    }

    [Fact]
    public void StoringBookingRecords()
    {
        var flight = new Flight(seatCapacity: 150);
        flight.Book(email: "g@g.com", numberOfSeats: 2);
        // Should().Contain() compares reference, this compares value.
        flight.BookingList.Should().ContainEquivalentOf(new Booking("g@g.com", 2));

    }

    [Theory]
    [InlineData(3, 1, 1, 3)]
    [InlineData(20, 20, 20, 20)]
    [InlineData(100, 2, 2, 100)]
    public void CancelingBooking(
        int seatCapacity,
        int bookedSeats,
        int canceledSeates,
        int RemainingSeats
    )
    {
        var flight = new Flight(seatCapacity: seatCapacity);
        flight.Book(email: "g@g.com", numberOfSeats: bookedSeats);
        flight.CancelBooking(email: "g@g.com", numberOfSeats: canceledSeates);
        flight.RemainingSeats.Should().Be(RemainingSeats);

    }

    [Fact]
    public void DoesntCancelBookingForPassengersWhoDidntBook()
    {
        var flight = new Flight(seatCapacity: 3);
        var error = flight.CancelBooking(email: "g@g.com", numberOfSeats: 1);
        error.Should().BeOfType<BookingNotFoundError>();
    }

    [Fact]
    public void ReturnNullWhenCancelSucceed()
    {
        var flight = new Flight(seatCapacity: 3);
        flight.Book(email: "g@g.com", numberOfSeats: 1);
        var error = flight.CancelBooking(email: "g@g.com", numberOfSeats: 1);
        error.Should().BeNull();
    }

    [Fact]
    public void BookingRecordDeletedAfterCancelation()
    {
        var flight = new Flight(seatCapacity: 3);
        flight.Book(email: "g@g.com", numberOfSeats: 1);
        flight.CancelBooking(email: "g@g.com", numberOfSeats: 1);
        flight.BookingList.Should().NotContainEquivalentOf(new Booking(email: "g@g.com", numberOfSeats: 1));
    }

    [Theory]
    [InlineData(10, 2, 3, 7)]
    [InlineData(10, 2, 1, 9)]
    [InlineData(2, 2, 1, 1)]
    public void ChangeBookingWithMoreSeats(
        int seatCapacity,
        int oldNumberOfSeats,
        int newNumberOfSeats,
        int RemainingSeats
        )
    {
        var flight = new Flight(seatCapacity: seatCapacity);
        flight.Book(email: "g@g.com", numberOfSeats: oldNumberOfSeats);
        flight.ChangeBooking(email: "g@g.com", newNumberOfSeats: newNumberOfSeats);
        flight.BookingList.Should().ContainEquivalentOf(new Booking(email: "g@g.com", numberOfSeats: newNumberOfSeats));
        flight.RemainingSeats.Should().Be(RemainingSeats);
    }

    [Fact]
    public void ChangeBookingWithOverbookingError()
    {
        // Given
        var flight = new Flight(seatCapacity: 3);
        flight.Book(email: "g@g.com", numberOfSeats: 2);
        // When
        var error = flight.ChangeBooking(email: "g@g.com", newNumberOfSeats: 4);
        // Then
        error.Should().BeOfType<OverBookingError>();
    }


}
