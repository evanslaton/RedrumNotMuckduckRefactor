using System;
using System.Linq;
using System.Threading;

namespace redrum_not_muckduck_game
{
    // This class controls the solution to win the game and the amount of live user has left
    // You can find the methods for determining a right or wrong guess
    public class Solution
    {
        public static string[] Solutions = new string[] { "dwight", "beet stained cigs", "breakroom" };
        public static string[] AllTheWaysToSayYes = new string[] { "y", "yes", "yeah", "yep", "yea" };

        public static bool AskToSolvePuzzle()
        {
            Console.Write("> ");
            string userInput = Console.ReadLine().ToLower();
            if (AllTheWaysToSayYes.Contains(userInput)) { return true; }
            else { return false;};
        }

        public static bool CheckSolution()
        {
            string[] questions = new string[] { "Michael: \"Who did it?\"", "Michael: \"What did they use?\"", "Michael: \"Where did it happen?\"" };
            for (int i = 0; i < Solutions.Length; i++)
            {
                Delete.Scene();
                Render.OneLineQuestionOrQuote(questions[i]);
                Game.Board.Render();
                Console.Write("> ");
                string userGuess = Console.ReadLine();
                if (userGuess.ToLower() != Solutions[i])
                {
                    LoseALife();
                    WrongGuess();
                    return false; //Wrong guess - return false so that the game continues
                }
                RightGuess();
            }
            return true; //At this point all guesses were correct so the game ends
        }

        private static void WrongGuess()
        {
            Delete.Scene();
            Render.OneLineQuestionOrQuote("Michael: \"That sounds off - OUCH, lose a life\"");
            Game.Board.Render();
            Thread.Sleep(2000);//Display if the guess was wrong for 2 second
            Delete.Scene();
            Game.Board.Render();
        }

        private static void RightGuess()
        {
            Delete.Scene();
            Render.OneLineQuestionOrQuote("Michael : \"That's right!\"");
            Game.Board.Render();
            Thread.Sleep(2000); //Display if the guess was right/wrong for 2 second
        }

        private static void LoseALife()
        {
            //first character column of life starts at 50 and ends at 78
            //first character row of life starts at 2 and ends at 6
            int COLUMN_WHERE_HEARTS_START = 50;
            int ROW_WHERE_HEARTS_START = 2;
            int WIDTH_OF_HEART = 9;
            int HEIGHT_OF_HEART = 4;
            int heartDeletionStartColumn = 
                COLUMN_WHERE_HEARTS_START +
                //Adjusts Column to first char of last heart
                ((Game.Number_of_Lives-1) * WIDTH_OF_HEART) +
                //Adjusts for spaces between hearts
                (Game.Number_of_Lives-1);
            for (int i = 0; i < HEIGHT_OF_HEART; i++)
            {
                for (int j = 0; j < WIDTH_OF_HEART; j++)
                {
                    Board.board[ROW_WHERE_HEARTS_START + i, heartDeletionStartColumn + j] = ' ';
                }
            }
            Game.Number_of_Lives -= 1;
        }
    }
}

