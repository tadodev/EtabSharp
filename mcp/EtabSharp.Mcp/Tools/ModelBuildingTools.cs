using ModelContextProtocol.Server;
using System.ComponentModel;
using EtabSharp.Core;
using System.Text.Json;

namespace EtabSharp.Mcp.Tools;

/// <summary>
/// MCP tools for building ETABS models by adding points, frames, and loads
/// </summary>
public class ModelBuildingTools
{
    [McpServerTool]
    [Description("Add a point (joint/node) to the model at specified coordinates")]
    public string AddPoint(
        [Description("X coordinate")] double x,
        [Description("Y coordinate")] double y,
        [Description("Z coordinate")] double z,
        [Description("Optional custom name for the point")] string name = "")
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
            var pointName = model.Points.AddPoint(x, y, z, name);

            return JsonSerializer.Serialize(new
            {
                success = true,
                pointName = pointName,
                coordinates = new { x, y, z }
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
    [Description("Add a frame element (beam, column, brace) between two points")]
    public string AddFrame(
        [Description("Name of the start point")] string point1,
        [Description("Name of the end point")] string point2,
        [Description("Section name to use (default: 'Default')")] string sectionName = "Default",
        [Description("Optional custom name for the frame")] string name = "")
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
            var frameName = model.Frames.AddFrame(point1, point2, sectionName, name);

            return JsonSerializer.Serialize(new
            {
                success = true,
                frameName = frameName,
                point1 = point1,
                point2 = point2,
                section = sectionName
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
    [Description("Set restraint (support) conditions at a point")]
    public string SetRestraint(
        [Description("Name of the point")] string pointName,
        [Description("Restrain translation in X (true/false)")] bool ux = false,
        [Description("Restrain translation in Y (true/false)")] bool uy = false,
        [Description("Restrain translation in Z (true/false)")] bool uz = false,
        [Description("Restrain rotation about X (true/false)")] bool rx = false,
        [Description("Restrain rotation about Y (true/false)")] bool ry = false,
        [Description("Restrain rotation about Z (true/false)")] bool rz = false)
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
            var restraint = new EtabSharp.Elements.PointObj.Models.PointRestraint
            {
                Ux = ux,
                Uy = uy,
                Uz = uz,
                Rx = rx,
                Ry = ry,
                Rz = rz
            };

            model.Points.SetRestraint(pointName, restraint);

            return JsonSerializer.Serialize(new
            {
                success = true,
                pointName = pointName,
                restraints = new { ux, uy, uz, rx, ry, rz }
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
    [Description("Add a point load to a joint")]
    public string AddPointLoad(
        [Description("Name of the point")] string pointName,
        [Description("Load pattern name")] string loadPattern,
        [Description("Force in X direction")] double fx = 0,
        [Description("Force in Y direction")] double fy = 0,
        [Description("Force in Z direction")] double fz = 0,
        [Description("Moment about X axis")] double mx = 0,
        [Description("Moment about Y axis")] double my = 0,
        [Description("Moment about Z axis")] double mz = 0)
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
            var load = new EtabSharp.Elements.PointObj.Models.PointLoad
            {
                PointName = pointName,
                LoadPattern = loadPattern,
                Fx = fx,
                Fy = fy,
                Fz = fz,
                Mx = mx,
                My = my,
                Mz = mz
            };

            model.Points.SetLoadForce(pointName, load);

            return JsonSerializer.Serialize(new
            {
                success = true,
                pointName = pointName,
                loadPattern = loadPattern,
                forces = new { fx, fy, fz },
                moments = new { mx, my, mz }
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
    [Description("Add a distributed load to a frame element")]
    public string AddFrameDistributedLoad(
        [Description("Name of the frame")] string frameName,
        [Description("Load pattern name")] string loadPattern,
        [Description("Load value (force per length)")] double loadValue,
        [Description("Direction: 1=Local1, 2=Local2, 3=Local3, 4=X, 5=Y, 6=Z, 7=Gravity")] int direction = 6,
        [Description("Distance from I-end to start of load (relative 0-1)")] double distanceA = 0,
        [Description("Distance from I-end to end of load (relative 0-1)")] double distanceB = 1)
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

            // Direction: 1=Local1, 2=Local2, 3=Local3, 4=GlobalX, 5=GlobalY, 6=GlobalZ, 7=Gravity
            model.Frames.SetLoadDistributed(frameName, loadPattern, 1, direction, distanceA, distanceB, loadValue, loadValue, "Global", true, false, ETABSv1.eItemType.Objects);

            return JsonSerializer.Serialize(new
            {
                success = true,
                frameName = frameName,
                loadPattern = loadPattern,
                loadValue = loadValue,
                direction = direction,
                range = new { start = distanceA, end = distanceB }
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
}
