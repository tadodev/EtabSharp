# Requirements Document

## Introduction

This feature implements the core element object managers for the EtabSharp ETABS API wrapper. The system needs concrete implementations of the IPoint, IFrame, and IArea interfaces to provide structural engineers with a strongly-typed, intuitive API for managing structural elements (joints, beams/columns, and slabs/walls) in ETABS models.

## Glossary

### Core System Components
- **ETABS_API**: The native ETABS COM API (ETABSv1.dll) that provides low-level access to ETABS functionality
- **Element_Manager**: A class that implements element interface methods and wraps ETABS API calls with error handling
- **ETABS_Model**: The active ETABS model instance accessed through the API (cSapModel)

### Priority Element Objects (Current Implementation Focus)
- **Point_Object**: A joint/node in the structural model where elements connect and loads/supports are applied (cPointObj)
- **Frame_Object**: A 1D structural element (beam, column, brace) connecting two points (cFrameObj)
- **Area_Object**: A 2D structural element (slab, wall) defined by multiple corner points (cAreaObj)

### Supporting ETABS API Classes (Future Implementation)
- **cPropMaterial**: Material property definitions (concrete, steel, rebar)
- **cPropFrame**: Frame section property definitions (beam/column sizes)
- **cPropArea**: Area section property definitions (slab/wall thickness)
- **cLoadPatterns**: Load pattern management for applying loads
- **cLoadCases**: Load case definitions for analysis
- **cCombo**: Load combination definitions
- **cStory**: Building story/level management
- **cGroup**: Element grouping functionality
- **cSelect**: Element selection management
- **cConstraint**: Constraint and diaphragm definitions
- **cDiaphragm**: Rigid diaphragm assignments
- **cAnalyze**: Analysis execution and control
- **cAnalysisResults**: Results extraction and processing
- **cDesignConcrete**: Concrete design functionality
- **cDesignSteel**: Steel design functionality
- **cDesignShearWall**: Shear wall design functionality
- **cFile**: File operations (new, open, save)
- **cHelper**: Utility functions and helpers
- **cOptions**: ETABS program options and settings
- **cView**: Model view and display control

### Load and Analysis Classes (Future Priority)
- **cCaseStaticLinear**: Linear static analysis cases
- **cCaseModalEigen**: Modal analysis (eigenvalue) cases
- **cCaseResponseSpectrum**: Response spectrum analysis cases
- **cCaseDirectHistoryLinear**: Linear time history analysis
- **cAutoSeismic**: Automatic seismic load generation

### Design Code Classes (Future Implementation)
- **cDStAISC360_22**: AISC 360-22 steel design
- **cDCoACI318_19**: ACI 318-19 concrete design
- **cDStCanadian_S16_24**: Canadian S16-24 steel design
- **cDCoEurocode_2_2004**: Eurocode 2 concrete design

### Element Analysis Classes (Future Implementation)
- **cPointElm**: Point element analysis results
- **cFrameElm**: Frame element analysis results  
- **cAreaElm**: Area element analysis results
- **cLineElm**: Line element analysis results

### Advanced Element Types (Future Implementation)
- **cLinkObj**: Link/support element objects
- **cTendonObj**: Post-tensioning tendon objects

### Property Spring Classes (Future Implementation)
- **cPropPointSpring**: Point spring property definitions
- **cPropLineSpring**: Line spring property definitions
- **cPropAreaSpring**: Area spring property definitions

### Label and Organization Classes (Future Implementation)
- **cPierLabel**: Pier label definitions for lateral analysis
- **cSpandrelLabel**: Spandrel label definitions for lateral analysis
- **cTower**: Tower definition for multi-tower buildings

### Database and Results Classes (Future Implementation)
- **cDatabaseTables**: Database table access and manipulation
- **cAnalysisResultsSetup**: Analysis results configuration
- **cDesignResults**: Design results extraction
- **cDesignForces**: Design force extraction

### Specialized Design Classes (Future Implementation)
- **cDesignCompositeBeam**: Composite beam design
- **cDesignCompositeColumn**: Composite column design
- **cDesignConcreteSlab**: Concrete slab design
- **cDesignStrip**: Strip design for slabs
- **cDetailing**: Reinforcement detailing
- **cConcreteShellDesignRequest**: Concrete shell design requests

### Grid and Geometry Classes (Future Implementation)
- **cGridSys**: Grid system definitions
- **cGenDispl**: General displacement assignments
- **cFunction**: Function definitions (time history, response spectrum)
- **cFunctionRS**: Response spectrum function definitions

### Plugin and Extension Classes (Future Implementation)
- **cPluginCallback**: Plugin callback interface
- **cPluginContract**: Plugin contract definitions
- **cOAPI**: Object API interface

