using EtabSharp.Properties.Areas.Models;
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
    /// Gets the properties of an existing solid slab section.
    /// </summary>
    /// <param name="name">The name of the slab section.</param>
    /// <returns>A <see cref="PropSlab"/> object containing the slab properties.</returns>
    PropSlab GetSlab(string name);

    /// <summary>
    /// Creates a solid slab property.
    /// </summary>
    /// <param name="name">Unique identifier for the slab section.</param>
    /// <param name="slabType">Type of slab (slab, drop, mat, footing).</param>
    /// <param name="shellType">Shell behavior type (shell-thin, shell-thick, membrane, plate).</param>
    /// <param name="materialName">Name of the material (must exist in model).</param>
    /// <param name="thickness">Slab thickness. Must be positive.</param>
    /// <returns>A <see cref="PropSlab"/> object containing the slab properties.</returns>
    /// <exception cref="ArgumentException">If name or material is invalid.</exception>
    /// <exception cref="ArgumentOutOfRangeException">If thickness ≤ 0.</exception>
    /// <exception cref="Exceptions.EtabsException">If ETABS API fails.</exception>
    PropSlab AddSlab(string name, eSlabType slabType, eShellType shellType, string materialName, double thickness);

    #endregion

    #region Wall Sections

    /// <summary>
    /// Creates a solid wall property.
    /// </summary>
    /// <param name="name">Unique identifier for the wall section.</param>
    /// <param name="wallType"></param>
    /// <param name="shellType">Shell behavior type (shell-thin, shell-thick, membrane).</param>
    /// <param name="materialName">Name of the material (must exist in model).</param>
    /// <param name="thickness">Wall thickness. Must be positive.</param>
    /// <returns>A <see cref="PropWall"/> object containing the wall properties.</returns>
    /// <exception cref="ArgumentException">If name or material is invalid.</exception>
    /// <exception cref="ArgumentOutOfRangeException">If thickness ≤ 0.</exception>
    /// <exception cref="EtabSharp.Exceptions.EtabsException">If ETABS API fails.</exception>
    PropWall AddWall(
        string name,
        eWallPropType wallType,
        eShellType shellType,
        string materialName,
        double thickness);

    /// <summary>
    /// Gets the properties of an existing wall section.
    /// </summary>
    /// <param name="name">The name of the wall section.</param>
    /// <returns>A <see cref="PropWall"/> object containing the wall properties.</returns>
    PropWall GetWall(string name);

    /// <summary>
    /// Creates an auto-select list wall property that allows ETABS to optimize wall thickness.
    /// </summary>
    /// <param name="name">Unique identifier for the auto-select list.</param>
    /// <param name="autoSelectList">Array of wall section names to include in the auto-select list.</param>
    /// <param name="startingSection">Name of the section to use initially (must be in the list).</param>
    /// <returns>A <see cref="PropWallAutoSelect"/> object containing the auto-select properties.</returns>
    /// <exception cref="ArgumentException">If name is invalid or section list is empty.</exception>
    /// <exception cref="EtabSharp.Exceptions.EtabsException">If ETABS API fails.</exception>
    PropWallAutoSelect AddWallAutoSelectList(
        string name,
        string[] autoSelectList,
        string startingSection);

    /// <summary>
    /// Gets the properties of an existing wall auto-select list.
    /// </summary>
    /// <param name="name">The name of the auto-select list.</param>
    /// <returns>A <see cref="PropWallAutoSelect"/> object containing the auto-select properties.</returns>
    PropWallAutoSelect GetWallAutoSelectList(string name);

    #endregion

    #region Design Parameters

    /// <summary>
    /// Assigns design parameters for shell-type area properties (slabs and walls).
    /// Controls whether the section is designed and which design code to use.
    /// </summary>
    /// <param name="name">The name of the area property.</param>
    /// <param name="designParameters">Shell design parameter data.</param>
    /// <returns>0 if successful, non-zero otherwise.</returns>
    int SetShellDesign(string name, PropShellDesign designParameters);

    /// <summary>
    /// Gets design parameters for shell-type area properties.
    /// </summary>
    /// <param name="name">The name of the area property.</param>
    /// <returns>Shell design parameter data.</returns>
    PropShellDesign GetShellDesign(string name);

    #endregion

    #region Modifiers

    /// <summary>
    /// Sets property modifiers for an area section.
    /// Modifiers scale the section properties (membrane stiffness, bending stiffness, etc.).
    /// </summary>
    /// <param name="name">Name of the area section.</param>
    /// <param name="modifiers">Property modifier values.</param>
    /// <returns>0 if successful, non-zero otherwise.</returns>
    int SetModifiers(string name, PropAreaModifiers modifiers);

    /// <summary>
    /// Gets property modifiers for an area section.
    /// </summary>
    /// <param name="name">Name of the area section.</param>
    /// <returns>Property modifier values.</returns>
    PropAreaModifiers GetModifiers(string name);

    #endregion
}