using System;
using System.Threading;
using System.Collections.Generic;

namespace OthelloLogic
{
     public delegate void FlipEventHandler(object i_PlayerTurn, int i_Row, int i_Column);

     public class Game
     {
          public event FlipEventHandler m_FlipOccured;

          private const string k_NoAvailableMove = "NO_AVAILABLE_MOVE";
          private const string k_QuitGame        = "Q";
          private const char k_BlackCoinValue    = 'X';
          private const char k_WhiteCoinValue    = 'O';
          private const char k_ColumnStartLetter = 'A';
          private const char k_RowStartNumber    = '1';
          private const char k_EmptySquareValue  = ' ';
          private const int k_SleepTime          = 1000;
          private const int k_AiDifficulty       = 4;
          private int m_Player1WinsCount         = 0;
          private int m_Player2WinsCount         = 0;
          private bool m_IsGameOver              = false;
          private Player m_Player1               = new Player();
          private Player m_Player2               = new Player();
          private Random m_RandomNumber          = new Random();
          private bool[,] m_AvailableMovesMatrix;
          private Board m_Board;

          public Game(ePlayerType i_PlayerType, int i_BoardSize)
          {
               gameRegistration(i_PlayerType, i_BoardSize);
          }

          public void PlayTurn(Player i_CurrentPlayerTurn, Player i_OpponentPlayer, string i_UserMove)
          {
               bool isSuccessfulMove = false;
               string computerMove   = string.Empty;
               bool isIlligialMove   = false;

               if (i_CurrentPlayerTurn.PlayerType == ePlayerType.Computer)
               {
                    computerMove = generateComputerMove(i_CurrentPlayerTurn, i_OpponentPlayer);
                    m_IsGameOver = flipDisks(m_Board, computerMove, out isIlligialMove, true);

                    updateScore(i_CurrentPlayerTurn, i_OpponentPlayer, m_Board);
                    Thread.Sleep(k_SleepTime);
               }
               else
               {
                    while (m_IsGameOver == false && isSuccessfulMove == false)
                    {
                         if (i_UserMove == k_QuitGame)
                         {
                              m_IsGameOver = true;
                         }
                         else
                         {
                              flipDisks(m_Board, i_UserMove, out isIlligialMove, true);
                              updateScore(i_CurrentPlayerTurn, i_OpponentPlayer, m_Board);

                              if (isIlligialMove == false)
                              {
                                   isSuccessfulMove = true;
                              }
                         }
                    }
               }

               manageNextTurn(m_Player1, m_Player2, m_Board);

               if (m_IsGameOver == true)
               {
                    if (m_Player1.Score > m_Player2.Score)
                    {
                         m_Player1WinsCount++;
                    }
                    else if (m_Player1.Score < m_Player2.Score)
                    {
                         m_Player2WinsCount++;
                    }
               }
          }

          public void RestartGame()
          {
               m_IsGameOver = false;

               m_Player1.ClearData();
               m_Player2.ClearData();
               initAvailableMovesMatrix(m_AvailableMovesMatrix);
               m_Board.RestartBoard();
               manageNextTurn(m_Player1, m_Player2, m_Board);
          }

          private void updateScore(Player i_Player1, Player i_Player2, Board i_Board)
          {
               int player1ScoreCounter = 0;
               int player2ScoreCounter = 0;

               for (int Row = 0; Row < i_Board.Size; Row++)
               {
                    for (int Column = 0; Column < i_Board.Size; Column++)
                    {
                         if (i_Board.Matrix[Row, Column].Color == i_Player1.DiskColor)
                         {
                              player1ScoreCounter++;
                         }
                         else if (i_Board.Matrix[Row, Column].Color == i_Player2.DiskColor)
                         {
                              player2ScoreCounter++;
                         }
                    }
               }

               i_Player1.Score = player1ScoreCounter;
               i_Player2.Score = player2ScoreCounter;
          }

          private void gameRegistration(ePlayerType i_PlayerType, int i_BoardSize)
          {
               m_Player1.DiskColor = k_BlackCoinValue;
               m_Player2.DiskColor = k_WhiteCoinValue;

               if (i_PlayerType == ePlayerType.Computer)
               {
                    m_Player2.PlayerType = ePlayerType.Computer;
               }

               m_Board                = new Board(i_BoardSize);
               m_AvailableMovesMatrix = new bool[i_BoardSize, i_BoardSize];

               manageNextTurn(m_Player1, m_Player2, m_Board);
          }

