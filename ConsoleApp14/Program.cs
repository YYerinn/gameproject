using System;
using System.Threading;


namespace ConsoleApp14
{

    class Program
    {
        static int screenwidth = 100;
        static int screenheight = 30;

        static int[] xpos = new int[100];
        static int[] ypos = new int[100];
        static int wormLength = 1;

        static int foodx, foody;
        static Random randomnummerfood = new Random();

        static ConsoleKeyInfo keyInfo;
        static string direction = "RIGHT";

        static void Main(string[] args)
        {
            Console.WindowHeight = screenheight;
            Console.WindowWidth = screenwidth;
            intro();
            Gameposition();
            

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    keyInfo = Console.ReadKey();
                    ChangeDirection(keyInfo.Key);
                }

                Move();
                ObjectActivity();

                Thread.Sleep(100);
            }
        }

        static void Gameposition()
        {
           
            xpos[0] = screenwidth / 2;
            ypos[0] = screenheight / 2;

            foodx = randomnummerfood.Next(1, screenwidth - 1);
            foody = randomnummerfood.Next(1, screenheight - 1);
        }

        static void intro()
        {
            Console.WriteLine("                                                                                                                   ");
            Console.WriteLine("                                                                                                                   ");
            Console.WriteLine("                                                                                                                   ");
            Console.WriteLine("                                                                                                                   ");
            Console.WriteLine("          ##   ##   #####   ######   ##   ##           #####      ###     ##   ##      ######  ");
            Console.WriteLine("          ##   ##  ##   ##  ##   ##  ### ###          ##         ## ##    ### ###      ##      ");
            Console.WriteLine("          ## # ##  ##   ##  ##   ##  #######          ##        ##  ##    #######      ##      ");
            Console.WriteLine("          #######  ##   ##  ##  ##   #######          ##  ###  ##   ##    #######      #####   ");
            Console.WriteLine("          #######  ##   ##  #####    ## # ##          ##    ## #######   ##  #  ##     ##      ");
            Console.WriteLine("           ## ##   ##   ##  ## ###   ##   ##          ##   ##  ##   ##  #         #    ##      ");
            Console.WriteLine("           #   #    #####   ##  ###  ##   ##           #####  ##    ## #           #   ######  ");
            Console.WriteLine("                                                                                                                   ");
            Console.WriteLine("                                                                                                                   ");
            Console.WriteLine("                                   gamestart(아무 키를 누르세요)                                                                    ");


            String s = Console.ReadLine();
            Console.Clear();
        }

        static void mapdraw()
        {
            // 위
            for (int i = 0; i < screenwidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("-");
            }

            // 아래
            for (int i = 0; i < screenwidth; i++)
            {
                Console.SetCursorPosition(i, screenheight - 1);
                Console.Write("-");
            }

            // 왼
            for (int i = 0; i < screenheight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("|");
            }

            // 오
            for (int i = 0; i < screenheight; i++)
            {
                Console.SetCursorPosition(screenwidth - 1, i);
                Console.Write("|");
            }

        }

        static void Draw()
        {

            Console.Clear();

            Console.SetCursorPosition(foodx, foody);
            Console.Write("△");

            for (int i = 0; i < wormLength; i++)
            {
                Console.SetCursorPosition(xpos[i], ypos[i]);
                if (i == 0)
                    Console.Write("○");
                else
                    Console.Write("o");
            }
        }

        static void Move()
        {
            for (int i = wormLength - 1; i > 0; i--)
            {
                xpos[i] = xpos[i - 1];
                ypos[i] = ypos[i - 1];
            }

            switch (direction)
            {
                case "UP":
                    ypos[0]--;
                    break;
                case "DOWN":
                    ypos[0]++;
                    break;
                case "LEFT":
                    xpos[0]--;
                    break;
                case "RIGHT":
                    xpos[0]++;
                    break;
            }

            Draw();
            mapdraw();
        }

        static void ChangeDirection(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (direction != "DOWN")
                        direction = "UP";
                    break;
                case ConsoleKey.DownArrow:
                    if (direction != "UP")
                        direction = "DOWN";
                    break;
                case ConsoleKey.LeftArrow:
                    if (direction != "RIGHT")
                        direction = "LEFT";
                    break;
                case ConsoleKey.RightArrow:
                    if (direction != "LEFT")
                        direction = "RIGHT";
                    break;
            }
        }

        static void ObjectActivity()
        {
            //  벽
            if (xpos[0] == 0 || xpos[0] == screenwidth - 1 || ypos[0] == 0 || ypos[0] == screenheight - 1)
                GameOver();

            // 자신
            for (int i = 1; i < wormLength; i++)
            {
                if (xpos[0] == xpos[i] && ypos[0] == ypos[i])
                    GameOver();
            }

            //  음식
            if (xpos[0] == foodx && ypos[0] == foody)
            {
                wormLength++;
                foodx = randomnummerfood.Next(1, screenwidth - 1);
                foody = randomnummerfood.Next(1, screenheight - 1);
            }
        }

        static void GameOver()
        {
            Console.Clear();
            Console.SetCursorPosition(screenwidth / 5, screenheight / 2);
            Console.Write("                          Game over.                 ");
            Console.ReadLine();
            
        }
    }
}


