using NUnit.Framework;

namespace SmellyMarsRover.Tests;

public class RoverPositionTests
{
    [Test]
    public void Facing_North_Move_Forward()
    {
        var rover = new Rover(0, 0, "N");

        rover.Receive("f");

        Assert.That(rover, Is.EqualTo(new Rover(0, 1, "N")));
    }

    [Test]
    public void Facing_North_Move_Backward()
    {
        var rover = new Rover(0, 0, "N");

        rover.Receive("b");

        Assert.That(rover, Is.EqualTo(new Rover(0, -1, "N")));
    }

    [Test]
    public void Facing_South_Move_Forward()
    {
        var rover = new Rover(0, 0, "S");

        rover.Receive("f");

        Assert.That(rover, Is.EqualTo(new Rover(0, -1, "S")));
    }

    [Test]
    public void Facing_South_Move_Backward()
    {
        var rover = new Rover(0, 0, "S");

        rover.Receive("b");

        Assert.That(rover, Is.EqualTo(new Rover(0, 1, "S")));
    }

    [Test]
    public void Facing_West_Move_Forward()
    {
        var rover = new Rover(0, 0, "W");

        rover.Receive("f");

        Assert.That(rover, Is.EqualTo(new Rover(-1, 0, "W")));
    }

    [Test]
    public void Facing_West_Move_Backward()
    {
        var rover = new Rover(0, 0, "W");

        rover.Receive("b");

        Assert.That(rover, Is.EqualTo(new Rover(1, 0, "W")));
    }

    [Test]
    public void Facing_East_Move_Forward()
    {
        var rover = new Rover(0, 0, "E");

        rover.Receive("f");

        Assert.That(rover, Is.EqualTo(new Rover(1, 0, "E")));
    }

    [Test]
    public void Facing_East_Move_Backward()
    {
        var rover = new Rover(0, 0, "E");

        rover.Receive("b");

        Assert.That(rover, Is.EqualTo(new Rover(-1, 0, "E")));
    }
}