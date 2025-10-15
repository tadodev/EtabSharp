using EtabSharp.Exceptions;
using EtabSharp.Frames.Models;
using EtabSharp.Interfaces;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Frames;

public partial class PropFrame:IPropFrame
{
    private readonly cSapModel _sapModel;
    private readonly ILogger _logger;

    internal PropFrame(cSapModel sapModel, ILogger logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _sapModel = sapModel ?? throw new ArgumentNullException(nameof(sapModel));
    }

    #region Section Information

    ///<inheritdoc/>
    public string[] GetNameList()
    {
        try
        {
            int numberOfNames = 0;
            string[] names = null;

            int ret = _sapModel.PropFrame.GetNameList(ref numberOfNames, ref names);
            if (ret != 0)
            {
                _logger.LogError("Failed to get frame section name list. Return code: {ReturnCode}", ret);
                throw new EtabsException(ret, "GetNameList", "Failed to retrieve frame section names from ETABS model.");
            }

            _logger.LogDebug("Retrieved {Count} frame sections", numberOfNames);
            return names ?? Array.Empty<string>();
        }
        catch (Exception ex) when (ex is not EtabsException)
        {
            _logger.LogError(ex, "Unexpected error getting frame section name list");
            throw new EtabsException("Unexpected error retrieving frame section names", ex);
        }
    }

    ///<inheritdoc/>
    public eFramePropType GetSectionType(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Section name cannot be empty.", nameof(name));

        try
        {
            eFramePropType propType = eFramePropType.I;

            int ret = _sapModel.PropFrame.GetTypeOAPI(name, ref propType);
            if (ret != 0)
            {
                _logger.LogError("Failed to get section type for '{SectionName}'. Return code: {ReturnCode}", name, ret);
                throw new EtabsException(ret, "GetTypeOAPI", $"Failed to get section type for '{name}'.");
            }

            _logger.LogDebug("Section '{SectionName}' is of type {PropType}", name, propType);
            return propType;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error getting section type for '{SectionName}'", name);
            throw new EtabsException($"Unexpected error getting section type for '{name}'", ex);
        }
    }

    ///<inheritdoc/>
    public int Delete(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Section name cannot be empty.", nameof(name));

        try
        {
            int ret = _sapModel.PropFrame.Delete(name);
            if (ret != 0)
            {
                _logger.LogWarning("Failed to delete frame section '{SectionName}'. Return code: {ReturnCode}", name, ret);
            }
            else
            {
                _logger.LogInformation("Successfully deleted frame section '{SectionName}'", name);
            }

            return ret;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error deleting frame section '{SectionName}'", name);
            throw new EtabsException($"Unexpected error deleting frame section '{name}'", ex);
        }
    }

    #endregion

    #region Rectangular Sections

    ///<inheritdoc/>
    public PropFrameRectangle AddRectangularSection(string name, string materialName, double depth, double width, int color = -1)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Section name cannot be empty.", nameof(name));

        if (string.IsNullOrWhiteSpace(materialName))
            throw new ArgumentException("Material name cannot be empty.", nameof(materialName));

        if (depth <= 0)
            throw new ArgumentOutOfRangeException(nameof(depth), "Depth must be positive.");

        if (width <= 0)
            throw new ArgumentOutOfRangeException(nameof(width), "Width must be positive.");

