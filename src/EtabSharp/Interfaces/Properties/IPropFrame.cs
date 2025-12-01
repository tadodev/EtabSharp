using EtabSharp.Properties.Frames.Models;
using ETABSv1;

namespace EtabSharp.Interfaces.Properties;

/// <summary>
/// Provides methods for managing frame(beam and column) properties in the ETABS model.
/// This interface handles creation, modification, and retrieval of frame sections including
/// rectangular columns, circular columns, rectangular beams, and importing from libraries.
/// </summary>
public interface IPropFrame
{
    #region Section Information

    /// <summary>
    /// Retrieves the names of all defined frame sections.
    /// </summary>
    /// <returns>An array of frame section names.</returns>
    string[] GetNameList();

    /// <summary>
    /// Gets the section type (I-beam, rectangular, circular, etc.) for a given frame section.
    /// </summary>
    /// <param name="name">The name of the frame section.</param>
    /// <returns>The section type enumeration.</returns>
    eFramePropType GetSectionType(string name);

    /// <summary>
    /// Deletes a frame section from the model.
    /// </summary>
    /// <param name="name">The name of the frame section to delete.</param>
    /// <returns>0 if successful, non-zero otherwise.</returns>
    int Delete(string name);

    #endregion

    #region Rectangular Sections

    /// <summary>
    /// Creates a rectangular frame section (for beams or columns).
    /// </summary>
    /// <param name="name">Unique identifier for the frame section.</param>
    /// <param name="materialName">Name of the material (must exist in model).</param>
    /// <param name="depth">Section depth (local 3-axis direction). Must be positive.</param>
    /// <param name="width">Section width (local 2-axis direction). Must be positive.</param>
    /// <param name="color">Optional color for the section (default: -1 for auto).</param>
    /// <returns>A <see cref="PropFrameRectangle"/> object containing the section properties.</returns>
    /// <exception cref="ArgumentException">If name or material is invalid.</exception>
    /// <exception cref="ArgumentOutOfRangeException">If depth or width ≤ 0.</exception>
    /// <exception cref="Exceptions.EtabsException">If ETABS API fails.</exception>
    PropFrameRectangle AddRectangularSection(string name, string materialName, double depth, double width,
        int color = -1);

    /// <summary>
    /// Gets the properties of an existing rectangular frame section.
    /// </summary>
    /// <param name="name">The name of the rectangular section.</param>
    /// <returns>A <see cref="PropFrameRectangle"/> object containing the section properties.</returns>
    PropFrameRectangle GetRectangularSection(string name);

    #endregion

    #region Circular Sections

    /// <summary>
    /// Creates a circular frame section (typically for columns).
    /// </summary>
    /// <param name="name">Unique identifier for the frame section.</param>
    /// <param name="materialName">Name of the material (must exist in model).</param>
    /// <param name="diameter">Outside diameter of the circular section. Must be positive.</param>
    /// <param name="color">Optional color for the section (default: -1 for auto).</param>
    /// <returns>A <see cref="PropFrameCircle"/> object containing the section properties.</returns>
    /// <exception cref="ArgumentException">If name or material is invalid.</exception>
    /// <exception cref="ArgumentOutOfRangeException">If diameter ≤ 0.</exception>
    /// <exception cref="Exceptions.EtabsException">If ETABS API fails.</exception>
    PropFrameCircle AddCircularSection(string name, string materialName, double diameter, int color = -1);

    /// <summary>
    /// Gets the properties of an existing circular frame section.
    /// </summary>
    /// <param name="name">The name of the circular section.</param>
    /// <returns>A <see cref="PropFrameCircle"/> object containing the section properties.</returns>
    PropFrameCircle GetCircularSection(string name);

    #endregion

    #region Reinforcement

    /// <summary>
    /// Assigns reinforcement to a rectangular column section.
    /// </summary>
    /// <param name="sectionName">Name of the rectangular column section.</param>
    /// <param name="rebarData">Reinforcement data for the column.</param>
    /// <returns>0 if successful, non-zero otherwise.</returns>
    int SetColumnRebarRectangular(string sectionName, PropColumnRebarRect rebarData);

    /// <summary>
    /// Assigns reinforcement to a circular column section.
    /// </summary>
    /// <param name="sectionName">Name of the circular column section.</param>
    /// <param name="rebarData">Reinforcement data for the column.</param>
    /// <returns>0 if successful, non-zero otherwise.</returns>
    int SetColumnRebarCircular(string sectionName, PropColumnRebarCirc rebarData);

    /// <summary>
    /// Assigns reinforcement to a rectangular beam section.
    /// </summary>
    /// <param name="sectionName">Name of the rectangular beam section.</param>
    /// <param name="rebarData">Reinforcement data for the beam.</param>
    /// <returns>0 if successful, non-zero otherwise.</returns>
    int SetBeamRebar(string sectionName, PropBeamRebar rebarData);

    /// <summary>
    /// Gets reinforcement data for a rectangular column.
    /// </summary>
    /// <param name="sectionName">Name of the column section.</param>
    /// <returns>Reinforcement data for the column.</returns>
    PropColumnRebarRect GetColumnRebarRectangular(string sectionName);

    /// <summary>
    /// Gets reinforcement data for a circular column.
    /// </summary>
    /// <param name="sectionName">Name of the column section.</param>
    /// <returns>Reinforcement data for the column.</returns>
    PropColumnRebarCirc GetColumnRebarCircular(string sectionName);

    /// <summary>
    /// Gets reinforcement data for a beam.
    /// </summary>
    /// <param name="sectionName">Name of the beam section.</param>
    /// <returns>Reinforcement data for the beam.</returns>
    PropBeamRebar GetBeamRebar(string sectionName);

    #endregion

    #region Import from Library

    /// <summary>
    /// Imports a frame section from the ETABS section database.
    /// </summary>
    /// <param name="sectionName">Name to assign to the imported section.</param>
    /// <param name="materialName">Material to assign to the section.</param>
    /// <param name="fileName">Name of the property file (e.g., "AISC.xml", "EURO.xml").</param>
    /// <param name="shapeName">Name of the shape in the file (e.g., "W14X82", "IPE300").</param>
    /// <returns>0 if successful, non-zero otherwise.</returns>
    /// <exception cref="ArgumentException">If any parameter is invalid.</exception>
    /// <exception cref="Exceptions.EtabsException">If import fails.</exception>
    int ImportSectionFromLibrary(string sectionName, string materialName, string fileName, string shapeName);

    /// <summary>
    /// Gets available section property files in the ETABS database.
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="propType"></param>
    /// <returns>Array of available property file names.</returns>
    string[] GetAvailableSectionFiles(string fileName, eFramePropType propType);

    #endregion

    #region Modifiers

    /// <summary>
    /// Sets property modifiers for a frame section.
    /// Modifiers scale the section properties (area, moment of inertia, etc.).
    /// </summary>
    /// <param name="sectionName">Name of the frame section.</param>
    /// <param name="modifiers">Property modifier values.</param>
    /// <returns>0 if successful, non-zero otherwise.</returns>
    int SetModifiers(string sectionName, PropFrameModifiers modifiers);

    /// <summary>
    /// Gets property modifiers for a frame section.
    /// </summary>
    /// <param name="sectionName">Name of the frame section.</param>
    /// <returns>Property modifier values.</returns>
    PropFrameModifiers GetModifiers(string sectionName);

    #endregion
}