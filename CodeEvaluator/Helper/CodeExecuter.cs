using CodeEvaluator.Interface;

namespace CodeEvaluator.Helper
{
	/// <summary>
	/// Summary description for CodeExecuter
	/// </summary>
	internal class CodeExecuter
	{
		private readonly ICodeExecuter _codeExecuter;
		public CodeExecuter(ICodeExecuter codeExecuter)
		{
			_codeExecuter = codeExecuter;
		}

		public ExecutedCodeProperties Execute()
		{
			return _codeExecuter.ExecuteCode();
		}
	}
}