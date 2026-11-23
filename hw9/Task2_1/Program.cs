using System;

public interface ICombatant
{
    public string Name { get; }
    public int HP { get; set; }
    public bool IsDead { get; }
    public int Damage { get; }
    public void Attack(ICombatant target);
    public void TakeDamage(int amount);
}

public class Knight : ICombatant
{
    public string Name { get; }
    public int HP { get; set; } = 150;
    public int Damage { get; } = 25;
    public bool IsDead => HP <= 0;

    public Knight(string name)
    {
        Name = name;
    }

    public void Attack(ICombatant target)
    {
        if (IsDead)
        {
            Console.WriteLine($"Рыцарь {Name} мёртв и не может атаковать");
            return;
        }

        if (target.IsDead)
        {
            Console.WriteLine($"Рыцарь {Name} не может атаковать: {target.Name} уже мёртв");
            return;
        }

        Console.WriteLine($"Рыцарь {Name} атакует!");
        target.TakeDamage(Damage);
    }

    public void TakeDamage(int amount)
    {
        HP = Math.Max(0, HP - amount);
        Console.WriteLine($"Рыцарь {Name} получил {amount} урона. Осталось HP: {HP}");
    }
}

public class Mage : ICombatant
{
    public string Name { get; }
    public int HP { get; set; } = 80;
    public int Damage { get; } = 40;
    public bool IsDead => HP <= 0;

    public Mage(string name)
    {
        Name = name;
    }

    public void Attack(ICombatant target)
    {
        if (IsDead)
        {
            Console.WriteLine($"Маг {Name} мёртв и не может атаковать");
            return;
        }

        if (target.IsDead)
        {
            Console.WriteLine($"Маг {Name} не может атаковать мёртвую цель");
            return;
        }

        Console.WriteLine($"Маг {Name} атакует двойным заклинанием!");

        for (int i = 0; i < 2; i++)
        {
            if (target.IsDead)
            {
                break;
            }

            target.TakeDamage(Damage);
        }
    }

    public void TakeDamage(int amount)
    {
        HP = Math.Max(0, HP - amount);
        Console.WriteLine($"Маг {Name} получил {amount} урона. Осталось HP: {HP}");
    }
}

public class Program
{
    private static string Title(ICombatant c) => c is Knight ? "Рыцарь" : "Маг";

    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        ICombatant knight = new Knight("Артур");
        ICombatant mage = new Mage("Мерлин");

        ICombatant attacker = knight;
        ICombatant defender = mage;

        while (!knight.IsDead && !mage.IsDead)
        {
            attacker.Attack(defender);
            (attacker, defender) = (defender, attacker);
        }

        ICombatant winner = knight.IsDead ? mage : knight;
        Console.WriteLine($"Победил {Title(winner)} {winner.Name}!");
    }
}