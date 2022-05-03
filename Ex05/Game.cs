using System;

namespace Ex05
{
    public class Game
    {
        private Player m_FirstPlayer = null;
        private Player m_SecondPlayer = null;
        private Player m_AiPlayer = null;
        public char[,] m_GameBoard = null;
        private int m_BoardSize = 3;

        public Game(bool i_IsPlayerHuman, SettingsData i_TheChosenData)
        {
            this.m_FirstPlayer = new Player(i_TheChosenData.FirstPlayer);
            this.m_FirstPlayer.PlayerTurn = true;
            this.m_FirstPlayer.Symbol = 'X';

            if (i_IsPlayerHuman == true)
            {
                this.m_SecondPlayer = new Player(i_TheChosenData.SecondPlayer);
                this.m_SecondPlayer.PlayerTurn = false;
                this.m_SecondPlayer.Symbol = 'O';
            }
            else
            {
                this.m_AiPlayer = new Player("Computer");
                this.m_AiPlayer.PlayerTurn = false;
                this.m_AiPlayer.Symbol = 'O';
            }

            m_BoardSize = i_TheChosenData.SizeOfTheBoard;
            this.m_GameBoard = new char[m_BoardSize, m_BoardSize];
            InitializeBoard(m_GameBoard, m_BoardSize);
        }

        public static bool ValidCheckForAi(Game i_Game, int i_RandomRow, int i_RandomCol)
        {
            bool isAiPlacmentValid = false;
            if (i_Game.GetMatValue(i_Game.m_GameBoard, i_RandomRow, i_RandomCol) == ' ')
            {
                isAiPlacmentValid = true;
            }

            return isAiPlacmentValid;
        }

        public static bool SymbolPlacmentAndCheck(Game i_Game, int i_row, int i_cols)
        {
            bool isInputValid = false;
            isInputValid = ValidCheck(i_Game, i_row, i_cols);
            if (isInputValid == true)
            {
                if (i_Game.FirstPlayer.PlayerTurn == true)
                {
                    SetMatValue(ref i_Game.m_GameBoard, i_row, i_cols, i_Game.FirstPlayer.Symbol);
                }
                else
                {
                    SetMatValue(ref i_Game.m_GameBoard, i_row, i_cols, i_Game.SecondPlayer.Symbol);
                }
            }

            return isInputValid;
        }

        public static bool SymbolPlacmentAndCheckWithAI(Game i_Game, int i_row, int i_cols)
        {
            bool isInputValid = false;
            if (i_Game.m_FirstPlayer.PlayerTurn == true)
            {
                i_Game.m_FirstPlayer.PlayerTurn = false;
                i_Game.m_AiPlayer.PlayerTurn = true;
            }

            isInputValid = ValidCheck(i_Game, i_row, i_cols);
            if (isInputValid == true)
            {
                SetMatValue(ref i_Game.m_GameBoard, i_row, i_cols, i_Game.FirstPlayer.Symbol);
            }
            
            return isInputValid;
        }

        public static void InitializeBoard(char[,] i_Board, int i_RequestedBoardSize)
        {
            for (int row = 0; row < i_RequestedBoardSize; row++)
            {
                for (int col = 0; col < i_RequestedBoardSize; col++)
                {
                    i_Board[row, col] = ' ';
                }
            }
        }

        public static void InitializeStats(Game i_Game)
        {
            i_Game.FirstPlayer.IsWon = false;
            i_Game.FirstPlayer.PlayerTurn = true;
            if(i_Game.AiPlayer != null)
            {
                i_Game.AiPlayer.IsWon = false;
                i_Game.AiPlayer.PlayerTurn = true;
            }
            else
            {
                i_Game.SecondPlayer.IsWon = false;
                i_Game.SecondPlayer.PlayerTurn = true;
            }
        }

        public static void SetMatValue(ref char[,] io_Board, int i_Row, int i_Cols, char i_Value)
        {
            io_Board[i_Row, i_Cols] = i_Value;
        }
       
        public static bool DrawCheck(Game i_Game)
        {
            bool isDraw = false;
            int count = 0;
            for (int row = 0; row < i_Game.BoardSize; row++)
            {
                for (int cols = 0; cols < i_Game.BoardSize; cols++)
                {
                    if (i_Game.GetMatValue(i_Game.m_GameBoard, row, cols) == ' ')
                    {
                        count++;
                    }
                }
            }

            if (count == 0)
            {
                isDraw = true;
            }

            return isDraw;
        }

