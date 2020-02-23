using System;
using System.Data;
/// <summary>
/// Summary description for SupportedProgrammingLanguages
/// </summary>
public static class SupportedProgrammingLanguages
{
    // There is no master table in database for languages
    public enum Languages
    {
        Csharp = 1
        // C,
    }

    public const string LangTableName = "CodeLangTable";
    public const string LangTableIdColumnName = "Id";
    public const string LangTableLangColumnName = "LanguageName";

    public static DataTable GetDataTableOfSupportedLang()
    {
        return CreateDataTableOfSupportedLang();
    }

    private static DataTable CreateDataTableOfSupportedLang()
    {
        using (DataTable codeLangTable = new DataTable(LangTableName))
        {
            codeLangTable.Columns.Add(LangTableIdColumnName, typeof(int));
            codeLangTable.Columns.Add(LangTableLangColumnName, typeof(String));

            foreach (Languages lang in Enum.GetValues(typeof(Languages)))
            {
                DataRow codeLangRow = codeLangTable.NewRow();
                codeLangRow[LangTableIdColumnName] = Convert.ToInt32(lang);
                codeLangRow[LangTableLangColumnName] = Convert.ToString(lang);
                codeLangTable.Rows.Add(codeLangRow);
            }
            return codeLangTable;
        }
    }

}