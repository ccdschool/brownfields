cmake_minimum_required(VERSION 3.13)
project(brownfield CXX)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_CXX_EXTENSIONS OFF)
set_property(GLOBAL PROPERTY USE_FOLDERS ON)

list(APPEND CMAKE_MODULE_PATH "${CMAKE_SOURCE_DIR}/CMake")
enable_testing()
include(Testing)

add_subdirectory(FormatCSV)
add_subdirectory(hexdump)
add_subdirectory(hexdump_Aligned)
add_subdirectory(Snake)
add_subdirectory(TicTacToe)
