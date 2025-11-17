using EtabSharp.Core;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text.Json;

namespace EtabSharp.Mcp.Tools;

/// <summary>
/// MCP tools for retrieving building information from ETABS model
/// </summary>
[McpServerToolType]
public static class BuildingInformationTools
{
    [McpServerTool]
    [Description("Get comprehensive building information including story count, heights, materials, sections, load patterns, and load cases from the active ETABS model")]
    public static string GetBuildingInformation()
    {
        try
        {
            // Connect to running ETABS instance
            var etabs = ETABSWrapper.Connect();
            if (etabs == null)
            {
                return JsonSerializer.Serialize(new
                {
                    success = false,
                    error = "No active ETABS instance found. Please open ETABS first."
                });
            }

            var model = etabs.Model;
            var buildingInfo = new
            {
                success = true,

                // Model Information
                modelInfo = new
                {
                    filename = model.ModelInfo.GetModelFilepath(),
                    version = model.ModelInfo.GetVersion(),
                    units = new
                    {
                        force = model.Units.GetPresentUnits().Force.ToString(),
                        length = model.Units.GetPresentUnits().Length.ToString(),
                        temperature = model.Units.GetPresentUnits().Temperature.ToString()
                    }
                },

                // Story Information
                stories = GetStoryInformation(model),

                // Materials Summary
                materials = GetMaterialsSummary(model),

                // Frame Sections Summary
                frameSections = GetFrameSectionsSummary(model),

                // Area Sections Summary
                areaSections = GetAreaSectionsSummary(model),

                // Load Patterns
                loadPatterns = GetLoadPatternsSummary(model),

                // Load Cases
                loadCases = GetLoadCasesSummary(model),

                // Load Combinations
                loadCombinations = GetLoadCombinationsSummary(model),

                // Groups
                groups = GetGroupsSummary(model)
            };

            return JsonSerializer.Serialize(buildingInfo, new JsonSerializerOptions
            {
                WriteIndented = true
            });
        }
        catch (Exception ex)
        {
            return JsonSerializer.Serialize(new
            {
                success = false,
                error = ex.Message,
                stackTrace = ex.StackTrace
            });
        }
    }

    private static object GetStoryInformation(ETABSModel model)
    {
        try
        {
            var storyData = model.Story.GetStories();
            var storyNames = storyData.StoryNames;
            var storyHeights = storyData.StoryHeights;
            var storyElevations = storyData.StoryElevations;

            return new
            {
                count = storyNames.Length,
                stories = storyNames.Select((name, index) => new
                {
                    name = name,
                    height = storyHeights[index],
                    elevation = storyElevations[index],
                    isMasterStory = storyData.IsMasterStory[index],
                    similarToStory = storyData.SimilarToStory[index]
                }).ToList(),
                totalHeight = storyElevations.Max() - storyElevations.Min()
            };
        }
        catch (Exception ex)
        {
            return new { error = $"Failed to get story information: {ex.Message}" };
        }
    }

    private static object GetMaterialsSummary(ETABSModel model)
    {
        try
        {
            var allMaterials = model.Materials.GetNameList();
            var materialsByType = new Dictionary<string, List<string>>();

            foreach (var matName in allMaterials)
            {
                var (matType, _, _, _) = model.Materials.GetMaterial(matName);
                var typeName = matType.ToString();

                if (!materialsByType.ContainsKey(typeName))
                    materialsByType[typeName] = new List<string>();

                materialsByType[typeName].Add(matName);
            }

            return new
            {
                totalCount = allMaterials.Length,
                byType = materialsByType.Select(kvp => new
                {
                    type = kvp.Key,
                    count = kvp.Value.Count,
                    materials = kvp.Value
                }).ToList()
            };
        }
        catch (Exception ex)
        {
            return new { error = $"Failed to get materials: {ex.Message}" };
        }
    }

