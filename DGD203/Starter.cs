namespace DGD203
{
    using System;
    using System.Numerics;
    using System.Threading;

    public class Starter
    {
        public string _virtualFriendsName;
        public string _playerName;

        public static void Main(string[] args)
        {
            Starter starter = new Starter();
            starter.GetFriendName();
            starter.StartCountdown();
        }

        private void GetFriendName()
        {
            Console.WriteLine("Welcome to 'VIRTUAL FRIEND FOR LOSERS'!");
            Console.WriteLine("What is your name?");
            _playerName = Console.ReadLine();
            Console.WriteLine($"Nice to meet you {_playerName}!");
            Console.WriteLine("While we create your new friend, why don't you give them a name?");
            _virtualFriendsName = Console.ReadLine();

            if (_virtualFriendsName == "Onur")
            {
                Console.WriteLine($"Jeez... Isn't it depressing to not be able to think of another name other than yours? Anyways... {_virtualFriendsName} is your new virtual friend's name too. ");
                Thread.Sleep(1000);
            }
            else if (string.IsNullOrEmpty(_virtualFriendsName))
            {
                Console.WriteLine("You haven't given them a name. That's sad... From now on, we will call them Onur then!");
                _virtualFriendsName = "Onur";
                Thread.Sleep(1000);
            }
            else
            {
                Console.WriteLine($"What a wonderful name! {_virtualFriendsName} is your new virtual friend's name.");
                Thread.Sleep(1000);
            }
        }

        private void StartCountdown()
        {
            Console.WriteLine("Your virtual friend is being generated");

            for (int i = 5; i > 0; i--)
            {
                Console.WriteLine(i);
                Thread.Sleep(1000);
            }

            Console.WriteLine("...");
            Thread.Sleep(2500);

            for (int i = -1; i > -11; i--)
            {
                Console.WriteLine(i);
                Thread.Sleep(100);
            }

            Console.WriteLine($"\r\nVirtual Friends for Losers\r\nThe 'VirtualFriendforLosers_{_virtualFriendsName}' package did not load correctly.\r\nThe problem may have been caused by a configuration change or by the installation of another extension.\r\nThe file seems to be located elsewhere.\r\n'C:\\Users\\{_virtualFriendsName}\\AppData\\Roaming\\Virtual Friends for Losers\\15.0_15c78df\r\n6\\ActivityLog.xml'.\r\nYou can press '1' to choose option 1, '2' to choose option 2");

            // Ask the player to make a choice
            Console.WriteLine("What do you want to do?");
            Console.WriteLine($"1. Find where your new virtual friend {_virtualFriendsName} is");
            Console.WriteLine("2. Exit");

            // Get the player's choice
            int playerChoice = int.Parse(Console.ReadLine());

            // Process the player's choice
            switch (playerChoice)
            {
                case 1:
                    // Create an instance of TheGame class
                    TheGame game = new TheGame(_virtualFriendsName);
                    game.StartGame(); // Start the game
                    break;
                case 2:
                    Console.WriteLine("You have been a bad friend! YOU LOST THE GAME!");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}