          private bool flipDisks(Board i_Board, string i_Move, out bool o_IsIllegalMove, bool i_IsInvokeFlipEvent)
          {
               int column          = i_Move[0] - k_ColumnStartLetter;
               int row             = i_Move[1] - k_RowStartNumber;
               char myPiece        = k_EmptySquareValue;
               char opponentPiece  = k_EmptySquareValue;
               bool isUp           = false;
               bool isUpRight      = false;
               bool isUpLeft       = false;
               bool isLeft         = false;
               bool isRight        = false;
               bool isDownRight    = false;
               bool isDownLeft     = false;
               bool isDown         = false;
               bool isGameOver     = false;

               if (i_Move == k_NoAvailableMove)
               {
                    o_IsIllegalMove = false;
                    isGameOver      = true;
               }
               else
               {
                    if (i_Board.Matrix[row, column].Color != k_EmptySquareValue)
                    {
                         o_IsIllegalMove = true;
                    }
                    else
                    {
                         if (m_Player1.IsTurn == true)
                         {
                              myPiece       = k_BlackCoinValue;
                              opponentPiece = k_WhiteCoinValue;
                         }
                         else
                         {
                              myPiece       = k_WhiteCoinValue;
                              opponentPiece = k_BlackCoinValue;
                         }

                         isUp = checkDirection(i_Board, row, column, myPiece, opponentPiece, eDirections.Up, eDirections.None, false, true, i_IsInvokeFlipEvent);
                         isUpRight = checkDirection(i_Board, row, column, myPiece, opponentPiece, eDirections.Up, eDirections.Right, false, true, i_IsInvokeFlipEvent);
                         isUpLeft = checkDirection(i_Board, row, column, myPiece, opponentPiece, eDirections.Up, eDirections.Left, false, true, i_IsInvokeFlipEvent);
                         isLeft = checkDirection(i_Board, row, column, myPiece, opponentPiece, eDirections.None, eDirections.Left, false, true, i_IsInvokeFlipEvent);
                         isRight = checkDirection(i_Board, row, column, myPiece, opponentPiece, eDirections.None, eDirections.Right, false, true, i_IsInvokeFlipEvent);
                         isDownRight = checkDirection(i_Board, row, column, myPiece, opponentPiece, eDirections.Down, eDirections.Right, false, true, i_IsInvokeFlipEvent);
                         isDownLeft = checkDirection(i_Board, row, column, myPiece, opponentPiece, eDirections.Down, eDirections.Left, false, true, i_IsInvokeFlipEvent);
                         isDown = checkDirection(i_Board, row, column, myPiece, opponentPiece, eDirections.Down, eDirections.None, false, true, i_IsInvokeFlipEvent);

                         o_IsIllegalMove = isUp && isUpRight && isUpLeft && isLeft && isRight && isDown && isDownLeft && isDownRight;
                    }
               }

               return isGameOver;
          }

          public bool checkDirection(Board i_Board, int i_Row, int i_Column, char i_MyColor, char i_OpponentColor, eDirections i_RowDirection, eDirections i_ColumnDirection, bool i_isOpponentColorSeen, bool i_isFlip, bool i_IsInvokeFlipEvent)
          {
               bool res      = false;
               int newRow    = i_Row + (int)i_RowDirection;
               int newColumn = i_Column + (int)i_ColumnDirection;

               if (isOutOfBorder(newRow, newColumn) || i_Board.Matrix[newRow, newColumn].Color == k_EmptySquareValue)
               {
                    res = true;
               }
               else if (i_Board.Matrix[newRow, newColumn].Color == i_MyColor)
               {
                    if (i_isOpponentColorSeen == true)
                    {
                         if (i_isFlip == true)
                         {
                              changeColor(i_Board.Matrix[i_Row, i_Column], i_MyColor);

                              if (i_IsInvokeFlipEvent == true)
                              {
                                   m_FlipOccured.Invoke(i_MyColor == k_BlackCoinValue ? m_Player1 : m_Player2, i_Row, i_Column);
                              }
                         }

                         res = false;
                    }
                    else
                    {
                         res = true;
                    }
               }
               else
               {
                    res = checkDirection(i_Board, newRow, newColumn, i_MyColor, i_OpponentColor, i_RowDirection, i_ColumnDirection, true, i_isFlip, i_IsInvokeFlipEvent);

                    if (i_isFlip == true && res == false)
                    {
                         changeColor(i_Board.Matrix[i_Row, i_Column], i_MyColor);

                         if (i_IsInvokeFlipEvent == true)
                         {
                              m_FlipOccured.Invoke(i_MyColor == k_BlackCoinValue ? m_Player1 : m_Player2, i_Row, i_Column);
                         }
                    }
               }

               return res;
          }

