#include <vector>
#include <thread>
#include <chrono>
#include <cstdlib>
#include <ctime>
#include <stdio.h>
#include <algorithm>
#include <stdexcept>

#include "Console.h"

enum class Direction {
    Up,
    Down,
    Left,
    Right
};

struct Position {
    int Top;
    int Left;
    Position() = default;
    Position(int top, int left) : Top(top), Left(left) {}
    bool operator==(const Position& other) const {
        return Top == other.Top && Left == other.Left;
    }
    Position RightBy(int n) const { return Position(Top, Left + n); }
    Position DownBy(int n) const { return Position(Top + n, Left); }
};

class IRenderable {
public:
    virtual void Render(Console* console) const = 0;
};

class Apple : public IRenderable {
public:
    Position position;

    Apple() = default;
    Apple(const Position& position) : position(position) {}

    void Render(Console* console) const override {
        console->SetCursorPosition(position.Left, position.Top);
        console->Write('A');
    }
    Position GetPosition() const {
        return position;
    }
};

class Snake : public IRenderable {
private:
    std::vector<Position> _body;
    int _growthSpurtsRemaining;
    bool _dead;

public:
    Snake(const Position& spawnLocation, int initialSize = 1)
        : _growthSpurtsRemaining(std::max(0, initialSize - 1)), _dead(false) {
        _body.push_back(spawnLocation);
    }

    bool IsDead() const { return _dead; }
    const Position& GetHead() const { return _body.front(); }
    std::vector<Position> GetBody() const { return {_body.begin()+1, _body.end()}; }

    void Move(Direction direction) {
        if (_dead) return;

        Position newHead;

        switch (direction) {
            case Direction::Up:
                newHead = GetHead().DownBy(-1);
                break;

            case Direction::Left:
                newHead = GetHead().RightBy(-1);
                break;

            case Direction::Down:
                newHead = GetHead().DownBy(1);
                break;

            case Direction::Right:
                newHead = GetHead().RightBy(1);
                break;

            default:
                throw std::out_of_range("Invalid direction");
        }

        if (std::find(_body.begin(), _body.end(), newHead) != _body.end() || !(newHead.Top >= 0 && newHead.Left >= 0)) {
            _dead = true;
            return;
        }

        _body.insert(_body.begin(), newHead);

        if (_growthSpurtsRemaining > 0) {
            _growthSpurtsRemaining--;
        } else {
            _body.pop_back();
        }
    }

    void Grow() {
        if (_dead) return;
        _growthSpurtsRemaining++;
    }

    void Render(Console* console) const override {
        console->SetCursorPosition(GetHead().Left, GetHead().Top);
        console->Write('*');

        for (const auto& position : GetBody()) {
            console->SetCursorPosition(position.Left, position.Top);
            console->Write('#');
        }
    }
};

class SnakeGame : public IRenderable {
private:
    static const Position Origin;

    Direction _currentDirection;
    Direction _nextDirection;
    Snake _snake;
    Apple _apple;

public:
    SnakeGame() : _snake(Origin, 5), _currentDirection(Direction::Right), _nextDirection(Direction::Right) {
        std::srand(static_cast<unsigned>(std::time(nullptr)));
        int numberOfRows = 20;
        int numberOfColumns = 20;
        int top = std::rand() % (numberOfRows + 1);
        int left = std::rand() % (numberOfColumns + 1);
        Position position(top, left);
        _apple = Apple(position);
    }

    bool IsGameOver() const { return _snake.IsDead(); }

    void OnKeyPress(char key) {
        Direction newDirection;

        switch (key) {
            case 'w':
                newDirection = Direction::Up;
                break;

            case 'a':
                newDirection = Direction::Left;
                break;

            case 's':
                newDirection = Direction::Down;
                break;

            case 'd':
                newDirection = Direction::Right;
                break;

            default:
                return;
        }

        Direction oppositeDirection;

        switch (_currentDirection) {
            case Direction::Up:
                oppositeDirection = Direction::Down;
                break;

            case Direction::Left:
                oppositeDirection = Direction::Right;
                break;

            case Direction::Down:
                oppositeDirection = Direction::Up;
                break;

            case Direction::Right:
                oppositeDirection = Direction::Left;
                break;

            default:
                throw std::out_of_range("Invalid direction");
        }

        if (newDirection != oppositeDirection) {
            _nextDirection = newDirection;
        }
    }

    void OnGameTick() {
        if (IsGameOver()) return;

        _currentDirection = _nextDirection;
        _snake.Move(_currentDirection);

        if (_snake.GetHead() == _apple.GetPosition()) {
            _snake.Grow();

            int numberOfRows = 20;
            int numberOfColumns = 20;
            int top = std::rand() % (numberOfRows + 1);
            int left = std::rand() % (numberOfColumns + 1);
            Position position(top, left);
            Apple apple(position);
            _apple = apple;
        }
    }

    void Render(Console* console) const override {
        _snake.Render(console);
        _apple.Render(console);
        console->Flush();
    }
};

const Position SnakeGame::Origin(0, 0);

int main() {
    SnakeGame snakeGame;
    Console console(80, 30);

    while (!snakeGame.IsGameOver()) {
        if (console.HasHit()) {
            char key = console.GetKey();
            snakeGame.OnKeyPress(key);
        }
        snakeGame.OnGameTick();
        console.Clear();
        snakeGame.Render(&console);
        std::this_thread::sleep_for(std::chrono::milliseconds(100));
    }

    // Allow time for user to weep before application exits.
    for (int i = 0; i < 3; i++) {
        console.Clear();
        std::this_thread::sleep_for(std::chrono::milliseconds(500));
        snakeGame.Render(&console);
        std::this_thread::sleep_for(std::chrono::milliseconds(500));
    }

    return 0;
}
