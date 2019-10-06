namespace OthelloUI
{
     public partial class OthelloBoardForm
     {
          /// <summary>
          /// Required designer variable.
          /// </summary>
          private System.ComponentModel.IContainer components = null;

          /// <summary>
          /// Clean up any resources being used.
          /// </summary>
          /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
          protected override void Dispose(bool disposing)
          {
               if (disposing && (components != null))
               {
                    components.Dispose();
               }

               base.Dispose(disposing);
          }

          #region Windows Form Designer generated code

          /// <summary>
          /// Required method for Designer support - do not modify
          /// the contents of this method with the code editor.
          /// </summary>
          private void InitializeComponent()
          {
               this.Score = new System.Windows.Forms.Label();
               this.Black = new System.Windows.Forms.Label();
               this.White = new System.Windows.Forms.Label();
               this.SuspendLayout();
               // 
               // Score
               // 
               this.Score.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
               this.Score.AutoSize = true;
               this.Score.BackColor = System.Drawing.Color.Transparent;
               this.Score.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.Score.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
               this.Score.Location = new System.Drawing.Point(164, 0);
               this.Score.Name = "Score";
               this.Score.Size = new System.Drawing.Size(178, 55);
               this.Score.TabIndex = 0;
               this.Score.Text = "02 : 02";
               // 
               // Black
               // 
               this.Black.AutoSize = true;
               this.Black.BackColor = System.Drawing.Color.Transparent;
               this.Black.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.Black.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
               this.Black.Location = new System.Drawing.Point(10, 10);
               this.Black.Name = "Black";
               this.Black.Size = new System.Drawing.Size(124, 46);
               this.Black.TabIndex = 1;
               this.Black.Text = "Black";
               // 
               // White
               // 
               this.White.AutoSize = true;
               this.White.BackColor = System.Drawing.Color.Transparent;
               this.White.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.White.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
               this.White.Location = new System.Drawing.Point(0, 0);
               this.White.Name = "White";
               this.White.Size = new System.Drawing.Size(127, 46);
               this.White.TabIndex = 2;
               this.White.Text = "White";
               // 
               // OthelloBoardForm
               // 
               this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
               this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
               this.AutoSize = true;
               this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
               this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
               this.ClientSize = new System.Drawing.Size(348, 162);
               this.Controls.Add(this.White);
               this.Controls.Add(this.Black);
               this.Controls.Add(this.Score);
               this.Cursor = System.Windows.Forms.Cursors.Arrow;
               this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
               this.MaximizeBox = false;
               this.MinimizeBox = false;
               this.Name = "OthelloBoardForm";
               this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
               this.Text = "Othello - Black\'s turn";
               this.ResumeLayout(false);
               this.PerformLayout();

          }

          #endregion

          private System.Windows.Forms.Label Score;
          private System.Windows.Forms.Label Black;
          private System.Windows.Forms.Label White;
     }
}