          private bool isOutOfBorder(int i_Row, int i_Column)
          {
               return i_Row < 0 || i_Row >= m_Board.Size || i_Column < 0 || i_Column >= m_Board.Size;
          }

          private void changeColor(Square i_Disk, char i_Color)
          {
               i_Disk.Color = i_Color;
          }

          private void manageNextTurn(Player i_Player1, Player i_Player2, Board i_Board)
          {
               if (i_Player1.IsTurn == false)
               {
                    i_Player1.IsPlayerTurnAgain = false;
                    i_Player2.HasAvailableMoves = false;
                    updateAvailableMovesMatrix(i_Board, i_Player1);

                    if (i_Player1.HasAvailableMoves == true)
                    {
                         i_Player1.UpdateAvailableMoves(m_AvailableMovesMatrix, m_Board.Size);
                         i_Player2.IsTurn = false;
                         i_Player1.IsTurn = true;
                    }
                    else
                    {
                         i_Player2.IsPlayerTurnAgain = true;
                         updateAvailableMovesMatrix(i_Board, i_Player2);

                         if (i_Player2.HasAvailableMoves == false)
                         {
                              m_IsGameOver = true;
                         }
                    }
               }
               else
               {
                    i_Player2.IsPlayerTurnAgain = false;
                    i_Player1.HasAvailableMoves = false;
                    updateAvailableMovesMatrix(i_Board, i_Player2);

                    if (i_Player2.HasAvailableMoves == true)
                    {
                         i_Player2.UpdateAvailableMoves(m_AvailableMovesMatrix, m_Board.Size);
                         i_Player1.IsTurn = false;
                         i_Player2.IsTurn = true;
                    }
                    else
                    {
                         i_Player1.IsPlayerTurnAgain = true;
                         updateAvailableMovesMatrix(i_Board, i_Player1);

                         if (i_Player1.HasAvailableMoves == false)
                         {
                              m_IsGameOver = true;
                         }
                    }
               }
          }

          private void initAvailableMovesMatrix(bool[,] i_AvailableMovesMatrix)
          {
               for (int Row = 0; Row < m_Board.Size; Row++)
               {
                    for (int Column = 0; Column < m_Board.Size; Column++)
                    {
                         i_AvailableMovesMatrix[Row, Column] = false;
                    }
               }
          }

          private void updateAvailableMovesMatrix(Board i_Board, Player i_Player)
          {
               char opponentDiskColor = k_EmptySquareValue;
               bool hasAvailableMoves = false;

               initAvailableMovesMatrix(m_AvailableMovesMatrix);
               opponentDiskColor = i_Player.DiskColor == k_BlackCoinValue ? k_WhiteCoinValue : k_BlackCoinValue;

               for (int Row = 0; Row < i_Board.Size; Row++)
               {
                    for (int Column = 0; Column < i_Board.Size; Column++)
                    {
                         if (i_Board.Matrix[Row, Column].Color == k_EmptySquareValue)
                         {
                              m_AvailableMovesMatrix[Row, Column] =
                              !(
                                  checkDirection(i_Board, Row, Column, i_Player.DiskColor, opponentDiskColor, eDirections.Down, eDirections.None, false, false, false) &&
                                  checkDirection(i_Board, Row, Column, i_Player.DiskColor, opponentDiskColor, eDirections.Up, eDirections.None, false, false, false) &&
                                  checkDirection(i_Board, Row, Column, i_Player.DiskColor, opponentDiskColor, eDirections.Up, eDirections.Right, false, false, false) &&
                                  checkDirection(i_Board, Row, Column, i_Player.DiskColor, opponentDiskColor, eDirections.Up, eDirections.Left, false, false, false) &&
                                  checkDirection(i_Board, Row, Column, i_Player.DiskColor, opponentDiskColor, eDirections.None, eDirections.Left, false, false, false) &&
                                  checkDirection(i_Board, Row, Column, i_Player.DiskColor, opponentDiskColor, eDirections.Down, eDirections.Right, false, false, false) &&
                                  checkDirection(i_Board, Row, Column, i_Player.DiskColor, opponentDiskColor, eDirections.Down, eDirections.Left, false, false, false) &&
                                  checkDirection(i_Board, Row, Column, i_Player.DiskColor, opponentDiskColor, eDirections.None, eDirections.Right, false, false, false)
                              );

                              if (m_AvailableMovesMatrix[Row, Column] == true && hasAvailableMoves == false)
                              {
                                   hasAvailableMoves = true;
                              }
                         }
                         else
                         {
                              m_AvailableMovesMatrix[Row, Column] = false;
                         }
                    }
               }

               i_Player.HasAvailableMoves = hasAvailableMoves == false ? false : true;
          }

