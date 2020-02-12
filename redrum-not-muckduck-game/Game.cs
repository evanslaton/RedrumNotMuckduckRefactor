using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Console = Colorful.Console;

namespace redrum_not_muckduck_game
{
    // This class controls the game logic
    // You can find the game loop, user turn loop, navigation logic, and sets the scene for the game
    class Game
    {
        public static Room Accounting { get; set; }
        public static Room Sales { get; set; }
        public static Room Kitchen { get; set; }
        public static Room Breakroom { get; set; }
        public static Room Reception { get; set; }
        public static Room Annex { get; set; }
        public static Room Exit { get; set; }
        public static Room CurrentRoom { get; set; }
        public static List<Room> List_Of_All_Rooms { get; set; }
        public static int Number_of_Lives { get; set; } = 3;
        public static int Number_of_Items { get; set; } = 0;
        public static int Number_of_Rooms { get; set; } = 0;
        public static int Number_of_Names { get; set; } = 0;
        public static bool Is_Game_Over { get; set; } = false;
        public static bool userQuitGame { get; set; } = false;
        public static List<string> Collected_Hints { get; set; } = new List<string>();
        public static List<string> Visited_Rooms { get; set; } = new List<string>();
        public Map Map = new Map();

        //Instances of all "pages/scences" within the game
        public static Board Board = new Board();
        public static HelpPage HelpPage = new HelpPage();
        public static HintPage HintPage = new HintPage();
        //Checks OS of user
        public static readonly bool Is_Windows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        public Game()
        {
            Accounting = new Room(
               "Accounting",
               "Your desk is covered in M&Ms.  " +
               "*Oscar is trying to find an exit.  " +
               "*Out of the corner of your eye, you " +
               "*see a drawer slowly open. ",
               "Angela's cat, Bandit",
              new Dictionary<string, string>()
               {
                    { "Oscar", " : \"Angela, stay here. I am going*up into the ceiling to find*a way out and get help!\"" },
                    { "Angela", " : \"Oscar! Take me with you!\"" }
               },
               true
               );
            Sales = new Room(
               "Sales",
               "Chaos ensues as the smoke thickens. " +
               "*Andy is frantically running in circles and " +
               "*knocks over his trash can, something makes " +
               "*a thud sound as it falls out.",
               "a random torch",
               new Dictionary<string, string>()
                   {
                    { "Andy", " : \"This would never happen at Cornell...\"" },
                    { "Stanley", " : \"What'll happen to Pretzal Day?!\"" },
                    { "Jim", " : \"Let's ram the door with the copier!\"" }
                   },
               true
               );
            Kitchen = new Room(
               "Kitchen",
               "Why is Phyllis just standing here? " +
               "*She seems very disturbed... ",
                "Oscar falling out of ceiling",
                new Dictionary<string, string>()
                {
                    { "Phyllis", " : \"I saw Dwight come from the breakroom\"" }
                },
                false
                );
            Breakroom = new Room(
                "Break Room",
                "You are hungry but is there " +
                "*time? Probably right?",
                "vending machine",
                new Dictionary<string, string>(){},
                false
                );
            Reception = new Room(
                "Reception",
                "Michael waits to hear what " +
                "*you think happened today",
                "no item",
                new Dictionary<string, string>()
                {
                    { "Michael", " : \"Would you like to solve the puzzle?\"" }
                },
                false
                );
            Annex = new Room(
                "Annex",
                "You have made it to the " +
                "*back of the office " +
                "*you should probably go back. " +
                "*Kelly waits around for Ryan. " +
                "*He doesn't smoke cigarettes does " +
                "*he?",
                "beet stained cigs",
                new Dictionary<string, string>()
                {
                    { "Kelly", " : \"Why does Dwight have a blow horn?\"" },
                    { "Toby", " : \"I wish I were in Costa Rica still...\"" }
                },
                true
                );

            CurrentRoom = Accounting;

            Accounting.AdjacentRooms = new List<Room> { Sales };
            Sales.AdjacentRooms = new List<Room> { Reception, Accounting, Kitchen };
            Reception.AdjacentRooms = new List<Room> { Sales };
            Kitchen.AdjacentRooms = new List<Room> { Sales, Annex };
            Annex.AdjacentRooms = new List<Room> { Kitchen, Breakroom };
            Breakroom.AdjacentRooms = new List<Room> { Annex };
            List_Of_All_Rooms = new List<Room> { Accounting, Sales, Reception, Kitchen, Annex, Breakroom };
        }

        public void Play()
        {
            CheckForSavedData();
            Board.Render();
            while (!Is_Game_Over)
            {
                UserTurn();
            }
            EndOfGame();
        }

        public void CheckForSavedData()
        {
            //Find the files where the data is being stored
            SaveVisitedRooms.GetWorkingVisitedRoomsDirectory();
            SaveHintQuotes.GetWorkingHintQuotesDirectory();
            SaveWholeBoard.GetWorkingBoardDirectory();
            SaveElements.GetWorkingElementDirectory();
            SaveHints.GetWorkingHintDirectory();

            //Create files
            if (!File.Exists(SaveWholeBoard.WorkingBoardDirectory))
            {
                File.Create(SaveVisitedRooms.WorkingVisitedRoomsDirectory);
                File.Create(SaveHintQuotes.WorkingHintQuotesDirectory);
                File.Create(SaveWholeBoard.WorkingBoardDirectory);
                File.Create(SaveElements.WorkingElementDirectory);
                File.Create(SaveHints.WorkingHintDirectory);
            }

            //If there is saved data - load it
            if (new FileInfo(SaveWholeBoard.WorkingBoardDirectory).Length != 0)
            {
                SaveVisitedRooms.Stored();
                SaveHintQuotes.Stored();
                SaveHints.Stored();
                SaveWholeBoard.Stored();
                SaveElements.StoredElements();
            }
            else //Otherwise - setup a new game
            {
                StartSetUp();
            }
        }

