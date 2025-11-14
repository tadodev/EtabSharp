using EtabSharp.Properties.Materials.Models;
using ETABSv1;

namespace EtabSharp.Interfaces.Properties;

/// <summary>
/// Comprehensive interface for managing material properties in ETABS.
/// Covers all functionality from cPropMaterial API including creation, modification,
/// and retrieval of materials with full property support.
/// </summary>
public interface IPropMaterial
{
    #region Material Management

    /// <summary>
    /// Adds a material from ETABS library by region, standard, and grade.
    /// Wraps cSapModel.PropMaterial.AddMaterial.
    /// </summary>
    /// <param name="name">Material name (will be modified by ETABS if needed)</param>
    /// <param name="matType">Material type</param>
    /// <param name="region">Region (e.g., "US", "China", "Europe")</param>
    /// <param name="standard">Standard (e.g., "ACI", "AISC", "EN")</param>
    /// <param name="grade">Grade (e.g., "A992Fy50", "4000Psi", "C30")</param>
    /// <param name="userName">Optional user-defined name</param>
    /// <returns>Actual material name created (may differ from input)</returns>
    string AddMaterialFromLibrary(ref string name, eMatType matType, string region,
        string standard, string grade, string userName = "");

    /// <summary>
    /// Changes the name of an existing material.
    /// Wraps cSapModel.PropMaterial.ChangeName.
    /// </summary>
    /// <param name="currentName">Current material name</param>
    /// <param name="newName">New material name</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int ChangeName(string currentName, string newName);

    /// <summary>
    /// Gets the count of materials in the model.
    /// Wraps cSapModel.PropMaterial.Count.
    /// </summary>
    /// <param name="matType">Material type filter (0 for all)</param>
    /// <returns>Number of materials</returns>
    int Count(eMatType matType = 0);

    /// <summary>
    /// Deletes a material from the model.
    /// Wraps cSapModel.PropMaterial.Delete.
    /// </summary>
    /// <param name="name">Material name to delete</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int Delete(string name);

    /// <summary>
    /// Gets the names of all materials in the model.
    /// Wraps cSapModel.PropMaterial.GetNameList.
    /// </summary>
    /// <param name="matType">Material type filter (0 for all)</param>
    /// <returns>Array of material names</returns>
    string[] GetNameList(eMatType matType = 0);

    /// <summary>
    /// Gets basic material information.
    /// Wraps cSapModel.PropMaterial.GetMaterial.
    /// </summary>
    /// <param name="name">Material name</param>
    /// <returns>Tuple of (MatType, Color, Notes, GUID)</returns>
    (eMatType MatType, int Color, string Notes, string GUID) GetMaterial(string name);

