using System.Collections.Generic;

/// <summary>
/// Evaluate TestCases from ExpectedOutput dictionary(int TestCaseId, string Expected Output) and ActualOutput dictionary(int TestCaseId, string Actual Output)
/// and return a result dictionary(int TestCaseId, bool result)
/// </summary>
public class EvaluateTestCases
{
	private Dictionary<int, bool> _ResultDictonary = new Dictionary<int, bool>();
	private Dictionary<int, int> _TimeTakenDictonary = new Dictionary<int, int>();
	private Dictionary<int, int> _MemoryCosumedDictonary = new Dictionary<int, int>();

	private bool _AllTestcasesRanSuccessFully = true;

	public bool AllTestcasesRanSuccessFully
	{
		get
		{
			return _AllTestcasesRanSuccessFully;
		}
	}

	public Dictionary<int, bool> ResultDictonary
	{
		get
		{
			return _ResultDictonary;
		}
	}

	public Dictionary<int, int> TimeTakenDictonary
	{
		get
		{
			return _TimeTakenDictonary;
		}
	}

	public Dictionary<int, int> MemoryCosumedDictonary
	{
		get
		{
			return _MemoryCosumedDictonary;
		}
	}


	public EvaluateTestCases(Dictionary<int, string> expectedOutput, Dictionary<int, string> actualOutput)
	{
		_AllTestcasesRanSuccessFully = true;
		MatchInputAndOutput(expectedOutput, actualOutput);
	}

	private void MatchInputAndOutput(Dictionary<int, string> expectedOutput, Dictionary<int, string> actualOutput)
	{
		if (expectedOutput.Count < 1)
		{
			_AllTestcasesRanSuccessFully = false;
			return;
		}

		foreach (var item in expectedOutput)
		{
			string value;
			if (actualOutput.TryGetValue(item.Key, out value) && ReplaceCarriageReturnWithNewline(item.Value) == ReplaceCarriageReturnWithNewline(actualOutput[item.Key]))
			{
				_ResultDictonary.Add(item.Key, true);
				_MemoryCosumedDictonary.Add(item.Key, 0);
				_TimeTakenDictonary.Add(item.Key, 0);
			}
			else
			{
				_AllTestcasesRanSuccessFully = false;
				_ResultDictonary.Add(item.Key, false);
				_MemoryCosumedDictonary.Add(item.Key, 0);
				_TimeTakenDictonary.Add(item.Key, 0);
			}
		}
	}

	private string ReplaceCarriageReturnWithNewline(string str)
	{
		return (str.Replace("\r", string.Empty)).Trim('\n');
	}
}