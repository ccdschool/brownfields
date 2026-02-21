using NUnit.Framework;

namespace SmellyMarsRover.Tests;

public class RoverReceivingCommandsListTests
{
    [Test]
    public void No_Commands()
    {
        var rover = new Rover(0, 0, "N");

        rover.Receive("");

        Assert.That(rover, Is.EqualTo(new Rover(0, 0, "N")));
    }

    [Test]
    public void Two_Commands()
    {
        var rover = new Rover(0, 0, "N");

        rover.Receive("lf");

        Assert.That(rover, Is.EqualTo(new Rover(-1, 0, "W")));
    }

    [Test]
    public void Many_Commands()
    {
        var rover = new Rover(0, 0, "N");

        rover.Receive("ffrbbrfflff");

        Assert.That(rover, Is.EqualTo(new Rover(0, 0, "E")));
    }
}