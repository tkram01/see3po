# Introduction #

This plan details the interactions between the various components
  * [GUI](GUI.md) - This will hold an instance of Host
  * [Host](Host.md)  - The core of the program, this will hold instances of the other components
  * [Status](Status.md)
    1. [FloorPlan](FloorPlanProcessing.md) - will be constructed with  ` FloorPlan(Image)` and store a FloorPlanTile[.md](.md)[.md](.md) representing the floor
  * [PathFinder](PathFinding.md) - will have `Queue<MoveCommand> GetPath(Location)` method to get a queue of MoveCommand objects
  * [LocalBrain](LocalBrain.md) / [LocalBrainEmulator](LocalBrainEmulator.md)
  * [VoodooAlgorithm](VoodooAlgorithm.md) will have `Location GetLocation(Image)` to return a Location object

![http://i45.photobucket.com/albums/f69/FMarino/interactionsLeft.jpg](http://i45.photobucket.com/albums/f69/FMarino/interactionsLeft.jpg) ![http://i45.photobucket.com/albums/f69/FMarino/interactionsRight.jpg](http://i45.photobucket.com/albums/f69/FMarino/interactionsRight.jpg)