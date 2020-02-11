using System;
using System.Text;

namespace redrum_not_muckduck_game
{
    class Map
    {
        private static string[] ALL_ROOM_NAMES = { 
            "Reception", "Sales", "Break Room", "Accounting",
            "Quality", "Assurance", "Kitchen", "Annex" };
        private string[] Rooms { get; set; }
        private StringBuilder StringBuilder { get; set; }
        private string CurrentRoom { get; set; }
        private char prependCharacter;
        private char appendCharacter;

        public Map()
        {
            Rooms = new string[ALL_ROOM_NAMES.Length];
            StringBuilder = new StringBuilder();
        }

        public void Render(string currentRoom)
        {
            CurrentRoom = currentRoom;
            string map = GenerateMap();
            Console.Clear();
            Console.WriteLine(map);
        }

        private string GenerateMap()
        {
            GenerateRoomNamesToDisplay();
            return $"" +
           $"\n                Current room: {CurrentRoom}.\n" +
            "                Press ENTER to exit the map.\n\n" +
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
                if (ALL_ROOM_NAMES[i].Equals(CurrentRoom) ||
                    (CurrentRoom.Contains("quality") &&
                    (ALL_ROOM_NAMES[i].Equals("Quality") || ALL_ROOM_NAMES[i].Equals("Assurance"))))
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
