## Introduction ##

The first live demo will display FloorPlanProcessing, PathFinding and Host-Remote coordination.


## Details ##
### Goals ###
  * starting point & destination: Location nearby M-1-620 (demo room)
  * Locations are assigned automatically via GUI. Users need to click the floor plan in GUI.
  * Robot is moving in 4 directions along tiles.
  * A "Location" feature might be provided manually.

### Why this milestone? ###
> If we can deliver this on first sprint, we can then prove that the floor plan and path finding program are working correctly. It also proves that we are able to totally control the robot movement through the program automatically. Finally, it will put us in a good position to accept Tyler's program and immediately begin integrating it.

### Necessary Steps ###
#### Software ####
  * Integration of path-finding program and robot movement  ([Nick](Nick.md) / 10 hours)
  * Define data structure to store floor plan to files. Import and export ([Debarati](Debarati.md) / 5 hours)
  * Handling exceptions in path-finding such as the chosen points are not
> available / path is not available.([Nick](Nick.md) [Frank](Frank.md) / 10 hours)
  * Estimate / Reduce processing time of path-finding ([Frank](Frank.md) [Nick](Nick.md) / 10 hours)
  * Enhance GUI to enable manual Location  ([Frank](Frank.md) / 10 hours)
  * Robot-side programming (LocalBrain): a robot could take a sequence of commands (turns or forward)( [Nick](Nick.md) [Hao](Hao.md) [Jacky](Jacky.md) / 30 Hours)
  * Figure out solutions for hardware: PCMCIA card drivers ([Hao](Hao.md) [Jacky](Jacky.md) / 20 Hours)
  * WhereAmI Locator must be completed when this sprint is over (MERL / ?)

#### Hardware ####
  * Due to the lack of driver, we need to confirm Tyler to use another type of camera.
  * Test the robot speed and write some code to make the robot to move forward in discrete increments and turn in increments of 90 degrees, 180 degrees, 270 degree.
  * Combine the software and hardware to test the robot movement.
  * If we still have much more time, we need to think about the command buffer, which method will make it much more efficient.(The buffer in the host or in the local, how to quote the command query to void of losing the command and so on)

#### Other ####
  * FloorPlan - an accurately measured floorplan will be needed.


### Testing ###
  * Floor Plan Processing: several testing classes are available to test the functions. Someone not on out team could understand the usages of functions from comments and ReadMe and carry out the expected return values.
  * Path-Finding: some testing classes are provided. The tester could try to break the codes by sending invalid positions.
  * Robot movement: use "from one tile to the next tile" as a movement unit. Provide user interface to move UP, DOWN, LEFT, and RIGHT on the floor plan. Robot should make turns (90, 180, or -90 degrees) correctly and forward for a desired distance.


Responsibilities:
  * Frank: GUI
  * Nick: Path-finding & Integration of robot movement
  * Jacky & Hao: Hardware, WinCE programming.
  * Debarati: FloorPlan Processing

Schedule
  * Jan28 - Feb 4: Floor Plan / Path Finding Done.
  * Feb 4 - Feb 11 Be able to have sequence of commands
  * Feb 11 - Feb 18 Testing on Robot / Robot programs, Robot Incremental Movements
  * Feb 18 - Feb 25 Integrate GUI, Path, and Robot
  * Feb 25 - Mar 2 Robot could walk by assigning a sequence of
> move commands.
