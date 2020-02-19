using System;
using System.Collections.Generic;

namespace redrum_not_muckduck_game
{
    class Render
    {
        private static string[] Actions = new string[] { "- explore", "- talk to someone", "- leave the current room", "- map", "- help", "- quit playing" };
        public static void ActionQuote(string actionQuote)
        {
            int actionStartRow = 14;
            int COLUMN_WHERE_ACTION_STARTS = 2;
            int currentColumn = 0;

            for (int i = 0; i < actionQuote.Length; i++)
            {
                if (actionQuote[i] == '*')
                {
                    i++;
                    actionStartRow++;
                    currentColumn = 0;
                }
                Board.GameBoard[actionStartRow, COLUMN_WHERE_ACTION_STARTS + currentColumn] = actionQuote[i];
                currentColumn++;
            }
        }

        public static void TalkChoices(Dictionary<string, string> choices)
        {
            int talkChoicesStartRow = 14;
            int COLUMN_WHERE_OPTIONS_START = 2;
            string header = "Who do you want to talk to: ";

            for (int i = 0; i < header.Length; i++)
            {
                Board.GameBoard[talkChoicesStartRow, COLUMN_WHERE_OPTIONS_START + i] = header[i];
            }
            talkChoicesStartRow++;

            foreach (KeyValuePair<string, string> str in choices)
            {
                string person = str.Key;
                for (int i = 0; i < person.Length; i++)
                {
                    Board.GameBoard[talkChoicesStartRow, COLUMN_WHERE_OPTIONS_START + i] = person[i];
                }
                talkChoicesStartRow++;
            }
        }

        public static void ExploreChoices(Dictionary<string, bool> itemInRoom)
        {
            int exploreChoicesStartRow = 14;
            int COLUMN_WHERE_OPTIONS_START = 2;
            string header = "You see the following items around you: ";

            for (int i = 0; i < header.Length; i++)
            {
                Board.GameBoard[exploreChoicesStartRow, COLUMN_WHERE_OPTIONS_START + i] = header[i];
            }
            exploreChoicesStartRow++;

            foreach (KeyValuePair<string, bool> pair in itemInRoom)
            {
                if (pair.Value == false)
                {
                    string itemName = pair.Key;
                    for (int i = 0; i < itemName.Length; i++)
                    {
                        Board.GameBoard[exploreChoicesStartRow, COLUMN_WHERE_OPTIONS_START + i] = itemName[i];
                    }
                    exploreChoicesStartRow++;
                }
            }
        }

        //Maximum chars in a line, is 48
        public static void Quote(string quote)
        {
            int quoteStartRow = 14;
            int COLUMN_WHERE_QUOTE_STARTS = 2;
            int currentColumn = 0;

            for (int i = 0; i < quote.Length; i++)
            {
                if (quote[i] == '*')
                {
                    i++;
                    quoteStartRow++;
                    currentColumn = 0;
                }
                Board.GameBoard[quoteStartRow, COLUMN_WHERE_QUOTE_STARTS + currentColumn] = quote[i];
                currentColumn++;
            }
        }

        public static void Action()
        {
            int actionStartRow = 5;
            int COLUMN_WHERE_ACTIONS_START = 2;

            for (int i = 0; i < Actions.Length; i++)
            {
                for (int j = 0; j < Actions[i].Length; j++)
                {
                    Board.GameBoard[actionStartRow, COLUMN_WHERE_ACTIONS_START + j] = Actions[i][j];
                }
                actionStartRow++;
            }
        }

        public static void SceneDescription(string sceneText)
        {
            int sceneStartRow = 14;
            int COLUMN_WHERE_SCENE_STARTS = 2;
            int currentColumn = 0;

            for (int i = 0; i < sceneText.Length; i++)
            {
                // Text after asterisk start on a new line 
                if (sceneText[i] == '*')
                {
                    i++;
                    sceneStartRow++;
                    currentColumn = 0;
                }
                Board.GameBoard[sceneStartRow, COLUMN_WHERE_SCENE_STARTS + currentColumn] = sceneText[i];
                currentColumn++;
            }
        }

        public static void Location(Room currentRoom)
        {
            int ROW_WHERE_LOCATION_STARTS = 1;
            int COLUMN_WHERE_LOCATION_STARTS = 16;

            for (int i = 0; i < currentRoom.Name.Length; i++)
            {
                Board.GameBoard[ROW_WHERE_LOCATION_STARTS, COLUMN_WHERE_LOCATION_STARTS + i] = currentRoom.Name[i];
            }
        }

        public static void FoundItemsList(string foundItem)
        {
            int ROW_WHERE_ITEMS_START = 8;
            int COLUMN_WHERE_ITEMS_START = 50;
            int ROW_TO_INSERT_NEW_ITEM = ROW_WHERE_ITEMS_START + Game.NumberOfItemsFound;

            for (int i = 0; i < foundItem.Length; i++)
            {
                Board.GameBoard[ROW_TO_INSERT_NEW_ITEM, COLUMN_WHERE_ITEMS_START + i] = foundItem[i];
            }
        }

        public static void VistedRooms(string room)
        {
            int ROW_WHERE_ROOM_START = 18;
            int COLUMN_WHERE_ROOM_START = 50;
            int insertRoomRow = ROW_WHERE_ROOM_START + Game.VisitedRooms.Count;

            for (int i = 0; i < room.Length; i++)
            {
                Board.GameBoard[insertRoomRow, COLUMN_WHERE_ROOM_START + i] = room[i];
            }
        }

        public static void TypeByElement(string[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                Console.WriteLine(input[i]);
                Console.ReadKey(true);  // Waits for user to press key to continue
            }
            MusicController.outputDevice.Dispose();
        }
    }
}
