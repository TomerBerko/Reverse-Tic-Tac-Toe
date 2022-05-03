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
    public partial class GameSettingsForm : Form
    {
        private SettingsData m_TheChosenData = null;

        public GameSettingsForm(ref SettingsData theChosenData)
        {
            m_TheChosenData = theChosenData;
            InitializeComponent();
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown ourNumeric = sender as NumericUpDown;
            if (ourNumeric == numericUpDownRows)
            {
                numericUpDownCols.Value = numericUpDownRows.Value;
            }
            else
            {
                numericUpDownRows.Value = numericUpDownCols.Value;
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            this.Hide();
            string player2;
            string player1 = textBoxPlayer1.Text;
            if (this.textBoxPlayer2.Text != "[Computer]")
            {
                player2 = textBoxPlayer2.Text;
                this.m_TheChosenData.SecondPlayer = player2;
            }
            else
            {
                player2 = textBoxPlayer2.Text;
                this.m_TheChosenData.AIPlayer = player2;
            }

            int sizeOfTheBoard = Convert.ToInt32(numericUpDownCols.Value);
            this.m_TheChosenData.FirstPlayer = player1;
            this.m_TheChosenData.SizeOfTheBoard = sizeOfTheBoard;
            this.m_TheChosenData.IsWantToStart = true;
            this.Close();
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxPlayer2.Checked)
            {
                this.textBoxPlayer2.Enabled = true;
                this.textBoxPlayer2.Text = string.Empty;
            }
            else
            {
                this.textBoxPlayer2.Enabled = false;
                this.textBoxPlayer2.Text = "[Computer]";
            }
        }
    }
}
