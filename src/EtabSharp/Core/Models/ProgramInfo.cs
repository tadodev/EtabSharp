namespace EtabSharp.Core.Models;

public class ProgramInfo
{
    public string ProgramName { get; set; }
    public string ProgramVersion { get; set; }
    public string ProgramLevel { get; set; }

    public override string ToString()
    {
        return $"Program Name: {ProgramName}, Version: {ProgramVersion}, Level: {ProgramLevel}";
    }
}