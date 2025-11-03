using EtabSharp.AnalysisResults.Models.AnalysisResults.Areas;
using EtabSharp.AnalysisResults.Models.AnalysisResults.Areas.ShellLayered;
using EtabSharp.AnalysisResults.Models.AnalysisResults.AssembledMass;
using EtabSharp.AnalysisResults.Models.AnalysisResults.BaseReactions;
using EtabSharp.AnalysisResults.Models.AnalysisResults.Bucklings;
using EtabSharp.AnalysisResults.Models.AnalysisResults.Frames;
using EtabSharp.AnalysisResults.Models.AnalysisResults.GeneralizedDisplacements;
using EtabSharp.AnalysisResults.Models.AnalysisResults.Joints;
using EtabSharp.AnalysisResults.Models.AnalysisResults.Links;
using EtabSharp.AnalysisResults.Models.AnalysisResults.Modals;
using EtabSharp.AnalysisResults.Models.AnalysisResults.PanelZones;
using EtabSharp.AnalysisResults.Models.AnalysisResults.Piers;
using EtabSharp.AnalysisResults.Models.AnalysisResults.SectionCuts;
using EtabSharp.AnalysisResults.Models.AnalysisResults.Spandrels;
using EtabSharp.AnalysisResults.Models.AnalysisResults.StoryResults;
using EtabSharp.Interfaces.AnalysisResults;
using ETABSv1;

namespace EtabSharp.Interfaces;

/// <summary>
/// Interface for retrieving analysis results from ETABS model.
/// Provides access to forces, stresses, displacements, reactions, and other analysis outputs.
/// </summary>
public interface IAnalysisResults
{
    #region Setup Property

    /// <summary>
    /// Gets the analysis results setup interface for configuring output options.
    /// </summary>
    IAnalysisResultsSetup Setup { get; }

    #endregion

    #region Area Results

    /// <summary>
    /// Gets shell internal forces for area elements.
    /// Wraps cSapModel.Results.AreaForceShell.
    /// </summary>
    /// <param name="name">Name of area object or group (use empty string "" for all)</param>
    /// <param name="itemTypeElm">Element type filter</param>
    /// <returns>AreaForceShellResults containing all force data</returns>
    AreaForceShellResults GetAreaForceShell(string name, eItemTypeElm itemTypeElm);

    /// <summary>
    /// Gets joint forces for area elements.
    /// Wraps cSapModel.Results.AreaJointForceShell.
    /// </summary>
    /// <param name="name">Name of area object or group (use empty string "" for all)</param>
    /// <param name="itemTypeElm">Element type filter</param>
    /// <returns>AreaJointForceResults containing joint force data</returns>
    AreaJointForceResults GetAreaJointForceShell(string name, eItemTypeElm itemTypeElm);

    /// <summary>
    /// Gets shell stresses for area elements.
    /// Wraps cSapModel.Results.AreaStressShell.
    /// </summary>
    /// <param name="name">Name of area object or group (use empty string "" for all)</param>
    /// <param name="itemTypeElm">Element type filter</param>
    /// <returns>AreaStressShellResults containing stress data</returns>
    AreaStressShellResults GetAreaStressShell(string name, eItemTypeElm itemTypeElm);

    /// <summary>
    /// Gets layered shell stresses for area elements.
    /// Wraps cSapModel.Results.AreaStressShellLayered.
    /// </summary>
    /// <param name="name">Name of area object or group (use empty string "" for all)</param>
    /// <param name="itemTypeElm">Element type filter</param>
    /// <returns>AreaStressShellLayeredResults containing layered stress data</returns>
    AreaStressShellLayeredResults GetAreaStressShellLayered(string name, eItemTypeElm itemTypeElm);

    /// <summary>
    /// Gets shell strains for area elements.
    /// Wraps cSapModel.Results.AreaStrainShell.
    /// </summary>
    /// <param name="name">Name of area object or group (use empty string "" for all)</param>
    /// <param name="itemTypeElm">Element type filter</param>
    /// <returns>AreaStrainShellResults containing strain data</returns>
    AreaStrainShellResults GetAreaStrainShell(string name, eItemTypeElm itemTypeElm);

