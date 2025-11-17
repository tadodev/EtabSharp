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
[McpServerToolType]
public static class ResultsTools
{
    /// <summary>
    /// Ensures cases and combos are selected for output before retrieving results.
    /// If specific names are provided, selects those; otherwise selects all.
    /// </summary>
    private static void EnsureOutputSetup(ETABSApplication etabs, string[] caseNames = null, string[] comboNames = null)
    {
        var model = etabs.Model;

        // If specific cases provided, select them; otherwise select all
        if (caseNames != null && caseNames.Length > 0)
        {
            foreach (var caseName in caseNames)
            {
                model.AnalysisResultsSetup.SetCaseSelectedForOutput(caseName, true);
            }
        }
        else
        {
            model.AnalysisResultsSetup.SetCaseSelectedForOutput("all", true);
        }

        // If specific combos provided, select them; otherwise select all
        if (comboNames != null && comboNames.Length > 0)
        {
            foreach (var comboName in comboNames)
            {
                model.AnalysisResultsSetup.SetComboSelectedForOutput(comboName, true);
            }
        }
        else
        {
            model.AnalysisResultsSetup.SetComboSelectedForOutput("all", true);
        }
    }

    [McpServerTool]
    [Description("Get base reactions for all load cases and combinations including forces and moments at the base of the structure")]
    public static string GetBaseReactions(
        [Description("Comma-separated list of specific load case names, or 'all' for all cases")] string cases = "all",
        [Description("Comma-separated list of specific combo names, or 'all' for all combos")] string combos = "all")
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

            // Parse input parameters
            string[] caseNames = null;
            string[] comboNames = null;

            if (!string.IsNullOrEmpty(cases) && !cases.Equals("all", StringComparison.OrdinalIgnoreCase))
            {
                caseNames = cases.Split(',').Select(c => c.Trim()).Where(c => !string.IsNullOrEmpty(c)).ToArray();
            }

            if (!string.IsNullOrEmpty(combos) && !combos.Equals("all", StringComparison.OrdinalIgnoreCase))
            {
                comboNames = combos.Split(',').Select(c => c.Trim()).Where(c => !string.IsNullOrEmpty(c)).ToArray();
            }

            // Ensure output setup
            EnsureOutputSetup(etabs, caseNames, comboNames);

            // Get results
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
                setupInfo = new
                {
                    casesSelected = caseNames?.Length ?? model.LoadCases.Count(),
                    combosSelected = comboNames?.Length ?? model.LoadCombinations.Count()
                },
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
    public static string GetJointDisplacements(
        [Description("Name of specific joint/point, or 'all' for all joints")] string pointName = "all",
        [Description("Comma-separated list of specific load case names, or 'all' for all cases")] string cases = "all",
        [Description("Comma-separated list of specific combo names, or 'all' for all combos")] string combos = "all")
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

            // Parse input parameters
            string[] caseNames = null;
            string[] comboNames = null;

            if (!string.IsNullOrEmpty(cases) && !cases.Equals("all", StringComparison.OrdinalIgnoreCase))
            {
                caseNames = cases.Split(',').Select(c => c.Trim()).Where(c => !string.IsNullOrEmpty(c)).ToArray();
            }

            if (!string.IsNullOrEmpty(combos) && !combos.Equals("all", StringComparison.OrdinalIgnoreCase))
            {
                comboNames = combos.Split(',').Select(c => c.Trim()).Where(c => !string.IsNullOrEmpty(c)).ToArray();
            }

            // Ensure output setup
            EnsureOutputSetup(etabs, caseNames, comboNames);

            // Get results
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
                setupInfo = new
                {
                    casesSelected = caseNames?.Length ?? model.LoadCases.Count(),
                    combosSelected = comboNames?.Length ?? model.LoadCombinations.Count()
                },
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
    public static string GetFrameForces(
        [Description("Name of specific frame element, or 'all' for all frames")] string frameName = "all",
        [Description("Comma-separated list of specific load case names, or 'all' for all cases")] string cases = "all",
        [Description("Comma-separated list of specific combo names, or 'all' for all combos")] string combos = "all")
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

            // Parse input parameters
            string[] caseNames = null;
            string[] comboNames = null;

            if (!string.IsNullOrEmpty(cases) && !cases.Equals("all", StringComparison.OrdinalIgnoreCase))
            {
                caseNames = cases.Split(',').Select(c => c.Trim()).Where(c => !string.IsNullOrEmpty(c)).ToArray();
            }