        public static bool ValidCheck(Game i_Game, int i_Row, int i_Cols)
        {
            bool isTheCellEmpty = false;
                if (i_Game.GetMatValue(i_Game.m_GameBoard, i_Row, i_Cols) == ' ')
                {
                    isTheCellEmpty = true;
                }
                
            return isTheCellEmpty;
        }
      
        public static bool IsThereWinner(Game i_Game)
        {
            bool isThereWinner = false;
            bool isSecondPlayerWon = false;
            isSecondPlayerWon = WinnerCheck(i_Game);
            if(isSecondPlayerWon == true)
            {
                isThereWinner = true;
                if(i_Game.AiPlayer == null)
                {
                    i_Game.SecondPlayer.IsWon = true;
                }
                else
                {
                    i_Game.AiPlayer.IsWon = true;
                }
            }
            else if(i_Game.FirstPlayer.IsWon || DrawCheck(i_Game))
            {
                isThereWinner = true;
            }

            i_Game.AddScore(isSecondPlayerWon);
            return isThereWinner;
        }

        public static bool WinnerCheck(Game i_Game)
        {
            bool isSecondPlayerWon = false;
            int leftToRightDiagonalX = 0;
            int leftToRightDiagonalO = 0;
            int rightToLeftDiagonalX = 0;
            int rightToLeftDiagonalO = 0;
            int colsXCounter = 0;
            int colsOCounter = 0;
            int rowsXcount = 0;
            int rowsOcount = 0;
            for (int row = 0; row < i_Game.BoardSize; row++)
            {
                rowsXcount = 0;
                rowsOcount = 0;
                for (int cols = 0; cols < i_Game.BoardSize; cols++)
                {
                    if (i_Game.GetMatValue(i_Game.m_GameBoard, row, cols) == 'X')
                    {
                        rowsXcount++;
                    }
                    else if (i_Game.GetMatValue(i_Game.m_GameBoard, row, cols) == 'O')
                    {
                        rowsOcount++;
                    }
                }

                if (rowsXcount == i_Game.BoardSize)
                {
                    isSecondPlayerWon = true;
                }
                else if (rowsOcount == i_Game.BoardSize)
                {
                    i_Game.FirstPlayer.IsWon = true;
                }
            }

            for (int cols = 0; cols < i_Game.BoardSize; cols++)
            {
                colsXCounter = 0;
                colsOCounter = 0;
                for (int row = 0; row < i_Game.BoardSize; row++)
                {
                    if (i_Game.GetMatValue(i_Game.m_GameBoard, row, cols) == 'X')
                    {
                        colsXCounter++;
                    }
                    else if (i_Game.GetMatValue(i_Game.m_GameBoard, row, cols) == 'O')
                    {
                        colsOCounter++;
                    }
                }

                if (colsXCounter == i_Game.BoardSize)
                {
                    isSecondPlayerWon = true;
                }
                else if (colsOCounter == i_Game.BoardSize)
                {
                    i_Game.FirstPlayer.IsWon = true;
                }
            }

            for (int row = 0; row < i_Game.BoardSize; row++)
            {
                if (i_Game.GetMatValue(i_Game.m_GameBoard, row, row) == 'X')
                {
                    leftToRightDiagonalX++;
                }
                else if (i_Game.GetMatValue(i_Game.m_GameBoard, row, row) == 'O')
                {
                    leftToRightDiagonalO++;
                }
            }

            for (int row = 0, cols = i_Game.BoardSize - 1; row < i_Game.BoardSize; row++, cols--)
            {
                if (i_Game.GetMatValue(i_Game.m_GameBoard, row, cols) == 'X')
                {
                    rightToLeftDiagonalX++;
                }
                else if (i_Game.GetMatValue(i_Game.m_GameBoard, row, cols) == 'O')
                {
                    rightToLeftDiagonalO++;
                }
            }

            if (rightToLeftDiagonalX == i_Game.BoardSize || leftToRightDiagonalX == i_Game.BoardSize)
            {
                isSecondPlayerWon = true;
            }

            if (rightToLeftDiagonalO == i_Game.BoardSize || leftToRightDiagonalO == i_Game.BoardSize)
            {
                i_Game.FirstPlayer.IsWon = true;
            }
            
            return isSecondPlayerWon;
        }