        private void StartSetUp()
        {
            //if (Is_Windows) { Sound.PlaySound("Theme.mp4", 1000); } //If device is windows - play music
            WelcomePage.AcsiiArt();
            WelcomePage.StoryIntro();
            Render.Location(CurrentRoom);
            Render.Action();
            Render.SceneDescription();
        }

        private void UserTurn()
        {
            Console.Write("> ");
            string userChoice = Console.ReadLine().ToLower();
            Console.WriteLine();

            switch (userChoice)
            {
                case "leave":
                    LeaveTheRoom();
                    Board.Render();
                    break;
                case "explore":
                    CheckIfItemHasBeenFound();
                    break;
                case "talk":
                    TalkToPerson();
                    break;
                case "quit":
                    Is_Game_Over = true;
                    userQuitGame = true;
                    break;
                case "save":
                    SaveTheGame();
                    break;
                case "map":
                    Map.Render(CurrentRoom.Name);
                    ExitMap();
                    Board.Render();
                    break;
                case "help":
                    HelpPage.Render();
                    break;
                case "hint":
                    HintPage.Render();
                    break;
                default:
                    Board.Render();
                    Console.WriteLine("Please enter a valid option: (explore, talk, leave, map, quit)");
                    break;
            }
        }

        private void ExitMap()
        {
            ConsoleKey key = Console.ReadKey(true).Key;
            while (key != ConsoleKey.Enter)
            {
                key = Console.ReadKey(true).Key;
            }
        }

        private void LeaveTheRoom()
        {
            string nextRoom = Map.LeaveRoom(CurrentRoom.Name);
            Delete.Scene();
            UpdateCurrentRoom(nextRoom);

        }

        private void CheckIfItemHasBeenFound()
        {
            Delete.Scene();
            if (CurrentRoom.HasItem)
            {
                Render.OneLineQuestionOrQuote($"You found: {CurrentRoom.ItemInRoom}");
                Render.ItemToFoundItems(CurrentRoom.ItemInRoom);
                CurrentRoom.HasItem = !CurrentRoom.HasItem;
                Number_of_Items++;
            }
            else
            {
                Render.OneLineQuestionOrQuote("Nothing left to explore");
            }
            Board.Render();
        }

        private void TalkToPerson()
        {
            //Removes prior scene text
            Delete.Scene();
            if (CurrentRoom.PersonsInRoom.Count == 0)
            {
                Render.Quote("There is no one in the room to talk to.");
                Board.Render();
            }
            else
            {
            //Lists name of person in the current room then renders to UI
            Render.TalkChoices(CurrentRoom.PersonsInRoom);
            Board.Render();
            //Prompts user input for name of who they want to talk to
            AskUserWhoToTalkTo();
            //AddQuoteToHintPage();
            CheckIfTalkingToMichael();
            Board.Render();
            }
        }

        private void AskUserWhoToTalkTo()
        {
            string nameSelected;
            do
            {
                Console.Write("> ");
                nameSelected = Console.ReadLine().ToLower();
            }
            while (!ValidateTalkSelection(nameSelected));
            //Renders quote of selected person upon valid player input
            Board.Render();
        }

        private bool ValidateTalkSelection(string nameSelected)
        {
            string quote;
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
                    //If the user would like to solve the puzzle - check their answers
                    Is_Game_Over = Solution.CheckSolution();
                    CheckHealth();
                }
                else
                {
                    //Otherwise - Tell them to come back when they are ready
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
            foreach (Room Room in List_Of_All_Rooms)
            {
                if (nextRoom.ToLower() == Room.GetNameToLowerCase())
                {
                    CheckIfVistedRoom(CurrentRoom.Name); //Check if user has been to this room
                    CurrentRoom = Room; //Update the current room
                }
            }
            Render.Location(CurrentRoom);
            Render.SceneDescription();
        }

        private void CheckIfVistedRoom(string roomName)
        {
            if (!Visited_Rooms.Contains(roomName))
            {
                Visited_Rooms.Add(roomName); //Add room to list of seen rooms
                Render.VistedRooms(roomName); //Render to the board
                Number_of_Rooms++;
            }
        }

        private void SaveTheGame()
        {
             Console.Clear();
             SaveVisitedRooms.Saved();
             SaveHintQuotes.Saved();
             SaveElements.Saved();
             SaveWholeBoard.Saved();
             Console.WriteLine("Your game has been saved.");
        }

        private void CheckHealth()
        {
            if (Number_of_Lives == 0)
            {
                Is_Game_Over = true;
            }
        }

        private void EndOfGame()
        {
            if (Number_of_Lives == 0)
            { 
                EndPage.LoseScene();
            }
            else if (userQuitGame)
            {
                EndPage.QuitScene();
            }
            else
            {
                EndPage.WinScene();
            }
            EndPage.ThankYouAsciiArt();
            SaveHintQuotes.ResetHintQuotesFile();
            SaveVisitedRooms.ResetVisitedRoomsFile();
            SaveHints.ResetHintsFile();
            SaveWholeBoard.ResetBoardFile();
            SaveElements.ResetElementsFile();
        }
    }
}