## Requirements

### Requirement 1

**User Story:** As a structural engineer, I want to create and manage point objects programmatically, so that I can define structural joints and connection points efficiently.

#### Acceptance Criteria

1. WHEN the engineer calls AddPoint with coordinates, THE Point_Manager SHALL create a new point object in the ETABS_Model
2. WHEN the engineer requests point coordinates, THE Point_Manager SHALL retrieve accurate coordinate data from the ETABS_API
3. WHEN the engineer assigns restraints to a point, THE Point_Manager SHALL apply boundary conditions through the ETABS_API
4. WHEN the engineer applies loads to a point, THE Point_Manager SHALL assign concentrated forces and moments correctly
5. IF the ETABS_API returns an error, THEN THE Point_Manager SHALL throw a meaningful EtabsException with context

### Requirement 2

**User Story:** As a structural engineer, I want to create and manage frame objects programmatically, so that I can define beams, columns, and braces with proper connectivity and properties.

#### Acceptance Criteria

1. WHEN the engineer calls AddFrame with point names, THE Frame_Manager SHALL create a frame element connecting the specified points
2. WHEN the engineer assigns section properties, THE Frame_Manager SHALL apply the correct structural properties through the ETABS_API
3. WHEN the engineer applies distributed loads, THE Frame_Manager SHALL assign uniform or trapezoidal loads along the frame length
4. WHEN the engineer sets end releases, THE Frame_Manager SHALL configure connection fixity conditions correctly
5. WHILE the frame has assigned loads, THE Frame_Manager SHALL maintain load assignments when properties are modified

### Requirement 3

**User Story:** As a structural engineer, I want to create and manage area objects programmatically, so that I can define floor slabs and walls with appropriate loading and boundary conditions.

#### Acceptance Criteria

1. WHEN the engineer calls AddArea with point names, THE Area_Manager SHALL create an area element with the specified boundary points
2. WHEN the engineer assigns uniform loads, THE Area_Manager SHALL apply distributed loads per unit area correctly
3. WHEN the engineer sets diaphragm assignments, THE Area_Manager SHALL configure rigid floor behavior through the ETABS_API
4. WHEN the engineer designates openings, THE Area_Manager SHALL create void areas that do not carry loads
5. WHERE the area has spring supports, THE Area_Manager SHALL assign foundation spring properties correctly

### Requirement 4

**User Story:** As a developer using EtabSharp, I want consistent error handling across all element managers, so that I can reliably catch and handle API failures.

#### Acceptance Criteria

1. WHEN any ETABS_API call fails, THE Element_Manager SHALL wrap the error in an EtabsException
2. WHEN invalid parameters are provided, THE Element_Manager SHALL validate inputs and throw ArgumentException with clear messages
3. WHEN the ETABS_Model is not available, THE Element_Manager SHALL throw InvalidOperationException
4. WHILE processing bulk operations, THE Element_Manager SHALL continue processing valid items and report failed items
5. THE Element_Manager SHALL log all API interactions for debugging purposes

### Requirement 5

**User Story:** As a developer using EtabSharp, I want strongly-typed model classes for all output data, so that I can work with structured data instead of raw arrays and have compile-time type safety.

#### Acceptance Criteria

1. WHEN retrieving point data, THE Point_Manager SHALL return PointObj model instances with strongly-typed properties
2. WHEN retrieving frame data, THE Frame_Manager SHALL return FrameObj model instances with all frame properties
3. WHEN retrieving area data, THE Area_Manager SHALL return AreaObj model instances with area-specific properties
4. WHEN retrieving load assignments, THE Element_Manager SHALL return typed load model classes instead of object arrays
5. THE Element_Manager SHALL provide model classes that encapsulate all related data for each element type

### Requirement 6

**User Story:** As a structural engineer, I want efficient bulk operations for element management, so that I can work with large models without performance issues.

#### Acceptance Criteria

1. WHEN retrieving all elements of a type, THE Element_Manager SHALL use efficient bulk API calls where available
2. WHEN applying properties to multiple elements, THE Element_Manager SHALL batch operations to minimize API calls
3. THE Element_Manager SHALL cache frequently accessed data to reduce API round trips
4. WHEN processing large datasets, THE Element_Manager SHALL provide progress feedback for long-running operations
5. THE Element_Manager SHALL dispose of resources properly to prevent memory leaks

### Requirement 7

**User Story:** As a structural engineer, I want all information in and out to be manage with type safe.

#### Acceptance Criteria

1. WHEN input data, it can be double for string, but when output it should related to the object it represent to
