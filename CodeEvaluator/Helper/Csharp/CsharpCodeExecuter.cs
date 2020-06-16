using CodeEvaluator.Exception;
using CodeEvaluator.Property;
using System;
using System.IO;
using System.Reflection;

namespace CodeEvaluator.Interface.Csharp
{

	/// <summary>
	/// Summary description for CodeExecuter
	/// </summary>
	public sealed class CsharpCodeExecuter : ICodeExecuter
	{
		private readonly Assembly _assembly;
		private readonly ProgramProperties _programProperties;
		private readonly ExecutedCodeProperties _excecutedCodeProperties;

		private delegate void MainMethodInvoker(string[] args);


		public CsharpCodeExecuter(ProgramProperties programProperties, ExecutedCodeProperties excecutedCodeProperties)
		{
			if(excecutedCodeProperties.CompiledCodeProp.IsCompiledSuccessfully == false ||
				excecutedCodeProperties.CompiledCodeProp.SuccessfullyCompiledObject == null)
			{
				throw new System.Exception("Can't execute a unsuccessful compiled assembly or a null assembly object.");
			}
			_assembly = excecutedCodeProperties.CompiledCodeProp.SuccessfullyCompiledObject as Assembly;
			_excecutedCodeProperties = excecutedCodeProperties;
			_programProperties = programProperties;
		}

		public ExecutedCodeProperties ExecuteCode()
		{
			ValidateProgramProperties();
			return ProcessExecution();
		}

		private void ValidateProgramProperties()
		{
			if(string.IsNullOrEmpty(_programProperties.Namespacename))
				throw new MissingProgramPropertiesValueException(nameof(_programProperties.Namespacename));

			if(string.IsNullOrEmpty(_programProperties.Classname))
				throw new MissingProgramPropertiesValueException(nameof(_programProperties.Classname));

			if(string.IsNullOrEmpty(_programProperties.Functionname))
				throw new MissingProgramPropertiesValueException(nameof(_programProperties.Functionname));
		}

		private Type GetTypeFromAssembly()
		{
			Type type;

			if(_programProperties.IsStaticFunction)
			{
				type = _assembly.GetType(_programProperties.Namespacename + "." + _programProperties.Classname);
			}
			else
			{
				object instance = _assembly.CreateInstance(_programProperties.Namespacename + "." + _programProperties.Classname);
				type = instance.GetType();
			}

			return type;
		}

		private string FormatProgramInputString()
		{
			return _programProperties.AllProgramInputs.Replace("\n", Environment.NewLine);
		}
		private ExecutedCodeProperties ProcessExecution()
		{
			using(var sr = new StringReader(_programProperties.AllProgramInputs)) // Put all the inputs for the program in one go
			{
				using(StringWriter stringWriter = new StringWriter())
				{
					// To put value in the console
					Console.SetIn(sr);
					// To read the console screen text
					Console.SetOut(stringWriter);
					// Execute the method 
					MethodInfo methodInfo = GetTypeFromAssembly().GetMethod(_programProperties.Functionname, BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);
					//var watch = new System.Diagnostics.Stopwatch();
					//watch.Start();
					MainMethodInvoker mainMethodInvoker = (MainMethodInvoker)methodInfo.CreateDelegate(typeof(MainMethodInvoker));
					mainMethodInvoker.Invoke(new string[] { _programProperties.Arguments });
					//watch.Stop();
					// Convert.ToString(watch.Elapsed.TotalMilliseconds) + " ms";

					// Retrieve console values
					_excecutedCodeProperties.CodeOutput = stringWriter.ToString();
				}
			}
			return _excecutedCodeProperties;
		}
	}
}