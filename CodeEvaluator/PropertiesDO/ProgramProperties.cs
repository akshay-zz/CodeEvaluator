using CodeEvaluator.Helper;

namespace CodeEvaluator.Property
{
	/// <summary>
	/// Summary description for CodeProperties
	/// </summary>
	public sealed class ProgramProperties
	{
		public string Namespacename { get; set; }
		public string Classname { get; set; }
		public string Functionname { get; set; }
		public bool IsStaticFunction { get; set; }
		public string Arguments { get; set; }
		public string AllProgramInputs { get; set; }
		public string CodeText { get; set; }
		public SupportedProgrammingLanguages.Languages Languages { get; set; }
	}
}