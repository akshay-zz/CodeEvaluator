/// <summary>
/// Provides a basic code template for programming languages
/// </summary>
public class BasicCodeTemplate
{
    public static string Csharp
    {
        get
        {
            return CsharpBasicTemplate();
        }
    }

    private static string CsharpBasicTemplate()
    {
        return @"
using System;

namespace Solution
{
  
    class Program
    {
        public static void Main(string [] args)
        {
            // Write your code here
        }
    }
}
";
    }
}