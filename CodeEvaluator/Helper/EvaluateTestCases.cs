using System.Collections.Generic;

namespace CodeEvaluator.Helper
{

	/// <summary>
	/// Evaluate TestCases from ExpectedOutput dictionary(int TestCaseId, string Expected Output) and ActualOutput dictionary(int TestCaseId, string Actual Output)
	/// and return a result dictionary(int TestCaseId, bool result)
	/// </summary>
	public class EvaluateTestCases
	{
		public bool AllTestcasesRanSuccessFully { get; private set; } = true;
		public Dictionary<int, bool> ResultDictonary { get; } = new Dictionary<int, bool>();
		public Dictionary<int, int> TimeTakenDictonary { get; } = new Dictionary<int, int>();
		public Dictionary<int, int> MemoryCosumedDictonary { get; } = new Dictionary<int, int>();


		public EvaluateTestCases(Dictionary<int, string> expectedOutput, Dictionary<int, string> actualOutput)
		{
			AllTestcasesRanSuccessFully = true;
			MatchInputAndOutput(expectedOutput, actualOutput);
		}

		private void MatchInputAndOutput(Dictionary<int, string> expectedOutput, Dictionary<int, string> actualOutput)
		{
			if(expectedOutput.Count < 1)
			{
				AllTestcasesRanSuccessFully = false;
				return;
			}

			foreach(var item in expectedOutput)
			{
				if(actualOutput.TryGetValue(item.Key, out _) &&
					ReplaceCarriageReturnWithNewline(item.Value) == ReplaceCarriageReturnWithNewline(actualOutput[item.Key])
				)
				{
					ResultDictonary.Add(item.Key, true);
					MemoryCosumedDictonary.Add(item.Key, 0);
					TimeTakenDictonary.Add(item.Key, 0);
				}
				else
				{
					AllTestcasesRanSuccessFully = false;
					ResultDictonary.Add(item.Key, false);
					MemoryCosumedDictonary.Add(item.Key, 0);
					TimeTakenDictonary.Add(item.Key, 0);
				}
			}
		}

		private string ReplaceCarriageReturnWithNewline(string str)
		{
			return str.Replace("\r", string.Empty).Trim('\n');
		}
	}
}