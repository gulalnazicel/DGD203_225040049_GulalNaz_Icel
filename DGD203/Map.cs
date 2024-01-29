using System;
using System.Numerics;

public class Map
{
    private const int MapWidth = 11;
    private const int MapHeight = 11;

    private char[,] _map;

    public Map()
    {
        _map = new char[MapWidth, MapHeight];
        InitializeMap();
    }

    private void InitializeMap()
    {
        for (int i = 0; i < MapWidth; i++)
        {
            for (int j = 0; j < MapHeight; j++)
            {
                _map[i, j] = '.';
            }
        }
    }

    public void DisplayCurrentLocation(Vector2 playerCoordinates)
    {
        Console.Clear();
        DrawMap(playerCoordinates);
    }

    private void DrawMap(Vector2 playerCoordinates)
    {
        for (int i = 0; i < MapWidth; i++)
        {
            for (int j = 0; j < MapHeight; j++)
            {
                if (i == (int)playerCoordinates.X && j == (int)playerCoordinates.Y)
                {
                    Console.Write("P "); // Player's position
                }
                else
                {
                    Console.Write(_map[i, j] + " ");
                }
            }
            Console.WriteLine();
        }
    }

    public bool CanMoveTo(Vector2 newCoordinates)
    {
        return !(newCoordinates.X < 0 || newCoordinates.X >= MapWidth || newCoordinates.Y < 0 || newCoordinates.Y >= MapHeight);
    }
}
