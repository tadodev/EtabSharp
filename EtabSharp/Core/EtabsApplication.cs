using ETABSv1;

namespace EtabSharp.Core;

/// <summary>
/// Entry point for ETABS API operations
/// </summary>
public class EtabsApplication: IDisposable
{
    public cSapModel Model { get; private set; }
    public bool IsConnected { get; private set; }

    public EtabsApplication()
    {
        
       
    }

    public void Dispose()
    {
       
    }
}