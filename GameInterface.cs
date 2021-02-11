using System;
namespace SheepWolfAndCabbage
{
    class GameInterface
    {
        Game game;

        public void Show()
        {
            while (true)
            {
                this.StartMenu();
                this.GamePlay();
            }
        }

        private void StartMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Choose starting position:");
                Console.WriteLine("A) Start: SWC End: [Normal]");
                Console.WriteLine("B) Start: SC End: W");
                Console.WriteLine("C) Start: WC End: S");
                Console.WriteLine("D) Start: S  End: WC");
                Console.WriteLine();
                Console.WriteLine("H) Help & Credits");
                Console.WriteLine("ESC) Exit Game");


                var input = Console.ReadKey(true).Key;
                if (input == ConsoleKey.A) game = new Game(Game.Position.Start, Game.Position.Start, Game.Position.Start);
                else if (input == ConsoleKey.B) game = new Game(Game.Position.Start, Game.Position.End, Game.Position.Start);
                else if (input == ConsoleKey.C) game = new Game(Game.Position.End, Game.Position.Start, Game.Position.Start);
                else if (input == ConsoleKey.D) game = new Game(Game.Position.Start, Game.Position.End, Game.Position.End);
                else if (input == ConsoleKey.H) HelpMenu();
                else if (input == ConsoleKey.Escape) Environment.Exit(0);

                if (game != null) break;
            }
        }

        private void GamePlay()
        {
            bool hint = false;
            while (true)
            {
                Game.GameObjects[] boatSide = game.Start;
                if (game.BoatPos == Game.Position.End) boatSide = game.End;

                string options = "";
                if (game.Boat != Game.GameObjects.None) options += $"\nU) Unload {game.Boat}";
                if (boatSide[0] != Game.GameObjects.None) options += "\nS) Load Sheep";
                if (boatSide[1] != Game.GameObjects.None) options += "\nW) Load Wolf";
                if (boatSide[2] != Game.GameObjects.None) options += "\nC) Load Cabbage";
                if ((game.Start[0] == Game.GameObjects.None || game.Start[1] == Game.GameObjects.None || game.Start[2] == Game.GameObjects.None))
                    options += "\nSPACE) Move Boat";
                options += $"\nP) Hint text[{(hint?"ON":"OFF")}]";
                options += "\nH) Help & Credits";
                options += "\nESC) Main Menu";

                char onBoat = ' ';
                if (game.Boat != Game.GameObjects.None) onBoat = Convert.ToString(game.Boat)[0];


                Console.Clear();
                if(hint) Console.WriteLine(game.GetHint()+"\n");
                Console.Write($"{(game.Start[0] != Game.GameObjects.None ? "S" : " ")}{(game.Start[1] != Game.GameObjects.None ? "W" : " ")}{(game.Start[2] != Game.GameObjects.None ? "C" : " ")}");
                Console.Write($" {(game.BoatPos == Game.Position.Start ? onBoat : ' ')}      {(game.BoatPos == Game.Position.End ? onBoat : ' ')} ");
                Console.Write($"{(game.End[0] != Game.GameObjects.None ? "S" : " ")}{(game.End[1] != Game.GameObjects.None ? "W" : " ")}{(game.End[2] != Game.GameObjects.None ? "C" : " ")}\n");
                Console.WriteLine($"###{(game.BoatPos == Game.Position.Start ? "|_|" : "...")}....{(game.BoatPos == Game.Position.End ? "|_|" : "...")}###");
                Console.WriteLine(options);

                if (game.Win)
                {
                    MessageBox("You Won!!!", ConsoleColor.Green);
                    game = null;
                    break;
                }
                else if (game.GameOver)
                {
                    MessageBox("Game Over!!!", ConsoleColor.Red);
                    game = null;
                    break;
                }

                ConsoleKey input = Console.ReadKey(true).Key;
                if (input == ConsoleKey.U && game.Boat != Game.GameObjects.None) game.UnloadFromShip();
                else if (input == ConsoleKey.S && boatSide[0] == Game.GameObjects.Sheep) game.LoadOnShip(Game.GameObjects.Sheep);
                else if (input == ConsoleKey.W && boatSide[1] == Game.GameObjects.Wolf) game.LoadOnShip(Game.GameObjects.Wolf);
                else if (input == ConsoleKey.C && boatSide[2] == Game.GameObjects.Cabbage) game.LoadOnShip(Game.GameObjects.Cabbage);
                else if (input == ConsoleKey.Spacebar && (game.Start[0] == Game.GameObjects.None || game.Start[1] == Game.GameObjects.None || game.Start[2] == Game.GameObjects.None)) game.Move();
                else if (input == ConsoleKey.H) HelpMenu();
                else if (input == ConsoleKey.P) hint = hint ? false : true;
                else if (input == ConsoleKey.Escape) break;
            }
        }

        private void HelpMenu()
        {
            Console.Clear();
            Console.WriteLine("Help Menu");
            Console.WriteLine("=========");
            Console.WriteLine("Letter Meaning:");
            Console.WriteLine("S = Sheep");
            Console.WriteLine("W = Wolf");
            Console.WriteLine("C = Cabbage");
            Console.WriteLine("---------------");
            Console.WriteLine("Credits");
            Console.WriteLine("=======");
            Console.WriteLine("Made by Filip Gajdušek on 3. 2. 2021 (in one day) for SSPŠ");
            Console.WriteLine("\nPress any button to exit the menu...");
            Console.ReadKey(true);
        }


        private void MessageBox(string text, ConsoleColor color = ConsoleColor.White)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine("\n" + text);
            Console.ForegroundColor = defaultColor;
            Console.WriteLine("Progress any button to continue...");
            Console.ReadKey();
        }

    }


}