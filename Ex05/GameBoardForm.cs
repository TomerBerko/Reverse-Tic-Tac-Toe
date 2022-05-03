using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex05
{
    public partial class GameBoardForm : Form
    {
        private const byte k_MarginSpace = 6;
        private const byte k_CellSize = 50;
        private const byte k_SpaceBetweenButtons = 6;
        private const byte k_SpaceForResult = 50;
        private Button[,] m_ButtonMatrix;
        private Game m_CurrentGame = null;

        internal GameBoardForm(Game i_NewGame)
        {
            InitializeComponent();
            this.m_CurrentGame = i_NewGame;
            int widthSize = (4 * k_MarginSpace) + (i_NewGame.BoardSize * (k_CellSize + k_SpaceBetweenButtons));
            int heighthSize = (3 * k_MarginSpace) + ((i_NewGame.BoardSize + 1) * (k_CellSize + k_SpaceBetweenButtons)) + k_SpaceForResult;
            this.Size = new Size(widthSize, heighthSize);
            drawGameBoard();
            drawGameScore();
        }

        private void drawGameBoard()
        {
            m_ButtonMatrix = new Button[m_CurrentGame.BoardSize, m_CurrentGame.BoardSize];
            int currentYLocation = 0;
            int currentXLocation = 0;
            for (int row = 0; row < m_CurrentGame.BoardSize; row++)
            {
                for (int cols = 0; cols < m_CurrentGame.BoardSize; cols++)
                {
                    m_ButtonMatrix[row, cols] = new Button();
                    m_ButtonMatrix[row, cols].Width = k_CellSize;
                    m_ButtonMatrix[row, cols].Height = k_CellSize;
                    m_ButtonMatrix[row, cols].Text = m_CurrentGame.GetMatValue(m_CurrentGame.m_GameBoard, row, cols) + string.Empty;
                    currentYLocation = (row * (k_CellSize + k_SpaceBetweenButtons)) + k_MarginSpace;
                    currentXLocation = (cols * (k_CellSize + k_SpaceBetweenButtons)) + k_MarginSpace;
                    m_ButtonMatrix[row, cols].Location = new Point(currentXLocation, currentYLocation);
                    m_ButtonMatrix[row, cols].Click += new System.EventHandler(InsertButton_Click);
                    this.Controls.Add(m_ButtonMatrix[row, cols]);
                }
            }
        }

        private void drawGameScore()
        {
            Label firstPlayerLabel = new Label();
            firstPlayerLabel.MaximumSize = new Size(this.Width / 2, firstPlayerLabel.Height);
            firstPlayerLabel.AutoSize = true;
            firstPlayerLabel.Text = string.Format("{0} : {1}", m_CurrentGame.FirstPlayer.PlayerName, m_CurrentGame.FirstPlayer.Score);
            firstPlayerLabel.Location = new Point(0, this.Height - (10 * k_MarginSpace));
            this.Controls.Add(firstPlayerLabel);

            if (m_CurrentGame.AiPlayer == null)
            {
                Label secondPlayerLabel = new Label();
                firstPlayerLabel.MaximumSize = new Size(this.Width / 2, firstPlayerLabel.Height);
                secondPlayerLabel.AutoSize = true;
                secondPlayerLabel.Text = string.Format("{0} : {1}", this.m_CurrentGame.SecondPlayer.PlayerName, this.m_CurrentGame.SecondPlayer.Score);
                secondPlayerLabel.Location = new Point(this.Width - secondPlayerLabel.Width, this.Height - (10 * k_MarginSpace));
                this.Controls.Add(secondPlayerLabel);
            }
            else
            {
                Label secondPlayerLabel = new Label();
                firstPlayerLabel.MaximumSize = new Size(this.Width / 2, firstPlayerLabel.Height);
                secondPlayerLabel.AutoSize = true;
                secondPlayerLabel.Text = string.Format("{0} : {1}", this.m_CurrentGame.AiPlayer.PlayerName, this.m_CurrentGame.AiPlayer.Score);
                secondPlayerLabel.Location = new Point(this.Width - secondPlayerLabel.Width, this.Height - (10 * k_MarginSpace));
                this.Controls.Add(secondPlayerLabel);
            }
        }

        private void InsertButton_Click(object i_Sender, EventArgs e)
        {
            if (i_Sender is Button)
            {
                Button currentClickedButton = i_Sender as Button;
                int colToInsert = 0;
                int rowToInsert = 0;
                bool isAiChoiceValid = false;
                bool isThereWinner = false;
                for (int row = 0; row < m_CurrentGame.BoardSize; row++)
                {
                    for (int cols = 0; cols < m_CurrentGame.BoardSize; cols++)
                    {
                        if (currentClickedButton == m_ButtonMatrix[row, cols])
                        {
                            colToInsert = cols;
                            rowToInsert = row;
                        }
                    }
                }

                if (m_CurrentGame.AiPlayer == null)
                {
                    if (Game.SymbolPlacmentAndCheck(m_CurrentGame, rowToInsert, colToInsert))
                    {
                        drawPlayerSymbol(rowToInsert, colToInsert);
                        m_CurrentGame.RoundWithHeumen();
                    }

                    currentClickedButton.Enabled = false;
                    isThereWinner = Game.IsThereWinner(m_CurrentGame);
                    if (isThereWinner)
                    {
                        DisplayWinningOrDrowMessageBox();
                    }
                }
                else
                {
                    if (Game.SymbolPlacmentAndCheckWithAI(m_CurrentGame, rowToInsert, colToInsert))
                    {
                        drawPlayerSymbol(rowToInsert, colToInsert);
                        isThereWinner = Game.IsThereWinner(m_CurrentGame);
                        currentClickedButton.Enabled = false;
                        if (isThereWinner)
                        {
                            DisplayWinningOrDrowMessageBox();
                        }

                        m_CurrentGame.FirstPlayer.PlayerTurn = true;
                        Random rd = new Random();
                        while (isAiChoiceValid == false)
                        {
                            colToInsert = rd.Next(0, m_CurrentGame.BoardSize);
                            rowToInsert = rd.Next(0, m_CurrentGame.BoardSize);
                            isAiChoiceValid = Game.ValidCheckForAi(m_CurrentGame, rowToInsert, colToInsert);
                        }

                        Game.SetMatValue(ref m_CurrentGame.m_GameBoard, rowToInsert, colToInsert, m_CurrentGame.AiPlayer.Symbol);
                        m_ButtonMatrix[rowToInsert, colToInsert].Enabled = false;
                        drawPlayerSymbol(rowToInsert, colToInsert);
                        m_CurrentGame.AiPlayer.PlayerTurn = false;
                        isThereWinner = Game.IsThereWinner(m_CurrentGame);
                        if (isThereWinner)
                        {
                            DisplayWinningOrDrowMessageBox();
                        }
                    }
                }
            }
        }

        private void drawPlayerSymbol(int i_RowToInsert, int i_ColToInsert)
        {
            m_ButtonMatrix[i_RowToInsert, i_ColToInsert].Text = m_CurrentGame.GetMatValue(m_CurrentGame.m_GameBoard, i_RowToInsert, i_ColToInsert) + string.Empty;
        }

        private void DisplayWinningOrDrowMessageBox()
        {
            DialogResult result = MessageBox.Show(
                string.Format(
                    "{0}{1}Would you like to play another round?",
                    this.m_CurrentGame.GetWinningPlayer(),
                    Environment.NewLine),
                this.m_CurrentGame.GetMessageTitle(),
                MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                this.Close();
            }
            else
            {
                ReGame();
            }
        }

        private void ReGame()
        {
            this.Hide();
            Game.InitializeBoard(m_CurrentGame.m_GameBoard, m_CurrentGame.BoardSize);
            Game.InitializeStats(m_CurrentGame);
            new GameBoardForm(m_CurrentGame).ShowDialog();
            this.Close();
        }
    }
}
