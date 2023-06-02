using System;
using System.IO;
using DotNetEnv;

public class Program
{
    public static void Main()
    {
        Env.Load(); // Loads the environment variables from the .env file

        string apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
        Console.WriteLine(apiKey);
    }
}