        try
        {
            _logger.LogInformation("Creating rectangular section '{Name}' with material={Material}, depth={Depth}, width={Width}",
                name, materialName, depth, width);

            int ret = _sapModel.PropFrame.SetRectangle(name, materialName, depth, width, color);
            if (ret != 0)
            {
                _logger.LogError("Error creating rectangular section '{SectionName}'. Return code: {ReturnCode}", name, ret);
                throw new EtabsException(ret, "SetRectangle", $"Failed to create rectangular section '{name}'.");
            }

            _logger.LogInformation("Successfully created rectangular section '{Name}'", name);

            return new PropFrameRectangle
            {
                Name = name,
                Material = materialName,
                Depth = depth,
                Width = width,
                Color = color
            };
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error creating rectangular section '{Name}'", name);
            throw new EtabsException($"Unexpected error creating rectangular section '{name}'", ex);
        }
    }

    ///<inheritdoc/>
    public PropFrameRectangle GetRectangularSection(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Section name cannot be empty.", nameof(name));

        try
        {
            string fileName = "";
            string materialName = "";
            double t3 = 0;
            double t2 = 0;
            int color = 0;
            string notes = "";
            string guid = "";

            int ret = _sapModel.PropFrame.GetRectangle(name, ref fileName, ref materialName, ref t3, ref t2, ref color, ref notes, ref guid);
            if (ret != 0)
            {
                _logger.LogError("Failed to get rectangular section '{SectionName}'. Return code: {ReturnCode}", name, ret);
                throw new EtabsException(ret, "GetRectangle", $"Failed to get rectangular section '{name}'.");
            }

            return new PropFrameRectangle
            {
                Name = name,
                Material = materialName,
                Depth = t3,
                Width = t2,
                Color = color
            };
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error getting rectangular section '{Name}'", name);
            throw new EtabsException($"Unexpected error getting rectangular section '{name}'", ex);
        }
    }

    #endregion

    #region Circular Sections

    ///<inheritdoc/>
    public PropFrameCircle AddCircularSection(string name, string materialName, double diameter, int color = -1)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Section name cannot be empty.", nameof(name));

        if (string.IsNullOrWhiteSpace(materialName))
            throw new ArgumentException("Material name cannot be empty.", nameof(materialName));

        if (diameter <= 0)
            throw new ArgumentOutOfRangeException(nameof(diameter), "Diameter must be positive.");

        try
        {
            _logger.LogInformation("Creating circular section '{Name}' with material={Material}, diameter={Diameter}",
                name, materialName, diameter);

            int ret = _sapModel.PropFrame.SetCircle(name, materialName, diameter, color);
            if (ret != 0)
            {
                _logger.LogError("Error creating circular section '{SectionName}'. Return code: {ReturnCode}", name, ret);
                throw new EtabsException(ret, "SetCircle", $"Failed to create circular section '{name}'.");
            }

            _logger.LogInformation("Successfully created circular section '{Name}'", name);

            return new PropFrameCircle
            {
                Name = name,
                Material = materialName,
                Diameter = diameter,
                Color = color
            };
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error creating circular section '{Name}'", name);
            throw new EtabsException($"Unexpected error creating circular section '{name}'", ex);
        }
    }

    ///<inheritdoc/>
    public PropFrameCircle GetCircularSection(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Section name cannot be empty.", nameof(name));

        try
        {
            string fileName = "";
            string materialName = "";
            double diameter = 0;
            int color = 0;
            string notes = "";
            string guid = "";

            int ret = _sapModel.PropFrame.GetCircle(name, ref fileName, ref materialName, ref diameter, ref color, ref notes, ref guid);
            if (ret != 0)
            {
                _logger.LogError("Failed to get circular section '{SectionName}'. Return code: {ReturnCode}", name, ret);
                throw new EtabsException(ret, "GetCircle", $"Failed to get circular section '{name}'.");
            }

            return new PropFrameCircle
            {
                Name = name,
                Material = materialName,
                Diameter = diameter,
                Color = color
            };
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error getting circular section '{Name}'", name);
            throw new EtabsException($"Unexpected error getting circular section '{name}'", ex);
        }
    }

    #endregion

    #region Import from Library

    ///<inheritdoc/>
    public int ImportSectionFromLibrary(string sectionName, string materialName, string fileName, string shapeName)
    {
        if (string.IsNullOrWhiteSpace(sectionName))
            throw new ArgumentException("Section name cannot be empty.", nameof(sectionName));

        if (string.IsNullOrWhiteSpace(materialName))
            throw new ArgumentException("Material name cannot be empty.", nameof(materialName));

        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("File name cannot be empty.", nameof(fileName));

        if (string.IsNullOrWhiteSpace(shapeName))
            throw new ArgumentException("Shape name cannot be empty.", nameof(shapeName));

        try
        {
            _logger.LogInformation("Importing section '{SectionName}' from library: file={FileName}, shape={ShapeName}",
                sectionName, fileName, shapeName);

            int ret = _sapModel.PropFrame.ImportProp(sectionName, materialName, fileName, shapeName);

            if (ret != 0)
            {
                _logger.LogError("Failed to import section '{SectionName}' from library. Return code: {ReturnCode}", sectionName, ret);
                throw new EtabsException(ret, "ImportProp", $"Failed to import section '{sectionName}' from library.");
            }

            _logger.LogInformation("Successfully imported section '{SectionName}' from library", sectionName);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error importing section '{SectionName}' from library", sectionName);
            throw new EtabsException($"Unexpected error importing section '{sectionName}' from library", ex);
        }
    }

    ///<inheritdoc/>
    public string[] GetAvailableSectionFiles()
    {
        try
        {
            int numberNames = 0;
            string[] fileNames = null;

            int ret = _sapModel.PropFrame.GetPropFileNameList(ref numberNames, ref fileNames);

            if (ret != 0)
            {
                _logger.LogError("Failed to get available section files. Return code: {ReturnCode}", ret);
                throw new EtabsException(ret, "GetPropFileNameList", "Failed to get available section property files.");
            }

            _logger.LogDebug("Retrieved {Count} section property files", numberNames);
            return fileNames ?? Array.Empty<string>();
        }
        catch (Exception ex) when (ex is not EtabsException)
        {
            _logger.LogError(ex, "Unexpected error getting available section files");
            throw new EtabsException("Unexpected error getting available section files", ex);
        }
    }

    #endregion

    #region Modifiers

    ///<inheritdoc/>
    public int SetModifiers(string sectionName, PropFrameModifiers modifiers)
    {
        if (string.IsNullOrWhiteSpace(sectionName))
            throw new ArgumentException("Section name cannot be empty.", nameof(sectionName));

        if (modifiers == null)
            throw new ArgumentNullException(nameof(modifiers));

        try
        {
            _logger.LogInformation("Setting property modifiers for section '{SectionName}'", sectionName);

            double[] modifierValues = new double[8];
            modifierValues[0] = modifiers.Area;
            modifierValues[1] = modifiers.ShearArea2;
            modifierValues[2] = modifiers.ShearArea3;
            modifierValues[3] = modifiers.Torsion;
            modifierValues[4] = modifiers.Inertia2;
            modifierValues[5] = modifiers.Inertia3;
            modifierValues[6] = modifiers.Mass;
            modifierValues[7] = modifiers.Weight;

            int ret = _sapModel.PropFrame.SetModifiers(sectionName, ref modifierValues);

            if (ret != 0)
            {
                _logger.LogError("Failed to set modifiers for '{SectionName}'. Return code: {ReturnCode}", sectionName, ret);
                throw new EtabsException(ret, "SetModifiers", $"Failed to set modifiers for '{sectionName}'.");
            }

            _logger.LogInformation("Successfully set modifiers for '{SectionName}'", sectionName);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error setting modifiers for '{SectionName}'", sectionName);
            throw new EtabsException($"Unexpected error setting modifiers for '{sectionName}'", ex);
        }
    }

    ///<inheritdoc/>
    public PropFrameModifiers GetModifiers(string sectionName)
    {
        if (string.IsNullOrWhiteSpace(sectionName))
            throw new ArgumentException("Section name cannot be empty.", nameof(sectionName));

        try
        {
            double[] modifierValues = new double[8];

            int ret = _sapModel.PropFrame.GetModifiers(sectionName, ref modifierValues);

            if (ret != 0)
            {
                _logger.LogError("Failed to get modifiers for '{SectionName}'. Return code: {ReturnCode}", sectionName, ret);
                throw new EtabsException(ret, "GetModifiers", $"Failed to get modifiers for '{sectionName}'.");
            }

            return new PropFrameModifiers
            {
                Area = modifierValues[0],
                ShearArea2 = modifierValues[1],
                ShearArea3 = modifierValues[2],
                Torsion = modifierValues[3],
                Inertia2 = modifierValues[4],
                Inertia3 = modifierValues[5],
                Mass = modifierValues[6],
                Weight = modifierValues[7]
            };
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error getting modifiers for '{SectionName}'", sectionName);
            throw new EtabsException($"Unexpected error getting modifiers for '{sectionName}'", ex);
        }
    }

    #endregion


}