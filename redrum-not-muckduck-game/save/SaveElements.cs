using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace redrum_not_muckduck_game
{
    class SaveElements
    {
        public string AccountingItem { get; set; }
        public string SalesItem { get; set; }
        public string AnnexItem { get; set; }
        public string TheCurrentRoom { get; set; }
        public int NumberofVisitedRooms { get; set; }
        public int NumberofItems { get; set; }
        public int NumberofLives { get; set; }
        public int NumberofHints { get; set; }
        public static string WorkingElementDirectory { get; set; }

        public static void Saved()
        {
            SaveElements SaveElements = new SaveElements
            {
                //AccountingItem = Game.Accounting.HasItem.ToString(),
                //SalesItem = Game.Sales.HasItem.ToString(),
                //AnnexItem = Game.Annex.HasItem.ToString(),
                TheCurrentRoom = Game.CurrentRoom.Name,
                NumberofVisitedRooms = Game.NumberOfRooms,
                NumberofItems = Game.NumberOfItems,
                NumberofLives = Game.NumberOfLives,
                NumberofHints = Game.CollectedHints.Count(),
            };
            
            File.WriteAllText(WorkingElementDirectory, JsonConvert.SerializeObject(SaveElements));
        }

        public static void StoredElements()
        { 
            var myJsonFile = File.ReadAllText(WorkingElementDirectory);
            myJsonFile = myJsonFile.Replace("{", string.Empty).Replace("}", string.Empty).Replace("\"", string.Empty);

            var dict = myJsonFile.Split(',')
              .Select(s => s.Split(':'))
              .ToDictionary(a => a[0].Trim(), a => a[1].Trim());

            //Game.Accounting.HasItem = Convert.ToBoolean(dict["AccountingItem"]);
            //Game.Sales.HasItem = Convert.ToBoolean(dict["SalesItem"]);
            //Game.Annex.HasItem = Convert.ToBoolean(dict["AnnexItem"]);
            Game.CurrentRoom.Name = dict["TheCurrentRoom"];
            Game.NumberOfRooms = Int32.Parse(dict["NumberofVisitedRooms"]);
            Game.NumberOfItems = Int32.Parse(dict["NumberofItems"]);
            Game.NumberOfLives = Int32.Parse(dict["NumberofLives"]);
            HintPage.Saved_Hints = Int32.Parse(dict["NumberofHints"]);

            UpdateRoom();
        }

        public static void UpdateRoom()
        {
            foreach (Room room in Game.AllRooms)
            {
                if(Game.CurrentRoom.Name == room.Name)
                {
                    Game.CurrentRoom.Name = "Accounting";
                    Game.CurrentRoom = room;
                }
            }
        } 

        public static void ResetElementsFile()
        {
            File.WriteAllText(WorkingElementDirectory, string.Empty);
        }

        public static void GetWorkingElementDirectory()
        {
            if (Game.Is_Windows)
            {
                WorkingElementDirectory = @"..\..\..\save\save-data\Elements.json";
            }
            else
            {
                WorkingElementDirectory = @"../../../save/save-data/Elements.json";
            }
        }
    }
}
