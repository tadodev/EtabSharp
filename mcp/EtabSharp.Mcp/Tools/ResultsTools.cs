using EtabSharp.Core;
using EtabSharp.Loads.LoadCases.Models;
using ETABSv1;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text.Json;


namespace EtabSharp.Mcp.Tools;

/// <summary>
/// MCP tools for retrieving analysis results from ETABS
/// </summary>
public class ResultsTools
{
    [McpServerTool]
    [Description("Get base reactions for all load cases and combinations including forces and moments at the base of the structure")]
    public string GetBaseReactions()
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
            //setup all load case for base reaction retrieval
            var cases = model.LoadCases.GetAllLoadCases();

            foreach (LoadCase loadCase in cases)
            {
                model.AnalysisResultsSetup.SetCaseSelectedForOutput(loadCase.Name);
            }

            var results = model.AnalysisResults.GetBaseReact();

            var reactions = results.Results.Select(r => new
            {
                loadCase = r.LoadCase,
                stepType = r.StepType,
                stepNumber = r.StepNum,
                forces = new
                {
                    fx = r.FX,
                    fy = r.FY,
                    fz = r.FZ
                },
                moments = new
                {
                    mx = r.MX,
                    my = r.MY,
                    mz = r.MZ
                }
            }).ToList();

            return JsonSerializer.Serialize(new
            {
                success = true,
                count = reactions.Count,
                reactions = reactions
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
    [Description("Get joint displacements for specified points or all points in the model")]
    public string GetJointDisplacements(
        [Description("Name of specific joint/point, or 'all' for all joints")] string pointName = "all")
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
            var name = pointName.ToLower() == "all" ? "" : pointName;
            var results = model.AnalysisResults.GetJointDispl(name, eItemTypeElm.ObjectElm);

            var displacements = results.Results.Take(200).Select(r => new
            {
                point = r.ObjectName,
                element = r.ElementName,
                loadCase = r.LoadCase,
                stepType = r.StepType,
                stepNumber = r.StepNum,
                displacements = new
                {
                    ux = r.U1,
                    uy = r.U2,
                    uz = r.U3,
                    rx = r.R1,
                    ry = r.R2,
                    rz = r.R3
                }
            }).ToList();

            return JsonSerializer.Serialize(new
            {
                success = true,
                totalResults = results.Results.Count,
                resultsShown = displacements.Count,
                note = results.Results.Count > 200 ? "Showing first 200 results only" : "All results shown",
                displacements = displacements
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
    [Description("Get frame forces for specified frame elements including axial, shear, and moment values")]
    public string GetFrameForces(
        [Description("Name of specific frame element, or 'all' for all frames")] string frameName = "all")
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
            var name = frameName.ToLower() == "all" ? "" : frameName;
            var results = model.AnalysisResults.GetFrameForce(name, eItemTypeElm.ObjectElm);

            var forces = results.Results.Take(200).Select(r => new
            {
                frame = r.ObjectName,
                element = r.ElementName,
                loadCase = r.LoadCase,
                stepType = r.StepType,
                objectStation = r.ObjectStation,
                elementStation = r.ElementStation,
                forces = new
                {
                    axial = r.P,
                    shear2 = r.V2,
                    shear3 = r.V3,
                    torsion = r.T,
                    moment2 = r.M2,
                    moment3 = r.M3
                }
            }).ToList();

            return JsonSerializer.Serialize(new
            {
                success = true,
                totalResults = results.Results.Count,
                resultsShown = forces.Count,
                note = results.Results.Count > 200 ? "Showing first 200 results only" : "All results shown",
                forces = forces
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
    [Description("Get modal analysis results including periods, frequencies, and participation factors")]
    public string GetModalResults()
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

            // Get modal periods
            var periods = model.AnalysisResults.GetModalPeriod();

            // Get participation factors
            var participation = model.AnalysisResults.GetModalParticipationFactors();

            // Get participating mass ratios
            var massRatios = model.AnalysisResults.GetModalParticipatingMassRatios();

            var modes = periods.Results.Select(r => new
            {
                loadCase = r.LoadCase,
                mode = r.StepNum,
                period = r.Period,
                frequency = r.Frequency,
                circularFrequency = r.CircularFrequency,
                eigenvalue = r.EigenValue
            }).ToList();

            var participationFactors = participation.Results.Select(r => new
            {
                loadCase = r.LoadCase,
                mode = r.StepNum,
                period = r.Period,
                participationFactors = new
                {
                    ux = r.UX,
                    uy = r.UY,
                    uz = r.UZ,
                    rx = r.RX,
                    ry = r.RY,
                    rz = r.RZ
                }
            }).ToList();

            var massRatiosList = massRatios.Results.Select(r => new
            {
                loadCase = r.LoadCase,
                mode = r.StepNum,
                period = r.Period,
                massRatios = new
                {
                    ux = r.UX,
                    uy = r.UY,
                    uz = r.UZ,
                    sumUx = r.SumUX,
                    sumUy = r.SumUY,
                    sumUz = r.SumUZ
                }
            }).ToList();

            return JsonSerializer.Serialize(new
            {
                success = true,
                totalModes = modes.Count,
                modes = modes,
                participationFactors = participationFactors,
                massRatios = massRatiosList
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
    [Description("Get story drifts for all stories including maximum drift ratios")]
    public string GetStoryDrifts()
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
            var results = model.AnalysisResults.GetStoryDrifts();

            var drifts = results.Results.Select(r => new
            {
                story = r.Story,
                loadCase = r.LoadCase,
                stepType = r.StepType,
                direction = r.Direction,
                drift = r.Drift,
                label = r.Label,
                location = new
                {
                    x = r.X,
                    y = r.Y,
                    z = r.Z
                }
            }).ToList();

            return JsonSerializer.Serialize(new
            {
                success = true,
                count = drifts.Count,
                drifts = drifts
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