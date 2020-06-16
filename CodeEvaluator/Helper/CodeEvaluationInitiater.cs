using CodeEvaluator.Interface.Csharp;
using CodeEvaluator.Property;
using System;

namespace CodeEvaluator.Helper
{

	/// <summary>
	/// Summary description for CodeEvaluationInitiater
	/// </summary>
	internal class CodeEvaluationInitiater
	{
		private readonly ExecutedCodeProperties _excecutedCodeProperties = new ExecutedCodeProperties();
		private readonly CompiledCodeProperties _compiledCodeProperties = new CompiledCodeProperties();

		/// <summary>
		/// Compile code, return compilation status and return error message if any else return empty string
		/// </summary>
		public CompiledCodeProperties InitiateCompilation(ProgramProperties programProperties)
		{
			if(programProperties is null)
			{
				throw new ArgumentNullException(nameof(programProperties));
			}

			return Compile(programProperties);
		}

		private CompiledCodeProperties Compile(ProgramProperties programProperties)
		{
			switch(programProperties.Languages)
			{
				case SupportedProgrammingLanguages.Languages.Csharp:
					return CallCsharpCompiler(programProperties);
				default:
					return CallCsharpCompiler(programProperties);
			}
		}

		private ExecutedCodeProperties Execute(ProgramProperties programProperties)
		{
			switch(programProperties.Languages)
			{
				case SupportedProgrammingLanguages.Languages.Csharp:
					return CallCsharpExecuter(programProperties);
				default:
					return CallCsharpExecuter(programProperties);
			}
		}

		private CompiledCodeProperties CallCsharpCompiler(ProgramProperties programProperties)
		{
			CsharpCodeCompiler csharpCodeCompiler = new CsharpCodeCompiler(_compiledCodeProperties);
			CodeCompiler codeCompiler = new CodeCompiler(csharpCodeCompiler);

			return codeCompiler.Compile(programProperties.CodeText);
		}

		private ExecutedCodeProperties CallCsharpExecuter(ProgramProperties programProperties)
		{
			CsharpCodeExecuter csharpCodeExecuter = new CsharpCodeExecuter(programProperties, _excecutedCodeProperties);
			CodeExecuter codeExecuter = new CodeExecuter(csharpCodeExecuter);
			return codeExecuter.Execute();
		}

		/// <summary>
		/// Compile & execute code, return compilation status, return error message for failure and result after successful execution
		/// </summary>
		public ExecutedCodeProperties InitiateCodeExecution(
			ProgramProperties programProperties,
			CompiledCodeProperties compiledCodeProperties)
		{
			if(compiledCodeProperties is null)
			{
				throw new NullReferenceException("ExecutedCodeProperties can't be null");
			}

			if(!compiledCodeProperties.IsCompiledSuccessfully || compiledCodeProperties.SuccessfullyCompiledObject is null)
			{
				throw new System.Exception("Ensure that code has been already compiled successfully.");
			}

			_excecutedCodeProperties.CompiledCodeProp = compiledCodeProperties;

			return Execute(programProperties);
		}
	}
}