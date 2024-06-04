#include <iostream>
#include <fstream>
#include <iomanip>
#include <string>
#include <filesystem>

int main(int argc, char* argv[]) {
    if (argc != 2) {
        std::cerr << "Usage: hexdump <filename>" << std::endl;
        return 1;
    }

    std::ifstream input(argv[1], std::ios::binary);
    if (!input.is_open()) {
        std::cerr << "No such file: " << argv[1] << std::endl;
        return 2;
    }

    int position = 0;
    unsigned char buffer[16];
    uintmax_t inputLength = std::filesystem::file_size(argv[1]);

    while (position < inputLength) {
        input.read(reinterpret_cast<char*>(buffer), sizeof(buffer));
        int bytesRead = input.gcount();
        if (bytesRead > 0) {
            std::cout << std::hex << std::setw(4) << std::setfill('0') << position << ": ";
            position += bytesRead;

            for (int i = 0; i < 16; i++) {
                if (i < bytesRead) {
                    std::cout << std::hex << std::setw(2) << std::setfill('0') << static_cast<int>(buffer[i]) << " ";
                } else {
                    std::cout << "   ";
                }

                if (i == 7) {
                    std::cout << "-- ";
                }

                if (buffer[i] < 32 || buffer[i] > 250) {
                    buffer[i] = '.';
                }
            }

            std::cout << "  " << std::string(reinterpret_cast<char*>(buffer), bytesRead) << std::endl;
        }
    }

    return 0;
}
