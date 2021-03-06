﻿using CodeEvaluator.Exception;
using CodeEvaluator.Helper;
using CodeEvaluator.Property;
using CodeEvaluator.Utils;
using System;
using System.Collections.Generic;

namespace CodeEvaluator
{
	public class CodeEvaluation
	{
		private ExecutedCodeProperties _executedCodeProperties = new ExecutedCodeProperties();
		private CompiledCodeProperties _compiledCodeProperties = new CompiledCodeProperties();
		private EvaluateTestCases _evaluateTestCases;

		public string InitiateCompileCode(string code, SupportedProgrammingLanguages.Languages language)
		{
			CompileCode(code, language);

			return !String.IsNullOrWhiteSpace(_compiledCodeProperties.ErrorMessage)
				? _compiledCodeProperties.ErrorMessage
				: String.Empty;
		}

		public EvaluateTestCases InitiateEvaluateCodeWithTestCases(
			string code,
			SupportedProgrammingLanguages.Languages language,
			Dictionary<int, string> allInput,
			Dictionary<int, string> expectedOutput)
		{

			code = !string.IsNullOrEmpty(code) ? code : throw new ArgumentNullException(nameof(code));
			allInput = allInput ?? throw new ArgumentNullException(nameof(allInput));
			expectedOutput = expectedOutput ?? throw new ArgumentNullException(nameof(expectedOutput));

			EvaluateCodeWithTestCases(code, language, allInput, expectedOutput);

			return _evaluateTestCases;
		}

		private void CompileCode(string code, SupportedProgrammingLanguages.Languages language)
		{
			CodeEvaluationInitiater codeEvaluation = new CodeEvaluationInitiater();

			_compiledCodeProperties = codeEvaluation.InitiateCompilation(GetProgramProperties(code, language));
		}

		private bool ExecuteCode(string code, CompiledCodeProperties compiledCodeProperties, string programInput,
			SupportedProgrammingLanguages.Languages language)
		{
			try
			{
				if(compiledCodeProperties.IsCompiledSuccessfully)
				{
					CodeEvaluationInitiater codeEvaluation = new CodeEvaluationInitiater();
					_executedCodeProperties = codeEvaluation.InitiateCodeExecution(
						GetProgramProperties(code, language, programInput),
						compiledCodeProperties
						);
					_executedCodeProperties.ExecutedSuccessfully = true;
				}
				else
				{
					_executedCodeProperties.CompiledCodeProp = compiledCodeProperties;
				}
			}
			catch(MissingProgramPropertiesValueException)
			{
				_executedCodeProperties.ExecutedSuccessfully = false;
				return false;
			}
			catch(System.Exception)
			{
				_executedCodeProperties.ExecutedSuccessfully = false;
				return false;
			}
			return true;
		}

		private void EvaluateCodeWithTestCases(string code, SupportedProgrammingLanguages.Languages language,
			Dictionary<int, string> allInput, Dictionary<int, string> expectedOutput)
		{
			Dictionary<int, string> actualOutput = new Dictionary<int, string>();

			if(String.IsNullOrWhiteSpace(InitiateCompileCode(code, language)))
			{
				foreach(var item in allInput)
				{
					ExecuteCode(code, _compiledCodeProperties, Convert.ToString(item.Value), language);
					if(_executedCodeProperties.ExecutedSuccessfully)
					{
						actualOutput.Add(Convert.ToInt32(item.Key), _executedCodeProperties.CodeOutput);
					}
				}
			}

			_evaluateTestCases = new EvaluateTestCases(actualOutput, expectedOutput);
		}


		private ProgramProperties GetProgramProperties(string code, SupportedProgrammingLanguages.Languages language)
		{
			switch(language)
			{
				case SupportedProgrammingLanguages.Languages.Csharp:
					return CsharpUtil.GetProgramPropertiesForCompilation(code);
				default:
					return CsharpUtil.GetProgramPropertiesForCompilation(code);
			}
		}

		private ProgramProperties GetProgramProperties(string code, SupportedProgrammingLanguages.Languages language,
			string allProgramInputs)
		{
			switch(language)
			{
				case SupportedProgrammingLanguages.Languages.Csharp:
					return CsharpUtil.GetProgramPropertiesForExecution(code, allProgramInputs);
				default:
					return CsharpUtil.GetProgramPropertiesForExecution(code, allProgramInputs);
			}
		}
	}
}
