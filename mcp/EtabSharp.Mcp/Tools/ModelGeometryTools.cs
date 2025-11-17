
using ModelContextProtocol.Server;
using System.ComponentModel;
using EtabSharp.Core;
using System.Text.Json;

namespace EtabSharp.Mcp.Tools;

/// <summary>
/// MCP tools for retrieving model geometry information from ETABS
/// </summary>
[McpServerToolType]
public static class ModelGeometryTools
{
    [McpServerTool]
    [Description("Get detailed information about all points (joints/nodes) in the ETABS model including coordinates, restraints, and connectivity")]
    public static string GetPointsInformation()
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

            var model = etabs.Model;
            var pointNames = model.Points.GetNameList();

            List<object?> pointsInfo = pointNames.Take(100).Select(name => // Limit to 100 for performance
            {
                try
                {
                    var point = model.Points.GetPoint(name);
                    var restraint = model.Points.GetRestraint(name);
                    var connectivity = model.Points.GetConnectedObjects(name);

                    return (object?)new
                    {
                        name = name,
                        coordinates = new
                        {
                            x = point.X,
                            y = point.Y,
                            z = point.Z
                        },
                        hasRestraint = restraint != null,
                        restraintDOFs = restraint != null ? new
                        {
                            ux = restraint.Ux,
                            uy = restraint.Uy,
                            uz = restraint.Uz,
                            rx = restraint.Rx,
                            ry = restraint.Ry,
                            rz = restraint.Rz
                        } : null,
                        connectivity = new
                        {
                            frameCount = connectivity.ConnectedFrames?.Count ?? 0,
                            areaCount = connectivity.ConnectedAreas?.Count ?? 0,
                            linkCount = connectivity.ConnectedLinks?.Count ?? 0
                        }
                    };
                }
                catch
                {
                    return null;
                }
            }).Where(p => p != null).ToList();

            return JsonSerializer.Serialize(new
            {
                success = true,
                totalPoints = pointNames.Length,
                pointsShown = pointsInfo.Count,
                note = pointNames.Length > 100 ? "Showing first 100 points only" : "All points shown",
                points = pointsInfo
            }, new JsonSerializerOptions { WriteIndented = true });
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
    [Description("Get detailed information about frame elements (beams, columns, braces) including sections, materials, end releases, and loads")]
    public static string GetFrameElementsInformation()
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

            var model = etabs.Model;
            var frameNames = model.Frames.GetNameList();

            List<object?> framesInfo = frameNames.Take(100).Select(name =>
            {
                try
                {
                    var frame = model.Frames.GetFrame(name);
                    var (sectionName, _) = model.Frames.GetSection(name);
                    var releases = model.Frames.GetReleases(name);
                    var designProc = model.Frames.GetDesignProcedure(name);
                    var pier = model.Frames.GetPier(name);
                    var spandrel = model.Frames.GetSpandrel(name);

                    return (object?)new
                    {
                        name = name,
                        pointI = frame.Point1Name,
                        pointJ = frame.Point2Name,
                        section = sectionName,
                        material = "N/A", // Material is in section property, not frame
                        length = frame.Length,
                        designType = GetDesignTypeName(designProc),
                        pier = string.IsNullOrEmpty(pier) ? "None" : pier,
                        spandrel = string.IsNullOrEmpty(spandrel) ? "None" : spandrel,
                        hasEndReleases = releases != null,
                        endReleases = releases != null ? new
                        {
                            iEnd = new
                            {
                                axial = releases.IEndReleases.U1,
                                shear2 = releases.IEndReleases.U2,
                                shear3 = releases.IEndReleases.U3,
                                torsion = releases.IEndReleases.R1,
                                moment2 = releases.IEndReleases.R2,
                                moment3 = releases.IEndReleases.R3
                            },
                            jEnd = new
                            {
                                axial = releases.JEndReleases.U1,
                                shear2 = releases.JEndReleases.U2,
                                shear3 = releases.JEndReleases.U3,
                                torsion = releases.JEndReleases.R1,
                                moment2 = releases.JEndReleases.R2,
                                moment3 = releases.JEndReleases.R3
                            }
                        } : null
                    };
                }
                catch
                {
                    return null;
                }
            }).Where(f => f != null).ToList();

            return JsonSerializer.Serialize(new
            {
                success = true,
                totalFrames = frameNames.Length,
                framesShown = framesInfo.Count,
                note = frameNames.Length > 100 ? "Showing first 100 frames only" : "All frames shown",
                frames = framesInfo
            }, new JsonSerializerOptions { WriteIndented = true });
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
    [Description("Get detailed information about area elements (slabs, walls, ramps) including properties, assignments, and mesh information")]
    public static string GetAreaElementsInformation()
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

            var model = etabs.Model;
            var areaNames = model.Areas.GetNameList();

            List<object?> areasInfo = areaNames.Take(100).Select(name =>
            {
                try
                {
                    var area = model.Areas.GetArea(name);
                    var property = model.Areas.GetProperty(name);
                    var pier = model.Areas.GetPier(name);
                    var spandrel = model.Areas.GetSpandrel(name);
                    var diaphragm = model.Areas.GetDiaphragm(name);
                    var isOpening = model.Areas.GetOpening(name);

                    return (object?)new
                    {
                        name = name,
                        property = property,
                        numberOfPoints = area.PointNames?.Count ?? 0,
                        pier = string.IsNullOrEmpty(pier) ? "None" : pier,
                        spandrel = string.IsNullOrEmpty(spandrel) ? "None" : spandrel,
                        diaphragm = string.IsNullOrEmpty(diaphragm) ? "None" : diaphragm,
                        isOpening = isOpening,
                        area = area.AreaValue,
                        story = area.Story
                    };
                }
                catch
                {
                    return null;
                }
            }).Where(a => a != null).ToList();

            return JsonSerializer.Serialize(new
            {
                success = true,
                totalAreas = areaNames.Length,
                areasShown = areasInfo.Count,
                note = areaNames.Length > 100 ? "Showing first 100 areas only" : "All areas shown",
                areas = areasInfo
            }, new JsonSerializerOptions { WriteIndented = true });
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
    [Description("Get elements on a specific story level including frames, areas, and points")]
    public static string GetElementsOnStory(
        [Description("Name of the story (e.g., 'Story1', 'Story2', 'Roof')")] string storyName)
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

            var model = etabs.Model;

            var points = model.Points.GetPointsOnStory(storyName);
            var frames = model.Frames.GetFramesOnStory(storyName);
            var areas = model.Areas.GetAreasOnStory(storyName);

            return JsonSerializer.Serialize(new
            {
                success = true,
                story = storyName,
                elements = new
                {
                    pointCount = points.Length,
                    frameCount = frames.Length,
                    areaCount = areas.Length,
                    points = points.Take(50).ToList(),
                    frames = frames.Take(50).ToList(),
                    areas = areas.Take(50).ToList()
                }
            }, new JsonSerializerOptions { WriteIndented = true });
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

    private static string GetDesignTypeName(int designType)
    {
        return designType switch
        {
            0 => "No Design",
            1 => "Column",
            2 => "Beam",
            3 => "Brace",
            _ => "Unknown"
        };
    }
}