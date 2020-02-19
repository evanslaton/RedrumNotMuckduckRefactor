using System.Threading;
using System.Drawing;
using Console = Colorful.Console;

namespace redrum_not_muckduck_game
{
    class WelcomePage
    {
        public static void AcsiiArt()
        {
            Console.WriteLine(""); 
            Console.WriteLine("      ██▀███  ▓█████ ▓█████▄ ▄▄▄█████▓ █    ██  ███▄ ▄███▓  ", Color.FromArgb(204, 34, 0));
            Console.WriteLine("     ▓██ ▒ ██▒▓█   ▀ ▒██▀ ██▌▓  ██▒ ██▒██  ▓██▒▓██▒▀█▀ ██▒  ", Color.FromArgb(204, 34, 0));
            Console.WriteLine("     ▓██ ░▄█ ▒▒███   ░██   █▌▒ ▓██░ ▓░ ██  ▒██░▓██    ▓██░  ", Color.FromArgb(204, 34, 0));
            Console.WriteLine("     ▒██▀▀█▄  ▒▓█  ▄ ░▓█▄   ▌░ ▓██▀▀█▄ ▓█  ░██░▒██    ▒██   ", Color.FromArgb(204, 34, 0));
            Console.WriteLine("     ░██▓ ▒██▒░▒████▒░▒████▓   ▒██▒ ░██▒█████▓ ▒██▒   ░██▒  ", Color.FromArgb(204, 34, 0));
            Console.WriteLine("     ░ ▒▓ ░▒▓░░░ ▒░ ░ ▒▒▓  ▒   ▒ ░░   ░▒▓▒ ▒ ▒ ░ ▒░   ░  ░  ", Color.FromArgb(204, 34, 0));
            Console.WriteLine("       ░▒ ░ ▒░ ░ ░  ░ ░ ▒  ▒     ░    ░░▒░ ░ ░ ░  ░      ░  ", Color.FromArgb(204, 34, 0));
            Console.WriteLine("       ░░   ░    ░    ░ ░  ░   ░       ░░░ ░ ░ ░      ░     ", Color.FromArgb(204, 34, 0));
            Console.WriteLine("            ███▄    █  ▒█████  ▄▄▄█████▓", Color.FromArgb(204, 34, 0));
            Console.WriteLine("            ██ ▀█   █ ▒██▒  ██▒▓  ██▒ ▓▒", Color.FromArgb(204, 34, 0));
            Console.WriteLine("           ▓██  ▀█ ██▒▒██░  ██▒▒ ▓██░ ▒░", Color.FromArgb(204, 34, 0));
            Console.WriteLine("           ▓██▒  ▐▌██▒▒██   ██░░ ▓██▓ ░ ", Color.FromArgb(204, 34, 0));
            Console.WriteLine("           ▒██░   ▓██░░ ████▓▒░  ▒██▒ ░ ", Color.FromArgb(204, 34, 0));
            Console.WriteLine("           ░ ▒░   ▒ ▒ ░ ▒░▒░▒░   ▒ ░░   ", Color.FromArgb(204, 34, 0));
            Console.WriteLine("           ░ ░░   ░ ▒░  ░ ▒ ▒░     ░    ", Color.FromArgb(204, 34, 0));
            Console.WriteLine("     ███▄ ▄███▓ █    ██  ▄████▄   ██ ▄█▀▓█████▄  █    ██  ▄████▄   ██ ▄█▀", Color.FromArgb(204, 34, 0));
            Console.WriteLine("    ▓██▒▀█▀ ██▒ ██  ▓██▒▒██▀ ▀█   ██▄█▒ ▒██▀ ██▌ ██  ▓██▒▒██▀ ▀█   ██▄█▒ ", Color.FromArgb(204, 34, 0));
            Console.WriteLine("    ▓██    ▓██░▓██  ▒██░▒▓█    ▄ ▓███▄░ ░██   █▌▓██  ▒██░▒▓█    ▄ ▓███▄░ ", Color.FromArgb(204, 34, 0));
            Console.WriteLine("    ▒██    ▒██ ▓▓█  ░██░▒▓▓▄ ▄██▒▓██ █▄ ░▓█▄   ▌▓▓█  ░██░▒▓▓▄ ▄██▒▓██ █▄ ", Color.FromArgb(204, 34, 0));
            Console.WriteLine("    ▒██▒   ░██▒▒▒█████▓ ▒ ▓███▀ ░▒██▒ █▄░▒████▓ ▒▒█████▓ ▒ ▓███▀ ░▒██▒ █▄", Color.FromArgb(204, 34, 0));
            Console.WriteLine("    ░ ▒░   ░  ░░▒▓▒ ▒ ▒ ░ ░▒ ▒  ░▒ ▒▒ ▓▒ ▒▒▓  ▒ ░▒▓▒ ▒ ▒ ░ ░▒ ▒  ░▒ ▒▒ ▓▒", Color.FromArgb(204, 34, 0));
            Console.WriteLine("    ░  ░      ░░░▒░ ░ ░   ░  ▒   ░ ░▒ ▒░ ░ ▒  ▒ ░░▒░ ░ ░   ░  ▒   ░ ░▒ ▒░", Color.FromArgb(204, 34, 0));
            Console.WriteLine("    ░      ░    ░░░ ░ ░ ░        ░ ░░ ░  ░ ░  ░  ░░░ ░ ░ ░        ░ ░░ ░ ", Color.FromArgb(204, 34, 0));
       
            TypeByLetter("    Come on an adventure.. If you dare..", 1);
            Console.WriteLine();
            TypeByLetter("    Press any key to continue...", 1);
            Console.ReadKey(true); // Waits for user to hit key to start game
        }

        public static void TypeByLetter(string line, int milliseconds)
        {
            for (int i = 0; i < line.Length; i++)
            {
                Console.Write(line[i]);
                Thread.Sleep(milliseconds);
            }
        }

        public static void StoryIntro()
        {
            Console.Clear();
            Render.TypeByElement(intro);
        }

        private static string[] intro = { "\n\tAs you stare at Kevin scarfing down M&M’s directly from his",
        "\t\tcandy jar, you wonder if anyone actually likes you.",
        "\t\t\tDwight mentions the smell of smoke.",
        "\n\tAngela: \"Did you bring in your jerky again, Dwight?\"",
        "\t\tPam: \"SMOKE! FIRE!\"",
        "\t\t\tDwight: \"Oh no! What should we do?!\"",
        "\n\tCreed: \"It was the temp’s fault. Get ‘em guys.\"",
        "\t\tMichael: \"I'LL PUNISH THEM LATER! WE'RE TRAPPED!",
        "\t\t\tEVERYONE FOR THEMSELVES!\"",
        "\n\tYou can't lose this job! You must find the cause of the fire.",
        "\t\tEXPLORE the rooms for clues",
        "\t\t\tand TALK to others to gather information",
        "\n\tBefore you escape, you need to: Find WHO started the fire,",
        "\t\tWHAT they used to start the fire,",
        "\t\t\t& WHERE the fire started.",
        "\n\tWhen ready, approach Michael and let him know what happened,",
        "\t\tbut be careful, there are many hazards around the office,",
        "\t\t\tand an incorrect guess will lose you a life!",
        "\n\tPress any key to continue..."
        };
    }  
}