          private string nextComputerMove(Player i_Computer, Player i_Opponent)
          {
               bool isGameOver = false;
               string bestMove = string.Empty;

               minimax(i_Computer, i_Opponent, m_Board, ref isGameOver, 4, ref bestMove);

               return bestMove;
          }

          private int minimax(Player i_MaxPlayer, Player i_MinPlayer, Board i_Board, ref bool io_IsGameOver, int i_TurnsDepth, ref string io_BestMove)
          {
               bool isIllegalMove = false;
               int score = 0;

               if (i_TurnsDepth == 0 || io_IsGameOver == true)
               {
                    score = i_MaxPlayer.Score;
               }
               else if (i_MaxPlayer.IsTurn == true)
               {
                    int maxScore = int.MinValue;

                    if (i_MaxPlayer.HasAvailableMoves == true)
                    {
                         foreach (string move in i_MaxPlayer.AvailableMoves)
                         {
                              Board newBoard   = new Board(i_Board);
                              Player maxPlayer = new Player(i_MaxPlayer);
                              Player minPlayer = new Player(i_MinPlayer);

                              io_IsGameOver = flipDisks(newBoard, move, out isIllegalMove, false);
                              updateScore(maxPlayer, minPlayer, newBoard);
                              manageNextTurn(minPlayer, maxPlayer, newBoard);

                              score = minimax(maxPlayer, minPlayer, newBoard, ref io_IsGameOver, i_TurnsDepth - 1, ref io_BestMove);

                              if (score >= maxScore)
                              {
                                   if (i_TurnsDepth == k_AiDifficulty)
                                   {
                                        io_BestMove = move;
                                   }

                                   maxScore = score;
                              }
                         }
                    }

                    score = maxScore;
               }
               else
               {
                    int minScore = int.MaxValue;

                    if (i_MinPlayer.HasAvailableMoves == true)
                    {
                         foreach (string move in i_MinPlayer.AvailableMoves)
                         {
                              Board newBoard   = new Board(i_Board);
                              Player maxPlayer = new Player(i_MaxPlayer);
                              Player minPlayer = new Player(i_MinPlayer);

                              io_IsGameOver = flipDisks(newBoard, move, out isIllegalMove, false);
                              updateScore(maxPlayer, minPlayer, newBoard);
                              manageNextTurn(minPlayer, maxPlayer, newBoard);

                              score = minimax(maxPlayer, minPlayer, newBoard, ref io_IsGameOver, i_TurnsDepth - 1, ref io_BestMove);

                              if (score <= minScore)
                              {
                                   minScore = score;
                              }
                         }
                    }

                    score = minScore;
               }

               return score;
          }

          private string generateComputerMove(Player i_Computer, Player i_Opponent)
          {
               string computerMove = string.Empty;

               i_Computer.UpdateAvailableMoves(m_AvailableMovesMatrix, m_Board.Size);
               computerMove = i_Computer.AvailableMoves.Count == 0 ?
                              k_NoAvailableMove : nextComputerMove(i_Computer, i_Opponent);

               return computerMove;
          }

          public int Player1WinsCount
          {
               get
               {
                    return m_Player1WinsCount;
               }
          }

          public int Player2WinsCount
          {
               get
               {
                    return m_Player2WinsCount;
               }
          }

          public Board GameBoard
          {
               get
               {
                    return m_Board;
               }
          }

          public Player Player1
          {
               get
               {
                    return m_Player1;
               }
          }

          public Player Player2
          {
               get
               {
                    return m_Player2;
               }
          }

          public bool[,] AvailableMoves
          {
               get
               {
                    return m_AvailableMovesMatrix;
               }
          }

          public bool IsGameOver
          {
               get
               {
                    return m_IsGameOver;
               }
          }
     }
}