    /// <summary>
    /// Gets layered shell strains for area elements.
    /// Wraps cSapModel.Results.AreaStrainShellLayered.
    /// </summary>
    /// <param name="name">Name of area object or group (use empty string "" for all)</param>
    /// <param name="itemTypeElm">Element type filter</param>
    /// <returns>AreaStrainShellLayeredResults containing layered strain data</returns>
    AreaStrainShellLayeredResults GetAreaStrainShellLayered(string name, eItemTypeElm itemTypeElm);

    #endregion

    #region Frame Results

    /// <summary>
    /// Gets frame element forces.
    /// Wraps cSapModel.Results.FrameForce.
    /// </summary>
    /// <param name="name">Name of frame object or group (use empty string "" for all)</param>
    /// <param name="itemTypeElm">Element type filter</param>
    /// <returns>FrameForceResults containing force data</returns>
    FrameForceResults GetFrameForce(string name, eItemTypeElm itemTypeElm);

    /// <summary>
    /// Gets frame joint forces.
    /// Wraps cSapModel.Results.FrameJointForce.
    /// </summary>
    /// <param name="name">Name of frame object or group (use empty string "" for all)</param>
    /// <param name="itemTypeElm">Element type filter</param>
    /// <returns>FrameJointForceResults containing joint force data</returns>
    FrameJointForceResults GetFrameJointForce(string name, eItemTypeElm itemTypeElm);

    #endregion

    #region Joint Results

    /// <summary>
    /// Gets joint displacements.
    /// Wraps cSapModel.Results.JointDispl.
    /// </summary>
    /// <param name="name">Name of joint or group (use empty string "" for all)</param>
    /// <param name="itemTypeElm">Element type filter</param>
    /// <returns>JointDisplacementResults containing displacement data</returns>
    JointDisplacementResults GetJointDispl(string name, eItemTypeElm itemTypeElm);

    /// <summary>
    /// Gets absolute joint displacements.
    /// Wraps cSapModel.Results.JointDisplAbs.
    /// </summary>
    /// <param name="name">Name of joint or group (use empty string "" for all)</param>
    /// <param name="itemTypeElm">Element type filter</param>
    /// <returns>JointDisplacementResults containing absolute displacement data</returns>
    JointDisplacementResults GetJointDisplAbs(string name, eItemTypeElm itemTypeElm);

    /// <summary>
    /// Gets joint velocities.
    /// Wraps cSapModel.Results.JointVel.
    /// </summary>
    /// <param name="name">Name of joint or group (use empty string "" for all)</param>
    /// <param name="itemTypeElm">Element type filter</param>
    /// <returns>JointVelocityResults containing velocity data</returns>
    JointVelocityResults GetJointVel(string name, eItemTypeElm itemTypeElm);

    /// <summary>
    /// Gets absolute joint velocities.
    /// Wraps cSapModel.Results.JointVelAbs.
    /// </summary>
    /// <param name="name">Name of joint or group (use empty string "" for all)</param>
    /// <param name="itemTypeElm">Element type filter</param>
    /// <returns>JointVelocityResults containing absolute velocity data</returns>
    JointVelocityResults GetJointVelAbs(string name, eItemTypeElm itemTypeElm);

    /// <summary>
    /// Gets joint accelerations.
    /// Wraps cSapModel.Results.JointAcc.
    /// </summary>
    /// <param name="name">Name of joint or group (use empty string "" for all)</param>
    /// <param name="itemTypeElm">Element type filter</param>
    /// <returns>JointAccelerationResults containing acceleration data</returns>
    JointAccelerationResults GetJointAcc(string name, eItemTypeElm itemTypeElm);

    /// <summary>
    /// Gets absolute joint accelerations.
    /// Wraps cSapModel.Results.JointAccAbs.
    /// </summary>
    /// <param name="name">Name of joint or group (use empty string "" for all)</param>
    /// <param name="itemTypeElm">Element type filter</param>
    /// <returns>JointAccelerationResults containing absolute acceleration data</returns>
    JointAccelerationResults GetJointAccAbs(string name, eItemTypeElm itemTypeElm);

