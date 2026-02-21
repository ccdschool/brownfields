using NUnit.Framework;

namespace SmellyMarsRover.Tests;

public class RoverRotationTests
{
    [Test]
    public void Facing_North_Rotate_Left()
    {
        var rover = new Rover(0, 0, "N");

        rover.Receive("l");

        Assert.That(rover, Is.EqualTo(new Rover(0, 0, "W")));
    }

    [Test]
    public void Facing_North_Rotate_Right()
    {
        var rover = new Rover(0, 0, "N");

        rover.Receive("r");

        Assert.That(rover, Is.EqualTo(new Rover(0, 0, "E")));
    }

    [Test]
    public void Facing_South_Rotate_Left()
    {
        var rover = new Rover(0, 0, "S");

        rover.Receive("l");

        Assert.That(rover, Is.EqualTo(new Rover(0, 0, "E")));
    }

    [Test]
    public void Facing_South_Rotate_Right()
    {
        var rover = new Rover(0, 0, "S");

        rover.Receive("r");

        Assert.That(rover, Is.EqualTo(new Rover(0, 0, "W")));
    }

    [Test]
    public void Facing_West_Rotate_Left()
    {
        var rover = new Rover(0, 0, "W");

        rover.Receive("l");

        Assert.That(rover, Is.EqualTo(new Rover(0, 0, "S")));
    }

    [Test]
    public void Facing_West_Rotate_Right()
    {
        var rover = new Rover(0, 0, "W");

        rover.Receive("r");

        Assert.That(rover, Is.EqualTo(new Rover(0, 0, "N")));
    }

    [Test]
    public void Facing_East_Rotate_Left()
    {
        var rover = new Rover(0, 0, "E");

        rover.Receive("l");

        Assert.That(rover, Is.EqualTo(new Rover(0, 0, "N")));
    }

    [Test]
    public void Facing_East_Rotate_Right()
    {
        var rover = new Rover(0, 0, "E");

        rover.Receive("r");

        Assert.That(rover, Is.EqualTo(new Rover(0, 0, "S")));
    }
}