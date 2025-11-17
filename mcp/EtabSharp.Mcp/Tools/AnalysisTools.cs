using ModelContextProtocol.Server;
using System.ComponentModel;
using EtabSharp.Core;
using System.Text.Json;

namespace EtabSharp.Mcp.Tools;

/// <summary>
/// MCP tools for running analysis and checking analysis status
/// </summary>
[McpServerToolType]
internal static class AnalysisTools
{
    [McpServerTool]
    [Description("Get the current analysis status for all load cases including which cases have been run and their completion status")]
    public static string GetAnalysisStatus()
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
            var caseStatus = model.Analyze.GetCaseStatus();
            var runFlags = model.Analyze.GetRunCaseFlags();

            var statusInfo = caseStatus.Select(status =>
            {
                var runFlag = runFlags.FirstOrDefault(rf => rf.CaseName == status.CaseName);
                return new
                {
                    caseName = status.CaseName,
                    status = GetStatusName(status.Status),
                    isSetToRun = runFlag?.Run ?? false,
                    statusCode = status.Status
                };
            }).ToList();

            var summary = new
            {
                notRun = statusInfo.Count(s => s.status == "Not Run"),
                couldNotStart = statusInfo.Count(s => s.status == "Could Not Start"),
                notFinished = statusInfo.Count(s => s.status == "Not Finished"),
                finished = statusInfo.Count(s => s.status == "Finished")
            };

            return JsonSerializer.Serialize(new
            {
                success = true,
                totalCases = statusInfo.Count,
                summary = summary,
                cases = statusInfo
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
    [Description("Run the analysis for the ETABS model. This will create the analysis model and run all cases set to run.")]
    public static string RunAnalysis()
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
            
            // Create analysis model first
            var createResult = model.Analyze.CreateAnalysisModel();
            if (createResult != 0)
            {
                return JsonSerializer.Serialize(new
                {
                    success = false,
                    error = $"Failed to create analysis model. Error code: {createResult}"
                });
            }

            // Run analysis
            var runResult = model.Analyze.RunAnalysis();
            if (runResult != 0)
            {
                return JsonSerializer.Serialize(new
                {
                    success = false,
                    error = $"Failed to run analysis. Error code: {runResult}"
                });
            }

            // Get final status
            var caseStatus = model.Analyze.GetCaseStatus();
            var finished = caseStatus.Count(s => s.Status == 2); // 2 = Finished
            var total = caseStatus.Count;

            return JsonSerializer.Serialize(new
            {
                success = true,
                message = "Analysis completed successfully",
                casesFinished = finished,
                totalCases = total,
                allFinished = finished == total
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
    [Description("Set which load cases should run during analysis. Provide comma-separated case names, or use 'all' to run all cases.")]
    public static string SetCasesToRun(
        [Description("Comma-separated list of case names to run, or 'all' for all cases")] string caseNames)
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
            
            if (caseNames.Trim().ToLower() == "all")
            {
                model.Analyze.SetAllCasesToRun();
                var totalCases = model.LoadCases.GetNameList().Length;
                
                return JsonSerializer.Serialize(new
                {
                    success = true,
                    message = $"Set all {totalCases} cases to run"
                });
            }
            else
            {
                var cases = caseNames.Split(',').Select(c => c.Trim()).ToArray();
                int setCount = 0;
                
                foreach (var caseName in cases)
                {
                    var result = model.Analyze.SetRunCaseFlag(caseName, true, false);
                    if (result == 0)
                        setCount++;
                }

                return JsonSerializer.Serialize(new
                {
                    success = true,
                    message = $"Set {setCount} of {cases.Length} cases to run",
                    casesSet = setCount,
                    casesRequested = cases.Length
                });
            }
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
    [Description("Delete analysis results for all cases or specific cases")]
    public static string DeleteResults(
        [Description("Name of specific case to delete results for, or 'all' to delete all results")] string caseName = "all")
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
            
            if (caseName.Trim().ToLower() == "all")
            {
                var result = model.Analyze.DeleteResults("", true);
                
                return JsonSerializer.Serialize(new
                {
                    success = result == 0,
                    message = result == 0 ? "All analysis results deleted successfully" : $"Failed to delete results. Error code: {result}"
                });
            }
            else
            {
                var result = model.Analyze.DeleteResults(caseName, false);
                
                return JsonSerializer.Serialize(new
                {
                    success = result == 0,
                    message = result == 0 ? $"Results for case '{caseName}' deleted successfully" : $"Failed to delete results. Error code: {result}"
                });
            }
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

    private static string GetStatusName(int statusCode)
    {
        return statusCode switch
        {
            0 => "Not Run",
            1 => "Could Not Start",
            2 => "Finished",
            3 => "Not Finished",
            _ => "Unknown"
        };
    }
}