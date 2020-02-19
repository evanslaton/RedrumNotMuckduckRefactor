using System;

namespace redrum_not_muckduck_game
{
    class EndPage
    {
        public static string[] WinMessage = { "\n\tYou saved your job and found who did it!",
        "\t\tYou deserve some smores.",
        "\t\t\tHopefully, Dwight will be reprimanded for starting a fire in the office."};
        public static string[] LoseMessage = {"\n\tAfter breathing in too much smoke you wake up surrounded by firefighters. ",
        "\t\tNo one knows how the fire was started.",
        "\t\t\tPlay again to solve the puzzle!"};
        public static string[] QuitMessage = {"\n\tWe're sad to see you go ",
        "\t\tBut we understand you probably have other responsibilities.",
        "\t\t\tWe'll see you next time!"};

        public static void WinScene()
        {
            Console.Clear();
            Render.TypeByElement(WinMessage);
        }

        public static void LoseScene() 
        {
            Console.Clear();
            Render.TypeByElement(LoseMessage);
        }

        public static void QuitScene()
        {
            Console.Clear();
            Render.TypeByElement(QuitMessage);
        }

        public static void ThankYouAsciiArt()
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("    ████████╗██╗  ██╗ █████╗ ███╗   ██╗██╗  ██╗███████╗    ███████╗ ██████╗ ██████╗ ");
            Console.WriteLine("    ╚══██╔══╝██║  ██║██╔══██╗████╗  ██║██║ ██╔╝██╔════╝    ██╔════╝██╔═══██╗██╔══██╗");
            Console.WriteLine("       ██║   ███████║███████║██╔██╗ ██║█████╔╝ ███████╗    █████╗  ██║   ██║██████╔╝");
            Console.WriteLine("       ██║   ██╔══██║██╔══██║██║╚██╗██║██╔═██╗ ╚════██║    ██╔══╝  ██║   ██║██╔══██╗");
            Console.WriteLine("       ██║   ██║  ██║██║  ██║██║ ╚████║██║  ██╗███████║    ██║     ╚██████╔╝██║  ██║");
            Console.WriteLine("       ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝╚═╝  ╚═╝╚══════╝    ╚═╝      ╚═════╝ ╚═╝  ╚═╝");
            Console.WriteLine("               ██████╗ ██╗      █████╗ ██╗   ██╗██╗███╗   ██╗ ██████╗ ██╗");
            Console.WriteLine("               ██╔══██╗██║     ██╔══██╗╚██╗ ██╔╝██║████╗  ██║██╔════╝ ██║");
            Console.WriteLine("               ██████╔╝██║     ███████║ ╚████╔╝ ██║██╔██╗ ██║██║  ███╗██║");
            Console.WriteLine("               ██╔═══╝ ██║     ██╔══██║  ╚██╔╝  ██║██║╚██╗██║██║   ██║╚═╝");
            Console.WriteLine("               ██║     ███████╗██║  ██║   ██║   ██║██║ ╚████║╚██████╔╝██╗");
            Console.WriteLine("               ╚═╝     ╚══════╝╚═╝  ╚═╝   ╚═╝   ╚═╝╚═╝  ╚═══╝ ╚═════╝ ╚═╝");
        }
    }
}
