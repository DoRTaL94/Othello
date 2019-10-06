using System.Collections.Generic;
using System;

namespace OthelloLogic
{
     public class Player
     {
          private const char k_StartColumnLetter = 'A';
          private const int k_StartRowNumber     = '1';
          private int m_Score                    = 2;
          private bool m_IsTurn                  = false;
          private ePlayerType m_PlayerType       = ePlayerType.Player;
          private bool m_HasAvailableMoves       = false;
          private char m_DiskColor               = ' ';
          private bool m_IsPlayerTurnAgain       = false;
          private List<string> m_AvailableMoves  = new List<string>();

          public Player()
          {
          }

          public Player(Player i_Player)
          {
               m_Score             = i_Player.Score;
               m_IsTurn            = i_Player.IsTurn;
               m_HasAvailableMoves = i_Player.HasAvailableMoves;
               m_DiskColor         = i_Player.DiskColor;
               m_IsPlayerTurnAgain = i_Player.IsPlayerTurnAgain;
               copyList(i_Player.AvailableMoves);
          }

          private void copyList(List<string> i_List)
          {
               for (int i = 0; i < i_List.Count; i++)
               {
                    m_AvailableMoves.Add(i_List[i]);
               }
          }

          public List<string> AvailableMoves
          {
               get
               {
                    return m_AvailableMoves;
               }
          }

          public void UpdateAvailableMoves(bool[,] i_AvailableMovesMatrix, int i_Size)
          {
               char[] temp = new char[2];
               string nextMove = string.Empty;

               m_AvailableMoves.Clear();

               for (int Row = 0; Row < i_Size; Row++)
               {
                    for (int Column = 0; Column < i_Size; Column++)
                    {
                         if (i_AvailableMovesMatrix[Row, Column] == true)
                         {
                              temp[0] = (char)(k_StartColumnLetter + Column);
                              temp[1] = (char)(k_StartRowNumber + Row);
                              string computerLocationInput = new string(temp);

                              m_AvailableMoves.Add(computerLocationInput);
                         }
                    }
               }
          }

          public void ClearData()
          {
               m_Score             = 2;
               m_IsTurn            = false;
               m_HasAvailableMoves = false;
               m_IsPlayerTurnAgain = false;
               m_AvailableMoves.Clear();
          }

          public bool IsPlayerTurnAgain
          {
               get
               {
                    return m_IsPlayerTurnAgain;
               }

               set
               {
                    m_IsPlayerTurnAgain = value;
               }
          }

          public char DiskColor
          {
               get
               {
                    return m_DiskColor;
               }

               set
               {
                    m_DiskColor = value;
               }
          }

          public bool IsTurn
          {
               get
               {
                    return m_IsTurn;
               }

               set
               {
                    m_IsTurn = value;
               }
          }

          public bool HasAvailableMoves
          {
               get
               {
                    return m_HasAvailableMoves;
               }

               set
               {
                    m_HasAvailableMoves = value;
               }
          }

          public ePlayerType PlayerType
          {
               get
               {
                    return m_PlayerType;
               }

               set
               {
                    m_PlayerType = value;
               }
          }

          public int Score
          {
               get
               {
                    return m_Score;
               }

               set
               {
                    m_Score = value;
               }
          }
     }
}