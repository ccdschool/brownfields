#include <iostream>
#include <cstdlib>
#include <thread>
#include <chrono>

using namespace std;
//making array and   
//by default I am providing 0-9 where no use of zero
static char arr[10] = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
static int player = 1; //By default player 1 is set 
static int choice; //This holds the choice at which position user want to mark   

// The flag veriable checks who has won if it's value is 1 then some one has won the match if -1 then Match has Draw if 0 then match is still running  
static int flag = 0;

// Board method which creats board  
static void Board() 
{
    cout << "     |     |     \n";
    cout << "  " << arr[1] << "  |  " << arr[2] << "  |  " << arr[3] << "\n";
    cout << "_____|_____|_____\n";
    cout << "     |     |     \n";
    cout << "  " << arr[4] << "  |  " << arr[5] << "  |  " << arr[6] << "\n";
    cout << "_____|_____|_____\n";
    cout << "     |     |     \n";
    cout << "  " << arr[7] << "  |  " << arr[8] << "  |  " << arr[9] << "\n";
    cout << "     |     |     \n";
}

//Checking that any player has won or not  
static int CheckWin() 
{
#pragma region Horzontal Winning Condtion
    //Winning Condition For First Row   
    if (arr[1] == arr[2] && arr[2] == arr[3])
	{
		return 1;
	}
    //Winning Condition For Second Row  
    else if (arr[4] == arr[5] && arr[5] == arr[6]) 
	{
		return 1;
	}
    //Winning Condition For Third Row   
    else if (arr[7] == arr[8] && arr[8] == arr[9]) 
	{
		return 1;
	}
#pragma endregion

#pragma region Vertical Winning Condtion
    //Winning Condition For First Column   
    else if (arr[1] == arr[4] && arr[4] == arr[7]) 
	{
		return 1;
	}
    //Winning Condition For Second Column  
    else if (arr[2] == arr[5] && arr[5] == arr[8])
	{
		return 1;
	}
    //Winning Condition For Third Column
	else if (arr[3] == arr[6] && arr[6] == arr[9])
	{
		return 1;
	}
#pragma endregion

#pragma region Diagonal Winning Condition
    else if (arr[1] == arr[5] && arr[5] == arr[9])
	{
		return 1;
	}
    else if (arr[3] == arr[5] && arr[5] == arr[7])
	{ 
		return 1;
	}
#pragma endregion

#pragma region Checking For Draw
    // If all the cells or values filled with X or O then any player has won the match  
    else if (arr[1] != '1' && arr[2] != '2' && arr[3] != '3' && arr[4] != '4' && arr[5] != '5' && arr[6] != '6' && arr[7] != '7' && arr[8] != '8' && arr[9] != '9')
	{
		return -1;
	}
#pragma endregion
    
    return 0;
}

void Run() 
{
    do 
    {
        // whenever loop will be again start then screen will be clear
    #ifdef _WIN32
        system("cls");
    #else
        system("clear");
    #endif
        cout << "Player1: X  und  Player2: O" << endl << endl;

        if (player % 2 == 0)//checking the chance of the player  
        {
            cout << "Player 2 Chance" << endl;
        }
        else 
        {
            cout << "Player 1 Chance" << endl;
        }
        
        cout << "\n" << endl;
        Board();
        cin >> choice; //Taking users choice

        // checking that position where user want to run is marked (with X or O) or not 
        if(arr[choice] != 'X' && arr[choice] != 'O')
        {
            if (player % 2 == 0) //if chance is of player 2 then mark O else mark X
            {
                arr[choice] = 'O';
                player++;
            }
            else
            {
                arr[choice] = 'X';
                player++;
            }
        }
        else //If there is any possition where user want to run and that is already marked then show message and load board again
        {
            cout << "Sorry the row " << choice << "  is already marked with " << arr[choice] << endl;
            cout << "Please wait 2 second board is loading again....." << endl;
            this_thread::sleep_for(chrono::seconds(2));
        }

        flag = CheckWin();;// calling of check win  
    } 
    while(flag != 1 && flag != -1);// This loof will be run until all cell of the grid is not marked with X and O or some player is not win 

    // clearing the console (for Windows use system("cls"))
#ifdef _WIN32
    system("cls");
#else
    system("clear");
#endif
    Board();// getting filled board again 

    if(flag == 1)// if flag value is 1 then some one has win or means who played marked last time which has win  
    {
        cout << "Player " << (player % 2) + 1 << " wins!" << endl;
    }
    else // if flag value is -1 the match will be draw and no one is winner  
    {
        cout << "Unentschieden!" << endl;
    }
}
