﻿using System.Collections.Generic;

namespace redrum_not_muckduck_game
{
    public class Room
    {
        public static Dictionary<string, bool> HasEventHappened { get; set; } = new Dictionary<string, bool>();
        public string Name { get; set; }
        public string Description { get; set; }
        public Dictionary<string, bool> ItemInRoom { get; set; }
        public Dictionary<string, string> PersonsInRoom { get; set; }
        public string Action { get; set; }
        public bool HasItem { get; set; }
        public Room(string roomName, string description, Dictionary<string, bool> itemInRoom,
            Dictionary<string, string> personsInRoom, string action, bool hasItem)
        {
            Name = roomName;
            Description = description; 
            ItemInRoom = itemInRoom;
            PersonsInRoom = personsInRoom;
            Action = action;
            HasItem = hasItem;
        }

        public static List<Room> CreateRooms()
        {
            HasEventHappened.Add("Accounting", false);
            HasEventHappened.Add("Break Room", false);
            HasEventHappened.Add("Quality Assurance", false);
            HasEventHappened.Add("Sales", false);
            return new List<Room>
            {
                CreateAccounting(),
                CreateAnnex(),
                CreateBreakRoom(),
                CreateKitchen(),
                CreateQualityAssurance(),
                CreateReception(),
                CreateSales()
            };
        }

        public static Room CreateAccounting()
        {
            return new Room(
               "Accounting",
               "Your desk is covered in M&Ms.  " +
               "*Oscar is trying to find an exit.  " +
               "*Out of the corner of your eye, you " +
               "*see a drawer slowly open. ",
               new Dictionary<string, bool>
               {
                   { "Angela's cat, Bandit", false }
               },
              new Dictionary<string, string>()
               {
                    { "Oscar", " : \"Angela, stay here. I am going*up into the ceiling to find*a way out and get help!\"" },
                    { "Angela", " : \"Oscar! Take me with you!\"" }
               },
              "Oscar fell through the ceiling!!!! *You lose one Heart",
               true
            );
        }

        public static Room CreateAnnex()
        {
            return new Room(
                 "Annex",
                 "You have made it to the " +
                 "*back of the office " +
                 "*you should probably go back. " +
                 "*Kelly waits around for Ryan. " +
                 "*He doesn't smoke cigarettes does " +
                 "*he?",
                 new Dictionary<string, bool>
                 {
                     {"beet stained cigs", false },
                     {"cookies", false },
                     {"candle", false }
                 },
                 new Dictionary<string, string>()
                 {
                    { "Kelly", " : \"Why does Dwight have a blow horn?\"" },
                    { "Toby", " : \"I wish I were in Costa Rica still...\"" }
                 },
                 "",
                 true
             );
        }

        public static Room CreateBreakRoom()
        {
            return new Room(
                "Break Room",
                "You are hungry but is there " +
                "*time? Probably right?",
                 new Dictionary<string, bool>{},
                 new Dictionary<string, string>() { },
                "Kevin broke into the vending machine *and offers you a snack *You gain one Heart",
                false
            );
        }

        public static Room CreateKitchen()
        {
            return new Room(
                "Kitchen",
                "Why is Phyllis just standing here? " +
                "*She seems very disturbed... ",
                new Dictionary<string, bool>{},
                new Dictionary<string, string>()
                {
                    { "Phyllis", " : \"I saw Dwight come from the breakroom\"" }
                },
                "",
                false
            );
        }

        public static Room CreateQualityAssurance()
        {
            return new Room(
                "Quality Assurance",
                "Creed and Meredith are sitting at their desks",
                new Dictionary<string, bool>{},
                new Dictionary<string, string>()
                {
                    { "Creed", " : \"Hey I think I smell smoked ribs\""},
                    { "Meredith", " : \"Mmmmmmmmmmm\""}
                },
                "You help Jim and Andy ram a copier into *a door, the door didn't open and you *threw out your back.......*You lose a life",
                false
            );
        }

        public static Room CreateReception()
        {
            return new Room(
                "Reception",
                "Michael waits to hear what " +
                "*you think happened today",
                new Dictionary<string, bool>{},
                new Dictionary<string, string>()
                {
                     { "Michael", " : \"Would you like to solve the puzzle?\"" }
                },
                "",
                false
            );
        }

        public static Room CreateSales() {
            return new Room(
                "Sales",
                "Chaos ensues as the smoke thickens. " +
                "*Andy is frantically running in circles and " +
                "*knocks over his trash can, something makes " +
                "*a thud sound as it falls out.",
                new Dictionary<string, bool>
                {
                    { "torch", false }
                },
                new Dictionary<string, string>()
                {
                    { "Andy", " : \"This would never happen at Cornell...\"" },
                    { "Stanley", " : \"What'll happen to Pretzal Day?!\"" },
                    { "Jim", " : \"Let's ram the door with the copier!\"" }
                },
                "You see a pretzel on Stanley's Desk and *you choose to eat it *You gain one Heart",
                true
            );
        }
    }
}
