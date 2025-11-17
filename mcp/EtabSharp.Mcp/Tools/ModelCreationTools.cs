using System.ComponentModel;
using EtabSharp.Core;
using System.Text.Json;
using ETABSv1;
using ModelContextProtocol.Server;

namespace EtabSharp.Mcp.Tools;

/// <summary>
/// MCP tools for creating and modifying ETABS models
/// </summary>
[McpServerToolType]
public static class ModelCreationTools
{
    [McpServerTool]
    [Description("Create a new blank ETABS model")]
    public static string CreateBlankModel()
    {
        try
        {
            var etabs = ETABSWrapper.Connect();
            if (etabs == null)
            {
                return JsonSerializer.Serialize(new
                {
                    success = false,
                    error = "No active ETABS instance found."
                });
            }

            var result = etabs.Model.Files.NewBlankModel();
            
            return JsonSerializer.Serialize(new
            {
                success = result == 0,
                message = result == 0 ? "Blank model created successfully" : $"Failed to create blank model. Error code: {result}"
            });
        }
        catch (Exception ex)
        {
            return JsonSerializer.Serialize(new
            {
                success = false,
                error = ex.Message
            });
        }
    }

    [McpServerTool]
    [Description("Create a new grid-only model with specified parameters")]
    public static string CreateGridOnlyModel(
        [Description("Number of stories")] int numberStories,
        [Description("Typical story height in current units")] double typicalStoryHeight,
        [Description("Bottom story height in current units")] double bottomStoryHeight,
        [Description("Number of grid lines in X direction")] int numberLineX,
        [Description("Number of grid lines in Y direction")] int numberLineY,
        [Description("Spacing between X grid lines")] double spacingX,
        [Description("Spacing between Y grid lines")] double spacingY)
    {
        try
        {
            var etabs = ETABSWrapper.Connect();
            if (etabs == null)
            {
                return JsonSerializer.Serialize(new
                {
                    success = false,
                    error = "No active ETABS instance found."
                });
            }

            var result = etabs.Model.Files.NewGridOnlyModel(
                numberStories, 
                typicalStoryHeight, 
                bottomStoryHeight,
                numberLineX, 
                numberLineY, 
                spacingX, 
                spacingY);
            
            return JsonSerializer.Serialize(new
            {
                success = result == 0,
                message = result == 0 
                    ? $"Grid model created: {numberStories} stories, {numberLineX}x{numberLineY} grid" 
                    : $"Failed to create grid model. Error code: {result}",
                parameters = new
                {
                    stories = numberStories,
                    typicalHeight = typicalStoryHeight,
                    bottomHeight = bottomStoryHeight,
                    gridX = numberLineX,
                    gridY = numberLineY,
                    spacingX = spacingX,
                    spacingY = spacingY
                }
            });
        }
        catch (Exception ex)
        {
            return JsonSerializer.Serialize(new
            {
                success = false,
                error = ex.Message
            });
        }
    }

    [McpServerTool]
    [Description("Add a concrete material to the model")]
    public static string AddConcreteMaterial(
        [Description("Name for the concrete material")] string name,
        [Description("Compressive strength (fc) in current units")] double fc,
        [Description("Modulus of elasticity in current units")] double ec)
    {
        try
        {
            var etabs = ETABSWrapper.Connect();
            if (etabs == null)
            {
                return JsonSerializer.Serialize(new
                {
                    success = false,
                    error = "No active ETABS instance found."
                });
            }

            var material = etabs.Model.Materials.AddConcreteMaterial(name, fc, ec);
            
            return JsonSerializer.Serialize(new
            {
                success = true,
                message = $"Concrete material '{material.Name}' created successfully",
                material = new
                {
                    name = material.Name,
                    fc = material.Fc,
                    ec = material.IsotropicProps.E,
                    type = "Concrete"
                }
            });
        }
        catch (Exception ex)
        {
            return JsonSerializer.Serialize(new
            {
                success = false,
                error = ex.Message
            });
        }
    }

