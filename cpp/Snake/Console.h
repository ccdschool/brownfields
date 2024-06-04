#pragma once
#include <string>
#include <vector>

// Own simple console handling implementation to avoid extra library and be platform independent
struct Console
{
    Console(int width, int height);
    ~Console();

    void SetCursorPosition(int x, int y);
    void Write(char);
    void Flush() const;
    void Clear();
    bool HasHit();
    char GetKey();

private:
    int width;
    int height;
    int left;
    int top;
    std::vector<std::string> lines;
};

