using System;
using System.Reflection;

/// <summary>
/// Summary description for CodeEvaluationInitiater
/// </summary>
internal class CodeEvaluationInitiater
{
    private ExecutedCodeProperties _excecutedCodeProperties = new ExecutedCodeProperties();
    private CompiledCodeProperties _compiledCodeProperties = new CompiledCodeProperties();
    
    /// <summary>
    /// Compile code, return compilation status and return error message if any else return empty string
    /// </summary>
    public CompiledCodeProperties InitiateCompilation(ProgramProperties programProperties)
    {
        return Compile(programProperties);
    }

    private CompiledCodeProperties Compile(ProgramProperties programProperties)
    {
        switch (programProperties.Languages)
        {
            case SupportedProgrammingLanguages.Languages.Csharp:
                return CallCsharpCompiler(programProperties);
            default:
                return CallCsharpCompiler(programProperties);
        }
    }

    private ExecutedCodeProperties Execute(ProgramProperties programProperties)
    {
        switch (programProperties.Languages)
        {
            case SupportedProgrammingLanguages.Languages.Csharp:
                return CallCsharpExecuter(programProperties, _excecutedCodeProperties);
            default:
                return CallCsharpExecuter(programProperties, _excecutedCodeProperties);
        }
    }

    private CompiledCodeProperties CallCsharpCompiler(ProgramProperties programProperties)
    {
        CsharpCodeCompiler csharpCodeCompiler = new CsharpCodeCompiler(_compiledCodeProperties);
        CodeCompiler codeCompiler = new CodeCompiler(csharpCodeCompiler);

        return codeCompiler.Compile(programProperties.CodeText);
    }

    private ExecutedCodeProperties CallCsharpExecuter(ProgramProperties programProperties,
		ExecutedCodeProperties executedCodeProperties)
    {
        CsharpCodeExecuter csharpCodeExecuter = new CsharpCodeExecuter(programProperties, _excecutedCodeProperties);
        CodeExecuter codeExecuter = new CodeExecuter(csharpCodeExecuter);
        return codeExecuter.Execute();
    }

    /// <summary>
    /// Compile & execute code, return compilation status, return error message for failure and result after successfull execution
    /// </summary>
    public ExecutedCodeProperties InitiateCodeExecution(ProgramProperties programProperties,
		CompiledCodeProperties compiledCodeProperties)
    {
        if (compiledCodeProperties == null)
        {
            throw new NullReferenceException("ExecutedCodeProperties can't be null");
        }

        if (!compiledCodeProperties.IsCompiledSuccessfully || compiledCodeProperties.SuccessfulluyCompiledObject == null)
        {
            throw new Exception("Ensure that code has been already compiled successfully.");
        }
        _excecutedCodeProperties.CompiledCodeProp = compiledCodeProperties;
        return Execute(programProperties);
    }
}