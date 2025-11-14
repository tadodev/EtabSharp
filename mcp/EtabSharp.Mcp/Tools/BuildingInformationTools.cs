using ModelContextProtocol.Server;
using System.ComponentModel;

namespace EtabSharp.Mcp.Tools;

internal class BuildingInformationTools
{
    [McpServerTool]
    [Description("Get building information like stories, material, section used and loading types")]
    public int GetBuildingInformation()
    {
        return Random.Shared.Next(1, 100);
    }
}