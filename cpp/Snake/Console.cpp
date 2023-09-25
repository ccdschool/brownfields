#include "Console.h"
#include <iostream>

#include <cstdio>
#ifdef _WIN32
    #include <conio.h>
#else
    #include <unistd.h>
    #include <termios.h>
#endif

Console::Console(int w, int h)
    : width(w)
    , height(h)
    , left(0)
    , top(0)
    , lines(h, std::string(w, '\0'))
{
#ifndef _WIN32
    // Deaktivieren des Zeilenpuffermodus
    struct termios t;
    tcgetattr(STDIN_FILENO, &t);
    t.c_lflag &= ~(ICANON | ECHO);
    tcsetattr(STDIN_FILENO, TCSANOW, &t);
#endif
}

Console::~Console()
{
#ifndef _WIN32
    // Aktivieren des Zeilenpuffermodus
    struct termios t;
    tcgetattr(STDIN_FILENO, &t);
    t.c_lflag |= ICANON | ECHO;
    tcsetattr(STDIN_FILENO, TCSANOW, &t);
#endif
}

void Console::SetCursorPosition(int x, int y)
{
    left = x;
    top = y;
}

void Console::Write(char c)
{
    lines.at(top).at(left) = c;
    ++left;
}

void Console::Flush() const
{
    for(auto const& l : lines)
        std::cout << '\n' << l;
}

void Console::Clear()
{
#if defined _WIN32
    ::system("cls");
#else 
    ::system("clear");
#endif
    lines = std::vector<std::string>(height, std::string(width, ' '));
}

bool Console::HasHit()
{
#ifdef _WIN32
    return _kbhit();
#else
    return true;
#endif
}

char Console::GetKey()
{
#ifdef _WIN32
    return _getch();
#else
    char c;
    std::cin.get(c);
    return c;
#endif
}
