using System;
using System.Windows.Forms;
using OthelloLogic;

namespace OthelloUI
{
     public partial class OthelloBoardForm : Form
     {
          private const string k_BlackCoin            = "Black";
          private const string k_WhiteCoin            = "White";
          private const string k_Draw                 = "Draw";
          private const string k_WhiteTurn            = "Othello - White\'s turn";
          private const string k_BlackTurn            = "Othello - Black\'s turn";
          private const char k_BlackCoinValueInLogic  = 'X';
          private const char k_WhiteCoinValueInLogic  = 'O';
          private const int k_FormMargin              = 10;
          private const int k_SquareSize              = 60;
          private const int k_SquaresDistance         = 2;
          private GameSettings m_GameSettings;
          private Game m_Game;

          public OthelloBoardForm()
          {
               m_GameSettings = new GameSettings();

               InitializeComponent();
               m_GameSettings.ShowDialog();

               m_Game = new Game(m_GameSettings.PlayerType, m_GameSettings.BoardSize);
               initializeBoard(m_GameSettings.BoardSize);
               updateAvailableMoves();
          }

          private void initializeBoard(int i_BoardSize)
          {
               int top = this.Score.Height + k_FormMargin;
               int left = k_FormMargin;

               for (int Row = 0; Row < i_BoardSize; Row++)
               {
                    for(int Column = 0; Column < i_BoardSize; Column++)
                    {
                         OthelloPictureBox BoardSquare = new OthelloPictureBox();

                         BoardSquare.BackColor              = System.Drawing.Color.MediumSeaGreen;
                         BoardSquare.BackgroundImageLayout  = System.Windows.Forms.ImageLayout.Stretch;
                         BoardSquare.Location               = new System.Drawing.Point(left, top);
                         BoardSquare.Name                   = string.Format("Square{0}", (Row * m_GameSettings.BoardSize) + Column);
                         BoardSquare.Size                   = new System.Drawing.Size(k_SquareSize, k_SquareSize);
                         BoardSquare.TabIndex               = (Row * m_GameSettings.BoardSize) + Column;
                         BoardSquare.TabStop                = false;
                         BoardSquare.InitLocation(m_GameSettings.BoardSize);

                         BoardSquare.Click    += OnBoardClick;
                         m_Game.m_FlipOccured += BoardSquare.OnFlip;
                         BoardSquare.m_Fliped += this.OnFlip;

                         if (m_Game.GameBoard.Matrix[Row, Column].Color == k_BlackCoinValueInLogic)
                         {
                              BoardSquare.Image = Properties.Resources.Black;
                         }
                         else if(m_Game.GameBoard.Matrix[Row, Column].Color == k_WhiteCoinValueInLogic)
                         {
                              BoardSquare.Image = Properties.Resources.White;
                         }

                         Controls.Add(BoardSquare);
                         left += k_SquareSize + k_SquaresDistance;
                    }

                    left = k_FormMargin;
                    top += k_SquareSize + k_SquaresDistance;
               }

               this.ClientSize = new System.Drawing.Size(top - this.Score.Height + k_FormMargin - k_SquaresDistance, top + k_FormMargin - k_SquaresDistance);
               this.Score.Location = new System.Drawing.Point(this.Width/2 - this.Score.Width/2 - k_FormMargin + k_SquaresDistance, 5);
               this.White.Location = new System.Drawing.Point(this.ClientSize.Width - this.White.Width - k_FormMargin, k_FormMargin);
          }

          public void OnFlip(object sender)
          {
               (sender as OthelloPictureBox).Show();
          }

          public void OnBoardClick(object sender, EventArgs e)
          {
               OthelloPictureBox temp = sender as OthelloPictureBox;

               if (m_Game.AvailableMoves[temp.Row, temp.Column] == true)
               {
                    square_Click(temp.SquareLocation);
               }
          }

