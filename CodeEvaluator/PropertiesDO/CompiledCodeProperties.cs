namespace CodeEvaluator.Property
{
	/// <summary>
	/// Summary description for CompiledCodeProperties
	/// </summary>
	public class CompiledCodeProperties
	{
		public bool IsCompiledSuccessfully { get; set; }
		public object SuccessfullyCompiledObject { get; set; }
		public string ErrorMessage { get; set; }
	}
}