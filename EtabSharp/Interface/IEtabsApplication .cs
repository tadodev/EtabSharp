namespace EtabSharp.Interface;

/// <summary>
/// Represents the main ETABS application interface
/// </summary>
public interface IEtabsApplication: IDisposable
{
    IEtabsModel Model { get; }
    bool IsConnected { get; }
    string Version { get; }
    void SetVisible(bool visible);
    void Exit(bool saveModel = false);
}