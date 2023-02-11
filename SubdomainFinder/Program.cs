using System;
using System.Net;
using System.Collections.Generic;

class SubdomainFinder
{
    static void Main(string[] args)
    {
        Console.WriteLine("________________________________________");
        Console.WriteLine("SubdomainFinder V1");
        Console.WriteLine("Created By Ali kareem ");
        Console.WriteLine("fb.com/Ali.KareemP");
        Console.WriteLine("________________________________________");

         
        Console.WriteLine("Enter the Main Dowmin example : site.com");
        //Read Main Domain
        string targetDomain = Console.ReadLine();

        Console.WriteLine("Enter Wordlist Path");
        //Read Wordlist Path
        string wordlistPath = Console.ReadLine(); 

        List<string> subdomains = GetSubdomains(targetDomain, wordlistPath);

        Console.WriteLine("Found subdomains:");
        foreach (string subdomain in subdomains)
        {
            Console.WriteLine(subdomain);
        }
    }

    static List<string> GetSubdomains(string targetDomain, string wordlistPath)
    {
        List<string> subdomains = new List<string>();

        // Load the wordlist from a file
        string[] words = System.IO.File.ReadAllLines(wordlistPath);

        // Check each word in the wordlist as a subdomain
        foreach (string word in words)
        {
            string url = word + "." + targetDomain;
            if (IsValidSubdomain(url))
            {
                subdomains.Add(url);
            }
        }

        return subdomains;
    }

    static bool IsValidSubdomain(string subdomain)
    {
        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://" + subdomain);
            request.Timeout = 4000;
            request.Method = "HEAD";

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                return response.StatusCode == HttpStatusCode.OK;
            }
        }
        catch (WebException)
        {
            return false;
        }
    }
}
