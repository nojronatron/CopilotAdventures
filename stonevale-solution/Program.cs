using System;

class Program
{
  static void Main(string[] args)
  {
    var albert = new Player("Albert", new[] { Moves.Scissors, Moves.Paper, Moves.Scissors, Moves.Rock, Moves.Rock });
    var brad = new Player("Brad", new[] { Moves.Rock, Moves.Rock, Moves.Paper, Moves.Scissors, Moves.Paper });
    var game = new Game(albert, brad);

    game.Play();
  }
}

/// <summary>
/// Represents a player in the game.
/// </summary>
public class Player
{
  public string Name { get; set; }
  public int Score { get; set; }
  public Moves[] Moves { get; set; }

  /// <summary>
  /// Initializes a new instance of the <see cref="Player"/> class with the specified name and moves.
  /// </summary>
  /// <param name="name">The name of the player.</param>
  /// <param name="moves">The moves of the player.</param>
  /// <exception cref="ArgumentException">Thrown when the number of moves is not exactly 5.</exception>
  /// <exception cref="ArgumentNullException">Thrown when the moves array is null.</exception>
  public Player(string name, Moves[] moves)
  {
    if (moves.Length != 5)
    {
      throw new ArgumentException("Player must have exactly 5 moves", nameof(moves));
    }
    Name = name;
    Score = 0;
    Moves = moves ?? throw new ArgumentNullException(nameof(moves));
  }
}

public enum Moves
{
  Rock,
  Paper,
  Scissors
}

/// <summary>
/// Represents a game of Rock-Paper-Scissors between two players.
/// </summary>
public class Game
{
  bool CanPlay => Player1.Moves.Length == 5
    && Player2.Moves.Length == 5;
  private Player Player1 { get; set; }
  private Player Player2 { get; set; }

  /// <summary>
  /// Initializes a new instance of the <see cref="Game"/> class with the specified players.
  /// </summary>
  /// <param name="player1">The first player.</param>
  /// <param name="player2">The second player.</param>
  public Game(Player player1, Player player2)
  {
    Player1 = player1 ?? throw new ArgumentNullException(nameof(player1));
    Player2 = player2 ?? throw new ArgumentNullException(nameof(player2));
  }

  /// <summary>
  /// Plays the game by iterating through each round and determining the winner of each round based on the moves of each player.
  /// </summary>
  public void Play()
  {
    if (!CanPlay)
    {
      throw new InvalidOperationException("Game is not ready. Run Initialize once for each player to add their moves.");
    }

    for (int i = 0; i < 5; i++)
    {
      var player1Move = Player1.Moves[i];
      var player2Move = Player2.Moves[i];

      if (player1Move == player2Move)
      {
        Console.WriteLine($"Round {i + 1}: Draw");
      }
      else if ((player1Move == Moves.Rock && player2Move == Moves.Scissors) ||
               (player1Move == Moves.Paper && player2Move == Moves.Rock) ||
               (player1Move == Moves.Scissors && player2Move == Moves.Paper))
      {
        int points = GetPoints(player1Move);
        Console.WriteLine($"Round {i + 1}: {Player1.Name} wins and earns {points} points");
        Player1.Score += GetPoints(player1Move);
      }
      else
      {
        int points = GetPoints(player2Move);
        Console.WriteLine($"Round {i + 1}: {Player2.Name} wins and earns {points} points");
        Player2.Score += GetPoints(player2Move);
      }
    }

    Console.WriteLine($"{Player1.Name} total points: {Player1.Score}");
    Console.WriteLine($"{Player2.Name} total points: {Player2.Score}");

    if (Player1.Score > Player2.Score)
    {
      Console.WriteLine($"{Player1.Name} wins the game with {Player1.Score} points");
    }
    else if (Player2.Score > Player1.Score)
    {
      Console.WriteLine($"{Player2.Name} wins the game with {Player2.Score} points");
    }
    else
    {
      Console.WriteLine("The game is a draw");
    }
  }

  /// <summary>
  /// Returns the points earned for a given move.
  /// </summary>
  /// <param name="move">The move to evaluate.</param>
  /// <returns>The points earned for the move.</returns>
  private int GetPoints(Moves move)
  {
    switch (move)
    {
      case Moves.Rock:
        return 1;
      case Moves.Paper:
        return 2;
      case Moves.Scissors:
        return 3;
      default:
        throw new ArgumentOutOfRangeException(nameof(move), move, null);
    }
  }
}