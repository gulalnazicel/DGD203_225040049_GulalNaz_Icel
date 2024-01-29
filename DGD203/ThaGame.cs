using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

public class TheGame
{
    private const int DefaultMapWidth = 11;
    private const int DefaultMapHeight = 11;

    private Map _gameMap;
    private Vector2 _playerCoordinates;
    private List<Enemy> _enemies;

    private int _playerHealth = 5;
    private string _virtualFriendsName;

    private int _defeatedRegularEnemiesCount = 0;
    private bool _specialEnemySpawned = false;

    public TheGame(string virtualFriendsName)
    {
        _virtualFriendsName = virtualFriendsName;
        CreateGame();
    }

    private void CreateGame()
    {
        _gameMap = new Map();
        _playerCoordinates = new Vector2(DefaultMapWidth / 2, DefaultMapHeight / 2);
        _enemies = new List<Enemy>(); // Initialize the _enemies list
        InitializeEnemies();
    }

    private void InitializeEnemies()
    {
        Random random = new Random();

        // Spawn regular enemies
        for (int i = 0; i < 7; i++)
        {
            int x = random.Next(DefaultMapWidth);
            int y = random.Next(DefaultMapHeight);
            _enemies.Add(new Enemy(x, y));
        }

        // Spawn special enemy named after _virtualFriendsName
        int specialEnemyX = random.Next(DefaultMapWidth);
        int specialEnemyY = random.Next(DefaultMapHeight);
        _enemies.Add(new Enemy(specialEnemyX, specialEnemyY, _virtualFriendsName));
    }

    public void StartGame()
    {
       

        while (true)
        {
            _gameMap.DisplayCurrentLocation(_playerCoordinates);

            Console.WriteLine($"Welcome to our old game named 'dkfjskjdfn'! It seems like the dev was a little bit lazy for organizing and naming the files. \r\nYou now can write 'HELP' to learn the commands.");
            Console.WriteLine("Use W, A, S, D to move (North, West, South, East) or Q to quit.");
            string playerInput = Console.ReadLine()?.ToUpper() ?? "";

            if (playerInput == "Q")
            {
                EndGame();
                break;
            }

            ProcessInput(playerInput);
        }
    }

    private void ProcessInput(string input)
    {
        switch (input)
        {
            case "D":
                MovePlayer(0, 1);
                break;
            case "A":
                MovePlayer(0, -1);
                break;
            case "S":
                MovePlayer(1, 0);
                break;
            case "W":
                MovePlayer(-1, 0);
                break;
            case "EXIT":
                EndGame();
                break;
            case "SAVE":
                SaveGame();
                Console.WriteLine("Game saved");
                break;
            case "HELP":
                Console.WriteLine(HelpMessage());
                break;
            case "CLEAR":
                Console.Clear();
                break;
            default:
                Console.WriteLine("Invalid command. Type HELP for a list of commands.");
                break;
        }
    }

    private void MovePlayer(int deltaX, int deltaY)
    {
        Vector2 newCoordinates = _playerCoordinates + new Vector2(deltaX, deltaY);

        if (_gameMap.CanMoveTo(newCoordinates))
        {
            _playerCoordinates = newCoordinates;

            // Check for encounters with enemies
            EncounterEnemies();
        }
        else
        {
            Console.WriteLine("This game's map finishes here. You cannot go further. Now I understand why this game sucked...");
        }
    }

    

    private void Flee(Enemy enemy)
    {
        // Implement flee logic here
        Console.WriteLine($"You chose to flee from {enemy.Name}. Repeating your last movement command.");
    }

    private void DisplayHealthBars(Enemy enemy)
    {
        Console.WriteLine($"Player's Health: {_playerHealth} | {enemy.Name}'s Health: {enemy.Health}");
    }

    private void AttackEnemy(Enemy enemy)
    {
        // Generate random player damage between 1 and 4
        Random random = new Random();
        int playerDamage = random.Next(1, 4);

        // Check for a critical hit
        if (playerDamage == 3)
        {
            Console.WriteLine("Critical Hit!");
        }

        // Apply damage to the enemy
        enemy.TakeDamage(playerDamage);

        // Check if the enemy is defeated
        if (enemy.Health <= 0)
        {
            Console.WriteLine($"{enemy.Name} is dead. Keep searching for your friend.");

            // Remove the defeated enemy from the list
            _enemies.Remove(enemy);

            if (enemy.Name != _virtualFriendsName)
            {
                _defeatedRegularEnemiesCount++;
            }

            // If the defeated enemy is the special enemy, end the game
            if (enemy.Name == _virtualFriendsName)
            {
                Console.WriteLine($"You lost the one who betrayed. You won at the end of the game!");
                EndGame();
            }

            // Check if the player has defeated at least 3 regular enemies
            if (_defeatedRegularEnemiesCount == 3 && !_specialEnemySpawned)
            {
                // Spawn the special enemy named after _virtualFriendsName
                SpawnSpecialEnemy();
            }
        }
        else
        {
            // Display damage and current health information
            Console.WriteLine($"You dealt {playerDamage} damage to {enemy.Name}.");
            Console.WriteLine($"{enemy.Name}'s current health: {enemy.Health}");

            // Generate random enemy damage between 0 and 3
            int enemyDamage = random.Next(4);

            // Display enemy counterattack information
            Console.WriteLine($"{enemy.Name} counterattacks! You took {enemyDamage} damage.");

            // Display health bars after every attack
            DisplayHealthBars(enemy);

            // Check for player and enemy health
            CheckHealth(enemy);
        }
    }


