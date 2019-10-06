using System;

namespace OthelloLogic
{
     public class Board
     {
          private const char k_BlackCoinValue   = 'X';
          private const char k_WhiteCoinValue   = 'O';
          private const char k_EmptySquareValue = ' ';
          private Square[,] m_GameBoard;
          private int m_Size;

          public Board(int i_Size)
          {
               m_Size = i_Size;
               m_GameBoard = new Square[m_Size, m_Size];

               setStartBoard(i_Size);
          }

          public Board(Board i_Board)
          {
               m_Size      = i_Board.Size;
               m_GameBoard = new Square[m_Size, m_Size];

               setStartBoard(m_Size);

               for (int Row = 0; Row < m_Size; Row++)
               {
                    for (int Column = 0; Column < m_Size; Column++)
                    {
                         m_GameBoard[Row, Column].Color = i_Board.Matrix[Row, Column].Color;
                    }
               }
          }

          public Square[,] Matrix
          {
               get
               {
                    return m_GameBoard;
               }
          }

          public int Size
          {
               get
               {
                    return m_Size;
               }
          }

          private void setStartBoard(int i_Size)
          {
               initializeBoard();

               m_GameBoard[(i_Size / 2) - 1, (i_Size / 2) - 1].Color = k_WhiteCoinValue;
               m_GameBoard[(i_Size / 2) - 1, (i_Size / 2)].Color = k_BlackCoinValue;
               m_GameBoard[(i_Size / 2), (i_Size / 2) - 1].Color = k_BlackCoinValue;
               m_GameBoard[(i_Size / 2), (i_Size / 2)].Color = k_WhiteCoinValue;
          }

          private void initializeBoard()
          {
               for (int Row = 0; Row < m_Size; Row++)
               {
                    for (int Column = 0; Column < m_Size; Column++)
                    {
                         m_GameBoard[Row, Column] = new Square();
                    }
               }
          }

          public void RestartBoard()
          {
               for (int Row = 0; Row < m_Size; Row++)
               {
                    for (int Column = 0; Column < m_Size; Column++)
                    {
                         m_GameBoard[Row, Column].Color = k_EmptySquareValue;
                    }
               }

               m_GameBoard[(m_Size / 2) - 1, (m_Size / 2) - 1].Color = k_WhiteCoinValue;
               m_GameBoard[(m_Size / 2) - 1, (m_Size / 2)].Color = k_BlackCoinValue;
               m_GameBoard[(m_Size / 2), (m_Size / 2) - 1].Color = k_BlackCoinValue;
               m_GameBoard[(m_Size / 2), (m_Size / 2)].Color = k_WhiteCoinValue;
          }
     }
}