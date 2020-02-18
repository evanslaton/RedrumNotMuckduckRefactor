using redrum_not_muckduck_game.save;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Console = Colorful.Console;

namespace redrum_not_muckduck_game
{
    class Game
    {
        public static readonly bool Is_Windows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        private static Random rand = new Random();
        public static Room CurrentRoom { get; set; }
        public static List<Room> AllRooms { get; set; }
        public static int NumberOfLives { get; set; } = 3;
        public static int NumberOfItemsFound { get; set; } = 0;
        public static int NumberOfNames { get; set; } = 0;
        public static bool IsGameOver { get; set; } = false;
        public static bool UserQuitGame { get; set; } = false;
        public static List<string> CollectedHints { get; set; } = new List<string>();
        public static List<string> VisitedRooms { get; set; } = new List<string>();
        public static Board Board { get; set; } = new Board();
        public static HelpPage HelpPage = new HelpPage();
        public Map Map = new Map();

        public Game()
        {
            AllRooms = Room.CreateRooms();
            CurrentRoom = AllRooms[0];
        }

        public void Play()
        {
            CheckForSavedData();
            while (!IsGameOver)
            {
                UserTurn();
            }
            EndOfGame();
        }

        public void CheckForSavedData()
        {
            try
            {
                SaveData saveData = SaveData.GetSavedData();
                CurrentRoom = saveData.CurrentRoom;
                Board.GameBoard = saveData.GameBoard;
                VisitedRooms = saveData.VisitedRooms;
                NumberOfItemsFound = saveData.NumberOfItemsFound;
                NumberOfLives = saveData.NumberOfLives;
            }
            catch
            {
                StartSetUp();
            }
        }

        private void StartSetUp()
        {
            //if (Is_Windows) { Sound.PlaySound(@"utilities\Theme.mp4", 1000); } //If device is windows - play music
            WelcomePage.AcsiiArt();
            WelcomePage.StoryIntro();
            Render.Location(CurrentRoom);
            Render.Action();
            Render.SceneDescription();
        }

        private void UserTurn()
        {
            Board.Render();
            Console.WriteLine("Please enter a valid option: (explore, talk, leave, map, quit)");
            Console.Write("> ");
            string userChoice = Console.ReadLine().ToLower();

            switch (userChoice)
            {
                case "leave":
                    LeaveTheRoom();
                    break;
                case "explore":
                    ExploreRoom();
                    break;
                case "talk":
                    TalkToPerson();
                    break;
                case "quit":
                    IsGameOver = true;
                    UserQuitGame = true;
                    break;
                case "save":
                    SaveTheGame();
                    break;
                case "map":
                    Map.Render(CurrentRoom.Name);
                    ExitMap();
                    break;
                case "help":
                    HelpPage.Render();
                    break;
                default:
                    break;
            }
        }

        private void ExitMap()
        {
            ConsoleKey key;
            do
            {
                key = Console.ReadKey(true).Key;
            } while (key != ConsoleKey.Enter);
        }

        private void LeaveTheRoom()
        {
            string nextRoom = Map.LeaveRoom(CurrentRoom.Name);
            UpdateCurrentRoom(nextRoom);
        }

        private void ExploreRoom()
        {
            Delete.Scene();
            if (CurrentRoom.ItemInRoom.Count == 0)
            {
                Render.Quote("There is nothing of note in the room.");
            }
            else if (CurrentRoom.ItemInRoom.ContainsValue(false))
            {
                //Lists name of person in the current room then renders to UI
                Render.ExploreChoices(CurrentRoom.ItemInRoom);
                Board.Render();
                //Prompts user input for name of who they want to talk to
                string itemSelected;
                do
                {
                    itemSelected = UserSelection();
                } while (!ValidateExploreSelection(itemSelected));
            }
            else
            {
                Render.Quote("There is nothing left to find here.");
            }
        }

        private bool ValidateExploreSelection(string itemSelected)
        {
            if (itemSelected.Equals("exit"))
            {
                Delete.Scene();
                return true;
            }
            //Searches ItemInRoom dictionary keys for match of player input value
            foreach (KeyValuePair<string, bool> str in CurrentRoom.ItemInRoom)
            {
                //if (itemSelected == str.Key.ToLower() && str.Value == false)
                if (str.Key.ToLower().Contains(itemSelected) && str.Value == false && itemSelected.Length > 2)
                {
                    //Removes list of people in room
                    Delete.Scene();
                    Render.Quote($"You pick up {str.Key}");
                    CurrentRoom.ItemInRoom[str.Key] = true;
                    Render.FoundItemsList(str.Key);
                    NumberOfItemsFound++;
                    //Stops requesting input
                    return true;
                }
            }
            //If item is not found, board re-renders, notifies player, and continues input request
            Board.Render();
            Console.WriteLine("That item is not around. Maybe the smoke is getting to you...");
            return false;
        }