    private void EncounterEnemies()
    {
        List<Enemy> enemiesCopy = new List<Enemy>(_enemies);

        foreach (var enemy in enemiesCopy)
        {
            if (enemy.X == (int)_playerCoordinates.X && enemy.Y == (int)_playerCoordinates.Y)
            {
                Console.WriteLine($"You encountered an enemy ({enemy.Name}) at ({enemy.X}, {enemy.Y})!");

                // Present options to the player
                Console.WriteLine("Options:");
                Console.WriteLine("1. Flee");
                Console.WriteLine("2. HOW DARE YOU?");

                // Get the player's choice
                int choice = int.Parse(Console.ReadLine());

                // Process the player's choice
                switch (choice)
                {
                    case 1:
                        Flee(enemy);
                        break;
                    case 2:
                        AttackEnemy(enemy);
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
    }


   

    private void EndGameSpecialEnemy()
    {
        Console.WriteLine($"Good, you killed your new virtual friend {_virtualFriendsName}. I guess the game ended? (BAD ENDING)");
        EndGame();
    }

    private void EncounterSpecialEnemy(Enemy enemy)
    {
        Console.WriteLine($"You encountered a special enemy named {_virtualFriendsName} at ({enemy.X}, {enemy.Y})!");

        // Present options to the player
        Console.WriteLine("Options:");
        Console.WriteLine("1. Flee");
        Console.WriteLine("2. HOW DARE YOU?");

        // Get the player's choice
        int choice = int.Parse(Console.ReadLine());

        // Process the player's choice
        switch (choice)
        {
            case 1:
                Flee(enemy);
                break;
            case 2:
                AttackSpecialEnemy(enemy);
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }

    private void AttackSpecialEnemy(Enemy enemy)
    {
        // Generate random player damage between 1 and 4
        Random random = new Random();
        int playerDamage = random.Next(1, 4);

        // Check for a critical hit
        if (playerDamage == 3)
        {
            Console.WriteLine("Critical Hit!");
        }

        // Apply damage to the special enemy
        enemy.TakeDamage(playerDamage);

        // Display damage and current health information
        Console.WriteLine($"You dealt {playerDamage} damage to {_virtualFriendsName}.");
        Console.WriteLine($"{_virtualFriendsName}'s current health: {enemy.Health}");

        // Check if the special enemy is defeated
        if (enemy.Health <= 0)
        {
            Console.WriteLine($"Good, you killed your new virtual friend {_virtualFriendsName}. I guess the game ended? (BAD ENDING)");

            // Remove the special enemy from the list
            _enemies.Remove(enemy);

            // End the game
            EndGame();
        }
        else
        {
            // Generate random enemy damage between 0 and 3
            int enemyDamage = random.Next(4);

            // Display enemy counterattack information
            Console.WriteLine($"{_virtualFriendsName} counterattacks! You took {enemyDamage} damage.");

            // Display health bars after every attack
            DisplayHealthBars(enemy);

            // Check for player and special enemy health
            CheckHealth(enemy);
        }
    }

    private void SpawnSpecialEnemy()
    {
        Random random = new Random();
        int specialEnemyX = random.Next(DefaultMapWidth);
        int specialEnemyY = random.Next(DefaultMapHeight);

        _enemies.Add(new Enemy(specialEnemyX, specialEnemyY, _virtualFriendsName));

        Console.WriteLine($"After defeating 3 regular enemies, a special enemy named {_virtualFriendsName} appears at ({specialEnemyX}, {specialEnemyY})!");
        _specialEnemySpawned = true;
    }

    private void CheckHealth(Enemy enemy)
    {
        // ... (existing code)

        // Check if the player has defeated at least 3 regular enemies
        if (_defeatedRegularEnemiesCount >= 3 && !_specialEnemySpawned)
        {
            // Spawn the special enemy named after _virtualFriendsName
            SpawnSpecialEnemy();
        }
    }


    private void EndGame()
    {
        Console.WriteLine($"Hope you saved your progress to find {_virtualFriendsName} later! Hope to see you soon!");
        Environment.Exit(0);
    }

    private void SaveGame()
    {
        try
        {
            using (StreamWriter writer = new StreamWriter("saved_game.txt"))
            {
                // Save player coordinates
                writer.WriteLine($"Your coordinates are: {_playerCoordinates.X},{_playerCoordinates.Y}");

                // Save virtual friend's name
                writer.WriteLine($"Virtual friend's name: {_virtualFriendsName}");

                // Save player health
                writer.WriteLine($"Your health: {_playerHealth}");

                Console.WriteLine("Game saved successfully!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving game: {ex.Message}");
        }
    }

    private string HelpMessage()
    {
        return @"Here are the current commands:
N: go north
S: go south
W: go west
E: go east
load: Load saved game
save: save current game
exit: exit the game
who: view the player information

clear: clear the screen";
    }
}
