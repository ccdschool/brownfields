using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var tickRate = TimeSpan.FromMilliseconds(100);
        var snakeGame = new SnakeGame();

        using (var cts = new CancellationTokenSource())
        {
            async Task MonitorKeyPresses()
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey(intercept: true).Key;
                        snakeGame.OnKeyPress(key);
                    }

                    await Task.Delay(10);
                }
            }

            var monitorKeyPresses = MonitorKeyPresses();

            do
            {
                snakeGame.OnGameTick();
                snakeGame.Render();
                await Task.Delay(tickRate);
            } while (!snakeGame.GameOver);

            // Allow time for user to weep before application exits.
            for (var i = 0; i < 3; i++)
            {
                Console.Clear();
                await Task.Delay(500);
                snakeGame.Render();
                await Task.Delay(500);
            }

            cts.Cancel();
            await monitorKeyPresses;
        }
    }
}

enum Direction
{
    Up,
    Down,
    Left,
    Right
}

interface IRenderable
{
    void Render();
}

readonly struct Position
{
    public Position(int top, int left)
    {
        Top = top;
        Left = left;
    }
    public int Top { get; }
    public int Left { get; }

    public Position RightBy(int n) => new Position(Top, Left + n);
    public Position DownBy(int n) => new Position(Top + n, Left);
}

class Apple : IRenderable
{
    public Apple(Position position)
    {
        Position = position;
    }

    public Position Position { get; }

    public void Render()
    {
        Console.SetCursorPosition(Position.Left, Position.Top);
        Console.Write("🍏");
    }
}

class Snake : IRenderable
{
    private List<Position> _body;
    private int _growthSpurtsRemaining;

    public Snake(Position spawnLocation, int initialSize = 1)
    {
        _body = new List<Position> { spawnLocation };
        _growthSpurtsRemaining = Math.Max(0, initialSize - 1);
        Dead = false;
    }

    public bool Dead { get; private set; }
    public Position Head => _body.First();
    private IEnumerable<Position> Body => _body.Skip(1);

    public void Move(Direction direction)
    {
        if (Dead) throw new InvalidOperationException();

        Position newHead;

        switch (direction)
        {
            case Direction.Up:
                newHead = Head.DownBy(-1);
                break;

            case Direction.Left:
                newHead = Head.RightBy(-1);
                break;

            case Direction.Down:
                newHead = Head.DownBy(1);
                break;

            case Direction.Right:
                newHead = Head.RightBy(1);
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }

        if (_body.Contains(newHead) || !(newHead.Top >= 0 && newHead.Left >= 0))
        {
            Dead = true;
            return;
        }

        _body.Insert(0, newHead);

        if (_growthSpurtsRemaining > 0)
        {
            _growthSpurtsRemaining--;
        }
        else
        {
            _body.RemoveAt(_body.Count - 1);
        }
    }

    public void Grow()
    {
        if (Dead) throw new InvalidOperationException();

        _growthSpurtsRemaining++;
    }

    public void Render()
    {
        Console.SetCursorPosition(Head.Left, Head.Top);
        Console.Write("◉");

        foreach (var position in Body)
        {
            Console.SetCursorPosition(position.Left, position.Top);
            Console.Write("■");
        }
    }
}

class SnakeGame : IRenderable
{
    private static readonly Position Origin = new Position(0, 0);

    private Direction _currentDirection;
    private Direction _nextDirection;
    private Snake _snake;
    private Apple _apple;

    public SnakeGame()
    {
        _snake = new Snake(Origin, initialSize: 5);
        
        const int numberOfRows = 20;
        const int numberOfColumns = 20;

        var random = new Random();
        var top = random.Next(0, numberOfRows + 1);
        var left = random.Next(0, numberOfColumns + 1);
        var position = new Position(top, left);
        var apple = new Apple(position);

        _apple = apple;
        _currentDirection = Direction.Right;
        _nextDirection = Direction.Right;
    }

    public bool GameOver => _snake.Dead;

    public void OnKeyPress(ConsoleKey key)
    {
        Direction newDirection;

        switch (key)
        {
            case ConsoleKey.W:
                newDirection = Direction.Up;
                break;

            case ConsoleKey.A:
                newDirection = Direction.Left;
                break;

            case ConsoleKey.S:
                newDirection = Direction.Down;
                break;

            case ConsoleKey.D:
                newDirection = Direction.Right;
                break;

            default:
                return;
        }

        // Snake cannot turn 180 degrees.
        Direction ret;
        switch (_currentDirection)
        {
            case Direction.Up: ret = Direction.Down;
                break;
            case Direction.Left: ret = Direction.Right;
                break;
            case Direction.Right: ret = Direction.Left;
                break;
            case Direction.Down: ret = Direction.Up;
                break;
            default: throw new ArgumentOutOfRangeException();
        }

        if (newDirection == ret)
        {
            return;
        }

        _nextDirection = newDirection;
    }

    public void OnGameTick()
    {
        if (GameOver) throw new InvalidOperationException();

        _currentDirection = _nextDirection;
        _snake.Move(_currentDirection);

        // If the snake's head moves to the same position as an apple, the snake
        // eats it.
        if (_snake.Head.Equals(_apple.Position))
        {
            _snake.Grow();
            
            const int numberOfRows = 20;
            const int numberOfColumns = 20;

            var random = new Random();
            var top = random.Next(0, numberOfRows + 1);
            var left = random.Next(0, numberOfColumns + 1);
            var position = new Position(top, left);
            var apple = new Apple(position);

            _apple = apple;
        }
    }

    public void Render()
    {
        Console.Clear();
        _snake.Render();
        _apple.Render();
        Console.SetCursorPosition(0, 0);
    }
}