using System;

namespace CodeEvaluator.Exception
{

	/// <summary>
	/// Summary description for MissingProgramPropertiesValueException
	/// </summary>

	[Serializable]
	public class MissingProgramPropertiesValueException : System.Exception
	{
		public MissingProgramPropertiesValueException()
		   : base("Missing any ProgramProperties value")
		{

		}

		public MissingProgramPropertiesValueException(string propertyName)
		   : base(string.Format("Missing ProgramProperties value for {0}", propertyName))
		{

		}
	}
}