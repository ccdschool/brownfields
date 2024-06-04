#include <string>
#include <vector>
#include <iostream>
#include <sstream>
using namespace std;
namespace String
{
    vector<string> Split(string , char);
}

// "A;B;C\nD;E;FF" 
// => 
// A|BB|C |
// -+--+--+
// C|D |FF|
int main(int argc, char* argv[]) {
  string csvText = argv[1];
  if(csvText.empty())
      return 1;
  vector<vector<string>> csvRecords;
  for(auto line : String::Split(csvText , '\n'))
    csvRecords.push_back(String::Split(line , ';'));
  vector<int> colWidths(csvRecords.front().size(), 0);
  for(auto row : csvRecords) {
    int i = 0;
    for(auto col : row) {
      if(col.length() > colWidths[i])
        colWidths[i] = col.length();
      ++i;
    }
  }
  int i = 0;
  for(int i = 0; i < csvRecords.size(); ++i) {
    int j = 0;
    for(int j = 0; j < csvRecords[i].size(); ++j) {
      cout << csvRecords[i][j] << string(colWidths[j] - csvRecords[i][j].length(), ' ') << '|';
    }
    cout << endl;
    if(i == 0) {
      for(int w : colWidths)
        cout << string(w, '-') + '+';
      cout << endl;
    }
  }
  return 0;
}


vector<string> String::Split(string text, char sep) {
  stringstream ss(text);
  vector<string> result;
  string part;
  while(getline(ss, part, sep))
    result.push_back(move(part));
  return result;
}