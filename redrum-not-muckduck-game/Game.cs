using redrum_not_muckduck_game.save;
using System;
using System.Collections.Generic;
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
        public static bool IsGameOver { get; set; } = false;
        public static bool UserQuitGame { get; set; } = false;
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
                Room.HasEventHappened = saveData.HasEventHappened;
                AllRooms = saveData.AllRooms;
            }
            catch
            {
                StartSetUp();
            }
        }

        private void StartSetUp()
        {
            if (Is_Windows) { MusicController.PlaySound(@"utilities\Theme.mp4", 1000); } //If device is windows - play music
            WelcomePage.AcsiiArt();
            WelcomePage.StoryIntro();
            Render.Location(CurrentRoom);
            Render.Action();
            Render.SceneDescription(CurrentRoom.Description);
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
            Delete.SceneTextArea();
            if (CurrentRoom.ItemInRoom.Count == 0)
            {
                Render.Quote("There is nothing of note in the room.");
            }
            else if (CurrentRoom.ItemInRoom.ContainsValue(false))
            {
                Render.ExploreChoices(CurrentRoom.ItemInRoom);
                Board.Render();
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
                Delete.SceneTextArea();
                return true;
            }
            foreach (KeyValuePair<string, bool> str in CurrentRoom.ItemInRoom)
            {
                if (str.Key.ToLower().Contains(itemSelected) && str.Value == false && itemSelected.Length > 2)
                {
                    Delete.SceneTextArea();
                    Render.Quote($"You pick up {str.Key}.");
                    CurrentRoom.ItemInRoom[str.Key] = true;
                    Render.FoundItemsList(str.Key);
                    NumberOfItemsFound++;
                    return true;
                }
            }
            Board.Render();
            Console.WriteLine("That item is not around. Maybe the smoke is getting to you...");
            return false;
        }

        private void TalkToPerson()
        {
            Delete.SceneTextArea();
            if (CurrentRoom.PersonsInRoom.Count == 0)
            {
                Render.Quote("There is no one in the room to talk to.");
            }
            else
            {
                Render.TalkChoices(CurrentRoom.PersonsInRoom);
                Board.Render();
                string nameSelected;
                do
                {
                    nameSelected = UserSelection();
                } while (!ValidateTalkSelection(nameSelected));
                CheckIfTalkingToMichael();
            }
        }

        private string UserSelection()
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
                Delete.SceneTextArea();
                return true;
            }
            //Searches PersonsInRoom dictionary keys for match of player input value
            foreach (KeyValuePair<string, string> person in CurrentRoom.PersonsInRoom)
            {
                if (nameSelected == person.Key.ToLower())
                {
                    quote = person.Key + person.Value;
                    Delete.SceneTextArea();
                    Render.Quote(quote);
                    return true;
                }
            }
            Board.Render();
            Console.WriteLine("Not a person in this room. Please select someone in the room to talk to.");
            return false;
        }

        private void CheckIfTalkingToMichael()
        {
            if (CurrentRoom.Name == "Reception")
            {
                Board.Render();
                bool userWantsToSolve = Solution.AskToSolvePuzzle();
                if (userWantsToSolve)
                {
                    IsGameOver = Solution.CheckSolution();
                    Delete.SceneTextArea();
                    if (NumberOfLives == 0) IsGameOver = true;
                }
                else
                {
                    Delete.SceneTextArea();
                    Render.SceneDescription("Michael: \"Ok, come back when you are ready\"");
                }
            }
        }

        private void UpdateCurrentRoom(string nextRoom)
        {
            Delete.SceneTextArea();
            Delete.Location(CurrentRoom);
            foreach (Room Room in AllRooms)
            {
                if (nextRoom.ToLower() == Room.Name.ToLower())
                {
                    CheckIfVistedRoom(CurrentRoom.Name);
                    CurrentRoom = Room;
                }
            }
            Render.Location(CurrentRoom);
            RenderSpecialActionsInRooms();
            Delete.SceneTextArea();
            Render.SceneDescription(CurrentRoom.Description);
        }

        private void RenderSpecialActionsInRooms()
        {
            int randomPercentage = PercentChanceGenerator();
            if(Room.HasEventHappened.ContainsKey(CurrentRoom.Name) && Room.HasEventHappened[CurrentRoom.Name] == false)
            {
                Delete.SceneTextArea();
                if (randomPercentage > 49)
                {
                    Delete.SceneTextArea();
                    Render.ActionQuote(CurrentRoom.Action);
                    if (NumberOfLives < 3 && (CurrentRoom.Name.Equals("Break Room") || CurrentRoom.Name.Equals("Sales")))
                    {
                        Solution.AddAHeartToBoard();
                        NumberOfLives++;
                        Board.Render();
                        System.Console.WriteLine("Press any key to continue:");
                        Console.ReadKey(true);
                    }
                    else if (CurrentRoom.Name.Equals("Accounting") || CurrentRoom.Name.Equals("Quality Assurance"))
                    {
                        NumberOfLives--;
                        Solution.RemoveAHeartFromBoard();
                        if (NumberOfLives <= 0)
                            EndPage.LoseScene();
                        Board.Render();
                        System.Console.WriteLine("Press any key to continue:");
                        Console.ReadKey(true);
                    }
                    Room.HasEventHappened[CurrentRoom.Name] = true;
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