          private void updateAvailableMoves()
          {
               foreach(Control square in Controls)
               {
                    OthelloPictureBox temp = square as OthelloPictureBox;
                    if(temp != null)
                    {
                         if(m_Game.AvailableMoves[temp.Row, temp.Column] == true)
                         {
                              temp.Image = Properties.Resources.Available;
                         }
                         else if(m_Game.GameBoard.Matrix[temp.Row, temp.Column].Color != k_BlackCoinValueInLogic &&
                              m_Game.GameBoard.Matrix[temp.Row, temp.Column].Color != k_WhiteCoinValueInLogic)
                         {
                              temp.Image = null;
                         }
                    }
               }
          }

          private void refreshBoard()
          {
               foreach (Control control in Controls)
               {
                    OthelloPictureBox square = control as OthelloPictureBox;

                    if(square != null)
                    {
                         square.Refresh();
                    }
               }
          }

          private void square_Click(string i_Location)
          {
               if (m_Game.Player1.IsTurn == true)
               {
                    m_Game.PlayTurn(m_Game.Player1, m_Game.Player2, i_Location);

                    if(m_Game.Player2.IsTurn == true)
                    {
                         this.Text = k_WhiteTurn;
                    }
               }
               else
               {
                    m_Game.PlayTurn(m_Game.Player2, m_Game.Player1, i_Location);

                    if (m_Game.Player1.IsTurn == true)
                    {
                         this.Text = k_BlackTurn;
                    }
               }

               updateAvailableMoves();
               refreshBoard();
               updateScore();

               while (m_Game.Player2.PlayerType == ePlayerType.Computer && m_Game.Player2.IsTurn == true && m_Game.IsGameOver == false)
               {
                    m_Game.PlayTurn(m_Game.Player2, m_Game.Player1, i_Location);
                    updateAvailableMoves();
                    refreshBoard();
                    updateScore();
               }

               if(m_Game.IsGameOver == true)
               {
                    showAnotherGameDialog();
               }
               else
               {
                    if (m_Game.Player1.IsTurn == true)
                    {
                         this.Text = k_BlackTurn;
                    }
               }
          }

          private void showAnotherGameDialog()
          {
               string i_Winner;
               int winnerScore;
               int loserScore;

               if(m_Game.Player1.Score > m_Game.Player2.Score)
               {
                    i_Winner    = k_BlackCoin;
                    winnerScore = m_Game.Player1.Score;
                    loserScore  = m_Game.Player2.Score;
               }
               else if(m_Game.Player1.Score < m_Game.Player2.Score)
               {
                    i_Winner    = k_WhiteCoin;
                    winnerScore = m_Game.Player2.Score;
                    loserScore  = m_Game.Player1.Score;
               }
               else
               {
                    i_Winner    = k_Draw;
                    winnerScore = m_Game.Player1.Score;
                    loserScore  = m_Game.Player2.Score;
               }

               string message = string.Format(
@"{0} ({1}/{2}) ({3}/{4})
Would you like another round?", 
i_Winner == k_Draw ? "Its a draw!!" : i_Winner + " Won!!",
winnerScore,
loserScore,
m_Game.Player1WinsCount,
m_Game.Player2WinsCount);
               string title = "Othello";

               MessageBoxButtons buttons = MessageBoxButtons.YesNo;
               DialogResult result = MessageBox.Show(message, title, buttons);

               if (result == DialogResult.Yes)
               {
                    this.restartGame();
               }
               else
               {
                    this.Close();
               }
          }

          private void updateScore()
          {
               this.Score.Text = string.Format("{0}{1} : {2}{3}", m_Game.Player1.Score < 9 ? "0" : "", m_Game.Player1.Score, m_Game.Player2.Score < 9 ? "0" : "", m_Game.Player2.Score);
               this.Score.Refresh();
          }

          private void restartBoard()
          {
               foreach (Control square in Controls)
               {
                    OthelloPictureBox temp = square as OthelloPictureBox;

                    if (m_Game.GameBoard.Matrix[temp.Row, temp.Column].Color != ' ')
                    {
                         temp.Image = m_Game.GameBoard.Matrix[temp.Row, temp.Column].Color == 'X' ?
                              Properties.Resources.Black : Properties.Resources.White;
                    }
                    else
                    {
                         temp.Image = null;
                    }
               }
          }

          private void restartGame()
          {
               m_Game.RestartGame();
               restartBoard();
               updateAvailableMoves();
          }
     }
}
