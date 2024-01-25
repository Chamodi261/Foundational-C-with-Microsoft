using System;

class Program
{
    private static readonly Random random = new Random();
    private const int ConsoleBufferOffset = 5;
    private static int height;
    private static int width;
    private static bool shouldExit = false;
    private static int playerX;
    private static int playerY;
    private static int foodX;
    private static int foodY;
    private static string[] states = { "('-')", "(^-^)", "(X_X)" };
    private static string[] foods = { "@@@@@", "$$$$$", "#####" };
    private static string player = states[0];
    private static int food = 0;

    static void Main()
    {
        InitializeGame();

        while (!shouldExit)
        {
            if (TerminalResized())
            {
                Console.Clear();
                Console.Write("Console was resized. Program exiting.");
                shouldExit = true;
            }
            else
            {
                if (PlayerIsFaster())
                {
                    Move(1, false);
                }
                else if (PlayerIsSick())
                {
                    FreezePlayer();
                }
                else
                {
                    Move(otherKeysExit: false);
                }
                if (GotFood())
                {
                    ChangePlayer();
                    ShowFood();
                }
            }
        }
    }

    // Returns true if the Terminal was resized
    private static bool TerminalResized()
    {
        return height != Console.WindowHeight - 1 || width != Console.WindowWidth - ConsoleBufferOffset;
    }

    // Displays random food at a random location
    private static void ShowFood()
    {
        food = random.Next(0, foods.Length);
        foodX = random.Next(0, width - player.Length);
        foodY = random.Next(0, height - 1);

        Console.SetCursorPosition(foodX, foodY);
        Console.Write(foods[food]);
    }

    // Returns true if the player location matches the food location
    private static bool GotFood()
    {
        return playerY == foodY && playerX == foodX;
    }

    // Returns true if the player appearance represents a sick state
    private static bool PlayerIsSick()
    {
        return player.Equals(states[2]);
    }

    // Returns true if the player appearance represents a fast state
    private static bool PlayerIsFaster()
    {
        return player.Equals(states[1]);
    }

    // Changes the player to match the food consumed
    private static void ChangePlayer()
    {
        player = states[food];
        Console.SetCursorPosition(playerX, playerY);
        Console.Write(player);
    }

    // Temporarily stops the player from moving
    private static void FreezePlayer()
    {
        System.Threading.Thread.Sleep(1000);
        player = states[0];
    }

    // Reads directional input from the Console and moves the player
    private static void Move(int speed = 1, bool otherKeysExit = false)
    {
        int lastX = playerX;
        int lastY = playerY;

        switch (Console.ReadKey(true).Key)
        {
            case ConsoleKey.UpArrow:
                playerY--;
                break;
            case ConsoleKey.DownArrow:
                playerY++;
                break;
            case ConsoleKey.LeftArrow:
                playerX -= speed;
                break;
            case ConsoleKey.RightArrow:
                playerX += speed;
                break;
            case ConsoleKey.Escape:
                shouldExit = true;
                break;
            default:
                shouldExit = otherKeysExit;
                break;
        }

        Console.SetCursorPosition(lastX, lastY);
        for (int i = 0; i < player.Length; i++)
        {
            Console.Write(" ");
        }

        playerX = (playerX < 0) ? 0 : (playerX >= width ? width : playerX);
        playerY = (playerY < 0) ? 0 : (playerY >= height ? height : playerY);

        Console.SetCursorPosition(playerX, playerY);
        Console.Write(player);
    }

    // Clears the console, displays the food and player
    private static void InitializeGame()
    {
        Console.CursorVisible = false;
        height = Console.WindowHeight - 1;
        width = Console.WindowWidth - ConsoleBufferOffset;
        playerX = 0;
        playerY = 0;
        Console.Clear();
        ShowFood();
        Console.SetCursorPosition(0, 0);
        Console.Write(player);
    }
}