            if (!string.IsNullOrEmpty(combos) && !combos.Equals("all", StringComparison.OrdinalIgnoreCase))
            {
                comboNames = combos.Split(',').Select(c => c.Trim()).Where(c => !string.IsNullOrEmpty(c)).ToArray();
            }

            // Ensure output setup
            EnsureOutputSetup(etabs, caseNames, comboNames);

            // Get results
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
                setupInfo = new
                {
                    casesSelected = caseNames?.Length ?? model.LoadCases.Count(),
                    combosSelected = comboNames?.Length ?? model.LoadCombinations.Count()
                },
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
    public static string GetModalResults(
        [Description("Comma-separated list of specific modal case names, or 'all' for all modal cases")] string modalCases = "all")
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

            // Parse input parameters
            string[] caseNames = null;

            if (!string.IsNullOrEmpty(modalCases) && !modalCases.Equals("all", StringComparison.OrdinalIgnoreCase))
            {
                caseNames = modalCases.Split(',').Select(c => c.Trim()).Where(c => !string.IsNullOrEmpty(c)).ToArray();
            }

            // Ensure output setup for modal cases only
            EnsureOutputSetup(etabs, caseNames, null);

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
                setupInfo = new
                {
                    modalCasesSelected = caseNames?.Length ?? model.LoadCases.GetLoadCasesByType(eLoadCaseType.Modal).Count
                },
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
    public static string GetStoryDrifts(
        [Description("Comma-separated list of specific load case names, or 'all' for all cases")] string cases = "all",
        [Description("Comma-separated list of specific combo names, or 'all' for all combos")] string combos = "all")
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

            // Parse input parameters
            string[] caseNames = null;
            string[] comboNames = null;

            if (!string.IsNullOrEmpty(cases) && !cases.Equals("all", StringComparison.OrdinalIgnoreCase))
            {
                caseNames = cases.Split(',').Select(c => c.Trim()).Where(c => !string.IsNullOrEmpty(c)).ToArray();
            }

            if (!string.IsNullOrEmpty(combos) && !combos.Equals("all", StringComparison.OrdinalIgnoreCase))
            {
                comboNames = combos.Split(',').Select(c => c.Trim()).Where(c => !string.IsNullOrEmpty(c)).ToArray();
            }

            // Ensure output setup
            EnsureOutputSetup(etabs, caseNames, comboNames);

            // Get results
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
                setupInfo = new
                {
                    casesSelected = caseNames?.Length ?? model.LoadCases.Count(),
                    combosSelected = comboNames?.Length ?? model.LoadCombinations.Count()
                },
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

    [McpServerTool]
    [Description("Set specific load cases and combinations for output or select all if not specified")]
    public static string SetOutputSelection(
        [Description("Comma-separated list of load case names to select, or 'all' for all cases")] string cases = "all",
        [Description("Comma-separated list of combo names to select, or 'all' for all combos")] string combos = "all",
        [Description("Set to false to deselect instead of select")] bool select = true)
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
            int casesProcessed = 0;
            int combosProcessed = 0;

            // Process cases
            if (!string.IsNullOrEmpty(cases))
            {
                if (cases.Equals("all", StringComparison.OrdinalIgnoreCase))
                {
                    casesProcessed = model.AnalysisResultsSetup.SetCaseSelectedForOutput("all", select);
                }
                else
                {
                    var caseNames = cases.Split(',').Select(c => c.Trim()).Where(c => !string.IsNullOrEmpty(c)).ToArray();
                    foreach (var caseName in caseNames)
                    {
                        var result = model.AnalysisResultsSetup.SetCaseSelectedForOutput(caseName, select);
                        if (result == 0) casesProcessed++;
                    }
                }
            }

            // Process combos
            if (!string.IsNullOrEmpty(combos))
            {
                if (combos.Equals("all", StringComparison.OrdinalIgnoreCase))
                {
                    combosProcessed = model.AnalysisResultsSetup.SetComboSelectedForOutput("all", select);
                }
                else
                {
                    var comboNames = combos.Split(',').Select(c => c.Trim()).Where(c => !string.IsNullOrEmpty(c)).ToArray();
                    foreach (var comboName in comboNames)
                    {
                        var result = model.AnalysisResultsSetup.SetComboSelectedForOutput(comboName, select);
                        if (result == 0) combosProcessed++;
                    }
                }
            }

            return JsonSerializer.Serialize(new
            {
                success = true,
                action = select ? "selected" : "deselected",
                casesProcessed = casesProcessed,
                combosProcessed = combosProcessed,
                message = $"Successfully {(select ? "selected" : "deselected")} {casesProcessed} cases and {combosProcessed} combos for output"
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