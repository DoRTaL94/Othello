using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OthelloLogic;

namespace OthelloUI
{
     public partial class GameSettings : Form
     {
          private const int k_MaxBoardSize = 12;
          private const int k_MinBoardSize = 6;
          private const int k_SizeToAdd    = 2;
          private int m_BoardSize          = 6;
          private ePlayerType m_Player;

          public GameSettings()
          {
               InitializeComponent();
          }

          public int BoardSize
          {
               get
               {
                    return m_BoardSize;
               }
          }

          public ePlayerType PlayerType
          {
               get
               {
                    return m_Player;
               }
          }

          private void boardSizeButton_Click(object sender, EventArgs e)
          {
               if(m_BoardSize < k_MaxBoardSize)
               {
                    m_BoardSize += k_SizeToAdd;
               }
               else
               {
                    m_BoardSize = k_MinBoardSize;
               }

               boardSizeButton.Text = string.Format("Board Size: {0}x{0} (Click to increase)", m_BoardSize);
          }

          private void playerVcomputerButton_Click(object sender, EventArgs e)
          {
               m_Player = ePlayerType.Computer;
               this.Hide();
          }

          private void playerVplayerButton_Click(object sender, EventArgs e)
          {
               m_Player = ePlayerType.Player;
               this.Hide();
          }
     }
}