        public Player AiPlayer
        {
            get
            {
                return m_AiPlayer;
            }

            set
            {
                m_AiPlayer = value;
            }
        }

        public Player FirstPlayer
        {
            get
            {
                return m_FirstPlayer;
            }

            set
            {
                m_FirstPlayer = value;
            }
        }

        public Player SecondPlayer
        {
            get
            {
                return m_SecondPlayer;
            }

            set
            {
                m_SecondPlayer = value;
            }
        }

        public int BoardSize
        {
            get
            {
                return m_BoardSize;
            }

            set
            {
                m_BoardSize = value;
            }
        }

        private void AddScore(bool i_isSecondPlayerWon)
        {
            bool isDraw = false;
            if (this.FirstPlayer.IsWon == true)
            {
                this.FirstPlayer.Score = this.FirstPlayer.Score + 1;
            }
            else if (i_isSecondPlayerWon == true)
            {
                if (this.SecondPlayer != null)
                {
                    this.SecondPlayer.IsWon = true;
                    this.SecondPlayer.Score = this.SecondPlayer.Score + 1;
                }
                else
                {
                    this.AiPlayer.IsWon = true;
                    this.AiPlayer.Score = this.AiPlayer.Score + 1;
                }
            }
            else
            {
                isDraw = DrawCheck(this);
                if (isDraw == true)
                {
                    this.FirstPlayer.IsWon = true;
                    if (this.SecondPlayer != null)
                    {
                        this.SecondPlayer.IsWon = true;
                    }
                    else
                    {
                        this.AiPlayer.IsWon = true;
                    }
                }
            }
        }

        public void RoundWithHeumen()
        {
            if (this.m_FirstPlayer.PlayerTurn == true)
            {
                this.m_FirstPlayer.PlayerTurn = false;
                this.SecondPlayer.PlayerTurn = true;
            }
            else
            {
                this.m_FirstPlayer.PlayerTurn = true;
                this.m_SecondPlayer.PlayerTurn = false;
            }
        }

        public char GetMatValue(char[,] i_Board, int i_Row, int i_Cols)
        {
            return i_Board[i_Row, i_Cols];
        }

        public string GetMessageTitle()
        {
            string returnTitle = string.Empty;
            if (this.AiPlayer == null)
            {
                if (this.FirstPlayer.IsWon == true && this.SecondPlayer.IsWon == true)
                {
                    returnTitle = "A Tie!";
                }
                else if (this.FirstPlayer.IsWon == true || this.SecondPlayer.IsWon == true)
                {
                    returnTitle = "A Win!";
                }
            }
            else
            {
                if (this.FirstPlayer.IsWon == true && this.AiPlayer.IsWon == true)
                {
                    returnTitle = "A Tie!";
                }
                else if (this.FirstPlayer.IsWon == true || this.AiPlayer.IsWon == true)
                {
                    returnTitle = "A Win!";
                }
            }

            return returnTitle;
        }

        public string GetWinningPlayer()
        {
            string endOfGameMessage = string.Empty;
            if (this.AiPlayer == null)
            {
                if (this.FirstPlayer.IsWon == true && this.SecondPlayer.IsWon == true)
                {
                    endOfGameMessage = "Tie!";
                }
                else if (this.FirstPlayer.IsWon == true)
                {
                    endOfGameMessage = string.Format("The winner is {0}!", this.FirstPlayer.PlayerName);
                }
                else
                {
                    endOfGameMessage = string.Format("The winner is {0}!", this.SecondPlayer.PlayerName);
                }
            }
            else
            {
                if (this.FirstPlayer.IsWon == true && this.AiPlayer.IsWon == true)
                {
                    endOfGameMessage = "Tie!";
                }
                else if (this.FirstPlayer.IsWon == true)
                {
                    endOfGameMessage = string.Format("The winner is {0}!", this.FirstPlayer.PlayerName);
                }
                else
                {
                    endOfGameMessage = string.Format("The winner is {0}!", this.AiPlayer.PlayerName);
                }
            }

            return endOfGameMessage;
        }
    }
}