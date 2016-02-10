# Vision #

Add vision here.


# Requirements #
### User Interface [Debarati](Debarati.md), [Nick](Nick.md), [Frank](Frank.md) ###
  * This interface, located on the central computer, will manage loading the floor plans, selecting destinations, and will act as a front end to the rest of the software.

### Camera Interface [Hao](Hao.md), [Jacky](Jacky.md) ###
  * The camera must be made to interface directly with the robot's on-board computer

### Wireless Image Transmission (Robot to PC)[Hao](Hao.md), [Jacky](Jacky.md) ###
  * The robot will gather images from the on-board camera
  * [optional](optional.md) the on-board computer will compress the images for transmission
  * The robot will transmit to the desktop PC

### Wireless Control Transmission (PC to Robot)[Hao](Hao.md), [Jacky](Jacky.md) ###
  * The PC will must transmit instructions in such as way that poor connections will not result in catastrophe.

### [Floor Plan Processing](FloorPlanProcessing.md) [Debarati](Debarati.md), [Nick](Nick.md), [Frank](Frank.md) ###
  * Using some representation of a floor plan, possibly an image and a scale, the central computer must create a model of the floor for use by the path-finding algorithm.

### Location [Debarati](Debarati.md), [Nick](Nick.md), [Frank](Frank.md) ###
  * Using the images and the robot, and the floor plan, the central PC will establish the location of the robot.

### [Path-finding](PathFinding.md) [Debarati](Debarati.md), [Nick](Nick.md), [Frank](Frank.md) ###
  * Using the data from the floor plan, the central computer will design a route from the robot's current location to the destination
  * Path-finding must auto-correct based on new visual data from the robot
  * [optional](optional.md) Path-finding must account for discrepancies between the floor plan and reality, such as obstacles.