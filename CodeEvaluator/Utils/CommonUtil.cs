using System;
using System.Text.RegularExpressions;

namespace CodeEvaluator.Utils
{
	internal class CommonUtil
	{
		public static string FindNextWordFromAfterAParticularWordFromCode(string codeText, string word)
		{
			string OutString = String.Empty;

			codeText = RemoveMultipleSpaces(codeText);
			string[] SplitByCode = codeText.Split(new string[] { word }, StringSplitOptions.RemoveEmptyEntries);

			if(SplitByCode.Length > 1)
			{
				OutString = (SplitByCode[1].Trim()).Split(' ')[0];
			}
			else if(SplitByCode.Length == 1)
			{
				OutString = (SplitByCode[0].Trim()).Split(' ')[0];
			}

			return OutString;
		}

		public static string RemoveMultipleSpaces(string inputValue)
		{
			return Regex.Replace(inputValue, @"\s+", " ");
		}
	}
}
