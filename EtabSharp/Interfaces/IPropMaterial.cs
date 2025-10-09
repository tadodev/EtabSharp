using EtabSharp.Models.Materials;
using ETABSv1;

namespace EtabSharp.Interfaces;


/// <summary>
/// Holder for material property related methods in ETABS
/// </summary>
public interface IPropMaterial
{
    /// <summary>
    /// Retrieves the names of all defined material properties of the specified type. 
    /// </summary>
    /// <param name="MatType"/>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<string[]> GetNameList(eMatType MatType = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new material property to the model based on the Code-specified and other pre-defined material properties defined in the installed file "CSiMaterialLibrary*.xml" 
    /// located in subfolder "Property Libraries" under the program installation folder.
    /// </summary>
    /// <param name="matType"></param>
    /// <param name="Region"></param>
    /// <param name="standard"></param>
    /// <param name="grade"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<string> AddDefaultMaterial(eMatType matType, string Region, string standard, string grade, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new concrete material to the model with the specified properties.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="fpc"></param>
    /// <param name="IsLightweight"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PropConcrete> AddConcreteMaterial(string name, double fpc, double Ec, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new rebar material to the model with the specified properties.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="fyp"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PropRebar> AddRebarMaterial(string name, double fyp, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new steel material to the model with the specified properties.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="fy"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PropSteel> AddSteelMaterial(string name, double fy, CancellationToken cancellationToken = default);
}
