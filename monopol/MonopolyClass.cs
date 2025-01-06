﻿using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;

namespace monopol {
    internal class MonopolyClass {
        private List<GamePlayer> gameplayers = new List<GamePlayer>();
        private Board board = new Board();
        private int currentPlayer = 0;
        private Random random = new Random();
        private Canvas boardCanvas;
        private Dictionary<GamePlayer, Ellipse> playerCircles = new Dictionary<GamePlayer, Ellipse>();


        // Lägga till en bank

        // Get my ownership på boardklassen
        // Funktionen köpa gata på street, utility station. Fixa till ett till arv.
        // Köpa hus på street
        // Köpa hotell på street
        // Sälja hus på street


        public MonopolyClass(Canvas canvas) {
            boardCanvas = canvas;
            // Create all the squares on the board and add the swedish names to the streets and add them to the list
            // Load the board from XML
            board.loadBoard("boardspaces.xml");

            gameplayers.Add(new GamePlayer("Spelare 1", 1, 5000));
            gameplayers.Add(new GamePlayer("Spelare 2", 1, 5000));

            LoadBoardSpaces();
            InitializePlayerCircles();

            StartGameLoop();
        }

        // Create a turnbased gameloop for monopoly

        private void LoadBoardSpaces() {
            int boardSize = 11; // Assuming a 10x10 board
            int sqwith = 90;
            int sqheight = 60;
            for (int i = 0; i < board.boardSpaces.Count; i++) {
                BoardSpace space = board.boardSpaces[i];
                TextBlock textBlock = new TextBlock {
                    Text = space.Name,
                    Background = Brushes.White,
                    Foreground = Brushes.Black,
                    Width = sqwith,
                    Height = sqheight,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                // Position the TextBlock on the canvas
                if (i < boardSize) {
                    // Bottom row (0-9)
                    Canvas.SetLeft(textBlock, (boardSize - 1 - i) * sqwith);
                    Canvas.SetTop(textBlock, (boardSize - 1) * sqheight);
                } else if (i < 2 * boardSize - 1) {
                    // Left column (10-19)
                    Canvas.SetLeft(textBlock, 0);
                    Canvas.SetTop(textBlock, (boardSize - 1 - (i - boardSize + 1)) * sqheight);
                } else if (i < 3 * boardSize - 2) {
                    // Top row (20-29)
                    Canvas.SetLeft(textBlock, (i - 2 * boardSize + 2) * sqwith);
                    Canvas.SetTop(textBlock, 0);
                } else {
                    // Right column (30-39)
                    Canvas.SetLeft(textBlock, (boardSize - 1) * sqwith);
                    Canvas.SetTop(textBlock, (i - 3 * boardSize + 3) * sqheight);
                }

                boardCanvas.Children.Add(textBlock);
            }
        }
        private void InitializePlayerCircles() {
            foreach (var player in gameplayers) {
                Ellipse playerCircle = new Ellipse {
                    Width = 20,
                    Height = 20,
                    Fill = player.Name == "Spelare 1" ? Brushes.Blue : Brushes.Red
                };
                playerCircles[player] = playerCircle;
                boardCanvas.Children.Add(playerCircle);
                MovePlayerCircle(player);
            }
        }

        private void MovePlayerCircle(GamePlayer player) {
            int boardSize = 11; // Assuming a 10x10 board
            int sqwith = 90;
            int sqheight = 60;
            int position = player.Position;

            double left = 0, top = 0;
            if (position < boardSize) {
                // Bottom row (0-9)
                left = (boardSize - 1 - position) * sqwith + sqwith / 2 - 10;
                top = (boardSize - 1) * sqheight + sqheight / 2 - 10;
            } else if (position < 2 * boardSize - 1) {
                // Left column (10-19)
                left = sqwith / 2 - 10;
                top = (boardSize - 1 - (position - boardSize + 1)) * sqheight + sqheight / 2 - 10;
            } else if (position < 3 * boardSize - 2) {
                // Top row (20-29)
                left = (position - 2 * boardSize + 2) * sqwith + sqwith / 2 - 10;
                top = sqheight / 2 - 10;
            } else {
                // Right column (30-39)
                left = (boardSize - 1) * sqwith + sqwith / 2 - 10;
                top = (position - 3 * boardSize + 3) * sqheight + sqheight / 2 - 10;
            }

            Canvas.SetLeft(playerCircles[player], left);
            Canvas.SetTop(playerCircles[player], top);
        }

        private async void StartGameLoop() {
            bool gameRunning = true;

            while (gameRunning) {
                GamePlayer player = gameplayers[currentPlayer];
                Debug.WriteLine($"{player.Name}'s turn. ({player.Money})");

                // Roll dice
                if(player.JailTurns > 0) {
                    Debug.WriteLine($"{player.Name} is in jail for {player.JailTurns} more turns.");
                }
                int [] diceRoll = RollDice();
                Debug.WriteLine($"{player.Name} rolled a {diceRoll[0]} and a {diceRoll[1]}.");
                
                if ( (player.JailTurns>0)) {
                    if (diceRoll[0] == diceRoll[1]) {
                        player.JailTurns = 0;
                        Debug.WriteLine($"{player.Name} rolled a double and is out of jail.");
                    } else {
                        player.JailTurns--;
                        Debug.WriteLine($"{player.Name} did not roll a double and is still in jail.");
                        currentPlayer = (currentPlayer + 1) % gameplayers.Count;
                        continue;
                    }
                }
                int sum = diceRoll[0] + diceRoll[1];
                // Move player
                player.Position = (player.Position + sum) % 40;
                BoardSpace currentSpace = board.GetSpace(player.Position);
                Debug.WriteLine($"{player.Name} landed on {currentSpace.Name}.");

                // Move the player's circle
                MovePlayerCircle(player);

                // Pause for 1 second
                await Task.Delay(1000);


                // Handle the space the player landed on
                // HandleAction skall kanske sitta i board
                board.HandleAction(player);

                player.checkMoney(board.GetMySpaces(player));
                // Check for game-ending conditions
                gameRunning = CheckGameEnd();

                // Move to the next player
                currentPlayer = (currentPlayer + 1) % gameplayers.Count;
            }

            Debug.WriteLine("Game over!");
        }

        private int[] RollDice() {
            return [ random.Next(1, 7) ,random.Next(1, 7)];
        }

        private bool CheckGameEnd() {
            // Check if only one player has money left
            int playersWithMoney = 0;
            foreach (GamePlayer player in gameplayers) {
                if (player.Money > 0) {
                    playersWithMoney++;
                }
            }
            return playersWithMoney > 1;
        }

    }
}