    /// <summary>
    /// Gets joint reactions.
    /// Wraps cSapModel.Results.JointReact.
    /// </summary>
    /// <param name="name">Name of joint or group (use empty string "" for all)</param>
    /// <param name="itemTypeElm">Element type filter</param>
    /// <returns>JointReactionResults containing reaction data</returns>
    JointReactionResults GetJointReact(string name, eItemTypeElm itemTypeElm);

    #endregion

    #region Link Results

    /// <summary>
    /// Gets link element deformations.
    /// Wraps cSapModel.Results.LinkDeformation.
    /// </summary>
    /// <param name="name">Name of link object or group (use empty string "" for all)</param>
    /// <param name="itemTypeElm">Element type filter</param>
    /// <returns>LinkDeformationResults containing deformation data</returns>
    LinkDeformationResults GetLinkDeformation(string name, eItemTypeElm itemTypeElm);

    /// <summary>
    /// Gets link element forces.
    /// Wraps cSapModel.Results.LinkForce.
    /// </summary>
    /// <param name="name">Name of link object or group (use empty string "" for all)</param>
    /// <param name="itemTypeElm">Element type filter</param>
    /// <returns>LinkForceResults containing force data</returns>
    LinkForceResults GetLinkForce(string name, eItemTypeElm itemTypeElm);

    /// <summary>
    /// Gets link joint forces.
    /// Wraps cSapModel.Results.LinkJointForce.
    /// </summary>
    /// <param name="name">Name of link object or group (use empty string "" for all)</param>
    /// <param name="itemTypeElm">Element type filter</param>
    /// <returns>LinkJointForceResults containing joint force data</returns>
    LinkJointForceResults GetLinkJointForce(string name, eItemTypeElm itemTypeElm);

    #endregion

    #region Modal Results

    /// <summary>
    /// Gets modal periods and frequencies.
    /// Wraps cSapModel.Results.ModalPeriod.
    /// </summary>
    /// <returns>ModalPeriodResults containing period and frequency data</returns>
    ModalPeriodResults GetModalPeriod();

    /// <summary>
    /// Gets modal participation factors.
    /// Wraps cSapModel.Results.ModalParticipationFactors.
    /// </summary>
    /// <returns>ModalParticipationFactorResults containing participation factor data</returns>
    ModalParticipationFactorResults GetModalParticipationFactors();

    /// <summary>
    /// Gets modal participating mass ratios.
    /// Wraps cSapModel.Results.ModalParticipatingMassRatios.
    /// </summary>
    /// <returns>ModalParticipatingMassRatioResults containing mass ratio data</returns>
    ModalParticipatingMassRatioResults GetModalParticipatingMassRatios();

    /// <summary>
    /// Gets modal load participation ratios.
    /// Wraps cSapModel.Results.ModalLoadParticipationRatios.
    /// </summary>
    /// <returns>ModalLoadParticipationRatioResults containing load participation data</returns>
    ModalLoadParticipationRatioResults GetModalLoadParticipationRatios();

    /// <summary>
    /// Gets mode shapes for joints.
    /// Wraps cSapModel.Results.ModeShape.
    /// </summary>
    /// <param name="name">Name of joint or group (use empty string "" for all)</param>
    /// <param name="itemTypeElm">Element type filter</param>
    /// <returns>ModeShapeResults containing mode shape data</returns>
    ModeShapeResults GetModeShape(string name, eItemTypeElm itemTypeElm);

    #endregion

    #region Base Reactions

    /// <summary>
    /// Gets base reactions.
    /// Wraps cSapModel.Results.BaseReact.
    /// </summary>
    /// <returns>BaseReactionResults containing reaction data</returns>
    BaseReactionResults GetBaseReact();

    /// <summary>
    /// Gets base reactions with centroid information.
    /// Wraps cSapModel.Results.BaseReactWithCentroid.
    /// </summary>
    /// <returns>BaseReactionWithCentroidResults containing reaction and centroid data</returns>
    BaseReactionWithCentroidResults GetBaseReactWithCentroid();

    #endregion

    #region Buckling

    /// <summary>
    /// Gets buckling factors.
    /// Wraps cSapModel.Results.BucklingFactor.
    /// </summary>
    /// <returns>BucklingFactorResults containing buckling factor data</returns>
    BucklingFactorResults GetBucklingFactor();

