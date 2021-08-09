using System;
using System.Collections.Generic;

class PlayerInventory
{
    private int numberOfCoins = 0;
    private List<string> items = new List<string>();

    public void AddItem(string item) =>
        items.Add(item);

    public void AddCoins(int value) =>
        numberOfCoins += value;

    public void SeeInInventory()
    {
        if (numberOfCoins > 0)
            Console.WriteLine($"В кошельке {numberOfCoins} монет");
        if (items.Count > 0)
        {
            Console.WriteLine("Сейчас в инвенторе есть:\n");
            items.ForEach(item => Console.WriteLine($"{item}\n"));
        }
    }
}

