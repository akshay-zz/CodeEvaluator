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
            if (!String.IsNullOrWhiteSpace(_compiledCodeProperties.ErrorMessage))
            {
                return _compiledCodeProperties.ErrorMessage;
            }
            return String.Empty;
        }

		public EvaluateTestCases InitiateEvaluateCodeWithTestCases(
			string code, SupportedProgrammingLanguages.Languages language, Dictionary<int, string> AllInput,
			Dictionary<int, string> ExpectedOutput
		)
        {
            EvaluateCodeWithTestCases(code, language, AllInput, ExpectedOutput);
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
                if (compiledCodeProperties.IsCompiledSuccessfully)
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
            catch (MissingProgramPropertiesValueException ex)
            {
                _executedCodeProperties.ExecutedSuccessfully = false;
                return false;
            }
            catch (Exception ex)
            {
                _executedCodeProperties.ExecutedSuccessfully = false;
                return false;
            }
            return true;
        }

        private void EvaluateCodeWithTestCases(string code, SupportedProgrammingLanguages.Languages language, Dictionary<int, string> allInput, Dictionary<int, string> expectedOutput)
        {
            Dictionary<int, string> ActualOutput = new Dictionary<int, string>();
            if (String.IsNullOrWhiteSpace(InitiateCompileCode(code, language)))
            {
                foreach (var item in allInput)
                {
                    ExecuteCode(code, _compiledCodeProperties, Convert.ToString(item.Value), language);
                    if (_executedCodeProperties.ExecutedSuccessfully)
                    {
                        ActualOutput.Add(Convert.ToInt32(item.Key), _executedCodeProperties.CodeOutput);
                    }
                }
            }

            _evaluateTestCases = new EvaluateTestCases(ActualOutput, expectedOutput);
        }
	

        private ProgramProperties GetProgramProperties(string code, SupportedProgrammingLanguages.Languages language)
        {
            switch (language)
            {
                case SupportedProgrammingLanguages.Languages.Csharp:
                    return CsharpUtil.GetProgramPropertiesForCompilation(code);
                default:
                    return CsharpUtil.GetProgramPropertiesForCompilation(code);
            }
        }

        private ProgramProperties GetProgramProperties(string code, SupportedProgrammingLanguages.Languages language, string AllProgramInputs)
        {
            switch (language)
            {
                case SupportedProgrammingLanguages.Languages.Csharp:
                    return CsharpUtil.GetProgramPropertiesForExecution(code, AllProgramInputs);
                default:
                    return CsharpUtil.GetProgramPropertiesForExecution(code, AllProgramInputs);
            }
        }
    }
}
