using System;
using CodeEvaluator;

/// <summary>
/// Summary description for CsharpUtil
/// </summary>
public class CsharpUtil
{
    public static ProgramProperties GetProgramPropertiesForCompilation(string codeText)
    {
        ProgramProperties programProperties = new ProgramProperties();
        programProperties.CodeText = codeText;
        programProperties.Languages = SupportedProgrammingLanguages.Languages.Csharp;

        return programProperties;
    }

    public static ProgramProperties GetProgramPropertiesForExecution(string codeText, string allProgramInputs)
    {
        ProgramProperties programProperties = new ProgramProperties();
        programProperties.CodeText = codeText;
        programProperties.Languages = SupportedProgrammingLanguages.Languages.Csharp;
        programProperties.Classname = FindNextWord(codeText, "class");
        programProperties.Functionname = "Main";
        programProperties.Namespacename = FindNextWord(codeText, "namespace");
        programProperties.Arguments = "";
        programProperties.AllProgramInputs = allProgramInputs;
        programProperties.IsStaticFunction = true;

        return programProperties;
    }

    private static string FindNextWord(string codeText, string word)
    {
        string nextWord = String.Empty;
        try
        {
            nextWord = ClsCommonUtil.FindNextWordFromAfterAParticularWordFromCode(codeText, word);
        }
        catch
        {
            DetectionExceptionForClassAndNamespace();
        }

        if (nextWord == String.Empty)
        {
            DetectionExceptionForClassAndNamespace();
        }

        return nextWord;
    }

    private static void DetectionExceptionForClassAndNamespace()
    {
        throw new Exception("Unable to detect class name or namespace");
    }
}