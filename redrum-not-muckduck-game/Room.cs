using System.Collections.Generic;

namespace redrum_not_muckduck_game
{
    // This class creates new rooms for the game
    // Each room has a name, description, item, person, & adjacent rooms. 
    public class Room
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ItemInRoom { get; set; }
        public Dictionary<string, string> PersonsInRoom { get; set; }
        public string Action { get; set; }
        public bool HasItem { get; set; }
        public List<Room> AdjacentRooms { get; set; }

        public Room(string roomName, string description, string itemInRoom, Dictionary<string, string> personsInRoom, string action, bool hasItem)
        {
            Name = roomName;
            Description = description; 
            ItemInRoom = itemInRoom;
            PersonsInRoom = personsInRoom;
            Action = action;
            HasItem = hasItem;
        }

        public string GetNameToLowerCase()
        {
            return Name.ToLower();
        }

        public int GetNameLength()
        {
            return Name.Length;
        }

        //public string GetQuote(string nameSelected)
        //{
        //    //this needs to select person and get their quote
        //    //string characterQuote = Game.CurrentRoom.PersonsInRoom.
        //    //return PersonInRoom[nameSelected];
        //}

        //public int GetQuoteLength()
        //{
        //    //this needs to get the length of their quote
        //    //return PersonInRoom[1].Length;
        //}
    }
}
