using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class BookingHelper_OverlappingBookingsExistTests
    {
        Booking booking;
        Mock<IBookingRepository> repositoryMock;

        [SetUp]
        public void SetUp()
        {
            booking = new Booking
            {
                Id = 2,
                ArrivalDate = ArriveOn(2017, 1, 15),
                DepartureDate = DepartOn(2017, 1, 20),
                Reference = "a"
            };

            repositoryMock = new Mock<IBookingRepository>();
            repositoryMock.Setup(r => r.GetActiveBookings(1)).Returns(new List<Booking> { booking }.AsQueryable());
        }

        [Test]
        public void BookingStartsAndFinishesBeforeAnExistingBooking_ReturnEmptyString()
        {
            string result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(booking.ArrivalDate, days: 2),
                DepartureDate = Before(booking.ArrivalDate)
            }, repositoryMock.Object);

            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void BookingStartsBeforeAndFinishedInTheMiddleOfExistingBooking_ReturnExistingBooking()
        {
            string result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(booking.ArrivalDate),
                DepartureDate = After(booking.ArrivalDate)
            }, repositoryMock.Object);

            Assert.That(result, Is.EqualTo(booking.Reference));
        }

        [Test]
        public void BookingStartsBeforeAndFinishesaAfterExistingBooking_ReturnExistingBooking()
        {
            string result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(booking.ArrivalDate),
                DepartureDate = After(booking.DepartureDate)
            }, repositoryMock.Object);

            Assert.That(result, Is.EqualTo(booking.Reference));
        }

        [Test]
        public void BookingStartsAndFinishesInTheMiddleOfAnExistingBooking_ReturnsExistingBooking()
        {
            string result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(booking.ArrivalDate),
                DepartureDate = Before(booking.DepartureDate)
            }, repositoryMock.Object);

            Assert.That(result, Is.EqualTo(booking.Reference));
        }

        [Test]
        public void BookingStartsInTheMiddleOfAnExistringBookingButFinishesAfter_ReturnExistingBooking()
        {
            string result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(booking.ArrivalDate),
                DepartureDate = After(booking.DepartureDate)
            }, repositoryMock.Object);

            Assert.That(result, Is.EqualTo(booking.Reference));
        }

        [Test]
        public void BookingStartsAndFinishesAfterAnExistingBooking_ReturnEmptyString()
        {
            string result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(booking.ArrivalDate),
                DepartureDate = After(booking.DepartureDate, days: 2)
            }, repositoryMock.Object);

            Assert.That(result, Is.EqualTo(booking.Reference));
        }

        [Test]
        public void BookingOverlapButNewBookingIsCancelled_ReturnEmptyString()
        {
            string result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(booking.ArrivalDate),
                DepartureDate = After(booking.DepartureDate),
                Status = "Cancelled"
            }, repositoryMock.Object);

            Assert.That(result, Is.EqualTo(string.Empty));
        }

        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddHours(-days);
        }

        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddHours(days);
        }

        private DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }

        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }
    }
}
