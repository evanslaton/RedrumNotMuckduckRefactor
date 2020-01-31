﻿using System;
using System.Threading;

namespace redrum_not_muckduck_game
{
    public class Solution
    {
        public string[] Solutions = new string[] { "dwight", "beet stained cigs", "break room" };

        public void CheckSolution()
        {
            string[] questions = new string[] { "Michael: \"Who did it?\"", "Michael: \"What did they use?\"", "Michael: \"Where did it happen?\"" };
            for (int i = 0; i < Solutions.Length; i++)
            {
                Game.Render.DeleteQuestion();
                Game.Render.AskForSolution(questions[i]);
                Game.Board.Render();
                Console.Write("> ");
                string userGuess = Console.ReadLine();
                if (userGuess.ToLower() != Solutions[i])
                {
                    LoseALife();
                    WrongGuess();
                    return;
                }
                RightGuess();
                Thread.Sleep(1000);
            }
            Game.IsGameOver = true;
        }

        private void WrongGuess()
        {
            Game.Render.DeleteQuestion();
            Game.Render.AskForSolution("That sounds off, try again");
            Game.Board.Render();
            Thread.Sleep(1000);
            Game.Render.DeleteQuestion();
            Game.Board.Render();
        }

        private void RightGuess()
        {
            Game.Render.DeleteQuestion();
            Game.Render.AskForSolution("Thats right!");
            Game.Board.Render();
        }

        private void LoseALife()
        {
            int COLUMN_WHERE_HEARTS_START = 49;
            int ROW_WHERE_HEARTS_START = 2;
            int COLUMN_TO_DELETE_HEART_FROM = Game.Number_of_Lives + COLUMN_WHERE_HEARTS_START;
            Board.board[ROW_WHERE_HEARTS_START, COLUMN_TO_DELETE_HEART_FROM] = ' ';
            Game.Number_of_Lives -= 1;
        }
    }
}

