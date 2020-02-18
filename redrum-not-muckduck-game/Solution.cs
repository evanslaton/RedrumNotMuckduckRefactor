using System;
using System.Linq;
using System.Threading;

namespace redrum_not_muckduck_game
{
    public class Solution
    {
        private static string[] QUESTIONS = new string[] {
            "Michael: \"Who did it?\"",
            "Michael: \"What did they use?\"",
            "Michael: \"Where did it happen?\""
        };
        private static string[] SOLUTIONS = new string[] { "dwight", "beet stained cigs", "break room" };
        private static string[] WAYS_TO_SAY_YES = new string[] { "y", "yes", "yeah", "yep", "yea", "yeppers", "yesh" };
        private static string WRONG_ANSWER_TEXT = "Michael: \"That sounds off - OUCH, lose a life\"";
        private static string CORRECT_ANSWER_TEXT = "Michael : \"That's right!\"";
        private static int COLUMN_WHERE_HEARTS_START = 50; // and ends at column 78
        private static int ROW_WHERE_HEARTS_START = 2; // and ends at row 6
        private static int WIDTH_OF_HEART = 9;
        private static int HEIGHT_OF_HEART = 4;

        public static bool AskToSolvePuzzle()
        {
            Console.Write("> ");
            string userInput = Console.ReadLine().ToLower();
            return WAYS_TO_SAY_YES.Contains(userInput) ? true : false;
        }

        public static bool CheckSolution()
        {
            for (int i = 0; i < SOLUTIONS.Length; i++)
            {
                Delete.Scene();
                Render.OneLineQuestionOrQuote(QUESTIONS[i]);
                Game.Board.Render();
                Console.Write("> ");
                string userGuess = Console.ReadLine();
                if (userGuess.ToLower() != SOLUTIONS[i])
                {
                    Game.NumberOfLives--;
                    RemoveAHeartFromBoard();
                    DisplayText(WRONG_ANSWER_TEXT);
                    Delete.Scene();
                    Game.Board.Render();
                    return false;
                }
                DisplayText(CORRECT_ANSWER_TEXT);
            }
            return true;
        }

        private static void DisplayText(string text)
        {
            Delete.Scene();
            Render.OneLineQuestionOrQuote(text);
            Game.Board.Render();
            Thread.Sleep(2000);
        }

        public static void RemoveAHeartFromBoard()
        {
            int heartDeletionStartColumn = 
                COLUMN_WHERE_HEARTS_START +
                //Adjusts Column to first char of last heart
                ((Game.NumberOfLives) * WIDTH_OF_HEART) +
                //Adjusts for spaces between hearts
                (Game.NumberOfLives);
            for (int i = 0; i < HEIGHT_OF_HEART; i++)
            {
                for (int j = 0; j < WIDTH_OF_HEART; j++)
                {
                    Board.board[ROW_WHERE_HEARTS_START + i, heartDeletionStartColumn + j] = ' ';
                }
            }
        }

        public static void AddAHeartToBoard()
        {
            int currentColumn = 0;
            int heartStringIndex = 0;
            int heartAdditionStartColumn =
                COLUMN_WHERE_HEARTS_START +
                //Adjusts Column to first char of last heart
                ((Game.NumberOfLives) * WIDTH_OF_HEART) +
                //Adjusts for spaces between hearts
                (Game.NumberOfLives);
            string heart =  " .-. .-. " + "|   \'   |" + " \'~_ _~\' " + "    \'    ";
            for (int i = 0; i < HEIGHT_OF_HEART; i++)
            {
                for (int j = 0; j < WIDTH_OF_HEART; j++)
                {
                    Board.board[ROW_WHERE_HEARTS_START + i, heartAdditionStartColumn + currentColumn] = heart[heartStringIndex];
                    currentColumn++;
                    heartStringIndex++;
                }
                currentColumn = 0;
            }
        }
    }
}

