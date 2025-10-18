using ETABSv1;

namespace EtabSharp.Interfaces.Properties;

/// <summary>
/// Provides methods for managing area properties (slabs and walls) in the ETABS model.
/// This interface handles creation, modification, and retrieval of area sections including
/// slabs (solid, ribbed, waffle) and walls (solid, auto-select).
/// </summary>
public interface IPropArea
{
    #region General Section Information

    /// <summary>
    /// Retrieves the names of all defined area properties of the specified type.
    /// </summary>
    /// <param name="propType"></param>
    /// <returns>An array of area property names.</returns>
    string[] GetNameList(int propType = 0);

    /// <summary>
    /// Returns the total number of defined area properties in the model.
    /// </summary>
    /// <param name="propType"></param>
    /// <returns>The number of defined area properties.</returns>
    int Count(int propType = 0);

    /// <summary>
    /// Gets the property type for the specified area property.(GetTypeOAPI method)
    /// </summary>
    /// <param name="name">The name of the area property.</param>
    /// <returns>The area property type enumeration.</returns>
    int GetPropertyType(string name);

    /// <summary>
    /// Changes the name of an existing area property.
    /// </summary>
    /// <param name="currentName">The current name of the area property.</param>
    /// <param name="newName">The new name for the area property.</param>
    /// <returns>0 if successful, non-zero otherwise.</returns>
    int ChangeName(string currentName, string newName);

    /// <summary>
    /// Deletes a specified area property from the model.
    /// </summary>
    /// <param name="name">The name of the area property to delete.</param>
    /// <returns>0 if successful, non-zero otherwise.</returns>
    int Delete(string name);

    #endregion

    #region Slab Sections

    /// <summary>
    /// Creates a solid slab property.
    /// </summary>
    /// <param name="name">Unique identifier for the slab section.</param>
    /// <param name="materialName">Name of the material (must exist in model).</param>
    /// <param name="thickness">Slab thickness. Must be positive.</param>
    /// <param name="slabType">Type of slab (slab, drop, mat, footing).</param>
    /// <param name="shellType">Shell behavior type (shell-thin, shell-thick, membrane, plate).</param>
    /// <param name="color">Optional color for the section (default: -1 for auto).</param>
    /// <returns>A <see cref="PropSlab"/> object containing the slab properties.</returns>
    /// <exception cref="ArgumentException">If name or material is invalid.</exception>
    /// <exception cref="ArgumentOutOfRangeException">If thickness ≤ 0.</exception>
    /// <exception cref="Exceptions.EtabsException">If ETABS API fails.</exception>
    PropSlab AddSlab(
        string name,
        string materialName,
        double thickness,
        eSlabType slabType = eSlabType.Slab,
        eShellType shellType = eShellType.ShellThin,
        int color = -1);

    /// <summary>
    /// Gets the properties of an existing solid slab section.
    /// </summary>
    /// <param name="name">The name of the slab section.</param>
    /// <returns>A <see cref="PropSlab"/> object containing the slab properties.</returns>
    PropSlab GetSlab(string name);

#endregion
}