        private void TalkToPerson()
        {
            //Removes prior scene text
            Delete.Scene();
            if (CurrentRoom.PersonsInRoom.Count == 0)
            {
                Render.Quote("There is no one in the room to talk to.");
            }
            else
            {
            //Lists name of person in the current room then renders to UI
                Render.TalkChoices(CurrentRoom.PersonsInRoom);
                Board.Render();
                string nameSelected;
            //Prompts user input for name of who they want to talk to
                do
                {
                    nameSelected = UserSelection();
                } while (!ValidateTalkSelection(nameSelected));
                //AddQuoteToHintPage();
                CheckIfTalkingToMichael();
            }
        }

        private string UserSelection() //AskUserWhoToTalkTo()
        {
            Console.WriteLine("Type selection or type \'exit\' to return to Room menu");
            Console.Write("> ");
            return Console.ReadLine().ToLower();
        }

        private bool ValidateTalkSelection(string nameSelected)
        {
            string quote;
            if (nameSelected.Equals("exit"))
            {
                Delete.Scene();
                return true;
            }
            //Searches PersonsInRoom dictionary keys for match of player input value
            foreach (KeyValuePair<string, string> str in CurrentRoom.PersonsInRoom)
            {
                if (nameSelected == str.Key.ToLower())
                {
                    //Concatenates dictionary key + value to create quote
                    quote = str.Key + str.Value;
                    //Removes list of people in room
                    Delete.Scene();
                    Render.Quote(quote);
                    //Stops requesting input
                    return true;
                }
            }
            //If name is not found, board re-renders, notifies player, and continues input request
            Board.Render();
            Console.WriteLine("Not a person in this room. Please select someone in the room to talk to.");
            return false;
        }

        //private void AddQuoteToHintPage()
        //{
        //    //Check if quote has been added to hint page
        //    //Unless we're in Reception - because talking to Michael is to end the game not to get a hint
        //    if (!Collected_Hints.Contains(CurrentRoom.GetQuote()) && CurrentRoom.Name != "Reception")
        //    {
        //        Collected_Hints.Add(CurrentRoom.GetQuote());
        //        HintPage.DisplayHints(CurrentRoom.GetQuote());
        //    }
        //}

        private void CheckIfTalkingToMichael()
        {
            if (CurrentRoom.Name == "Reception")
            {
                Board.Render();
                bool userWantsToSolve = Solution.AskToSolvePuzzle();
                if (userWantsToSolve)
                {
                    IsGameOver = Solution.CheckSolution();
                    Delete.Scene();
                    CheckHealth();
                }
                else
                {
                    Delete.Scene();
                    Render.OneLineQuestionOrQuote("Michael: \"Ok, come back when you are ready\"");
                }
            }
        }

        private void UpdateCurrentRoom(string nextRoom)
        {
            Delete.Scene();
            Delete.Location(CurrentRoom);
            //Loop through adjacent rooms to see which one the user selected
            foreach (Room Room in AllRooms)
            {
                if (nextRoom.ToLower() == Room.Name.ToLower())
                {
                    CheckIfVistedRoom(CurrentRoom.Name); //Check if user has been to this room
                    CurrentRoom = Room; //Update the current room
                }
            }
            Render.Location(CurrentRoom);
            RenderSpecialActionsInRooms();
            Delete.Scene();
            Render.SceneDescription();
        }

        private void RenderSpecialActionsInRooms()
        {
            int randomPercentage = PercentChanceGenerator();

            Delete.Scene();
            if (randomPercentage > 0)
            {
                Delete.Scene();
                Render.ActionQuote(CurrentRoom.Action);
                if (CurrentRoom.Name.Equals("Break Room") || CurrentRoom.Name.Equals("Sales"))
                {
                    if (NumberOfLives < 3)
                    {
                        Solution.AddAHeartToBoard();
                        //This must occur prior to rendering the heart to prevent it being out of bounds
                        //This is opposite of actions that lose a life due to how the Gain/Loss methods 
                        NumberOfLives++;
                    }
                    Board.Render();
                    System.Console.WriteLine("Press any key to continue:");
                    Console.ReadKey(true);
                }
                else if (CurrentRoom.Name.Equals("Accounting"))
                {
                    NumberOfLives--;
                    Solution.RemoveAHeartFromBoard();
                    if (NumberOfLives <= 0)
                        EndPage.LoseScene();
                    Board.Render();
                    System.Console.WriteLine("Press any key to continue:");
                    Console.ReadKey(true);
                }
            }
        }

        private void CheckIfVistedRoom(string roomName)
        {
            if (!VisitedRooms.Contains(roomName))
            {
                VisitedRooms.Add(roomName); //Add room to list of seen rooms
                Render.VistedRooms(roomName); //Render to the board
            }
        }

        private int PercentChanceGenerator()
        {
            int percentage = rand.Next(1, 100);
            return percentage;
        }

        private void SaveTheGame()
        {
            Console.Clear();
            SaveData.Save();
            Console.WriteLine("Your game has been saved.");
        }

        private void CheckHealth()
        {
            if (NumberOfLives == 0)
            {
                IsGameOver = true;
            }
        }

        private void EndOfGame()
        {
            if (NumberOfLives == 0)
            { 
                EndPage.LoseScene();
                SaveData.Delete();
            }
            else if (UserQuitGame)
            {
                EndPage.QuitScene();
            }
            else
            {
                EndPage.WinScene();
                SaveData.Delete();
            }
            EndPage.ThankYouAsciiArt();
        }
    }
}
