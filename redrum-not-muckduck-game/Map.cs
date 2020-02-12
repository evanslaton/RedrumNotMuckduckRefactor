using System;
using System.Text;

namespace redrum_not_muckduck_game
{
    class Map
    {
        private static string[] ALL_ROOM_NAMES =
        { "Reception", "Sales", "Break Room", "Accounting", "Quality", "Assurance", "Kitchen", "Annex" };
        private static string[,] ROOM_LAYOUT =
        {
            { "Reception", "Sales", "", "Break Room" },
            { "Accounting", "Quality Assurance", "Kitchen", "Annex" }
        };
        private string[] Rooms { get; set; }
        private StringBuilder StringBuilder { get; set; }
        private string CurrentRoom { get; set; }
        private string RoomToGoTo { get; set; }
        private char prependCharacter;
        private char appendCharacter;
        private ConsoleKey UserInput { get; set; }
        private int[] Coordinates { get; set; } = new int[2];

        public Map()
        {
            Rooms = new string[ALL_ROOM_NAMES.Length];
            StringBuilder = new StringBuilder();
        }

        public string LeaveRoom(string currentRoom)
        {
            CurrentRoom = currentRoom;
            RoomToGoTo = currentRoom;
            Render(currentRoom, currentRoom);
            SetCurrentRoom();
            UserInput = ConsoleKey.Escape;
            return RoomToGoTo;
        }

        private void SetCurrentRoom()
        {
            SetInitialCoordinatesOfCurrentRoom();

            while (UserInput != ConsoleKey.Enter)
            {
                GetValidInput();
                if (UserInput == ConsoleKey.UpArrow)
                {
                    Coordinates[0] -= 1;
                    if (Coordinates[0] < 0) Coordinates[0] = 0;
                    if (ROOM_LAYOUT[Coordinates[0], Coordinates[1]].Equals("")) Coordinates[0] += 1;
                }
                else if (UserInput == ConsoleKey.DownArrow)
                {
                    Coordinates[0] += 1;
                    if (Coordinates[0] > 1) Coordinates[0] = 1;
                }
                else if (UserInput == ConsoleKey.LeftArrow)
                {
                    Coordinates[1] -= 1;
                    if (Coordinates[1] < 0) Coordinates[1] = 0;
                    if (ROOM_LAYOUT[Coordinates[0], Coordinates[1]].Equals("")) Coordinates[1] -= 1;
                }
                else if (UserInput == ConsoleKey.RightArrow)
                {
                    Coordinates[1] += 1;
                    if (Coordinates[1] > 3) Coordinates[1] = 3;
                    if (ROOM_LAYOUT[Coordinates[0], Coordinates[1]].Equals("")) Coordinates[1] += 1;
                }
                
                if (UserInput == ConsoleKey.UpArrow ||
                    UserInput == ConsoleKey.DownArrow ||
                    UserInput == ConsoleKey.LeftArrow ||
                    UserInput == ConsoleKey.RightArrow)
                {
                    RoomToGoTo = ROOM_LAYOUT[Coordinates[0], Coordinates[1]];
                    Render(CurrentRoom, RoomToGoTo);
                }
            }
        }

        private void SetInitialCoordinatesOfCurrentRoom()
        {
            for (int i = 0; i < ROOM_LAYOUT.GetLength(0); i++)
            {
                for (int j = 0; j < ROOM_LAYOUT.GetLength(1); j++)
                {
                    if (CurrentRoom.Equals(ROOM_LAYOUT[i, j]) ||
                        ROOM_LAYOUT[i, j].Contains(CurrentRoom))
                    {
                        Coordinates[0] = i;
                        Coordinates[1] = j;
                    }
                }
            }
        }

        private void GetValidInput()
        {
            do
            {
                UserInput = Console.ReadKey(true).Key;
            } while (!UserInputIsValid());
        }

