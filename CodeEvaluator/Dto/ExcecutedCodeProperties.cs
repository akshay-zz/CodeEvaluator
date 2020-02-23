/// <summary>
/// Summary description for ExcecutedCodeProperties
/// </summary>
public sealed class ExecutedCodeProperties
{
    //TODO: Need to write properties 
    public ExecutedCodeProperties()
    {
    }

    public CompiledCodeProperties CompiledCodeProp { get; set; }

    public string CodeOutput { get; set; }
    public bool ExecutedSuccessfully { get; set; }
    public string ErrorMessage { get; set; }
    //public int TimeTakenInNanoSec { get; set; }
    public int TimeTakenInSecond { get { return 0; } }
    //public int TimeTakenInMin { get; set; }
    //public int MemoryConsumedInMb { get; set; }
    public int MemoryConsumedInKb { get { return 0; } }
}

