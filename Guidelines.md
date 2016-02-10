# Introduction #

To integrate everybody's code, we need some guidelines to follow.

# Motivation (What's wrong?) #
  * All the codes are put into one project, when a small piece of code broke, it is hard to test/ modify the code.

# Guidelines (How to make things better) #

## Design and Implememtation ##

  * [UML Diagram](UMLDiagram.md): All implementation should follow class diagram that all team member agree. If there is any changes necessary, we should have a meeting to discuss it and evaluate the influence.
  * Try to avoid accessing the class properties directly. Provide a public get method.

## Code Organization ##

  * Each module has its own folder
  * Each module produce one dll file.
  * Each module has its own solution and testing project/class.
  * There is a ReadMe to explan what is in the folder.

## Testing ##

  * Testing class should be in the module.

## Distribution / Prerequisite ##

  * FloorPlanAndTile: no need to import other modules.
  * PathFinding: FloorPlanAndTile.dll, QuickGraph.dll
  * PositionFinding: FloorPlanAndTile.dll
  * Frank's GUI: PathFinding.dll, PositionFinding.dll, FloorPlanAndTile.dll, QuickGraph.dll

## Code Style & Naming ##

  * Namespace: See3PO
  * Class name:
  * Variable name:
  * Function name:
  * Testing Class name:
  * Testing Function name: