CLI: (broken) Command Line inferface for Host

FloorPlanAndTile: DLL holds FloorPlan and Tile classes for creating floorplans

GUI: Graphical Interface for host

Host: Central module - manages connection to robot as well as controlling data flow between Floor Plan, PathFinding and Locator

Old: out-of-date codes (but might be still useful some day)
  - FloorPlanProcessing: Older add-on classes for floor plan processing
  - Host_Old: previous combined Host-GUI
  - LocalBrain: Older version of Local brain software
  - Vilian Explorer: Tyler's original GUI
  - Vilian Camera - Camera control code

PathFinding: A module that could take a FloorPlan as input and generate walkable path.

ViLAN Explorer: Robot direct control host program.

ATOM: motor and servo control program on ATOM board.

LocalBrain-selfcontrol: program runs on robot embedded computer (WinCE) which is used to recieve 
   command from Host as well as pass the command to ATOM.

localbrainwin: Windows version LocalBrain program for testing purpose.

