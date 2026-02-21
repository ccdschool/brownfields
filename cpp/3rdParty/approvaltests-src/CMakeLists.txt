set (LIB_NAME approval_tests)
cmake_minimum_required(VERSION 3.13)
project(${LIB_NAME})

add_library(${LIB_NAME} INTERFACE)
target_sources(${LIB_NAME} INTERFACE approvaltests.hpp)
target_include_directories(${LIB_NAME} INTERFACE ${CMAKE_CURRENT_SOURCE_DIR})
set_target_properties(${LIB_NAME} PROPERTIES FOLDER "3rdParty")