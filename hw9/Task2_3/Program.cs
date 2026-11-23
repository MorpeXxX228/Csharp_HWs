using System;

public interface IDamageCalculator
{
    int CalculateDamage(int baseDamage, int targetArmor);
}

public class PhysicalDamageCalculator : IDamageCalculator
{
    public int CalculateDamage(int baseDamage, int targetArmor)
    {
        int damage = baseDamage - targetArmor / 2;
        return Math.Max(1, damage);
    }
}

public class MagicDamageCalculator : IDamageCalculator
{
    public int CalculateDamage(int baseDamage, int targetArmor)
    {
        int damage = Math.Max(1, baseDamage - targetArmor / 4);

        if (targetArmor == 0)
        {
            damage *= 2;
        }

        return damage;
    }
}

public class FireDamageCalculator : IDamageCalculator
{
    public int CalculateDamage(int baseDamage, int targetArmor)
    {
        int damage = Math.Max(1, baseDamage - targetArmor / 10);

        if (baseDamage > 50)
        {
            damage += 10;
        }

        return damage;
    }
}

public class Unit
{
    public string Name { get; }
    public int HP { get; set; }
    public int Armor { get; }

    public Unit(string name, int hp, int armor)
    {
        Name = name;
        HP = hp;
        Armor = armor;
    }
}

public class DamageSystem
{
    private readonly IDamageCalculator _calculator;

    public DamageSystem(IDamageCalculator calculator)
    {
        _calculator = calculator;
    }

    public void ApplyDamage(Unit target, int baseDamage)
    {
        int damage = _calculator.CalculateDamage(baseDamage, target.Armor);
        target.HP = Math.Max(0, target.HP - damage);
        Console.WriteLine($"Юнит {target.Name} получил {damage} урона. Осталось HP: {target.HP}");
    }
}

public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        const int baseDamage = 60;

        var calculators = new (string Title, IDamageCalculator Calc)[]
        {
            ("Физический урон", new PhysicalDamageCalculator()),
            ("Магический урон", new MagicDamageCalculator()),
            ("Огненный урон", new FireDamageCalculator())
        };

        Console.WriteLine($"Базовый урон: {baseDamage}");
        Console.WriteLine();

        foreach (var (title, calc) in calculators)
        {
            Console.WriteLine($"--- {title} ---");

            var golem = new Unit("Голем (броня 40)", hp: 200, armor: 40);
            var ghost = new Unit("Призрак (броня 0)", hp: 200, armor: 0);

            var system = new DamageSystem(calc);
            system.ApplyDamage(golem, baseDamage);
            system.ApplyDamage(ghost, baseDamage);
            Console.WriteLine();
        }
    }
}