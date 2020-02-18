using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace redrum_not_muckduck_game.save
{
    class SaveData
    {
        public static string SavedDataPath = GetPathToSavedData();
        public char[,] GameBoard { get; set; }
        public List<Room> AllRooms { get; set; }
        public List<string> VisitedRooms { get; set; }
        public Room CurrentRoom { get; set; }
        public int NumberOfItemsFound { get; set; }
        public int NumberOfLives { get; set; }

        public SaveData(char[,] gameBoard, List<Room> allRooms, List<string> visitedRooms,
            Room currentRoom, int numberOfItemsFound, int numberOfLives)
        {
            GameBoard = gameBoard;
            AllRooms = allRooms;
            VisitedRooms = visitedRooms;
            CurrentRoom = currentRoom;
            NumberOfItemsFound = numberOfItemsFound;
            NumberOfLives = numberOfLives;
        }

        public static void Save()
        {
            SaveData dataToSave = new SaveData(Board.GameBoard, Game.AllRooms, Game.VisitedRooms,
                Game.CurrentRoom, Game.NumberOfItemsFound, Game.NumberOfLives);
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
            if (Game.Is_Windows)
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
