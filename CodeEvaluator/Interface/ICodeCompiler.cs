/// <summary>
/// Summary description for ICodeCompiler
/// </summary>
public interface ICodeCompiler
{
    /// <summary>
    /// Compiles the code and return Assembly object on successfull compilation else return string error message.
    /// </summary>
    CompiledCodeProperties CompileCode(string codeText);
}