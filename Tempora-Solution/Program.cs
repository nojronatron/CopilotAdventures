using System;

namespace Tempora_Solution
{
  class Program
  {
    static void Main(string[] args)
    {
      var clocks = new List<TemporaClock>
      {
        new TemporaClock("Clock 1", new DateTime(2023, 11, 08, 14, 45, 00)),
        new TemporaClock("Clock 2", new DateTime(2023, 11, 08, 15, 05, 00)),
        new TemporaClock("Clock 3", new DateTime(2023, 11, 08, 15, 00, 00)),
        new TemporaClock("Clock 4", new DateTime(2023, 11, 08, 14, 40, 00))
      };

      var grandClockTower = new TemporaClock("GrandClockTower", new DateTime(2023, 11, 08, 15, 00, 00));

      Console.WriteLine();
      Console.WriteLine(grandClockTower.ShowClock());

      clocks.ForEach(clock => Console.WriteLine(clock.ShowClock()));

      Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
      Console.WriteLine("Now calculating offsets...");

      clocks.ForEach(clock => Console.WriteLine(grandClockTower.GetOffsetStatement(clock)));

      Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
      Console.WriteLine("Now synchronizing clocks...");

      clocks.ForEach(clock =>
      {
        clock.SyncWith(grandClockTower);
        Console.WriteLine(clock.ShowClock());
      });

      Console.WriteLine();
    }
  }
  public class TemporaClock
  {
    public string Name { get; set; }
    public DateTime Time { get; set; }

    public TemporaClock(string name, DateTime time)
    {
      Name = name;
      Time = time;
    }

    /// <summary>
    /// Calculates the difference in minutes between the current TemporaClock instance and another TemporaClock instance.
    /// </summary>
    /// <param name="other">The other TemporaClock instance to compare with.</param>
    /// <returns>The difference in minutes between the two TemporaClock instances.</returns>
    public int GetDifferenceInMinutes(TemporaClock other)
    {
      return (int)(other.Time - Time).TotalMinutes;
    }

    /// <summary>
    /// Returns a string that describes the offset between this TemporaClock and another TemporaClock instance.
    /// </summary>
    /// <param name="other">The TemporaClock instance to compare with.</param>
    /// <returns>A string that describes the offset between the two TemporaClock instances.</returns>
    public string GetOffsetStatement(TemporaClock other)
    {
      var diff = GetDifferenceInMinutes(other);
      if (diff > 0)
      {
        return $"{other.Name} is ahead of {Name} by {diff} minutes";
      }
      else if (diff < 0)
      {
        return $"{other.Name} is behind {Name} by {-diff} minutes";
      }
      else
      {
        return $"{other.Name} is in sync with {Name}";
      }
    }

    /// <summary>
    /// Synchronizes the current TemporaClock instance with another TemporaClock instance.
    /// </summary>
    /// <param name="other">The TemporaClock instance to synchronize with.</param>
    public void SyncWith(TemporaClock other)
    {
      Time = other.Time;
    }

    /// <summary>
    /// Returns a string that represents the current time for the given name.
    /// </summary>
    public string ShowClock()
    {
      return $"{Name} time reads {Time}";
    }
  }
}