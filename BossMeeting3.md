# Meeting with Tyler #
  * Date: 10/21/2009
  * Time: 6:30 PM
  * Location: S-3-135
  * Meeting called by:	Tyler
  * Type of meeting:	Business/Progress
  * Facilitator:	Frank/Tyler
  * Note taker:
  * Attendees: Frank, Hao Wu, Jacky, Nick
  * Absent: Debarati

## Minutes ##


### Agenda item: ###
  * Tyler will view our progress
  * Discuss the interface between his program/class and ours
  * Pathfinding questions:
    1. Location or Location and Facing?
    1. 8-way, 4-way directionality?
    1. Restrictions on floorplan image
  * Discuss Emulator
  * Hardware Questions:
    1. How to upload/update files onto robot's embedded computer?
    1. How to upload/update programs onto ATOM microchip?
    1. Need username and password to download [camera's](http://www.ptgrey.com/products/dragonfly2/index.asp) driver from [manufacture's website](http://www.ptgrey.com/support/downloads/index.asp).
  * Discuss Mission Statement

### Discussion: ###
> Software -
    1. Tyler would like to have a  robust data structure that stores the pathfinding data, including the floorplan and move data, as well as the current location from the Voodoo algorithm
    1. We discussed image-to-floorplan-to-pathfinding options.
> Hardware -
    1. Learned how to upload files on to embedded computer.
    1. Learned how to upload files on to ATOM.
    1. Checked the communication protocols with Tyler. And, we found out that the current program uploaded on ATOM is not the source code we expected.
    1. Reproduced the hardware issues on robot.
    1. Discussed how to perform tests on robot.
### Conclusions: ###
> Software -
    * We decided to choose 4-direction movement.
    * Definition of "Optimal Path": The lowest steps from starting porint to target.

> Hardware -
    * Tyler will try to find the correct source code of current version running on ATOM. If Tyler can't find it, we have to develop our own.
    * Tyler will provide a USB cable for uploading program to embedded computer and a serial cable for uploading program to ATOM.
    * Tyler will give us the username and password for accessing the download site of our video camera's manufacture.
    * Jacky have to revise the communication protocol document since the wrong source code was provided.
    * Hao Wu will be working on a testing version of local brain application which allows the embedded computer sends motor control commands to ATOM directly. Besides, we might consider log all transactions to a file as well as read commands from a file in the near future.
    * There are 2 issues we found today:
      1. Local brain might receive 2 commands at the same time.
      1. The responses to the command from local brain on ATOM, such as making sounds or motor movement, sometimes stall. i.e. User hits making beep bottom five times, but ATOM only makes sound one time.
### Other Information ###