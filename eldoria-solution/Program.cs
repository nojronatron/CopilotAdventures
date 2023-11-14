using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/// <summary>
/// The main class of the Eldoria solution.
/// </summary>
class Program
{
  static readonly HttpClient client = new HttpClient();
  static readonly Regex secretPattern = new Regex(@"\{\*(.*?)\*\}");
  static async Task Main(string[] args)
  {
    string url = "https://raw.githubusercontent.com/microsoft/CopilotAdventures/main/Data/scrolls.txt";
    string data = await FetchDataFromUrl(url);
    List<string> secrets = ExtractSecrets(data);
    PrintSecrets(secrets);
  }

  /// <summary>
  /// Fetches data from the specified URL using an HTTP GET request.
  /// </summary>
  /// <param name="url">The URL to fetch data from.</param>
  /// <returns>A string containing the data fetched from the URL.</returns>
  static async Task<string> FetchDataFromUrl(string url)
  {
    try
    {
      return await client.GetStringAsync(url);
    }
    catch (HttpRequestException e)
    {
      Console.WriteLine("\nException Caught!");
      Console.WriteLine("Message :{0} ", e.Message);
      return string.Empty;
    }
  }

  /// <summary>
  /// Extracts secrets from the given data using the secretPattern regular expression.
  /// </summary>
  /// <param name="data">The data to extract secrets from.</param>
  /// <returns>A list of secrets.</returns>
  static List<string> ExtractSecrets(string data)
  {
    MatchCollection matches = secretPattern.Matches(data);
    List<string> secrets = new List<string>();

    foreach (Match match in matches)
    {
      secrets.Add(match.Groups[1].Value);
    }

    return secrets;
  }

  /// <summary>
  /// Prints a list of secrets to the console.
  /// </summary>
  /// <param name="secrets">The list of secrets to print.</param>
  static void PrintSecrets(List<string> secrets)
  {
    if (secrets.Count == 0)
    {
      Console.WriteLine("No secrets found.");
      return;
    }

    foreach (string secret in secrets)
    {
      Console.WriteLine(secret);
    }
  }
}
