using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex05
{
    public class SettingsData
    {
        private string m_FirstPlayer = string.Empty;
        private string m_SecondPlayer = string.Empty;
        private string m_AIPlayer = string.Empty;
        private int m_SizeOfTheBoard = 4;
        private bool m_IsWantToStart = false;

        public SettingsData()
        {
            m_FirstPlayer = null;
            m_SecondPlayer = null;
            m_AIPlayer = null;
            m_SizeOfTheBoard = 4;
            m_IsWantToStart = false;
        }

        public bool IsWantToStart
        {
            get
            {
                return m_IsWantToStart;
            }

            set
            {
                m_IsWantToStart = value;
            }
        }

        public int SizeOfTheBoard
        {
            get
            {
                return m_SizeOfTheBoard;
            }

            set
            {
                m_SizeOfTheBoard = value;
            }
        }

        public string FirstPlayer
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

        public string SecondPlayer
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

        public string AIPlayer
        {
            get
            {
                return m_AIPlayer;
            }

            set
            {
                m_AIPlayer = value;
            }
        }

        public bool IsPlayerTwoHuman()
        {
            bool isPlayerHuman = false;
            if (this.SecondPlayer != null)
            {
                isPlayerHuman = true;
            }

            return isPlayerHuman;
        }
    }
}
