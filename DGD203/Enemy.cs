using System;

public class Enemy
{
    public int X { get; }
    public int Y { get; }
    public int Health { get; private set; }
    public string Name { get; }

    public Enemy(int x, int y)
    {
        X = x;
        Y = y;
        Health = 5;
        Name = "NOT YOUR FRIEND";
    }

    public Enemy(int x, int y, string name)
    {
        X = x;
        Y = y;
        Health = 5;
        Name = name;
    }

    public int Attack()
    {
        // Generate random attack damage between 0 and 3
        Random random = new Random();
        return random.Next(4);
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }
}
