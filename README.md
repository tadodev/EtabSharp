# EtabSharp

EtabSharp/
├── Core/                           # Application & Model wrappers
│   ├── ETABSApplication.cs
│   ├── ETABSModel.cs
│   ├── ETABSWrapper.cs
│   └── Models/
│
├── Properties/                     # Define → Section Properties
│   ├── Materials/                  # Define → Material
│   │   ├── MaterialManager.cs     (implements IPropMaterial)
│   │   ├── Constants/
│   │   └── Models/
│   ├── Frames/                     # Define → Frame Sections
│   │   ├── FramePropertyManager.cs (implements IPropFrame)
│   │   └── Models/
│   ├── Areas/                      # Define → Slab & Wall
│   │   ├── AreaPropertyManager.cs  (implements IPropArea)
│   │   └── Models/
│   ├── Links/                      # Define → Link/Support
│   │   └── (future)
│   └── Cables/                     # Define → Cable
│       └── (future)
│
├── Elements/                       # Draw → Objects
│   ├── Stories/                    # Edit → Story
│   │   ├── StoryManager.cs        (implements IStory)
│   │   └── Models/
│   ├── Points/                     # Draw → Point
│   │   ├── PointObjectManager.cs  (implements IPointObject)
│   │   └── Models/
│   ├── Frames/                     # Draw → Frame
│   │   ├── FrameObjectManager.cs  (implements IFrameObject)
│   │   └── Models/
│   ├── Areas/                      # Draw → Slab/Wall
│   │   ├── AreaObjectManager.cs   (implements IAreaObject)
│   │   └── Models/
│   └── Selection/                  # Select menu
│       ├── SelectionManager.cs    (implements ISelection)
│       └── Models/
│
├── Labels/                         # Define → Pier/Spandrel Labels
│   ├── Piers/
│   │   ├── PierLabelManager.cs    (implements IPierLabel)
│   │   └── Models/
│   └── Spandrels/
│       ├── SpandrelLabelManager.cs (implements ISpandrelLabel)
│       └── Models/
│
├── Groups/                         # Define → Groups
│   ├── GroupManager.cs            (implements IGroup)
│   └── Models/
│
├── Loads/                          # Define → Load Patterns/Cases/Combos
│   ├── Patterns/
│   │   ├── LoadPatternManager.cs  (implements ILoadPattern)
│   │   └── Models/
│   ├── Cases/
│   │   ├── LoadCaseManager.cs     (implements ILoadCase)
│   │   └── Models/
│   ├── Combos/
│   │   ├── LoadComboManager.cs    (implements ILoadCombo)
│   │   └── Models/
│   └── Assignment/                 # Assign → Loads
│       ├── LoadAssignmentManager.cs
│       └── Models/
│
├── Analysis/                       # Analyze menu
│   ├── AnalysisManager.cs         (implements IAnalysis)
│   ├── ResultSetup/
│   │   ├── ResultSetupManager.cs  (implements IResultSetup)
│   │   └── Models/
│   ├── Results/
│   │   ├── ResultsManager.cs      (implements IResults)
│   │   └── Models/
│   └── Models/
│
├── Design/                         # Design menu
│   ├── Concrete/
│   │   ├── ConcreteDesignManager.cs (implements IConcreteDesign)
│   │   └── Models/
│   ├── Steel/
│   │   ├── SteelDesignManager.cs   (implements ISteelDesign)
│   │   └── Models/
│   ├── Shearwall/
│   │   ├── ShearwallDesignManager.cs (implements IShearwallDesign)
│   │   └── Models/
│   ├── Composite/
│   │   └── (future)
│   └── Forces/                      # Design → Steel/Concrete Frame Design Forces
│       ├── DesignForceManager.cs
│       └── Models/
│
├── Tables/                          # Display → Show Tables (Ctrl+T)
│   ├── DatabaseTableManager.cs     (implements IDatabaseTable)
│   └── Models/
│
├── System/                          # File, Units, Model Info
│   ├── FileManager.cs              (implements IFiles)
│   ├── UnitManager.cs              (implements IUnitSystem)
│   ├── ModelInfoManager.cs         (implements ISapModelInfor)
│   └── Models/
│
├── Interfaces/
│   ├── Properties/
│   ├── Elements/
│   ├── Labels/
│   ├── Groups/
│   ├── Loads/
│   ├── Analysis/
│   ├── Design/
│   ├── Tables/
│   └── System/
│
└── Exceptions/
