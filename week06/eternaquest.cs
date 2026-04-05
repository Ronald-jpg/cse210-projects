using System;
using System.Collections.Generic;
using System.IO;

// Base class
abstract class Goal
{
    private string _name;
    private string _description;
    private int _points;

    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public string Name => _name;
    public string Description => _description;
    public int Points => _points;

    public abstract int RecordEvent();
    public virtual string GetDetailsString() => $"[ ] {_name} ({_description})";
}

// SimpleGoal
class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int points)
        : base(name, description, points)
    {
        _isComplete = false;
    }

    public override int RecordEvent()
    {
        if (!_isComplete)
        {
            _isComplete = true;
            return Points;
        }
        return 0;
    }

    public override string GetDetailsString()
    {
        return $"{(_isComplete ? "[X]" : "[ ]")} {Name} ({Description})";
    }
}

// EternalGoal
class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override int RecordEvent() => Points;

    public override string GetDetailsString()
    {
        return $"[∞] {Name} ({Description})";
    }
}

// ChecklistGoal
class ChecklistGoal : Goal
{
    private int _targetCount;
    private int _currentCount;
    private int _bonus;

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonus)
        : base(name, description, points)
    {
        _targetCount = targetCount;
        _bonus = bonus;
        _currentCount = 0;
    }

    public override int RecordEvent()
    {
        _currentCount++;
        if (_currentCount == _targetCount)
        {
            return Points + _bonus;
        }
        return Points;
    }

    public override string GetDetailsString()
    {
        return $"[{(_currentCount >= _targetCount ? "X" : " ")}] {Name} ({Description}) -- Completed {_currentCount}/{_targetCount}";
    }
}

// Manager
class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;
    private int _level = 1;

    public void AddGoal(Goal goal) => _goals.Add(goal);

    public void RecordGoalEvent(int index)
    {
        if (index >= 0 && index < _goals.Count)
        {
            _score += _goals[index].RecordEvent();
            CheckLevelUp();
        }
    }

    private void CheckLevelUp()
    {
        if (_score >= _level * 1000)
        {
            _level++;
            Console.WriteLine($"🎉 Congratulations! You leveled up to Level {_level}!");
        }
    }

    public void ShowGoals()
    {
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i+1}. {_goals[i].GetDetailsString()}");
        }
    }

    public void ShowScore()
    {
        Console.WriteLine($"Current Score: {_score} | Level: {_level}");
    }

    public void SaveGoals(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine(_score);
            writer.WriteLine(_level);
            foreach (var goal in _goals)
            {
                writer.WriteLine($"{goal.GetType().Name}|{goal.Name}|{goal.Description}|{goal.Points}");
            }
        }
    }

    public void LoadGoals(string filename)
    {
        _goals.Clear();
        using (StreamReader reader = new StreamReader(filename))
        {
            _score = int.Parse(reader.ReadLine());
            _level = int.Parse(reader.ReadLine());
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var parts = line.Split('|');
                string type = parts[0];
                string name = parts[1];
                string desc = parts[2];
                int points = int.Parse(parts[3]);

                if (type == nameof(SimpleGoal))
                    _goals.Add(new SimpleGoal(name, desc, points));
                else if (type == nameof(EternalGoal))
                    _goals.Add(new EternalGoal(name, desc, points));
                else if (type == nameof(ChecklistGoal))
                    _goals.Add(new ChecklistGoal(name, desc, points, 5, 500)); // default values
            }
        }
    }
}

// Program
class Program
{
    static void Main()
    {
        GoalManager manager = new GoalManager();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Create Goal");
            Console.WriteLine("2. Record Event");
            Console.WriteLine("3. Show Goals");
            Console.WriteLine("4. Show Score");
            Console.WriteLine("5. Save Goals");
            Console.WriteLine("6. Load Goals");
            Console.WriteLine("7. Quit");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Choose type: 1=Simple, 2=Eternal, 3=Checklist");
                    string type = Console.ReadLine();
                    Console.Write("Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Description: ");
                    string desc = Console.ReadLine();
                    Console.Write("Points: ");
                    int points = int.Parse(Console.ReadLine());

                    if (type == "1")
                        manager.AddGoal(new SimpleGoal(name, desc, points));
                    else if (type == "2")
                        manager.AddGoal(new EternalGoal(name, desc, points));
                    else if (type == "3")
                    {
                        Console.Write("Target count: ");
                        int target = int.Parse(Console.ReadLine());
                        Console.Write("Bonus: ");
                        int bonus = int.Parse(Console.ReadLine());
                        manager.AddGoal(new ChecklistGoal(name, desc, points, target, bonus));
                    }
                    break;

                case "2":
                    manager.ShowGoals();
                    Console.Write("Which goal number? ");
                    int index = int.Parse(Console.ReadLine()) - 1;
                    manager.RecordGoalEvent(index);
                    break;

                case "3":
                    manager.ShowGoals();
                    break;

                case "4":
                    manager.ShowScore();
                    break;

                case "5":
                    manager.SaveGoals("goals.txt");
                    break;

                case "6":
                    manager.LoadGoals("goals.txt");
                    break;

                case "7":
                    running = false;
                    break;
            }
        }
    }
}
