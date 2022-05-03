using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex05
{
    public class UI
    {
        public static void StartGame()
        {
            bool isPlayerTwoHuman = false;
            int boardSize = 0;

            SettingsData theChosenData = new SettingsData();

            GameSettingsForm gameSettingForm = new GameSettingsForm(ref theChosenData);
            gameSettingForm.ShowDialog();

            boardSize = theChosenData.SizeOfTheBoard;
            isPlayerTwoHuman = theChosenData.IsPlayerTwoHuman();

            Game currentGame = new Game(isPlayerTwoHuman, theChosenData);
            if (theChosenData.IsWantToStart == true)
            {
                GameBoardForm theChosenGame = new GameBoardForm(currentGame);
                theChosenGame.ShowDialog();
            }
        }
    }
}