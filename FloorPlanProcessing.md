The given floor plan (as image file) will be processed into a 2-dimension array to represent "tiles". Each "tile" should be either walkable or unwalkable.

Add your content here.


# Input/Output #

### Input ###
As assigned by client (Tyler), we will have a floor plan in image format (bmp, png, or jpg)

![http://www.cs.umb.edu/img/third_floor_map.gif](http://www.cs.umb.edu/img/third_floor_map.gif)

http://see3po.googlegroups.com/attach/2feb91d66cd52d11/UMass+-+Science+Building+-+3rd+Floor+(2).jpg?view=1&part=4

### Output ###
2 dimension array (as tiles of floor plan).
Tile size might be similar to actual size of robot.

http://see3po.googlegroups.com/web/FloorPlanBoxes.JPG

# Established Codes #
Box-Count Dimension module by [Nick](Nick.md). (Java)

# Issues #

  * 4-direction movement vs. 8-direction movement
  * Clear unnecessary information (such as numbers in image file)

# Important points #

  * Need to specify the tile size in order for the program to generate appropriate array
    1. Just put a field in the constructor for now - it is already existing. but in order to call the function to produce the array we still need to specify the block length.
  * Need to specify the image location.
    1. Please explain this further - the program needs to know the location of the actual image in disk as otherwise it won't have any way to understand what image to work on.
  * Any color of the pixel that is darker than gray(RGB values) will be treated as un-walkable(need to  confirm if this is acceptable)
  * If on trying to create a block any remainder part is there (for both height and width of the image) that part will be treated as un-walkable.

## Output Produced for the time being ##

> <u>For the 2nd floor plan image above, with block size = 5, output is</u> : http://see3po.googlegroups.com/web/derived.txt

> <u>For the 2nd floor plan image above, with block size = 1, output is</u> : http://see3po.googlegroups.com/web/original.txt

## Test Cases ##
  * Added unit tests for block lengths of 4-6.
  * Added one file comparison test for a newly created array and a standard array for the same image. (for now I tested this using files created by the same program, so is not of much use unless we have a standard array for a standard image of may be block length 1)

## Relevent Codes ##
  * Created a simple program which on getting the full path of an image file (till now tested with .jpg and .bmp) and also the block length(equivalent to tile length), will create a 2D array.
  * Array consists of 0s and 1s, where one 0 represents walkable block and one 1 represents non-walkable block.
  * Code has been uploaded under FloorPlanProcessing Folder, inside the Folder called ImageTo2DArray
  * Corresponding unit tests are also present inside one of the sub-folders under ImageTo2DArray.