    /// <summary>
    /// Sets basic material information.
    /// Wraps cSapModel.PropMaterial.SetMaterial.
    /// </summary>
    /// <param name="name">Material name</param>
    /// <param name="matType">Material type</param>
    /// <param name="color">Display color (-1 for auto)</param>
    /// <param name="notes">Material notes</param>
    /// <param name="guid">Material GUID</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetMaterial(string name, eMatType matType, int color = -1,
        string notes = "", string guid = "");

    /// <summary>
    /// Gets material type information for API compatibility.
    /// Wraps cSapModel.PropMaterial.GetTypeOAPI.
    /// </summary>
    /// <param name="name">Material name</param>
    /// <returns>Tuple of (MatType, SymType)</returns>
    (eMatType MatType, int SymType) GetTypeOAPI(string name);

    #endregion

    #region Isotropic Properties

    /// <summary>
    /// Gets isotropic mechanical properties.
    /// Wraps cSapModel.PropMaterial.GetMPIsotropic.
    /// </summary>
    /// <param name="name">Material name</param>
    /// <param name="temp">Temperature for property retrieval</param>
    /// <returns>IsotropicProperties object</returns>
    IsotropicProperties GetMPIsotropic(string name, double temp = 0.0);

    /// <summary>
    /// Sets isotropic mechanical properties.
    /// Wraps cSapModel.PropMaterial.SetMPIsotropic.
    /// </summary>
    /// <param name="name">Material name</param>
    /// <param name="e">Modulus of elasticity</param>
    /// <param name="u">Poisson's ratio</param>
    /// <param name="a">Coefficient of thermal expansion</param>
    /// <param name="temp">Temperature for these properties</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetMPIsotropic(string name, double e, double u, double a, double temp = 0.0);

    /// <summary>
    /// Sets isotropic properties using model object.
    /// </summary>
    /// <param name="name">Material name</param>
    /// <param name="props">IsotropicProperties object</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetMPIsotropic(string name, IsotropicProperties props);

    #endregion

    #region Concrete Materials

    /// <summary>
    /// Creates a concrete material with full parameters.
    /// Uses three API calls: SetMaterial, SetMPIsotropic, SetOConcrete_1.
    /// </summary>
    /// <param name="material">ConcreteMaterial object with all properties</param>
    /// <returns>Created ConcreteMaterial</returns>
    ConcreteMaterial AddConcreteMaterial(ConcreteMaterial material);

    /// <summary>
    /// Creates a concrete material with essential parameters and defaults.
    /// </summary>
    /// <param name="name">Material name</param>
    /// <param name="fc">Specified compressive strength</param>
    /// <param name="ec">Modulus of elasticity</param>
    /// <returns>Created ConcreteMaterial with default values</returns>
    ConcreteMaterial AddConcreteMaterial(string name, double fc, double ec);

    /// <summary>
    /// Gets concrete material properties (basic version).
    /// Wraps cSapModel.PropMaterial.GetOConcrete.
    /// </summary>
    /// <param name="name">Material name</param>
    /// <param name="temp">Temperature</param>
    /// <returns>Tuple with concrete properties</returns>
    (double Fc, bool IsLightweight, double FcsFactor, int SSType, int SSHysType,
        double StrainAtFc, double StrainUltimate, double FrictionAngle, double DilatationalAngle)
        GetOConcrete(string name, double temp = 0.0);

    /// <summary>
    /// Gets complete concrete material properties.
    /// Wraps cSapModel.PropMaterial.GetOConcrete_1.
    /// </summary>
    /// <param name="name">Material name</param>
    /// <param name="temp">Temperature</param>
    /// <returns>ConcreteMaterial object</returns>
    ConcreteMaterial GetConcreteMaterial(string name, double temp = 0.0);

    /// <summary>
    /// Sets concrete properties (basic version).
    /// Wraps cSapModel.PropMaterial.SetOConcrete.
    /// </summary>
    int SetOConcrete(string name, double fc, bool isLightweight, double fcsFactor,
        int ssType, int ssHysType, double strainAtFc, double strainUltimate,
        double frictionAngle = 0.0, double dilatationalAngle = 0.0, double temp = 0.0);

    /// <summary>
    /// Sets complete concrete properties.
    /// Wraps cSapModel.PropMaterial.SetOConcrete_1.
    /// </summary>
    int SetOConcrete_1(string name, double fc, bool isLightweight, double fcsFactor,
        int ssType, int ssHysType, double strainAtFc, double strainUltimate,
        double finalSlope, double frictionAngle = 0.0, double dilatationalAngle = 0.0,
        double temp = 0.0);

    #endregion

    #region Steel Materials

    /// <summary>
    /// Creates a steel material with full parameters.
    /// </summary>
    /// <param name="material">SteelMaterial object with all properties</param>
    /// <returns>Created SteelMaterial</returns>
    SteelMaterial AddSteelMaterial(SteelMaterial material);

    /// <summary>
    /// Creates a steel material with essential parameters and defaults.
    /// </summary>
    /// <param name="name">Material name</param>
    /// <param name="fy">Yield stress</param>
    /// <param name="fu">Ultimate stress</param>
    /// <returns>Created SteelMaterial with default values</returns>
    SteelMaterial AddSteelMaterial(string name, double fy, double fu);

    /// <summary>
    /// Gets steel material properties (basic version).
    /// Wraps cSapModel.PropMaterial.GetOSteel.
    /// </summary>
    (double Fy, double Fu, double EFy, double EFu, int SSType, int SSHysType,
        double StrainAtHardening, double StrainAtMaxStress, double StrainAtRupture)
        GetOSteel(string name, double temp = 0.0);

    /// <summary>
    /// Gets complete steel material properties.
    /// Wraps cSapModel.PropMaterial.GetOSteel_1.
    /// </summary>
    /// <param name="name">Material name</param>
    /// <param name="temp">Temperature</param>
    /// <returns>SteelMaterial object</returns>
    SteelMaterial GetSteelMaterial(string name, double temp = 0.0);

    /// <summary>
    /// Sets steel properties (basic version).
    /// Wraps cSapModel.PropMaterial.SetOSteel.
    /// </summary>
    int SetOSteel(string name, double fy, double fu, double eFy, double eFu,
        int ssType, int ssHysType, double strainAtHardening,
        double strainAtMaxStress, double strainAtRupture, double temp = 0.0);

    /// <summary>
    /// Sets complete steel properties.
    /// Wraps cSapModel.PropMaterial.SetOSteel_1.
    /// </summary>
    int SetOSteel_1(string name, double fy, double fu, double eFy, double eFu,
        int ssType, int ssHysType, double strainAtHardening,
        double strainAtMaxStress, double strainAtRupture, double finalSlope,
        double temp = 0.0);

    #endregion

    #region Rebar Materials

    /// <summary>
    /// Creates a rebar material with full parameters.
    /// </summary>
    /// <param name="material">RebarMaterial object with all properties</param>
    /// <returns>Created RebarMaterial</returns>
    RebarMaterial AddRebarMaterial(RebarMaterial material);

    /// <summary>
    /// Creates a rebar material with essential parameters and defaults.
    /// </summary>
    /// <param name="name">Material name</param>
    /// <param name="fy">Yield stress</param>
    /// <param name="fu">Ultimate stress</param>
    /// <returns>Created RebarMaterial with default values</returns>
    RebarMaterial AddRebarMaterial(string name, double fy, double fu);

    /// <summary>
    /// Gets rebar material properties (basic version).
    /// Wraps cSapModel.PropMaterial.GetORebar.
    /// </summary>
    (double Fy, double Fu, double EFy, double EFu, int SSType, int SSHysType,
        double StrainAtHardening, double StrainUltimate, bool UseCaltransSSDefaults)
        GetORebar(string name, double temp = 0.0);

    /// <summary>
    /// Gets complete rebar material properties.
    /// Wraps cSapModel.PropMaterial.GetORebar_1.
    /// </summary>
    /// <param name="name">Material name</param>
    /// <param name="temp">Temperature</param>
    /// <returns>RebarMaterial object</returns>
    RebarMaterial GetRebarMaterial(string name, double temp = 0.0);

    /// <summary>
    /// Sets rebar properties (basic version).
    /// Wraps cSapModel.PropMaterial.SetORebar.
    /// </summary>
    int SetORebar(string name, double fy, double fu, double eFy, double eFu,
        int ssType, int ssHysType, double strainAtHardening,
        double strainUltimate, bool useCaltransSSDefaults, double temp = 0.0);

    /// <summary>
    /// Sets complete rebar properties.
    /// Wraps cSapModel.PropMaterial.SetORebar_1.
    /// </summary>
    int SetORebar_1(string name, double fy, double fu, double eFy, double eFu,
        int ssType, int ssHysType, double strainAtHardening,
        double strainUltimate, double finalSlope, bool useCaltransSSDefaults,
        double temp = 0.0);

    #endregion

    #region Tendon Materials

    /// <summary>
    /// Creates a tendon material with full parameters.
    /// </summary>
    /// <param name="material">TendonMaterial object with all properties</param>
    /// <returns>Created TendonMaterial</returns>
    TendonMaterial AddTendonMaterial(TendonMaterial material);

    /// <summary>
    /// Creates a tendon material with essential parameters and defaults.
    /// </summary>
    /// <param name="name">Material name</param>
    /// <param name="fy">Yield stress</param>
    /// <param name="fu">Ultimate stress</param>
    /// <returns>Created TendonMaterial with default values</returns>
    TendonMaterial AddTendonMaterial(string name, double fy, double fu);

    /// <summary>
    /// Gets tendon material properties (basic version).
    /// Wraps cSapModel.PropMaterial.GetOTendon.
    /// </summary>
    (double Fy, double Fu, int SSType, int SSHysType)
        GetOTendon(string name, double temp = 0.0);

    /// <summary>
    /// Gets complete tendon material properties.
    /// Wraps cSapModel.PropMaterial.GetOTendon_1.
    /// </summary>
    /// <param name="name">Material name</param>
    /// <param name="temp">Temperature</param>
    /// <returns>TendonMaterial object</returns>
    TendonMaterial GetTendonMaterial(string name, double temp = 0.0);

    /// <summary>
    /// Sets tendon properties (basic version).
    /// Wraps cSapModel.PropMaterial.SetOTendon.
    /// </summary>
    int SetOTendon(string name, double fy, double fu, int ssType, int ssHysType,
        double temp = 0.0);

    /// <summary>
    /// Sets complete tendon properties.
    /// Wraps cSapModel.PropMaterial.SetOTendon_1.
    /// </summary>
    int SetOTendon_1(string name, double fy, double fu, int ssType, int ssHysType,
        double finalSlope, double temp = 0.0);

    #endregion

    #region No Design Materials

    /// <summary>
    /// Gets no design material properties.
    /// Wraps cSapModel.PropMaterial.GetONoDesign.
    /// </summary>
    (double FrictionAngle, double DilatationalAngle)
        GetONoDesign(string name, double temp = 0.0);

    /// <summary>
    /// Sets no design material properties.
    /// Wraps cSapModel.PropMaterial.SetONoDesign.
    /// </summary>
    int SetONoDesign(string name, double frictionAngle = 0.0,
        double dilatationalAngle = 0.0, double temp = 0.0);

    #endregion

    #region Damping Properties

    /// <summary>
    /// Gets material damping properties.
    /// Wraps cSapModel.PropMaterial.GetDamping.
    /// </summary>
    /// <param name="name">Material name</param>
    /// <param name="temp">Temperature</param>
    /// <returns>DampingProperties object</returns>
    DampingProperties GetDamping(string name, double temp = 0.0);

    /// <summary>
    /// Sets material damping properties.
    /// Wraps cSapModel.PropMaterial.SetDamping.
    /// </summary>
    /// <param name="name">Material name</param>
    /// <param name="modalRatio">Modal damping ratio</param>
    /// <param name="viscousMassCoeff">Viscous mass coefficient</param>
    /// <param name="viscousStiffCoeff">Viscous stiffness coefficient</param>
    /// <param name="hystereticMassCoeff">Hysteretic mass coefficient</param>
    /// <param name="hystereticStiffCoeff">Hysteretic stiffness coefficient</param>
    /// <param name="temp">Temperature</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetDamping(string name, double modalRatio, double viscousMassCoeff,
        double viscousStiffCoeff, double hystereticMassCoeff,
        double hystereticStiffCoeff, double temp = 0.0);

    /// <summary>
    /// Sets damping using model object.
    /// </summary>
    int SetDamping(string name, DampingProperties damping);

    #endregion

    #region Weight and Mass

    /// <summary>
    /// Gets weight and mass per unit volume.
    /// Wraps cSapModel.PropMaterial.GetWeightAndMass.
    /// </summary>
    /// <param name="name">Material name</param>
    /// <param name="temp">Temperature</param>
    /// <returns>WeightMassProperties object</returns>
    WeightMassProperties GetWeightAndMass(string name, double temp = 0.0);

    /// <summary>
    /// Sets weight and mass per unit volume.
    /// Wraps cSapModel.PropMaterial.SetWeightAndMass.
    /// </summary>
    /// <param name="name">Material name</param>
    /// <param name="myOption">Option (1=weight, 2=mass)</param>
    /// <param name="value">Weight or mass value</param>
    /// <param name="temp">Temperature</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetWeightAndMass(string name, int myOption, double value, double temp = 0.0);

    #endregion

    #region Stress-Strain Curves

    /// <summary>
    /// Gets stress-strain curve points.
    /// Wraps cSapModel.PropMaterial.GetSSCurve.
    /// </summary>
    /// <param name="name">Material name</param>
    /// <param name="sectName">Section name (optional)</param>
    /// <param name="rebarArea">Rebar area (optional)</param>
    /// <param name="temp">Temperature</param>
    /// <returns>List of StressStrainPoint objects</returns>
    List<StressStrainPoint> GetSSCurve(string name, string sectName = "",
        double rebarArea = 0.0, double temp = 0.0);

    /// <summary>
    /// Sets stress-strain curve points.
    /// Wraps cSapModel.PropMaterial.SetSSCurve.
    /// </summary>
    /// <param name="name">Material name</param>
    /// <param name="points">List of stress-strain points</param>
    /// <param name="temp">Temperature</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetSSCurve(string name, List<StressStrainPoint> points, double temp = 0.0);

    #endregion

    #region Temperature-Dependent Properties

    /// <summary>
    /// Gets list of temperatures for which properties are defined.
    /// Wraps cSapModel.PropMaterial.GetTemp.
    /// </summary>
    /// <param name="name">Material name</param>
    /// <returns>Array of temperature values</returns>
    double[] GetTemp(string name);

    /// <summary>
    /// Sets temperatures for which properties are defined.
    /// Wraps cSapModel.PropMaterial.SetTemp.
    /// </summary>
    /// <param name="name">Material name</param>
    /// <param name="temps">Array of temperature values</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetTemp(string name, double[] temps);

    #endregion

    #region Anisotropic and Orthotropic Properties (Advanced)

    /// <summary>
    /// Gets anisotropic mechanical properties.
    /// Wraps cSapModel.PropMaterial.GetMPAnisotropic.
    /// </summary>
    (double[] E, double[] U, double[] A, double[] G)
        GetMPAnisotropic(string name, double temp = 0.0);

    /// <summary>
    /// Sets anisotropic mechanical properties.
    /// Wraps cSapModel.PropMaterial.SetMPAnisotropic.
    /// </summary>
    int SetMPAnisotropic(string name, double[] e, double[] u, double[] a,
        double[] g, double temp = 0.0);

    /// <summary>
    /// Gets orthotropic mechanical properties.
    /// Wraps cSapModel.PropMaterial.GetMPOrthotropic.
    /// </summary>
    (double[] E, double[] U, double[] A, double[] G)
        GetMPOrthotropic(string name, double temp = 0.0);

    /// <summary>
    /// Sets orthotropic mechanical properties.
    /// Wraps cSapModel.PropMaterial.SetMPOrthotropic.
    /// </summary>
    int SetMPOrthotropic(string name, double[] e, double[] u, double[] a,
        double[] g, double temp = 0.0);

    /// <summary>
    /// Gets uniaxial mechanical properties.
    /// Wraps cSapModel.PropMaterial.GetMPUniaxial.
    /// </summary>
    (double E, double A) GetMPUniaxial(string name, double temp = 0.0);

    /// <summary>
    /// Sets uniaxial mechanical properties.
    /// Wraps cSapModel.PropMaterial.SetMPUniaxial.
    /// </summary>
    int SetMPUniaxial(string name, double e, double a, double temp = 0.0);

    #endregion

    #region Convenience Methods

    /// <summary>
    /// Checks if a material exists in the model.
    /// </summary>
    bool MaterialExists(string name);

    /// <summary>
    /// Gets all concrete materials in the model.
    /// </summary>
    List<ConcreteMaterial> GetAllConcreteMaterials();

    /// <summary>
    /// Gets all steel materials in the model.
    /// </summary>
    List<SteelMaterial> GetAllSteelMaterials();

    /// <summary>
    /// Gets all rebar materials in the model.
    /// </summary>
    List<RebarMaterial> GetAllRebarMaterials();

    /// <summary>
    /// Gets all tendon materials in the model.
    /// </summary>
    List<TendonMaterial> GetAllTendonMaterials();

    /// <summary>
    /// Creates a complete material from library with all properties retrieved.
    /// </summary>
    MaterialProperty GetCompleteMaterial(string name);

    /// <summary>
    /// Deletes all materials of a specific type.
    /// </summary>
    /// <param name="matType">Material type to delete</param>
    /// <returns>Number of materials deleted</returns>
    int DeleteMaterialsByType(eMatType matType);

    #endregion
}