# Introduction #

This program will handle user inputs such as
  * Floorplan input - the user will select an image file to be used as the floorplan
  * destination selection - the user will select the endpoint of the robot's path

It will also act as the middle-man between the other components:
  * Passing images from the [LocalBrain](LocalBrain.md) to the [VoodooAlgorithm](VoodooAlgorithm.md)
  * Passing location and facing data from [VoodooAlgorithm](VoodooAlgorithm.md) to [PathFinding](PathFinding.md)
  * Passing movement information from the [PathFinding](PathFinding.md) to the  [LocalBrain](LocalBrain.md)

# Details #