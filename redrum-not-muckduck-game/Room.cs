using System.Collections.Generic;

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
               "Everyone is running around.  " +
               "*Oscar is trying to find an exit.  " +
               "*Out of the corner of your eye, you " +
               "*see a drawer slowly open. ",
               new Dictionary<string, bool>()
               {
                   { "Angela's cat, Bandit", false }
               },
              new Dictionary<string, string>()
               {
                    { "Oscar", " : \"Angela, stay here. I am going" +
                    "*up into the ceiling to find" +
                    "*a way out and get help!\"" },
                    { "Angela", " : \"Oscar! Take me with you!\"" }
               },
              "Oscar fell through the ceiling*and lands on you!!!! *You lose one Heart",
               true
            );
        }

        public static Room CreateAnnex()
        {
            return new Room(
                 "Annex",
                 "You have made it to the " +
                 "*back of the office. " +
                 "*Kelly seems to be waiting around" +
                 "*for Ryan. He doesn't smoke cigarettes does " +
                 "*he?",
                 new Dictionary<string, bool>()
                 {
                     { "A Chad Flenderman Novel", false },
                 },
                 new Dictionary<string, string>()
                 {
                    { "Kelly", " : \"I saw Dwight walk by" +
                    "*with a blow horn earlier." +
                    "*Weird right? Then again," +
                    "*it is Dwight...\"" },
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
                "As you open the door a large puff" +
                "*of smoke pours out the door." +
                "*You see a large silhouette in" +
                "*the room.",
                 new Dictionary<string, bool>()
                 {
                     { "Cigarettes", false }
                 },
                 new Dictionary<string, string>() 
                 {
                     { "Kevin", " : \"What is going to happen to" +
                     "*all the vending machine snacks?!\"" }
                 },
                "Kevin broke into the vending machine *and offers you a snack *You gain one Heart",
                false
            );
        }

        public static Room CreateKitchen()
        {
            return new Room(
                "Kitchen",
                "Why is Phyllis just standing here? " +
                "*She has a puzzled look on her face... " +
                "*Stanley is very distressed over something.",
                new Dictionary<string, bool>()
                {
                    { "Burnt Cheese Pita", false }
                },
                new Dictionary<string, string>()
                {
                    { "Phyllis", " : \"Why would someone use the" +
                    "*\'oven\' setting on the" +
                    "*toaster-oven for a cheese pita?\"" },
                    { "Stanley", " : \"Oh my gosh! What's going" +
                    "*happen to the next Pretzal Day?!\"" }
                },
                "",
                false
            );
        }

        public static Room CreateQualityAssurance()
        {
            return new Room(
                "Quality Assurance",
                "Creed and Meredith are still*sitting at their desks.",
                new Dictionary<string, bool>(){},
                new Dictionary<string, string>()
                {
                    { "Creed", " : \"Hey, I think I smell smoked ribs." +
                    "*Why did no one tell me we were having ribs?!\""},
                    { "Meredith", " : \"Alright! I can take" +
                    "*another smoke break!\""}
                },
                "You help Jim and Andy ram a copier into *a door, the door didn't open and you *threw out your back.......*You lose a life",
                false
            );
        }

        public static Room CreateReception()
        {
            return new Room(
                "Reception",
                "Michael is panicking and you see Pam" +
                "*trying to calm him down.",
                new Dictionary<string, bool>()
                {
                    { "A Serenity by Jan Candle", false }
                },
                new Dictionary<string, string>()
                {
                    { "Michael", " : \"This better be important!*You didn't start the fire?*Do you know what happened?\"" },
                    { "Pam", " : \"Jim! Have Ryan help you" +
                    "*guys! Wait, where is Ryan?\""}
                },
                "",
                false
            );
        }

        public static Room CreateSales() {
            return new Room(
                "Sales",
                "Chaos ensues as the smoke thickens. " +
                "*Andy is frantically running in circles." +
                "*You notice someone smells like" +
                "*cigarettes.",
                new Dictionary<string, bool>()
                {
                    { "Butane Torch", false }
                },
                new Dictionary<string, string>()
                {
                    { "Andy", " : ~Firecrackers Popping~" +
                    "*\"THE FIRE IS SHOOTING AT US!!!\"" },
                    { "Jim", " : \"Let's ram the door with the copier!" +
                    "*Andy, come help me!\"" },
                    { "Dwight", " : \"Michael! Why did you light" +
                    "*Jan's candle. You know I*hate the smell.\"" }
                },
                "You see a pretzel on Stanley's Desk and *you choose to eat it. *You gain one Heart",
                true
            );
        }
    }
}
