
'Jacob Horsley
'RCET0265
'Fall 2025
'Shuffle The Deck 
'URL: 

Option Strict On
Option Explicit On

'TODO
'[x] Display Bingo 
'[x] Draw a random ball that has not already been drawn
'[x] Update display to show all drawn balls
'[x] Update display to show actual ball number
'[ ] Refresh tracking with "C" or when all balls have been drawn
Module Module1

    Dim rand As New Random()
    Dim availableCards As New List(Of Tuple(Of Integer, Integer))
    Sub Main()
        Dim userInput As String
        InitializeCards()

        Do
            Console.Clear()
            DisplayBoard()

            Console.WriteLine()
            'prompt
            userInput = Console.ReadLine()
            Select Case userInput
                Case "d"
                    drawCard()
                Case "c"
                    cardTracker(0, 0,, True)
                    InitializeCards() 'Helps prevent crashing
                    drawCard(True)
                Case Else
                    'pass

            End Select


        Loop Until userInput = "q"
        Console.Clear()
        Console.WriteLine("Have a nice day!")

    End Sub



    Sub InitializeCards()
        availableCards.Clear()
        For i = 0 To 14
            For j = 0 To 4
                availableCards.Add(Tuple.Create(i, j)) 'Look up reference for Tuple helped with lag
            Next
        Next
    End Sub

    Sub drawCard(Optional clearCount As Boolean = False)
        Dim temp(,) As Boolean = cardTracker(0, 0) 'Create a local copy of ball tracker array
        'Dim currentBallNumber As Integer
        'Dim currentBallLetter As Integer
        Static cardCounter As Integer

        If clearCount Then
            cardCounter = 0
            InitializeCards()
        Else
            If availableCards.Count = 0 Then
                Console.WriteLine("All cards have been drawn!")
                Console.WriteLine("Enter any key to continue...")
                Console.ReadLine()
                cardTracker(0, 0,, True)
                InitializeCards()
                cardCounter = 0

                Exit Sub
            End If

            ' Pick a random ball from the remaining list
            Dim index As Integer = rand.Next(availableCards.Count)
            Dim drawnCard = availableCards(index) 'Adding these two lines prevented the program from acting slow

            ' Mark the ball as drawn
            cardTracker(drawnCard.Item1, drawnCard.Item2, True)
            availableCards.RemoveAt(index)

            cardCounter += 1
        End If


    End Sub


    ''' <summary>
    ''' Iterates Through the tracker array and displays bingo board to the console
    ''' </summary>

    Sub DisplayBoard()

        Console.OutputEncoding = System.Text.Encoding.UTF8 'For special characters
        'Dim temp As String = " |"
        Dim heading() As String = {ChrW(&H2660), ChrW(&H2665), ChrW(&H2663), ChrW(&H2666)}
        Dim tracker(,) As Boolean = cardTracker(0, 0) '
        Dim cardColumn() As String = {" ", "2", "3", "4", "5", "6", "7", "8", "9",
                "10", "J", "Q", "K", "A"}
        Dim spades As String = ChrW(&H2660)
        Dim hearts As String = ChrW(&H2665)
        Dim clubs As String = ChrW(&H2663)
        Dim diamonds As String = ChrW(&H2666)

        Console.Write("Enter d to draw cards and q to quit program")
        Console.WriteLine()

        Console.Write("    ")
        For Each suit In heading
            Console.Write(suit.PadLeft(6))

        Next
        Console.WriteLine()

        Console.WriteLine(StrDup(40, "-"))

        For currentNumber = 1 To 13
            Console.Write(cardColumn(currentNumber).PadRight(6))

            For currentletter = 0 To 3
                Dim temp As String = If(tracker(currentNumber, currentletter), " X |", " |")
                Console.Write(temp.PadLeft(6))

            Next
            Console.WriteLine()
        Next

    End Sub

    Function randomNumberBetween(min As Integer, max As Integer) As Integer
        Dim rand As New Random()
        Return rand.Next(min, max + 1) ' Ensures max is included
    End Function


    ''' <summary>
    ''' Contains a persistent array that tracks all possible bingo balls
    ''' and whether they have been drawn during the current game.
    ''' </summary>
    ''' <param name="cardNumber"></param>
    ''' <param name="cardLetter"></param>
    ''' <param name="clear"></param>
    ''' <returns>Current Tracking Array</returns>

    Function cardTracker(cardNumber As Integer, cardLetter As Integer,
                              Optional update As Boolean = False, Optional clear As Boolean = False) _
                              As Boolean(,)
        Static _cardTracker(14, 4) As Boolean


        If update Then
            _cardTracker(cardNumber, cardLetter) = True
        End If

        If clear Then
            ReDim _cardTracker(14, 4) 'clears the array. Could also loop through array and set all elements 
        End If
        'actual code here
        '_bingoTracker(ballNumber, ballLetter) = True
        Return _cardTracker
    End Function

End Module



