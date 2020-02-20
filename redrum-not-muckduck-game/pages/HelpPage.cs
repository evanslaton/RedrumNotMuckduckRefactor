using System;

namespace redrum_not_muckduck_game
{
    public class HelpPage
    {
        public static string HelpPageMessage = "" +
            "   ╔════════════════════════════════════════════════╗\n" +
            "   ║                                                ║\n" +
            "   ║  In order to keep your job, identify           ║\n" +
            "   ║    WHO started the fire,                       ║\n" +
            "   ║     WHAT started the fire,                     ║\n" +
            "   ║      & WHERE the fire was started.             ║\n" +
            "   ║                                                ║\n" +
            "   ║  After gathering as much information as        ║\n" +
            "   ║   possible head to the reception area to       ║\n" +
            "   ║     show Michael what you have found.          ║\n" +
            "   ║                                                ║\n" +
            "   ║  Your options for navigating in the rooms are: ║\n" +
            "   ║ explore, talk, leave, map, help, save and quit ║\n" +
            "   ║                                                ║\n" +
            "   ║   Press ENTER to go back to the room.          ║\n" +
            "   ║                                                ║\n" +
            "   ║                                                ║\n" +
            "   ║                                                ║\n" +
            "   ║                                                ║\n" +
            "   ╚════════════════════════════════════════════════╝\n";

        public void Render()
        {
            Console.Clear();
            Console.WriteLine(HelpPageMessage);

            ConsoleKey key;
            do
            {
                key = Console.ReadKey(true).Key;
            } while (key != ConsoleKey.Enter);
        }
    }
}
