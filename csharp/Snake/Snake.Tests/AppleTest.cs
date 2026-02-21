using ApprovalTests;
using ApprovalTests.Reporters;
using JetBrains.Annotations;
using Xunit;

namespace Snake.Tests;

[TestSubject(typeof(Apple))]
public class AppleTest
{

    [Fact]
    public void TestApple()
    {
        var a = new Apple(new Position(0,0));
        
        // test something useful
        Assert.Equal(a.Position, new Position(0,0));
    }
    
    [UseReporter(typeof(DiffReporter))]
    [Fact]
    public void Approval()
    {
        var a = new Apple(new Position(0,0));
        
        // test something useful
        Approvals.Verify(a);
    }
}