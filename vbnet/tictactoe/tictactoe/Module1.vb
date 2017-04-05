Option Strict On
Option Explicit On
Option Infer Off
'By Sam Christy - 12/November/2010
Module Module1
    Sub Main()

        'Declare Variables
        Dim grid(2, 2) As String, turn As String = "", victor As String = "None"
        Dim input As Integer = 0, x As Integer = 0, y As Integer = 0, i As Integer = 1

Start:
        'Enumerate the grid
        grid(0, 0) = "1" : grid(0, 1) = "2" : grid(0, 2) = "3"
        grid(1, 0) = "4" : grid(1, 1) = "5" : grid(1, 2) = "6"
        grid(2, 0) = "7" : grid(2, 1) = "8" : grid(2, 2) = "9"

        'Set the Console's Title and text colour
        Console.Title = "Naughts and Crosses - by Sam Christy" : Console.ForegroundColor = ConsoleColor.Yellow

        'Begin the game, alternating between X's and O's turn
        For i = 1 To 9
            If i Mod 2 = 1 Then
                turn = "X" 'Crosses' Turn
                DrawBoard(grid)
                Console.WriteLine("{0}Player Input{0}═════════════{0}", vbNewLine)
                InputValidation(input, x, y, grid, turn)
                grid(x, y) = "X"
                input = 0
            End If
            If i Mod 2 = 0 Then
                turn = "O" 'Naughts' Turn
                DrawBoard(grid)
                Console.WriteLine("{0}Player Input{0}═════════════{0}", vbNewLine)
                InputValidation(input, x, y, grid, turn)
                grid(x, y) = "O"
                input = 0
            End If
            victor = CheckGame(grid)
            If victor <> "None" Then
                Exit For
            ElseIf i = 9 Then
                Console.Clear()
                Console.WriteLine("Draw - You both suck!")
            End If
        Next

        'Announce Winner!
        If victor = "O" Then
            Console.WriteLine("Naughts Win!")
        ElseIf victor = "X" Then
            Console.WriteLine("Crosses Win!")
        End If
        Console.Beep()

        DrawBoard(grid)
        Console.ReadLine() 'Keep the Console open
        Console.Clear()
        GoTo Start

    End Sub
    Sub InputValidation(ByRef input As Integer, ByRef x As Integer, ByRef y As Integer, ByRef grid(,) As String, ByVal turn As String)
        Dim d, n, m As Boolean
        Do Until d = True And n = True And m = True
            'Check that the user input is even an integer
            d = True
            Console.Write("{0}: ", turn)
            Try
                input = CInt(Console.ReadLine())
            Catch
                d = False
                DisplayError(1)
            End Try
            'Check that the number is between 1 and 9
            If d = True Then
                n = True
                If input < 1 Or input > 9 Then
                    n = False
                    DisplayError(2)
                End If
            End If
            'Obtain the desired coordinates and evaluate if the move is valid
            If d = True And n = True Then
                GetCoords(input, x, y)
                m = MoveValid(grid, x, y)
                If m = False Then
                    DisplayError(3)
                End If
            End If
        Loop
        Console.Clear()

    End Sub
    Function CheckGame(ByVal grid(,) As String) As String
        If grid(0, 0) = grid(0, 1) And grid(0, 1) = grid(0, 2) Then 'Check First Row
            Return grid(0, 0) 'Return Victor!
        ElseIf grid(1, 0) = grid(1, 1) And grid(1, 1) = grid(1, 2) Then 'Check Second Row
            Return grid(1, 0) 'Return Victor!
        ElseIf grid(2, 0) = grid(2, 1) And grid(2, 1) = grid(2, 2) Then 'Check Third Row
            Return grid(2, 0) 'Return Victor!

        ElseIf grid(0, 0) = grid(1, 0) And grid(1, 0) = grid(2, 0) Then 'Check First Column
            Return grid(0, 0) 'Return Victor!
        ElseIf grid(0, 1) = grid(1, 1) And grid(1, 1) = grid(2, 1) Then 'Check Second Column
            Return grid(0, 1) 'Return Victor!
        ElseIf grid(0, 2) = grid(1, 2) And grid(1, 2) = grid(2, 2) Then 'Check Third Column
            Return grid(0, 2) 'Return Victor!

        ElseIf grid(0, 0) = grid(1, 1) And grid(1, 1) = grid(2, 2) Then 'Check First Diagonal
            Return grid(0, 0) 'Return Victor!
        ElseIf grid(0, 2) = grid(1, 1) And grid(1, 1) = grid(2, 0) Then 'Check Second Diagonal
            Return grid(0, 2) 'Return Victor!
        Else
            Return "None"
        End If
    End Function
    Function MoveValid(ByVal grid(,) As String, ByVal x As Integer, ByVal y As Integer) As Boolean
        If grid(x, y) = "O" Or grid(x, y) = "X" Then
            Return False
        Else
            Return True
        End If
    End Function
    Sub GetCoords(ByVal input As Integer, ByRef x As Integer, ByRef y As Integer)
        Select Case input
            Case Is = 1 : x = 0 : y = 0
            Case Is = 2 : x = 0 : y = 1
            Case Is = 3 : x = 0 : y = 2
            Case Is = 4 : x = 1 : y = 0
            Case Is = 5 : x = 1 : y = 1
            Case Is = 6 : x = 1 : y = 2
            Case Is = 7 : x = 2 : y = 0
            Case Is = 8 : x = 2 : y = 1
            Case Is = 9 : x = 2 : y = 2
        End Select
    End Sub
    Sub DrawBoard(ByVal grid(,) As String)
        Console.WriteLine("┌───┬───┬───┐")
        Console.WriteLine("│ {0} │ {1} │ {2} │", grid(0, 0), grid(0, 1), grid(0, 2))
        Console.WriteLine("├───┼───┼───┤")
        Console.WriteLine("│ {0} │ {1} │ {2} │", grid(1, 0), grid(1, 1), grid(1, 2))
        Console.WriteLine("├───┼───┼───┤")
        Console.WriteLine("│ {0} │ {1} │ {2} │", grid(2, 0), grid(2, 1), grid(2, 2))
        Console.WriteLine("└───┴───┴───┘")
    End Sub
    Sub DisplayError(ByVal e As Integer)
        Select Case e
            Case 1
                Console.ForegroundColor = ConsoleColor.Red : Console.Beep()
                Console.WriteLine("ERROR! Input is invalid,{0}Please enter an integer which is between 1 and 9.", vbNewLine)
                Console.ForegroundColor = ConsoleColor.Yellow
            Case 2
                Console.ForegroundColor = ConsoleColor.Red : Console.Beep()
                Console.WriteLine("ERROR! The square you requested doesn't exist,{0}Please enter an integer which is between 1 and 9.", vbNewLine)
                Console.ForegroundColor = ConsoleColor.Yellow
            Case 3
                Console.ForegroundColor = ConsoleColor.Red : Console.Beep()
                Console.WriteLine("ERROR! Square is currently occupied,{0}Please choose one which still contains a number.", vbNewLine)
                Console.ForegroundColor = ConsoleColor.Yellow
        End Select
    End Sub
End Module