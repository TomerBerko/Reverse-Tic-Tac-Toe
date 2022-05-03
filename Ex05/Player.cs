using System;

namespace Ex05
{
    public class Player
    {
        private string m_PlayerName = string.Empty;
        private bool m_IsPlayerTurn = false;
        private bool m_IsWin = false;
        private char m_PlayerSymbol = 'X';
        private int m_ScoreCounter = 0;

        public Player(string i_PlayerName)
        {
            m_PlayerName = i_PlayerName;
            m_IsPlayerTurn = false;
            m_IsWin = false;
            m_PlayerSymbol = 'X';
            m_ScoreCounter = 0;
        }

        public string PlayerName
        {
            get
            {
                return m_PlayerName;
            }

            set
            {
                m_PlayerName = value;
            }
        }

        public int Score
        {
            get
            {
                return m_ScoreCounter;
            }

            set
            {
                m_ScoreCounter = value;
            }
        }

        public bool PlayerTurn
        {
            get
            {
                return m_IsPlayerTurn;
            }

            set
            {
                m_IsPlayerTurn = value;
            }
        }

        public bool IsWon
        {
            get
            {
                return m_IsWin;
            }

            set
            {
                m_IsWin = value;
            }
        }

        public char Symbol
        {
            get
            {
                return m_PlayerSymbol;
            }

            set
            {
                m_PlayerSymbol = value;
            }
        }
    }
}