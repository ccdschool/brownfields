option(RC_ENABLE_GMOCK "Build Google Mock integration" OFF)
if (RC_ENABLE_GMOCK OR RC_INSTALL_ALL_EXTRAS)
  add_subdirectory(gmock)
endif()

add_subdirectory(gtest)
add_subdirectory(catch)