    [McpServerTool]
    [Description("Add a steel material to the model")]
    public static string AddSteelMaterial(
        [Description("Name for the steel material")] string name,
        [Description("Yield stress (Fy) in current units")] double fy,
        [Description("Ultimate stress (Fu) in current units")] double fu)
    {
        try
        {
            var etabs = ETABSWrapper.Connect();
            if (etabs == null)
            {
                return JsonSerializer.Serialize(new
                {
                    success = false,
                    error = "No active ETABS instance found."
                });
            }

            var material = etabs.Model.Materials.AddSteelMaterial(name, fy, fu);
            
            return JsonSerializer.Serialize(new
            {
                success = true,
                message = $"Steel material '{material.Name}' created successfully",
                material = new
                {
                    name = material.Name,
                    fy = material.Fy,
                    fu = material.Fu,
                    type = "Steel"
                }
            });
        }
        catch (Exception ex)
        {
            return JsonSerializer.Serialize(new
            {
                success = false,
                error = ex.Message
            });
        }
    }

    [McpServerTool]
    [Description("Add a rectangular frame section (for beams or columns)")]
    public static string AddRectangularSection(
        [Description("Name for the section")] string name,
        [Description("Material name (must exist in model)")] string materialName,
        [Description("Section depth (local 3-axis) in current units")] double depth,
        [Description("Section width (local 2-axis) in current units")] double width)
    {
        try
        {
            var etabs = ETABSWrapper.Connect();
            if (etabs == null)
            {
                return JsonSerializer.Serialize(new
                {
                    success = false,
                    error = "No active ETABS instance found."
                });
            }

            var section = etabs.Model.PropFrame.AddRectangularSection(
                name, 
                materialName, 
                depth, 
                width);
            
            return JsonSerializer.Serialize(new
            {
                success = true,
                message = $"Rectangular section '{section.Name}' created successfully",
                section = new
                {
                    name = section.Name,
                    material = section.Material,
                    depth = section.Depth,
                    width = section.Width,
                    area = section.Depth * section.Width,
                    type = "Rectangular"
                }
            });
        }
        catch (Exception ex)
        {
            return JsonSerializer.Serialize(new
            {
                success = false,
                error = ex.Message
            });
        }
    }

    [McpServerTool]
    [Description("Add a wall section property")]
    public static string AddWallSection(
        [Description("Name for the wall section")] string name,
        [Description("Material name (must exist in model)")] string materialName,
        [Description("Wall thickness in current units")] double thickness)
    {
        try
        {
            var etabs = ETABSWrapper.Connect();
            if (etabs == null)
            {
                return JsonSerializer.Serialize(new
                {
                    success = false,
                    error = "No active ETABS instance found."
                });
            }

            var wallProperty = new EtabSharp.Properties.Areas.Models.WallProperty
            {
                Name = name,
                MaterialProperty = materialName,
                Thickness = thickness,
                ShellType = eShellType.ShellThin
            };

            var result = etabs.Model.PropArea.SetWall(name, wallProperty);
            
            return JsonSerializer.Serialize(new
            {
                success = result == 0,
                message = result == 0 
                    ? $"Wall section '{name}' created successfully" 
                    : $"Failed to create wall section. Error code: {result}",
                section = new
                {
                    name = name,
                    material = materialName,
                    thickness = thickness,
                    type = "Wall"
                }
            });
        }
        catch (Exception ex)
        {
            return JsonSerializer.Serialize(new
            {
                success = false,
                error = ex.Message
            });
        }
    }

    [McpServerTool]
    [Description("Add a slab section property")]
    public static string AddSlabSection(
        [Description("Name for the slab section")] string name,
        [Description("Material name (must exist in model)")] string materialName,
        [Description("Slab thickness in current units")] double thickness)
    {
        try
        {
            var etabs = ETABSWrapper.Connect();
            if (etabs == null)
            {
                return JsonSerializer.Serialize(new
                {
                    success = false,
                    error = "No active ETABS instance found."
                });
            }

            var slabProperty = new EtabSharp.Properties.Areas.Models.SlabProperty
            {
                Name = name,
                MaterialProperty = materialName,
                Thickness = thickness,
                ShellType = eShellType.ShellThin,
                SlabType = eSlabType.Slab
            };

            var result = etabs.Model.PropArea.SetSlab(name, slabProperty);
            
            return JsonSerializer.Serialize(new
            {
                success = result == 0,
                message = result == 0 
                    ? $"Slab section '{name}' created successfully" 
                    : $"Failed to create slab section. Error code: {result}",
                section = new
                {
                    name = name,
                    material = materialName,
                    thickness = thickness,
                    type = "Slab"
                }
            });
        }
        catch (Exception ex)
        {
            return JsonSerializer.Serialize(new
            {
                success = false,
                error = ex.Message
            });
        }
    }

