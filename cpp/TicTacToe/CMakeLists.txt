cmake_minimum_required(VERSION 3.13)
project(TicTacToe CXX)

add_library(TicTacToe.Lib STATIC)
target_sources(TicTacToe.Lib 
PRIVATE
	TicTacToe.cpp 
	TicTacToe.h
)

add_executable(TicTacToe main.cpp)
target_link_libraries(TicTacToe.Tests PRIVATE TicTacToe.Lib)

add_executable(TicTacToe.Tests TicTacToe.Tests.cpp)
target_link_libraries(TicTacToe.Tests PRIVATE TicTacToe.Lib)
AddGoogleTests(TicTacToe.Tests)