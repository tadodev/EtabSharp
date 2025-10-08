namespace EtabSharp.Interface;

/// <summary>
/// Represents the main ETABS application interface
/// </summary>
public interface IEtabsApplication: IDisposable
{
    /// <summary>Gets the model instance</summary>
    IEtabsModel Model { get; }

    /// <summary>Gets whether the application is connected</summary>
    bool IsConnected { get; }

    /// <summary>Gets the ETABS version</summary>
    string Version { get; }

    /// <summary>Shows or hides the ETABS window</summary>
    void SetVisible(bool visible);

    /// <summary>Exits ETABS application</summary>
    void Exit(bool saveModel = false);
}