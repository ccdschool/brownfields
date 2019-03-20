#include <iostream>
#include <sstream>
#include <sys/stat.h>
#include <fstream>


std::string tohex(int x, int w = 7, bool upper = false) {
    std::stringstream stream;
    stream << std::hex << (x & 0xFF);
    std::string result(stream.str());
    if (upper) {
        std::transform(result.begin(), result.end(), result.begin(), ::toupper);
    }
    int j = w - result.length();
    while (j-- > 0) { // zero padding
        result = '0' + result;
    }
    return result;
}

void print_xxd(std::string content, int w = 16, bool upper = false) {
    int len = content.size();
    int num = 0;
    std::string curline = "";
    int width = 9 + (w / 2) * 5 + 1;
    if (w & 1 == 1) {
        width += 3;
    }
    int cw = 1;
    int kk = 1;
    for (int i = 0; i < len ; i ++) {
        if ((i % w) == 0) {
            std::cout << tohex(num) << ": ";
            cw = 9;
        }
        char t = content[i];
        std::cout << tohex((int)t, 2, upper);
        cw += 2;
        curline += t;
        if ((i & 1) == kk) {
            std::cout << " ";
            cw += 1;
        }
        if ((i % w) == (w - 1)) {
            num += w;
            std::cout << "  ";
            cw += 2;
            int k = i - w + 1;
            for (int j = 0; j < w; j ++) {
                t = content[k ++];
                if ((int)t <= 32) { // non-printable characters
                    t = '.';
                }
                std::cout << t;
                cw ++;
            }
            std::cout << "\n\r";
            curline = "";
            cw = 0;
            if (w & 1 == 1) {
                kk = kk == 1 ? 0 : 1;
            }
        }
    }
    // remaining characters;
    int j = width - cw + 1;
    while (j-- > 0) {
        std::cout << " ";
    }
    for (int i = 0; i < curline.size(); i ++) {
        char t = content[i ++];
        if ((int)t < 32) { // non-printable characters
            t = '.';
        }
        std::cout << t;
    }
}

void help() {
    std::cout << "xxd:  make a hexdump" << std::endl;
    std::cout << "visit https://HelloACM.com" << std::endl;
    std::cout << "-u uppercase" << std::endl;
    std::cout << "-c column" << std::endl;
}

inline bool file_exists(const std::string& name) {
    struct stat buffer;
    return (stat (name.c_str(), &buffer) == 0);
}


std::string get_file_contents(const char *filename)
{
    std::ifstream in(filename, std::ios::in | std::ios::binary);
    if (in) {
        std::string contents;
        in.seekg(0, std::ios::end);
        contents.resize(in.tellg());
        in.seekg(0, std::ios::beg);
        in.read(&contents[0], contents.size());
        in.close();
        return(contents);
    }
    throw(errno);
}

void xxd(std::string filename, int w = 16, bool upper = false) {
    if (!file_exists(filename)) {
        std::cout << "xxd: " << filename << ": No such file or directory" << std::endl;
        return;
    }
    print_xxd(get_file_contents(filename.c_str()), w, upper);
}

int main(int argc, char ** argv) {
    bool upper = false;
    int w = 16;
    bool io = true;
    for (int i = 2; i <= argc; i ++) {
        std::string cur = argv[i - 1];
        if (cur == "-h") {
            help();
            return 0;
        }
        if (cur == "-u") {
            upper = true;
        }
        else if ((cur[0] == '-') && (cur.length() > 1)) {
            if (cur[1] == 'c') {
                if (cur.length() > 2) {
                    std::string num = cur.substr(2);
                    std::stringstream str(num);
                    int x;
                    str >> x;
                    if (str) {
                        if (x > 0) {
                            w = x;
                        }
                    }
                } else {
                    if (i != argc) {
                        std::string num = std::string(argv[i]);
                        std::stringstream str(num);
                        int x;
                        str >> x;
                        if (str) {
                            if (x > 0) {
                                w = x;
                            }
                        }
                        i ++;
                    }
                }
            }
        }
        else {
            xxd(cur, w, upper);
            io = false;
        }
    }
    if (io) {
        // standard input
        std::string line;
        std::string all = "";
        while (std::getline(std::cin, line))
        {
            all += line;
        }
        print_xxd(all, w, upper);
    }
    return 0;
}
