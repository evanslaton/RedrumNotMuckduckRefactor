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
            return WAYS_TO_SAY_YES.Contains(userInput);
        }

        public static bool CheckSolution()
        {
            int correctAnswers = 0;
            while(correctAnswers < 3 && Game.NumberOfLives > 0)
            {
                Delete.SceneTextArea();
                Render.OneLineQuestionOrQuote(QUESTIONS[correctAnswers]);
                Game.Board.Render();
                Console.WriteLine("Input your guess or type \'exit\' to stop guessing:");
                Console.Write("> ");
                string userGuess = Console.ReadLine();
                if (userGuess == "exit") break;
                else if (userGuess.ToLower() != SOLUTIONS[correctAnswers])
                {
                    Game.NumberOfLives--;
                    RemoveAHeartFromBoard();
                    DisplayText(WRONG_ANSWER_TEXT);
                    continue;
                }
                DisplayText(CORRECT_ANSWER_TEXT);
                correctAnswers++;
            }
            return correctAnswers == 3 || Game.NumberOfLives == 0;
        }

        private static void DisplayText(string text)
        {
            Delete.SceneTextArea();
            Render.OneLineQuestionOrQuote(text);
            Game.Board.Render();
            Thread.Sleep(2000);
        }

        public static void RemoveAHeartFromBoard()
        {
            int heartDeletionStartColumn = GetColumn();
            for (int i = 0; i < HEIGHT_OF_HEART; i++)
            {
                for (int j = 0; j < WIDTH_OF_HEART; j++)
                {
                    Board.GameBoard[ROW_WHERE_HEARTS_START + i, heartDeletionStartColumn + j] = ' ';
                }
            }
        }

        public static void AddAHeartToBoard()
        {
            int currentColumn = 0;
            int heartStringIndex = 0;
            int heartAdditionStartColumn = GetColumn();
            string heart =  " .-. .-. " + "|   \'   |" + " \'~_ _~\' " + "    \'    ";
            for (int i = 0; i < HEIGHT_OF_HEART; i++)
            {
                for (int j = 0; j < WIDTH_OF_HEART; j++)
                {
                    Board.GameBoard[ROW_WHERE_HEARTS_START + i, heartAdditionStartColumn + currentColumn] = heart[heartStringIndex];
                    currentColumn++;
                    heartStringIndex++;
                }
                currentColumn = 0;
            }
        }

        public static int GetColumn()
        {
            return COLUMN_WHERE_HEARTS_START + ((Game.NumberOfLives) * WIDTH_OF_HEART) + (Game.NumberOfLives);
        }
    }
}

