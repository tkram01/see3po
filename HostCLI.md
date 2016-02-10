# Commands #

The following commands will be available in the completed version:

| **Command** | **Description** |
|:------------|:----------------|
| Listen      | Listen for client connections |
| Stop        | Stop listening for client connections |
| Disconnect  | Disconnect from current client |
| # #         | Move the left and right wheels respective integer number of steps steps, between -10 and 10 |
| Location    | Give the current location of the robot |
| Facing      | Give the current facing of the robot |
| Exit        | exit the program |

## Moving ##
Move commands move in increments of 5 steps. This translates to (LeftSpeed - RightSpeed) `*` 2.5 degrees turned and (LeftSpeed + RightSpeed) `*` 2.5 steps forward.

For example:
| 5 5 | Move 25 steps forward |
|:----|:----------------------|
| -1 1 | turn 5 degrees clockwise |
| 9 -9 | turn 45 degrees counterclockwise |
| 9, 0 | move forward 22 steps, turn 22 degrees |