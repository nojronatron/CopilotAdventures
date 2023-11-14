using System;
using System.Collections.Generic;

namespace algora_solution
{
  class Program
  {
    static void Main(string[] args)
    {
      Creature Lox = new("Lox", new List<Moves> { Moves.Twirl, Moves.Leap, Moves.Spin, Moves.Twirl, Moves.Leap });
      Creature Faelis = new("Faelis", new List<Moves> { Moves.Spin, Moves.Twirl, Moves.Leap, Moves.Leap, Moves.Spin });

      Console.WriteLine(Lox.Greet());
      Console.WriteLine(Faelis.Greet());
      Console.WriteLine(MagicalEffect.GetInitialState());
      for (int i = 0; i < Lox.Moves.Count; i++)
      {
        Console.WriteLine($"{Lox.Name} {Lox.Moves[i]}s and {Faelis.Name} {Faelis.Moves[i]}s.");
        Console.WriteLine(MagicalEffect.GetEffect(Lox.Moves[i], Faelis.Moves[i]));
      }
    }
  }

  public class Creature
  {
    public string Name { get; set; }
    public List<Moves> Moves { get; set; }

    public Creature(string name, List<Moves> moves)
    {
      Name = name;
      Moves = moves;
    }
    public string Greet()
    {
      return $"Hello, my name is {Name}.";
    }
  }

  public static class MagicalEffect
  {
    public static List<string> Effects { get; set; } = new List<string> { "Fireflies light up the forest.", "Gentle rain starts falling.", "A rainbow appears in the sky." };
    public static string GetInitialState()
    {
      return "The forest is still and quiet.";
    }
    public static string GetEffect(Moves move1, Moves move2)
    {
      switch (move1)
      {
        case Moves.Twirl:
          switch (move2)
          {
            case Moves.Twirl:
              return MagicalEffect.Effects[0];
            case Moves.Leap:
              return MagicalEffect.Effects[1];
            case Moves.Spin:
              return MagicalEffect.Effects[2];
            default:
              throw new ArgumentException("Invalid move2 value");
          }
        case Moves.Leap:
          switch (move2)
          {
            case Moves.Twirl:
              return MagicalEffect.Effects[2];
            case Moves.Leap:
              return MagicalEffect.Effects[0];
            case Moves.Spin:
              return MagicalEffect.Effects[1];
            default:
              throw new ArgumentException("Invalid move2 value");
          }
        case Moves.Spin:
          switch (move2)
          {
            case Moves.Twirl:
              return MagicalEffect.Effects[1];
            case Moves.Leap:
              return MagicalEffect.Effects[2];
            case Moves.Spin:
              return MagicalEffect.Effects[0];
            default:
              throw new ArgumentException("Invalid move2 value");
          }
        default:
          throw new ArgumentException("Invalid move1 value");
      }
    }
  }

  public enum Moves
  {
    Twirl,
    Leap,
    Spin
  }
}
