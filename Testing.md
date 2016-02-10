This page will outline current testing goals and completed tests. As components advance, new tests will be introduced.

## Development Tools ##

### [Command Line Interface](HostCLI.md) ###

| Test Name | Description | Boolean: Success |  Completed |
|:----------|:------------|:-----------------|:-----------|
| Connect   | test connection | Connection recognized by Emulator | 10/27/09   |
| Send Move 1 1 | Test a move command | emulator sees command 1 1 | 10/27/09   |
| Send Move -1 1 | Test negative move command | emulator sees -1 1 | 10/27/09   |
| Receive Location | Test location return |  (250, 250)      |            |
| Receive Facing | Test facing return | (0)              |            |

### [Robot Emulator](LocalBrainEmulator.md) ###

| Test Name | Description | Boolean: Success |  Completed |
|:----------|:------------|:-----------------|:-----------|
| Receive Move 1 1 | Test correct move commands | Location: (250, 255) Facing: 0| 10/27/09   |
| Receive Move 1 -1 | Test correct move commands | Location:(250, 250) Facing: 5| 10/27/09   |
| Receive Move -1 1 | Test counterclockwise move | Location:(250, 250) Facing: 355|            |
| Send Location | Test location return | HostCLI displays (250, 250) |            |
| Send Facing | Test facing return | HostCLI displays (0) |            |
| Receive Path | Receive a path F3/L90/F2 from the CORE | Location: (260, 265) Facing: 90 |            |
## Components ##

### [CORE](CORE.md) ###

| Test Name | Description | Boolean: Success |  Completed |
|:----------|:------------|:-----------------|:-----------|
|           |             |                  |            |
| Store Path | Receive a Path from the PathFinding |                  |            |
| Store Location | Receive a Location from VooDoo |                  |            |
| Store FloorPlan | Receive a Floor Plan from FloorPlanProcessing |                  |            |

### [Floor Plan Processing](FloorPlanProcessing.md) ###
| Test Name | Description | Boolean: Success |  Completed |
|:----------|:------------|:-----------------|:-----------|
|           |             |                  |            |
| Send FloorPlan | Send a representation of the 3rd Floor floorplan to the CORE | Location: (260, 265) Facing: 90 |            |

### [GUI](GUI.md) ###
| Test Name | Description | Boolean: Success |  Completed |
|:----------|:------------|:-----------------|:-----------|
| Display FloorPlan | Display FloorPlan from CORE |                  |            |
| Display Path | Display Path from CORE |                  |            |

### [Path Finding](PathFinding.md) ###

Path Finding component is using the Unit Test tools in Visual Studio 2008. Each method in class will has its own testing method (MethodTest()) in testing class (`*`Test.cs).

| Test Name | Description | Boolean: Success |  Completed |
|:----------|:------------|:-----------------|:-----------|
|           |             |                  |            |
| Send Path | Send a path F3/L90/F2 to the CORE | Location: (260, 265) Facing: 90 |            |