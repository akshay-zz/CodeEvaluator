using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Text;

/// <summary>
/// Summary description for CsharpCodeCompiler
/// </summary>
public sealed class CsharpCodeCompiler : ICodeCompiler
{
    private Assembly _assembly;
    private CompiledCodeProperties _compiledCodeProperties;
    private const string SystemCoreLibrary = "System.Core.dll";
    private const string SystemDataLibrary = "System.Data.dll";
    StringBuilder ErrorMessage = new StringBuilder();

    public CsharpCodeCompiler(CompiledCodeProperties compiledCodeProperties)
    {
        _compiledCodeProperties = compiledCodeProperties;
    }

    private CompilerParameters GetCompilerParameters(string[] libraryList)
    {
        CompilerParameters compilerParameters = new CompilerParameters();
        // True - memory generation, false - external file generation
        compilerParameters.GenerateInMemory = true;
        // True - exe file generation, false - dll file generation
        compilerParameters.GenerateExecutable = false;

        compilerParameters.ReferencedAssemblies.AddRange(libraryList);

        return compilerParameters;
    }

    private CodeDomProvider GetCodeProvider()
    {
        CodeDomProvider codeProvider = CodeDomProvider.CreateProvider("CSharp");
        return codeProvider;
    }

    /// <summary>
    /// Compiles the code and return Assembly object on a successful compilation else returns a string error message.
    /// </summary>
    public CompiledCodeProperties CompileCode(string codeText)
    {
        string[] library = { SystemCoreLibrary, SystemDataLibrary }; // CRAP CODE, NEED TO BE CLEANED
        _compiledCodeProperties.IsCompiledSuccessfully = ProcessCompilation(GetCodeProvider(), GetCompilerParameters(library), codeText);

        if (_compiledCodeProperties.IsCompiledSuccessfully)
        {
            _compiledCodeProperties.SuccessfulluyCompiledObject = _assembly;
            _compiledCodeProperties.ErrorMessage = "";
        }
        else
        {
            _compiledCodeProperties.SuccessfulluyCompiledObject = null;
            _compiledCodeProperties.ErrorMessage = ErrorMessage.ToString();
        }

        return _compiledCodeProperties;
    }

    private bool ProcessCompilation(CodeDomProvider codeDomProvider, CompilerParameters compilerParameters, String Code)
    {
        Code = FormatMainMethodPramSignature(Code);
        CompilerResults results = codeDomProvider.CompileAssemblyFromSource(compilerParameters, Code.Trim());
        if (results.Errors.HasErrors)
        {
            ErrorMessage.Clear();
            foreach (CompilerError CompErr in results.Errors)
            {
                ErrorMessage.Append(
                            "Line number " + CompErr.Line +
                            ", Error Number: " + CompErr.ErrorNumber +
                            ", '" + CompErr.ErrorText + ";" +
                            Environment.NewLine + Environment.NewLine
                            );
            }

            return false;
        }
        else
        {
            // Successful Compile
            _assembly = results.CompiledAssembly;
        }

        return true;
    }

    // TODO: Need to implement to handle main mehtod with no argument 
    private string FormatMainMethodPramSignature(string Code)
    {
        return System.Text.RegularExpressions.Regex.Replace(Code, "(Main).*[)]", "Main(string [] args)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
    }
}