    #endregion

    #region Section Cuts

    /// <summary>
    /// Gets section cut analysis forces.
    /// Wraps cSapModel.Results.SectionCutAnalysis.
    /// </summary>
    /// <returns>SectionCutAnalysisResults containing section cut force data</returns>
    SectionCutAnalysisResults GetSectionCutAnalysis();

    /// <summary>
    /// Gets section cut design forces.
    /// Wraps cSapModel.Results.SectionCutDesign.
    /// </summary>
    /// <returns>SectionCutDesignResults containing section cut design force data</returns>
    SectionCutDesignResults GetSectionCutDesign();

    #endregion

    #region Pier and Spandrel

    /// <summary>
    /// Gets pier forces.
    /// Wraps cSapModel.Results.PierForce.
    /// </summary>
    /// <returns>PierForceResults containing pier force data</returns>
    PierForceResults GetPierForce();

    /// <summary>
    /// Gets spandrel forces.
    /// Wraps cSapModel.Results.SpandrelForce.
    /// </summary>
    /// <returns>SpandrelForceResults containing spandrel force data</returns>
    SpandrelForceResults GetSpandrelForce();

    #endregion

    #region Story and Joint Drifts

    /// <summary>
    /// Gets story drifts.
    /// Wraps cSapModel.Results.StoryDrifts.
    /// </summary>
    /// <returns>StoryDriftResults containing story drift data</returns>
    StoryDriftResults GetStoryDrifts();

    /// <summary>
    /// Gets joint drifts.
    /// Wraps cSapModel.Results.JointDrifts.
    /// </summary>
    /// <returns>JointDriftResults containing joint drift data</returns>
    JointDriftResults GetJointDrifts();

    #endregion

    #region Assembled Mass

    /// <summary>
    /// Gets assembled joint mass.
    /// Wraps cSapModel.Results.AssembledJointMass.
    /// </summary>
    /// <param name="name">Name of joint or group (use empty string "" for all)</param>
    /// <param name="itemTypeElm">Element type filter</param>
    /// <returns>AssembledJointMassResults containing mass data</returns>
    AssembledJointMassResults GetAssembledJointMass(string name, eItemTypeElm itemTypeElm);

    /// <summary>
    /// Gets assembled joint mass with mass source information.
    /// Wraps cSapModel.Results.AssembledJointMass_1.
    /// </summary>
    /// <param name="massSourceName">Name of mass source</param>
    /// <param name="name">Name of joint or group (use empty string "" for all)</param>
    /// <param name="itemTypeElm">Element type filter</param>
    /// <returns>AssembledJointMassResults containing mass data with source</returns>
    AssembledJointMassResults GetAssembledJointMass(string massSourceName, string name, eItemTypeElm itemTypeElm);

    #endregion

    #region Panel Zone

    /// <summary>
    /// Gets panel zone deformations.
    /// Wraps cSapModel.Results.PanelZoneDeformation.
    /// </summary>
    /// <param name="name">Name of joint or group (use empty string "" for all)</param>
    /// <param name="itemTypeElm">Element type filter</param>
    /// <returns>PanelZoneDeformationResults containing deformation data</returns>
    PanelZoneDeformationResults GetPanelZoneDeformation(string name, eItemTypeElm itemTypeElm);

    /// <summary>
    /// Gets panel zone forces.
    /// Wraps cSapModel.Results.PanelZoneForce.
    /// </summary>
    /// <param name="name">Name of joint or group (use empty string "" for all)</param>
    /// <param name="itemTypeElm">Element type filter</param>
    /// <returns>PanelZoneForceResults containing force data</returns>
    PanelZoneForceResults GetPanelZoneForce(string name, eItemTypeElm itemTypeElm);

    #endregion

    #region Generalized Displacement

    /// <summary>
    /// Gets generalized displacements.
    /// Wraps cSapModel.Results.GeneralizedDispl.
    /// </summary>
    /// <param name="name">Name of generalized displacement</param>
    /// <returns>GeneralizedDisplacementResults containing generalized displacement data</returns>
    GeneralizedDisplacementResults GetGeneralizedDispl(string name);

    #endregion
}