using guidelines described on [wikipedia](http://en.wikipedia.org/wiki/Software_Requirements_Specification)

### [PDF Version](http://see3po.googlecode.com/files/Software_Requirements_Specifications.pdf) ###

## Software Requirements Specifications (SRS) - Version 0.9 ##

  1. [INTRODUCTION](http://code.google.com/p/see3po/wiki/RequirementsSpec#1._Introduction)<br />
    1. [Product Overview](http://code.google.com/p/see3po/wiki/RequirementsSpec#1.1_Product_Overview) <br />
    1. [Purpose](http://code.google.com/p/see3po/wiki/RequirementsSpec#1.2_Purpose) <br />
    1. [Scope](http://code.google.com/p/see3po/wiki/RequirementsSpec#1.3_Scope) <br />
    1. [Reference](http://code.google.com/p/see3po/wiki/RequirementsSpec#1.4_Reference) <br />
    1. [Definition and Abbreviation](http://code.google.com/p/see3po/wiki/RequirementsSpec#1.5_Definition_and_Abbreviation) <br />
  1. [OVERALL DESCRIPTION](http://code.google.com/p/see3po/wiki/RequirementsSpec#2._Overall_Description)<br />
    1. [Product Perspective](http://code.google.com/p/see3po/wiki/RequirementsSpec#2.1_Product_Perspective)<br />
    1. [Product Functions](http://code.google.com/p/see3po/wiki/RequirementsSpec#2.2_Product_Functions)<br />
    1. [User Characteristics](http://code.google.com/p/see3po/wiki/RequirementsSpec#2.3_User_Characteristics)<br />
    1. [General Constraints](http://code.google.com/p/see3po/wiki/RequirementsSpec#2.4_General_Constraints)<br />
    1. [Assumptions and Dependencies](http://code.google.com/p/see3po/wiki/RequirementsSpec#2.5_Assumptions_and_Dependencies)<br />
  1. [SPECIFIC REQUIREMENTS](http://code.google.com/p/see3po/wiki/RequirementsSpec#3._Specific_Requirements)<br />
    1. [External Interface Requirements](http://code.google.com/p/see3po/wiki/RequirementsSpec#3.1_External_Interface_Requirements) <br />
      1. [User Interface](http://code.google.com/p/see3po/wiki/RequirementsSpec#3.1.1_User_Interfaces) <br />
      1. [Hardware Interface](http://code.google.com/p/see3po/wiki/RequirementsSpec#3.1.2_Hardware_Interfaces) <br />
      1. [Software Interface](http://code.google.com/p/see3po/wiki/RequirementsSpec#3.1.3_Software_Interfaces) <br />
      1. [Communications Protocols](http://code.google.com/p/see3po/wiki/RequirementsSpec#3.1.4_Communications_Protocols) <br />
    1. [Sofware Product Features](http://code.google.com/p/see3po/wiki/RequirementsSpec#3.2_Software_Product_Features) <br />
      1. [GUI](http://code.google.com/p/see3po/wiki/RequirementsSpec#3.2.1_Graphical_User_Interface) <br />
      1. [Floor Plan Processing](http://code.google.com/p/see3po/wiki/RequirementsSpec#3.2.2_Floor_Plan_Processing)<br />
      1. [Path Finding](http://code.google.com/p/see3po/wiki/RequirementsSpec#3.2.3_Path_Finding)<br />
    1. [Sofware Product Features](http://code.google.com/p/see3po/wiki/RequirementsSpec#3.3_Software_System_Attributes) <br />
      1. [Reliability](http://code.google.com/p/see3po/wiki/RequirementsSpec#3.3.1_Reliability)<br>
<ol><li><a href='http://code.google.com/p/see3po/wiki/RequirementsSpec#3.3.2_Availability'>Availability</a><br>
</li><li><a href='http://code.google.com/p/see3po/wiki/RequirementsSpec#3.3.3_Security'>Security</a><br>
</li><li><a href='http://code.google.com/p/see3po/wiki/RequirementsSpec#3.3.4_Maintainability'>Maintainability</a><br>
</li><li><a href='http://code.google.com/p/see3po/wiki/RequirementsSpec#3.3.5_Performance'>Performance</a><br>
</li></ol></li></ul><ol><li><a href='http://code.google.com/p/see3po/wiki/RequirementsSpec#4_Revision_History'>Revision History</a> <br /></li></ol>

<hr />
<br /><br />
<h1>1. Introduction</h1>
<br />
<h2>1.1 Product Overview</h2>
<blockquote>See3PO is a path finding robot, which can find the shortest path to reach its destination from its current location and reach there with minimum human intervention.<br>
<br />
<h2>1.2 Purpose</h2>
This is a research project. The main purpose of this project is to make a robot find the shortest path, between its current location and destination, in a given floor plan. The robot should be capable of updating its current route at a regular interval, in order to take care of any possible hindrance in it's path, that could have come to existence since the last time it calculated its route. And finally it should then reach the destination.<br>
<br />
<h2>1.3 Scope</h2>
</blockquote><ul><li>Assumptions: Algorithm to be provided by MERL is delivered on time.<br>
</li><li>Deliverables: GUI, Floor plan processing program, Path finding program.</li></ul>

<blockquote>When provided with a given floor plan in acceptable image format, the robot will be able to get its current location on the same, using the algorithm provided by MERL. It will then pass that location information to appropriate programs to compute the shortest path to the destination. It then will move along the computed path towards it's destination. However, the computation of the route is not an one time event, this will continue to be executed at a regular interval, in order to rule out any possible hindrance on the route that could cause the robot to not to reach its destination and if such problems come into existence since the last time the path was calculated, the robot will then re-evaluate the route and follow the newly calculated route.<br>
<br />
<h2>1.4 Reference</h2></blockquote>

<ol><li><a href='SystemArchitecture.md'>System Architecture</a><br />
</li><li><a href='UseCases.md'>Use Cases</a><br />
<h2>1.5 Definition and Abbreviation</h2>
<i>This section needs to be populated each time we use new abbreviations while working on SRS.</i>
<table><thead><th> <b>Terms</b></th><th> <b>Meaning</b> </th></thead><tbody>
<tr><td> SRS         </td><td> System Requirement Specification</td></tr>
<tr><td> MERL        </td><td> Mitsubishi Electric Research Laboratories</td></tr>
<tr><td> WhereAmI    </td><td> A Vision-Based Locating Program supplied by MERL </td></tr>
<tr><td> Host PC     </td><td> A Desktop PC used to interact wirelessly to the robot </td></tr>
<tr><td> See3PO      </td><td> The Pathfinding Robot </td></tr></li></ol></tbody></table>

<br /><br />
<h1>2. Overall Description</h1>
<br />
<h2>2.1 Product Perspective</h2>
<blockquote>See3PO will autonomously navigate its environment. Using only visual tools and a floorplan, <br />
the robot will walk to any reachable locations as directed by the user.<br>
<br />
<h2>2.2 Product Functions</h2>
The User will be able to direct the robot to a desired location within its environment.<br />
The robot will use the WhereAmI software to keep up-to-date information about its current position.<br />
Pathfinder will find the optimal route to the target location from the current position.<br />
The Host PC will direct the robot, step by step, to the target, testing accuracy along the way<br>
<br />
<h2>2.3 User Characteristics</h2>
The user should require no training.<br>
<br />
<h2>2.4 General Constraints</h2>
</blockquote><ul><li>Wireless Communication may limit the range and accuracy of See3PO's movement.<br>
</li><li>Current plans include known, indoor locatios with accessible floorplans.<br>
</li><li>Some preparation may be necessary for WhereAmI to accurately work in a given environment.<br>
<br />
<h2>2.5 Assumptions and Dependencies</h2>
It is assumed that the entire area to be covered by the robot will have the following properties:<br>
</li><li>Consistent wireless reception<br>
</li><li>Smooth, dry, level flooring<br>
</li><li>A relatively consistent floor plan<br>
<br /><br />
<h1>3. Specific Requirements</h1>
<br />
<h2>3.1 External Interface Requirements</h2>
<br />
<h3>3.1.1 User Interfaces</h3>
</li><li>The user will be the operator of the See3PO robot.<br>
</li><li>From the Host PC interface, the user will control connections to the robot and have the ability to delegate tasks to the robot.<br>
</li><li>From the Robot interface, the user can change settings such as the IP address of the Host pc.</li></ul>

<h3>3.1.2 Hardware Interfaces</h3>
<ul><li>Use wireless network (WiFi) over TCP/IP to communicate between host and robot.<br>
</li><li>Host acts as server and robot acts as client while establishing connection.<br>
</li><li>Robot is able to connect or disconnect from host through interface.<br>
</li><li>Robot can configure the IP address of host so the host can be deployed on different environment.<br>
</li><li>Use serial port with cable over RS-232 to communicate between robot and control unit.<br>
</li><li>Robot has interface for controlling its movement therefore it can be tested without receiving command from host.</li></ul>

<h3>3.1.3 Software Interfaces</h3>
<ul><li>The Operating System must be Windows, and has been tested on Windows XP, Windows Vista and Windows 7.<br>
</li><li>Host PC’s must have the most current version of .Net Framework.</li></ul>

<h3>3.1.4 Communications Protocols</h3>
<blockquote>Please refer to <a href='http://see3po.googlecode.com/svn/trunk/Documents/Hardware/communication%20protocols.doc'>Communications Protocol Document</a>.</blockquote>

<br />
<h2>3.2 Software Product Features</h2>

<h3>3.2.1 Graphical User Interface</h3>
<blockquote>The primary GUI will reside on the Host PC and handle the following tasks:<br>
<ul><li>Connection to the Robot<br>
</li><li>Floor Plan Loading:<br>
<ul><li>From Image<br>
</li><li>From Saved Floor Plan files<br>
</li></ul></li><li>Automated and Manual placement of Robot within the floorplan<br>
</li><li>Selection of Robot Destination<br>
</li></ul>The GUI will have three main components<br>
<ul><li>A Live View panel will show a video feed from the robot.<br>
</li><li>A text output window will display connection information and other messages<br>
</li><li>A Floorplan window will handle the following input:<br>
<ul><li>Setting floor plan scale by drawing a scale line and inputing the distance drawn<br>
</li><li>Manually setting the Robot's current position<br>
</li><li>Selecting the desired destination.<br>
</li></ul><blockquote>It will also display the following output:<br>
</blockquote><ul><li>A simplified image of the floorplan<br>
<ul><li>One pixel equals one square step.<br>
</li><li>Color will indicate walkablility.<br>
</li></ul></li><li>The Robot's current position will be indicated by a red arrow.<br>
</li><li>The current path will be indicated by a series of blue lines.<br>
</li><li>The destination will be indicated by a blue "X"</li></ul></li></ul></blockquote>


<h3>3.2.2 Floor Plan Processing</h3>
<blockquote>Floor plan processing consists of two components:<br>
<ul><li>Floor Plan<br>
</li><li>Floor Tile</li></ul></blockquote>

<blockquote>Floor Plan takes an image of an actual floor plan, (currently supporting both .bmp and .jpg format) and it converts the same to an array of floor tile objects. This creation of floor tile array works in the following way:</blockquote>

<ul><li>The dimension of each tile is set as the scale selected in GUI.<br>
</li><li>Each tile that has RGB component darker than that of the color gray will be treated as un-walkable, else it will be walkable.</li></ul>

<blockquote>Floor plan has another functionality that connects each tile in the it, with the four neighboring walkable tiles of the same(top, bottom, right and left). If any of the neighboring tile is un-walkable, it is not added to this list and the program continues with the next neighbor of the original tile, if any, or else with the next tile in the array of floor tiles. This walkable neighboring tiles information is kept in the floor tile object itself. Other than this floor plan works with GUI in setting the start and target tile in it.</blockquote>


<blockquote>Floor tile, as the name suggests, corresponds to the tile object for a floor plan. It has a location in X-Y co-ordinate system for the floor plan, with (0,0) being the location for the first tile of a floor plan. A tile could be either walkable or un-walkable, as mentioned earlier. Also it keeps a list of neighbors of itself as mentioned before.</blockquote>

<blockquote>For more details please look into <a href='http://code.google.com/p/see3po/wiki/FloorPlanProcessing'>Floor Plan Document</a>.</blockquote>

<h3>3.2.3 Path Finding</h3>

<ul><li>Requirement: Develop a simple path planning algorithm to generate a near optimal path between two locations.<br>
</li><li>Diliverable: Ability to automatically generate a near-optimal path for the robot to follow.<br>
</li><li>Design:<br>
<ul><li><b>Strategy Disign Patterh</b>: we define an interface to adapt other path finding algorithms for further usages<br>
</li><li><b>Adapter Design Pattern</b>: The implementation of the interface is in a concrete class QGPathFinder, which serves as an <b>adapter</b>. The class GQPathFinder transfers FloorPlan and FloorTile into a graph structure and returns a near optimal path between the starting and target point as a list of FloorTile.<br>
</li><li>We used a C# graph library <b>QuickGraph</b> since we have established works using C#.</li></ul></li></ul>


For more details please look into <a href='http://code.google.com/p/see3po/wiki/PathFinding'>Path Finding Document</a>.<br>
<br>
<h2>3.3 Software System Attributes</h2>
<h3>3.3.1 Reliability</h3>
<ul><li>It is extremely that the robot act correctly in the case of a lost connection. To accomplis this, the robot will recieve discrete commands. If the robot has not recieved a next command before completion of its current move, it will stop.<br>
</li><li>To ensure that moves are accurate, between each move, the robot will acquire its current position and compare this to an expected value. If the values don't match, the path will be recalculated.<br>
</li><li>Because the robot will deal with discrete directions (for, as of this writing), a readjustment algorithm will be used to bring the robot to true, in the case of a misalignment.<br>
<h3>3.3.2 Availability</h3>
</li></ul><blockquote>This software will be made free for download from <a href='http://code.google.com/p/see3po/downloads/list'>the wiki download page</a>
<h3>3.3.3 Security</h3>
Because the robot is searching for a specific host by IP, it would be difficult to hijack the robot with another PC.<br>
<h3>3.3.4 Maintainability</h3>
All source code except for WhereAmI will be publicly avaiable at <a href='http://code.google.com/p/see3po/'>the See3PO Google Code site</a>
<h3>3.3.5 Performance</h3>
Performance will be weighed against reliability. By reducing the calls to WhereAmI and the Pathfinder, performance is expected to increase, while reliability decreases. We intend to find a balance of the two.</blockquote>

<h1>4 Revision History</h1>
<table><thead><th> <b>Revision Date</b> </th><th> <b>Description Date</b> </th><th> <b>Document Version</b> </th><th> <b>Author</b> </th></thead><tbody>
<tr><td> 12/02/2009           </td><td> Created outline for draft verion of SRS </td><td> 0.1                     </td><td> <a href='Frank.md'>Frank</a></td></tr>
<tr><td> 12/04/2009           </td><td> Added Introduction      </td><td> 0.2                     </td><td> <a href='Debarati.md'>Debarati</a></td></tr>
<tr><td> 12/05/2009           </td><td> Added Added Hardware/Communications Requirements</td><td> 0.3                     </td><td> <a href='Jacky.md'>Jacky</a></td></tr>
<tr><td> 12/06/2009           </td><td> Added User Interface Requirements</td><td> 0.4                     </td><td> <a href='Frank.md'>Frank</a></td></tr>
<tr><td> 12/07/2009           </td><td> Added section 3.2.2 Floor Plan Processing </td><td> 0.5                     </td><td> <a href='Debarati.md'>Debarati</a></td></tr>
<tr><td> 12/07/2009           </td><td> Added Sections 2.1, 2.2. 2.4 (some skipped)</td><td> 0.6                     </td><td> <a href='Frank.md'>Frank</a></td></tr>
<tr><td> 12/08/2009           </td><td> Added Sections 3.2.3 Path Finding </td><td> 0.7                     </td><td> <a href='Nick.md'>Nick</a></td></tr>
<tr><td> 12/14/2009           </td><td> Added Section 3.3 Software System Attributes </td><td> 0.8                     </td><td> <a href='Frank.md'>Frank</a></td></tr>
<tr><td> 12/14/2009           </td><td> Made minor changes to Introduction and 3.2.2 </td><td> 0.9                     </td><td> <a href='Debarati.md'>Debarati</a></td></tr>