cmake_minimum_required(VERSION 3.13)
project(FormatCSV CXX)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_CXX_EXTENSIONS OFF)

add_executable(FormatCSV FormatCSV.cpp)
set_target_properties(FormatCSV PROPERTIES RUNTIME_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/bin)