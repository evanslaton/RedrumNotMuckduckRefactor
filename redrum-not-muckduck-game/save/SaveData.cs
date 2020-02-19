using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace redrum_not_muckduck_game.save
{
    class SaveData
    {
        public static string SavedDataPath = GetPathToSavedData();
        public char[,] GameBoard { get; set; }
        public List<Room> AllRooms { get; set; }
        public List<string> VisitedRooms { get; set; }
        public Dictionary<string, bool> HasEventHappened { get; set; }
        public Room CurrentRoom { get; set; }
        public int NumberOfItemsFound { get; set; }
        public int NumberOfLives { get; set; }

        public SaveData(char[,] gameBoard, List<Room> allRooms, List<string> visitedRooms,
            Dictionary<string, bool> hasEventHappened, Room currentRoom, int numberOfItemsFound, int numberOfLives)
        {
            GameBoard = gameBoard;
            AllRooms = allRooms;
            VisitedRooms = visitedRooms;
            HasEventHappened = hasEventHappened;
            CurrentRoom = currentRoom;
            NumberOfItemsFound = numberOfItemsFound;
            NumberOfLives = numberOfLives;
        }

        public static void Save()
        {
            SaveData dataToSave = new SaveData(Board.GameBoard, Game.AllRooms, Game.VisitedRooms,
                Room.HasEventHappened, Game.CurrentRoom, Game.NumberOfItemsFound, Game.NumberOfLives);
            File.WriteAllText(SavedDataPath, JsonConvert.SerializeObject(dataToSave));
        }

        public static SaveData GetSavedData()
        {
            string savedData = File.ReadAllText(SavedDataPath);
            return JsonConvert.DeserializeObject<SaveData>(savedData);
        }

        public static void Delete()
        {
            File.Delete(SavedDataPath);
        }

        public static string GetPathToSavedData()
        {
            if (Game.onWindows)
            {
                return @"..\..\..\save\saved-data.json";
            }
            else
            {
                return @"../../../save/saved-data.json";
            }
        }
    }
}
