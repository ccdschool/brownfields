enable_testing()

# we use shared crt so force googletest to do also (default: static crt)
set(gtest_force_shared_crt ON CACHE BOOL "" FORCE)
# disable googletest and googlemock install step otherwise libs and header will also copied to program directory
set(INSTALL_GTEST OFF CACHE BOOL "" FORCE) 
set(INSTALL_GMOCK OFF CACHE BOOL "" FORCE)
add_subdirectory(3rdParty/googletest-src)
include(GoogleTest)

add_subdirectory(3rdParty/rapidcheck-src)
add_subdirectory(3rdParty/approvaltests-src)

function(AddGoogleTests target)
  target_link_libraries(${target} PRIVATE gmock_main)
  target_link_libraries(${target} PRIVATE rapidcheck_gtest)
  target_link_libraries(${target} PRIVATE approval_tests)
  gtest_discover_tests(${target})
endfunction()