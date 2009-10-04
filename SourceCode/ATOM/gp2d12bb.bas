'Program name: GP2D12BB.BAS
'Author: Jim Frye

' This program tests the functionality of the three Sharp GP2D12 sensors. The 
' values are displayed in the Terminal1 window. Click on the Terminal1 Tab, 
' change the baud rate from 300 to 57.6kbs, then click Connect. You should see
' the values change as you pass your hand infront of the sensors. The values 
' will go up as things get closer to the sensors. The values should be around 
' 0-5 with nothing in front of the sensors. The Terminal should look like the 
' following [Left 2   Right 2   Rear 2].

'Bot Board Jumpers
' VS to VL
' AX 0-3 power bus to VL

'Connections
'AX1	Left GP2D12 Sensor
'AX2	Right GP2D12 Sensor
'AX3	Rear GP2D12 Sensor

right_detect	var	word
left_detect		var	word
rear_detect		var	word

main:
adin ax1, 2, AD_RON, right_detect
adin ax2, 2, AD_RON, left_detect
adin ax3, 2, AD_RON, rear_detect
serout S_OUT,i57600,["   Left ", dec left_detect, "   Right ", dec right_detect, "   Rear ", dec rear_detect, 13]
goto main
