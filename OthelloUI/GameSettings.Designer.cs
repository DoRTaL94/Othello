namespace OthelloUI
{
     public partial class GameSettings
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
               this.boardSizeButton = new System.Windows.Forms.Button();
               this.playerVcomputerButton = new System.Windows.Forms.Button();
               this.playerVplayerButton = new System.Windows.Forms.Button();
               this.SuspendLayout();
               // 
               // boardSizeButton
               // 
               this.boardSizeButton.Location = new System.Drawing.Point(12, 12);
               this.boardSizeButton.Name = "boardSizeButton";
               this.boardSizeButton.Size = new System.Drawing.Size(368, 47);
               this.boardSizeButton.TabIndex = 0;
               this.boardSizeButton.Text = "Board Size: 6x6 (Click to increase)";
               this.boardSizeButton.UseVisualStyleBackColor = true;
               this.boardSizeButton.Click += new System.EventHandler(this.boardSizeButton_Click);
               // 
               // playerVcomputerButton
               // 
               this.playerVcomputerButton.Location = new System.Drawing.Point(12, 77);
               this.playerVcomputerButton.Name = "playerVcomputerButton";
               this.playerVcomputerButton.Size = new System.Drawing.Size(177, 47);
               this.playerVcomputerButton.TabIndex = 1;
               this.playerVcomputerButton.Text = "Play against the computer";
               this.playerVcomputerButton.UseVisualStyleBackColor = true;
               this.playerVcomputerButton.Click += new System.EventHandler(this.playerVcomputerButton_Click);
               // 
               // playerVplayerButton
               // 
               this.playerVplayerButton.Location = new System.Drawing.Point(198, 77);
               this.playerVplayerButton.Name = "playerVplayerButton";
               this.playerVplayerButton.Size = new System.Drawing.Size(182, 47);
               this.playerVplayerButton.TabIndex = 2;
               this.playerVplayerButton.Text = "Play against your friend";
               this.playerVplayerButton.UseVisualStyleBackColor = true;
               this.playerVplayerButton.Click += new System.EventHandler(this.playerVplayerButton_Click);
               // 
               // GameSettings
               // 
               this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
               this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
               this.ClientSize = new System.Drawing.Size(392, 137);
               this.Controls.Add(this.playerVplayerButton);
               this.Controls.Add(this.playerVcomputerButton);
               this.Controls.Add(this.boardSizeButton);
               this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
               this.MaximizeBox = false;
               this.MinimizeBox = false;
               this.Name = "GameSettings";
               this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
               this.Text = "GameSettings";
               this.ResumeLayout(false);

          }

          #endregion

          private System.Windows.Forms.Button boardSizeButton;
          private System.Windows.Forms.Button playerVcomputerButton;
          private System.Windows.Forms.Button playerVplayerButton;
     }
}