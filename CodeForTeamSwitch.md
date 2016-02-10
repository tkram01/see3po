# Introduction #


# Details #

## Preparation ##
  * Use SVN to checkout the documents from http://see3po.googlecode.com/svn/trunk/
> > Folders that we need are:
> > For the Atom:
      * Atom
> > For the LocalBrain:
      * LocalBrain-Self Control
> > For the Host PC:
      * Pathfinding
      * FloorPlanAndTile
      * GUI
      * Host
  * Install Visual Studio 2008.
  * Install ActiveSync
  * Install Basic Micro Studio
## Overview ##
> > Download the [System Configuration Diagram](http://see3po.googlecode.com/files/See3PO_System.pdf) This describes an overview of the robot.
> > The Manual directory in SVN contains manuals for the hardware components involved in the robot.
> > The protocol["communication protocol.doc"](http://code.google.com/p/see3po/source/browse/trunk/Documents/Hardware/communication%20protocols.doc) Describes the command protocols used by the robot and between the robot and host.
#### Example ####
> > Motor controls are sent as an array of bytes. An example would be: {1 16 17 0 179 0 150 0 42 239}.
> > The first three bytes are static, and represent the type of message sent.
> > The 4th and 5th bytes (0 179) store the left wheel speed, while the 6th and 7th bytes(0 150) hold the right wheel speed
> > Bytes 8 and 9 (0, 42) hold the time, in units of 50ms. In this case, (0, 42) = 42 x 50ms = 2100ms. The last byte (239) indicates that this is the end of the message.
> > This command tells the robot to move forward at left speed 179 right speed 150 for 2100 ms.
## Instalation ##
### Installing ATOM code (not to be performed in Team Switch) ###
Working in the **ATOM** directory: This project is compiled by Basic Micro Studio, which is written by the lower level machine language called **Atom BASIC**. This program receives the command from local brain and then tells the robot how to move or react.
Our current working solution is  _/ATOM/program6.bas program6.bas_. To upload this program to the robot, we use a serial cable with Basic Micro Studio. Please refer to [atom pro tutorial.pdf](http://code.google.com/p/see3po/source/browse/trunk/Manual/atom%20pro%20tutorial.pdf) for more detail.
### Installing Local Brain ###
Working from the **LocalBrain-selfcontrol** directory: To update the local brain, first, compile the code in visual studio. Open the project file at _LocalBrian-selfcontrol/localbrain.sln_, using Visual Studio. Build the code, and find the _LocalBrain.exe_ in the _LocalBrain-selfcontrol\bin\Debug_ directory. To load the file onto the robot, connect robot with an USB cable, then power up the robot. When the ActiveSync application opens, copy-paste _LocalBrain.exe_ into **IPSM** folder of local brain storage.
### Installing Host ###
All modules are developed in Visual Studio 2008. Double click ModuleName.sln in corresponding folders for details. These must be done in order due to dependency issues.
#### FloorPlanAndTile ####
> > > Working from **FloorPlanAndTile\FloorPlanAndTile** folder: Open FloorPlanAndTile.sln and compile it. This will compile to a DLL and will be included in following files.
> > > This solution holds information about the floor plans and the methods to create them from images or open from saved files.
#### PathFinding ####
> > > Working from **PathFinding** folder: Open PathFinding.sln and compile it. This will compile to a DLL and will be included in following files.
> > > This solution creates a graph, based on the floor plan, and produces a path between the location and destination of the robot.
#### See3PO ####
> > > Working from **See3PO** folder: Open See3PO.sln and compile it. This will compile to a DLL and will be included in following files.
> > > This is the communication center of the host. It interacts with all of the components of host as well as with the robot.
#### GUI ####
> > > Working from **GUI** folder: Open GUI.sln and compile it. This will compile to an EXE. You will run this GUI.