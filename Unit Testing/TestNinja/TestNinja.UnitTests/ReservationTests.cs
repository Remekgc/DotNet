using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class ReservationTests
    {
        // Naming convention: MethodName_Scenario_ExpectedBehavior

        [Test]
        public void CanBeCancelledBy_UserIsAdmin_ReturnsTrue()
        {
            // Arrange
            Reservation reservation = new Reservation();

            // Act
            bool result = reservation.CanBeCancelledBy(new User { IsAdmin = true });

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CanBeCancelledBy_SameUserCancellingTheReservation_ReturnsTrue()
        {
            // Arrange
            User user = new User();
            Reservation reservation = new Reservation { MadeBy = user };

            // Act
            bool result = reservation.CanBeCancelledBy(user);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CanBeCancelledBy_AnotherUserCancellingTheReservation_ReturnsFalse()
        {
            // Arrange
            User user = new User();
            Reservation reservation = new Reservation { MadeBy = user };

            // Act
            bool result = reservation.CanBeCancelledBy(new User());

            // Assert
            Assert.That(result, Is.False);
        }
    }
}
