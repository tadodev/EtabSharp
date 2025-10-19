using EtabSharp.Properties.Materials.Models;
using ETABSv1;

namespace EtabSharp.Interfaces.Properties;

/// <summary>
/// Provides methods for managing material properties in the ETABS model.
/// This interface handles creation, modification, and retrieval of materials including
/// concrete, rebar, and steel materials.
/// </summary>
public interface IPropMaterial
{
    /// <summary>
    /// Add Default Material from Etabs library.
    /// </summary>
    /// <param name="matType">The type of material (Concrete, Steel, etc.).</param>
    /// <param name="region">The region of the material standard (e.g., "USA", "Europe").</param>
    /// <param name="standard">The standard (e.g., "ACI", "EN").</param>
    /// <param name="grade">The grade name of the material (e.g., "C30", "A992").</param>
    /// <returns>The name of the added material.</returns>
    string AddDefaultMaterial(eMatType matType, string region, string standard, string grade);

    /// <summary>
    /// Retrieves the names of all defined material properties of the specified type.
    /// </summary>
    /// <param name="matType">
    /// The type of materials to retrieve. Use <see cref="eMatType.Steel"/> for steel,
    /// <see cref="eMatType.Concrete"/> for concrete, <see cref="eMatType.Rebar"/> for rebar, etc.
    /// Default value returns all material types.
    /// </param>
    /// <returns>
    /// An array of material names that match the specified type.
    /// </returns>
    string[] GetNameList(eMatType matType = default);

    /// <summary>
    /// Adds a new concrete material to the model with the specified properties.
    /// Uses sensible defaults for advanced properties like Poisson's ratio,
    /// thermal expansion coefficient, and stress-strain behavior.
    /// </summary>
    /// <param name="name">Unique identifier for the concrete material (e.g., "C30").</param>
    /// <param name="fpc">Specified compressive strength of concrete (f'c). Must be positive.</param>
    /// <param name="Ec">Elastic modulus (Young's modulus). Must be positive.</param>
    /// <returns>
    /// A <see cref="PropConcrete"/> object containing all material properties including defaults.
    /// </returns>
    /// <exception cref="ArgumentException">If <paramref name="name"/> is invalid.</exception>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="fpc"/> or <paramref name="Ec"/> ≤ 0.</exception>
    /// <exception cref="Exceptions.EtabsMaterialException">If ETABS API fails to create the material.</exception>
    PropConcrete AddConcreteMaterial(string name, double fpc, double Ec);

    /// <summary>
    /// Adds a new reinforcing bar (rebar) material to the model with the specified properties.
    /// Uses sensible defaults for advanced properties like Poisson's ratio,
    /// thermal expansion coefficient, and stress-strain behavior.
    /// </summary>
    /// <param name="name">Unique identifier for the rebar material (e.g., "Grade60").</param>
    /// <param name="fy">Yield strength of rebar (must be positive).</param>
    /// <param name="fu">Ultimate tensile strength (must be > fy).</param>
    /// <returns>
    /// A <see cref="PropRebar"/> object containing all material properties including defaults.
    /// </returns>
    /// <exception cref="ArgumentException">If <paramref name="name"/> is invalid.</exception>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="fy"/> or <paramref name="fu"/> is invalid.</exception>
    /// <exception cref="Exceptions.EtabsMaterialException">If ETABS API fails to create the material.</exception>
    PropRebar AddRebarMaterial(string name, double fy, double fu);

    /// <summary>
    /// Adds a new structural steel material to the model with the specified properties.
    /// Uses sensible defaults for advanced properties like Poisson's ratio,
    /// thermal expansion coefficient, and stress-strain behavior.
    /// </summary>
    /// <param name="name">Unique identifier for the steel material (e.g., "A992").</param>
    /// <param name="fy">Yield strength of steel (must be positive).</param>
    /// <param name="fu">Ultimate tensile strength (must be > fy).</param>
    /// <returns>
    /// A <see cref="PropSteel"/> object containing all material properties including defaults.
    /// </returns>
    /// <exception cref="ArgumentException">If <paramref name="name"/> is invalid.</exception>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="fy"/> or <paramref name="fu"/> is invalid.</exception>
    /// <exception cref="Exceptions.EtabsMaterialException">If ETABS API fails to create the material.</exception>
    PropSteel AddSteelMaterial(string name, double fy, double fu);
}