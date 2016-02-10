# Contents #
  * `private Stack<Image> _Images` - A Stack of images to be used by the [VoodooAlgorithm ], produced by the LocalBrain
  * `private FloorPlan _FloorPlan`  - a [FloorPlan](FloorPlanProcessing.md) object, constructed with an image, which will store a `FloorPlanTile[][]`
  * `private Queue<MoveCommand> _Moves` - A Queue of moves, produced by [PathFinder](PathFinding.md) and used by LocalBrain
  * `private Position _Position` - A Position Object, representing the Robot's current position, produced by [VoodooAlgorithm](VoodooAlgorithm.md) and used by [PathFinder](PathFinding.md)