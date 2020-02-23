/// <summary>
/// Summary description for CodeCompiler
/// </summary>
internal class CodeCompiler
{
    private readonly ICodeCompiler _codeCompiler;
    public CodeCompiler(ICodeCompiler codeCompiler)
    {
        _codeCompiler = codeCompiler;
    }

    /// <summary>
    /// Compiles the code and return object on successfull compilation else return string error message.
    /// </summary>
    public CompiledCodeProperties Compile(string code)
    {
        return _codeCompiler.CompileCode(code);
    }
}