    private static object GetFrameSectionsSummary(ETABSModel model)
    {
        try
        {
            var sections = model.PropFrame.GetNameList();
            var sectionsByType = new Dictionary<string, List<string>>();

            foreach (var section in sections)
            {
                var sectionType = model.PropFrame.GetSectionType(section).ToString();

                if (!sectionsByType.ContainsKey(sectionType))
                    sectionsByType[sectionType] = new List<string>();

                sectionsByType[sectionType].Add(section);
            }

            return new
            {
                totalCount = sections.Length,
                byType = sectionsByType.Select(kvp => new
                {
                    type = kvp.Key,
                    count = kvp.Value.Count,
                    sections = kvp.Value
                }).ToList()
            };
        }
        catch (Exception ex)
        {
            return new { error = $"Failed to get frame sections: {ex.Message}" };
        }
    }

    private static object GetAreaSectionsSummary(ETABSModel model)
    {
        try
        {
            var sections = model.PropArea.GetNameList();
            var sectionsByType = new Dictionary<string, List<string>>();

            foreach (var section in sections)
            {
                var propType = model.PropArea.GetPropertyType(section).ToString();

                if (!sectionsByType.ContainsKey(propType))
                    sectionsByType[propType] = new List<string>();

                sectionsByType[propType].Add(section);
            }

            return new
            {
                totalCount = sections.Length,
                byType = sectionsByType.Select(kvp => new
                {
                    type = kvp.Key,
                    count = kvp.Value.Count,
                    sections = kvp.Value
                }).ToList()
            };
        }
        catch (Exception ex)
        {
            return new { error = $"Failed to get area sections: {ex.Message}" };
        }
    }

    private static object GetLoadPatternsSummary(ETABSModel model)
    {
        try
        {
            var patterns = model.LoadPatterns.GetNameList();
            var patternsByType = new Dictionary<string, List<string>>();

            foreach (var pattern in patterns)
            {
                var loadType = model.LoadPatterns.GetLoadType(pattern).ToString();

                if (!patternsByType.ContainsKey(loadType))
                    patternsByType[loadType] = new List<string>();

                patternsByType[loadType].Add(pattern);
            }

            return new
            {
                totalCount = patterns.Length,
                byType = patternsByType.Select(kvp => new
                {
                    type = kvp.Key,
                    count = kvp.Value.Count,
                    patterns = kvp.Value
                }).ToList()
            };
        }
        catch (Exception ex)
        {
            return new { error = $"Failed to get load patterns: {ex.Message}" };
        }
    }

    private static object GetLoadCasesSummary(ETABSModel model)
    {
        try
        {
            var cases = model.LoadCases.GetNameList();
            var casesByType = new Dictionary<string, List<string>>();

            foreach (var caseName in cases)
            {
                var (caseType, _) = model.LoadCases.GetTypeOAPI(caseName);
                var typeName = caseType.ToString();

                if (!casesByType.ContainsKey(typeName))
                    casesByType[typeName] = new List<string>();

                casesByType[typeName].Add(caseName);
            }

            return new
            {
                totalCount = cases.Length,
                byType = casesByType.Select(kvp => new
                {
                    type = kvp.Key,
                    count = kvp.Value.Count,
                    cases = kvp.Value
                }).ToList()
            };
        }
        catch (Exception ex)
        {
            return new { error = $"Failed to get load cases: {ex.Message}" };
        }
    }

    private static object GetLoadCombinationsSummary(ETABSModel model)
    {
        try
        {
            var combos = model.LoadCombinations.GetNameList();

            return new
            {
                totalCount = combos.Length,
                combinations = combos.ToList()
            };
        }
        catch (Exception ex)
        {
            return new { error = $"Failed to get load combinations: {ex.Message}" };
        }
    }

    private static object GetGroupsSummary(ETABSModel model)
    {
        try
        {
            var groups = model.Groups.GetNameList();
            var groupDetails = groups.Select(groupName =>
            {
                var count = model.Groups.GetAssignmentCount(groupName);
                return new
                {
                    name = groupName,
                    objectCount = count
                };
            }).ToList();

            return new
            {
                totalCount = groups.Length,
                groups = groupDetails
            };
        }
        catch (Exception ex)
        {
            return new { error = $"Failed to get groups: {ex.Message}" };
        }
    }
}