        private bool UserInputIsValid()
        {
            return UserInput == ConsoleKey.UpArrow ||
                UserInput == ConsoleKey.DownArrow ||
                UserInput == ConsoleKey.LeftArrow ||
                UserInput == ConsoleKey.RightArrow ||
                UserInput == ConsoleKey.Enter;
        }

        public void Render(string currentRoom, string roomToGoTo = null)
        {
            CurrentRoom = currentRoom;
            string map = GenerateMap(roomToGoTo);
            Console.Clear();
            Console.WriteLine(map);
        }

        private string GenerateMap(string roomToGoTo = null)
        {
            GenerateRoomNamesToDisplay();
            string map = $"\n                Current room: {CurrentRoom}\n";
            if (roomToGoTo == null)
            {
                map += "                Press ENTER to exit the map.\n\n";
            } 
            else
            {
                RoomToGoTo = roomToGoTo;
                map += $"                Press ENTER to go to: {RoomToGoTo}\n\n";
            }
            return map +
            "   ╔═════════════════════════════╗         ╔═══════════════════╗\n" +
            "   ║             .               ║         ║                   ║\n" +
            "   ║             .               ║         ║                   ║\n" +
            "   ║             .               ║         ║                   ║\n" +
            "   ║             .               ║         ║                   ║\n" +
           $"   ║ {Rooms[0]} .    {Rooms[1]}    ║         ║    {Rooms[2]}   ║\n" +
            "   ║             .               ║         ║                   ║\n" +
            "   ║             .               ║         ║                   ║\n" +
            "   ║             .               ║         ║                   ║\n" +
            "   ║             .               ║         ║                   ║\n" +
            "   ║ . . . . . . . . . . . . . . ║═════════║═══════════════════║\n" +
            "   ║             .               ║         ║                   ║\n" +
            "   ║             .               ║         ║                   ║\n" +
            "   ║             .               ║         ║                   ║\n" +
            "   ║             .               ║         ║                   ║\n" +
           $"   ║ {Rooms[3]}.   {Rooms[4]}   ║{Rooms[6]}║      {Rooms[7]}      ║\n" +
           $"   ║             .  {Rooms[5]}  ║         ║                   ║\n" +
            "   ║             .               ║         ║                   ║\n" +
            "   ║             .               ║         ║                   ║\n" +
            "   ║             .               ║         ║                   ║\n" +
            "   ╚═══════════════════════════════════════════════════════════╝\n";
        }

        private void GenerateRoomNamesToDisplay()
        {
            for (int i = 0; i < ALL_ROOM_NAMES.Length; i++)
            {
                if (RoomToGoTo != null &&
                    (ALL_ROOM_NAMES[i].Equals(RoomToGoTo) ||
                    ((RoomToGoTo.Contains("quality") || RoomToGoTo.Contains("Quality")) &&
                    (ALL_ROOM_NAMES[i].Equals("Quality") || ALL_ROOM_NAMES[i].Equals("Assurance")))))
                {
                    prependCharacter = '*';
                    appendCharacter = '*';
                }
                else if (RoomToGoTo == null &&
                    (ALL_ROOM_NAMES[i].Equals(CurrentRoom) ||
                    ((CurrentRoom.Contains("quality") || CurrentRoom.Contains("Quality")) &&
                    (ALL_ROOM_NAMES[i].Equals("Quality") || ALL_ROOM_NAMES[i].Equals("Assurance")))))
                {
                    prependCharacter = '*';
                    appendCharacter = '*';
                }
                else
                {
                    prependCharacter = ' ';
                    appendCharacter = ' ';
                }
                Rooms[i] = CreateRoomName(ALL_ROOM_NAMES[i]);
            }
        }

        private string CreateRoomName(string room)
        {
            StringBuilder.Clear();
            StringBuilder.Append(prependCharacter);
            StringBuilder.Append(room);
            StringBuilder.Append(appendCharacter);
            string roomName = StringBuilder.ToString();
            return roomName;
        }
    }
}
