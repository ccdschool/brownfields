cmake_minimum_required(VERSION 3.13)
project(Snake CXX)

add_library(Snake.Lib STATIC)
target_sources(Snake.Lib 
PRIVATE
	SnakeGame.cpp 
	SnakeGame.h 
	Console.cpp 
	Console.h
)

add_executable(Snake main.cpp)
target_link_libraries(Snake PRIVATE	Snake.Lib)

add_executable(Snake.Tests Snake.Tests.cpp)
target_link_libraries(Snake.Tests PRIVATE Snake.Lib)

AddGoogleTests(Snake.Tests)