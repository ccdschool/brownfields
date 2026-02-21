using NUnit.Framework;

namespace SmellyMarsRover.Tests
{
    public class RoverEqualityTest
    {
        [Test]
        public void Equal_Rovers()
        {
            Assert.That(new Rover(1, 1, "N"), Is.EqualTo(new Rover(1, 1, "N")));
        }

        [Test]
        public void Not_Equal_Rovers()
        {
            Assert.That(new Rover(1, 1, "N"), Is.Not.EqualTo(new Rover(1, 1, "S")));
            Assert.That(new Rover(1, 1, "N"), Is.Not.EqualTo(new Rover(1, 2, "N")));
            Assert.That(new Rover(1, 1, "N"), Is.Not.EqualTo(new Rover(0, 1, "N")));
        }
    }
}