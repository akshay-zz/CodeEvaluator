using System;

/// <summary>
/// Summary description for MissingProgramPropertiesValueException
/// </summary>

[Serializable]
public class MissingProgramPropertiesValueException : Exception
{
    public MissingProgramPropertiesValueException()
       : base("Missing any ProgramProperties value")
    {

    }

    public MissingProgramPropertiesValueException(string propertyName)
       : base(String.Format("Missing ProgramProperties value for {0}", propertyName))
    {

    }
}