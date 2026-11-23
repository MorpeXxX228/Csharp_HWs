using System;
using System.Collections.Generic;

public enum Rarity
{
    Common,
    Rare,
    Legendary
}

public interface IUsableItem
{
    string Name { get; }
    string Description { get; }
    Rarity Rarity { get; }
    void Use(Player player);
}

public class Player
{
    public string Name { get; }
    public int HP { get; private set; }
    public int Damage { get; private set; }
    public bool IsAlive => HP > 0;

    public Player(string name, int hp, int damage)
    {
        Name = name;
        HP = hp;
        Damage = damage;
    }

    public void Heal(int amount)
    {
        HP += amount;
    }

    public void IncreaseDamage(int amount)
    {
        Damage += amount;
    }

    public void TakeDamage(int amount)
    {
        HP = Math.Max(0, HP - amount);
    }
}

public class HealthPotion : IUsableItem
{
    public string Name => "Зелье здоровья";
    public string Description => "Восстанавливает 50 HP";
    public Rarity Rarity => Rarity.Common;

    public void Use(Player player)
    {
        player.Heal(50);
        Console.WriteLine("Вы выпили зелье и восстановили 50 HP");
    }
}

public class Sword : IUsableItem
{
    public string Name => "Меч";
    public string Description => "Увеличивает урон на 10";
    public Rarity Rarity => Rarity.Rare;

    public void Use(Player player)
    {
        player.IncreaseDamage(10);
        Console.WriteLine("Вы взяли меч в руки: урон увеличен на 10");
    }
}

public class AncientScroll : IUsableItem
{
    public string Name => "Древний свиток";
    public string Description => "Проклятие: наносит вам 20 урона, но увеличивает урон на 30";
    public Rarity Rarity => Rarity.Legendary;

    public void Use(Player player)
    {
        player.TakeDamage(20);
        player.IncreaseDamage(30);
        Console.WriteLine("Вы прочитали свиток: проклятие забрало 20 HP, но урон вырос на 30");
    }
}

public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var player = new Player("Герой", hp: 100, damage: 15);

        var items = new List<IUsableItem>
        {
            new HealthPotion(),
            new Sword(),
            new AncientScroll()
        };

        Console.WriteLine($"Начало: {player.Name}, HP = {player.HP}, урон = {player.Damage}");
        Console.WriteLine();

        foreach (var item in items)
        {
            Console.WriteLine($"[{item.Rarity}] {item.Name} - {item.Description}");
            item.Use(player);
            Console.WriteLine();
        }

        Console.WriteLine("Итоговые характеристики:");
        Console.WriteLine($"Имя: {player.Name}");
        Console.WriteLine($"HP: {player.HP}");
        Console.WriteLine($"Урон: {player.Damage}");
        Console.WriteLine($"Состояние: {(player.IsAlive ? "жив" : "мёртв")}");
    }
}
