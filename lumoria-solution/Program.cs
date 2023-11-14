using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace lumoria_solution
{
  public class Program
  {
    public static void Main()
    {
      var galaxy = new Galaxy
      {
        Planets = new List<Planet>
        {
          new Planet { Name = "Mercuria", DistanceFromSun = 0.4M, Size = 4879 },
          new Planet { Name = "Earthia", DistanceFromSun = 1M, Size = 12742 },
          new Planet { Name = "Marsia", DistanceFromSun = 1.5M, Size = 6779 },
          new Planet { Name = "Venusia", DistanceFromSun = 0.7M, Size = 12104 }
        }
      };
      galaxy.OutputPlanetLightIntensities();
    }
  }

  public class Galaxy
  {
    public List<Planet> Planets { get; set; }

    public Galaxy()
    {
      Planets = new List<Planet>();
    }
    public void OutputPlanetLightIntensities()
    {
      Planets.Sort();
      for (int i = 0; i < Planets.Count; i++)
      {
        var planet = Planets[i];
        planet.Illumination = IlluminationType.Full;
        int shadowCount = 0;
        for (int j = 0; j < i; j++)
        {
          var otherPlanet = Planets[j];
          if (otherPlanet.IsSmallerThan(planet))
          {
            shadowCount++;
            if (shadowCount > 1)
            {
              planet.Illumination = IlluminationType.NoneMultipleShadows;
              break;
            }
            planet.Illumination = IlluminationType.Partial;
          }
          else if (planet.IsSmallerThan(otherPlanet))
          {
            planet.Illumination = IlluminationType.None;
          }
        }
        Console.WriteLine($"{planet.Name}: {planet.Illumination}");
      }
    }
  }

  public class Planet : IComparable<Planet>
  {
    public Planet()
    {
      Illumination = IlluminationType.Full;
      Name = string.Empty;
    }
    public IlluminationType Illumination { get; set; }
    public string Name { get; set; }
    public decimal DistanceFromSun { get; set; }
    public int Size { get; set; }

    public string GetPlanetInfo()
    {
      return $"Name: {Name}, Distance: {DistanceFromSun} AU, Size: {Size} km";
    }
    public bool IsSmallerThan(Planet other)
    {
      return Size < other.Size;
    }
    public bool IsCloserThan(Planet other)
    {
      return DistanceFromSun < other.DistanceFromSun;
    }
    public int CompareTo(Planet? other)
    {
      if (other == null) return 1;

      return DistanceFromSun.CompareTo(other.DistanceFromSun);
    }
  }

  public enum IlluminationType
  {
    Full,
    Partial,
    None,
    [Description("None (Multiple Shadows)")]
    NoneMultipleShadows
  }
}
