cmake_minimum_required(VERSION 3.13)
project(hexdump)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_CXX_EXTENSIONS OFF)

add_executable(hexdump main.cpp)
set_target_properties(hexdump PROPERTIES RUNTIME_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/bin)
file(COPY example.bin DESTINATION ${CMAKE_BINARY_DIR}/bin)