    [McpServerTool]
    [Description("Add a load pattern to the model")]
    public static string AddLoadPattern(
        [Description("Name for the load pattern")] string name,
        [Description("Type of load: Dead, Live, Wind, Seismic, Snow, etc.")] string loadType,
        [Description("Self-weight multiplier (typically 1.0 for Dead, 0.0 for others)")] double selfWeightMultiplier = 0.0)
    {
        try
        {
            var etabs = ETABSWrapper.Connect();
            if (etabs == null)
            {
                return JsonSerializer.Serialize(new
                {
                    success = false,
                    error = "No active ETABS instance found."
                });
            }

            // Parse load type
            eLoadPatternType patternType = loadType.ToUpper() switch
            {
                "DEAD" => eLoadPatternType.Dead,
                "LIVE" => eLoadPatternType.Live,
                "WIND" => eLoadPatternType.Wind,
                "SEISMIC" or "EARTHQUAKE" => eLoadPatternType.Quake,
                "SNOW" => eLoadPatternType.Snow,
                "TEMPERATURE" => eLoadPatternType.Temperature,
                _ => eLoadPatternType.Other
            };

            var result = etabs.Model.LoadPatterns.Add(name, patternType, selfWeightMultiplier);
            
            return JsonSerializer.Serialize(new
            {
                success = result == 0,
                message = result == 0 
                    ? $"Load pattern '{name}' created successfully" 
                    : $"Failed to create load pattern. Error code: {result}",
                loadPattern = new
                {
                    name = name,
                    type = patternType.ToString(),
                    selfWeightMultiplier = selfWeightMultiplier
                }
            });
        }
        catch (Exception ex)
        {
            return JsonSerializer.Serialize(new
            {
                success = false,
                error = ex.Message
            });
        }
    }

    [McpServerTool]
    [Description("Save the current model to a file")]
    public static string SaveModel(
        [Description("Full file path to save the model (must end with .edb)")] string filePath)
    {
        try
        {
            var etabs = ETABSWrapper.Connect();
            if (etabs == null)
            {
                return JsonSerializer.Serialize(new
                {
                    success = false,
                    error = "No active ETABS instance found."
                });
            }

            var result = etabs.Model.Files.SaveFile(filePath);
            
            return JsonSerializer.Serialize(new
            {
                success = result == 0,
                message = result == 0 
                    ? $"Model saved successfully to '{filePath}'" 
                    : $"Failed to save model. Error code: {result}",
                filePath = filePath
            });
        }
        catch (Exception ex)
        {
            return JsonSerializer.Serialize(new
            {
                success = false,
                error = ex.Message
            });
        }
    }

    [McpServerTool]
    [Description("Set the unit system for the model")]
    public static string SetUnits(
        [Description("Unit system: 'US_Kip_Ft', 'US_Kip_In', 'Metric_kN_m', 'Metric_kN_mm', 'Metric_N_m', 'Metric_N_mm'")] string unitSystem)
    {
        try
        {
            var etabs = ETABSWrapper.Connect();
            if (etabs == null)
            {
                return JsonSerializer.Serialize(new
                {
                    success = false,
                    error = "No active ETABS instance found."
                });
            }

            var units = unitSystem.ToUpper() switch
            {
                "US_KIP_FT" => EtabSharp.System.Models.Units.US_Kip_Ft,
                "US_KIP_IN" => EtabSharp.System.Models.Units.US_Kip_In,
                "METRIC_KN_M" => EtabSharp.System.Models.Units.Metric_kN_m,
                "METRIC_KN_MM" => EtabSharp.System.Models.Units.Metric_kN_mm,
                "METRIC_N_M" => EtabSharp.System.Models.Units.Metric_N_m,
                "METRIC_N_MM" => EtabSharp.System.Models.Units.Metric_N_mm,
                _ => throw new ArgumentException($"Unknown unit system: {unitSystem}")
            };

            var result = etabs.Model.Units.SetPresentUnits(units);
            
            return JsonSerializer.Serialize(new
            {
                success = result == 0,
                message = result == 0 
                    ? $"Units set to {unitSystem}" 
                    : $"Failed to set units. Error code: {result}",
                units = new
                {
                    force = units.Force.ToString(),
                    length = units.Length.ToString(),
                    temperature = units.Temperature.ToString()
                }
            });
        }
        catch (Exception ex)
        {
            return JsonSerializer.Serialize(new
            {
                success = false,
                error = ex.Message
            });
        }
    }

}
