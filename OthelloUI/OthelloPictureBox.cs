using System;
using System.Windows.Forms;
using System.Text;
using OthelloLogic;

namespace OthelloUI
{
     public delegate void FlipEventHandler(object sender);

     public class OthelloPictureBox : PictureBox
     {
          public event FlipEventHandler m_Fliped;

          private const char k_StartColumnLetter  = 'A';
          private const int k_StartRowNumber      = '0';
          private const int k_RedCoinValueInLogic = 'X';
          private string m_Location;
          private int m_Row;
          private int m_Column;

          public string SquareLocation
          {
               get
               {
                    return m_Location;
               }
          }

          public int Row
          {
               get
               {
                    return m_Row;
               }
          }

          public int Column
          {
               get
               {
                    return m_Column;
               }
          }

          public void OnFlip(object i_PlayerTurn, int i_Row, int i_Column)
          {
               if(i_Row == m_Row && i_Column == m_Column)
               {
                    square_Flip(i_PlayerTurn);
               }
          }

          private void square_Flip(object i_PlayerTurn)
          {
               this.Image = (i_PlayerTurn as Player).DiskColor == k_RedCoinValueInLogic ? Properties.Resources.Black : Properties.Resources.White;

               m_Fliped.Invoke(this);
          }

          public void InitLocation(int i_BoardSize)
          {
               StringBuilder locationBuilder = new StringBuilder();

               m_Row       = this.TabIndex / i_BoardSize;
               m_Column    = this.TabIndex % i_BoardSize;
               char Column = (char)(k_StartColumnLetter + m_Column);
               char Row    = (char)(k_StartRowNumber + m_Row + 1);

               locationBuilder.Append(Column);
               locationBuilder.Append(Row);

               m_Location = locationBuilder.ToString();
          }
     }
}
