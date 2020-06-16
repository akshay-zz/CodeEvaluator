using CodeEvaluator.Property;
using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Text;

namespace CodeEvaluator.Interface.Csharp
{

	/// <summary>
	/// Summary description for CsharpCodeCompiler
	/// </summary>
	public sealed class CsharpCodeCompiler : ICodeCompiler
	{
		private const string SystemCoreLibrary = "System.Core.dll";
		private const string SystemDataLibrary = "System.Data.dll";

		private Assembly _assembly;
		private readonly CompiledCodeProperties _compiledCodeProperties;
		private readonly StringBuilder _errorMessage = new StringBuilder();

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

			if(_compiledCodeProperties.IsCompiledSuccessfully)
			{
				_compiledCodeProperties.SuccessfullyCompiledObject = _assembly;
				_compiledCodeProperties.ErrorMessage = "";
			}
			else
			{
				_compiledCodeProperties.SuccessfullyCompiledObject = null;
				_compiledCodeProperties.ErrorMessage = _errorMessage.ToString();
			}

			return _compiledCodeProperties;
		}

		private bool ProcessCompilation(CodeDomProvider codeDomProvider, CompilerParameters compilerParameters, string code)
		{
			code = FormatMainMethodPramSignature(code);
			CompilerResults results = codeDomProvider.CompileAssemblyFromSource(compilerParameters, code.Trim());

			if(results.Errors.HasErrors)
			{
				_errorMessage.Clear();

				foreach(CompilerError CompErr in results.Errors)
				{
					_errorMessage.Append(
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

		// TODO: Need to implement to handle main method with no argument 
		private string FormatMainMethodPramSignature(string Code)
		{
			return System.Text.RegularExpressions.Regex.Replace(
				Code, "(Main).*[)]", "Main(string [] args)", System.Text.RegularExpressions.RegexOptions.IgnoreCase
			);
		}
	}
}