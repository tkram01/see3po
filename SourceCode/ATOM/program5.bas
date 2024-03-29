'end of transmission
EOT con 0xEF

'in-packet types
PONG con 0x01
COMMAND con 0x10

'command classes
SYSTEM con 0x00
CONTINUOUS_DRIVE con 0x11

inPacket var byte(7)	' recieving buffer for serial port
i var word
left var word    		' left speed recieved from command
right var word			' right speed recieved from command

leftSpeed var word    	' left speed send to motor
rightSpeed var word   	' right speed send to motor
initialized var byte

left = 0
right = 0
leftSpeed = 3000   		' Left Scorpion stop value.
rightSpeed = 3000		' Right Scorpion stop value.
initialized = 0

sound p9, [200\500]		' make a sound when this program started

' Ensure pulsout commands are positive going.
low p12
low p13

sound p9, [200\3000]


main:
  gosub drive
  gosub receive_command
goto main


receive_command:
  'waiting commands from serial port for 3.5 seconds
  serin S_IN, i9600, ret, 350000, ret, [inPacket(0), inPacket(1), inPacket(2), inPacket(3), inPacket(4), inPacket(5), inPacket(6)]

  if (inPacket(0) = COMMAND) and (inPacket(6) = EOT ) then ' check start byte and end byte
    gosub parse_command    	'parse the commmand
  else
    sound p9, [100\1000, 100\1000] 	' make sound for packet check error
    gosub ret
  endif
return


ret:
  'rightSpeed = 3000    	'stop driving if there is a wrong command
  'leftSpeed = 3000   	' or there is no command came in
return


parse_command:
  if inPacket(1) = SYSTEM then 
    if inPacket(2) = 1 then ' make 5 different sounds for testing purpose
      sound p9, [100\1000, 100\1000, 100\3000]
    elseif inPacket(2) = 2
      sound p9, [100\880, 100\988, 100\1046, 100\1175]
    elseif inPacket(2) = 3
      sound p9, [200\1000, 100\2000, 400\8000]
    elseif inPacket(2) = 4
      sound p9, [200\3000, 100\6000, 50\50, 100\6000]
    elseif inPacket(2) = 5
      sound p9, [300\2000, 10\50, 300\2000]
    else
      gosub ret ' no such type of sound   
    endif
    return
  elseif inPacket(1) = CONTINUOUS_DRIVE
    ' read the speed from command
    left.highbyte = inPacket(2)
    left.lowbyte = inPacket(3)
    right.highbyte = inPacket(4)
    right.lowbyte = inPacket(5)
    
    'setting motor speed
    rightSpeed = 3000 - right
    leftSpeed = 3000 + left
    initialized = 1
    
  else
    sound p9, [100\1000, 100\1000, 100\3000]  ' make sound for command check error
    gosub ret  ' no such type of command
  endif
  
return


drive:
  if initialized = 1  then
     for i = 1 to 5
       pulsout p12, rightSpeed		' Left Scorpion channel.
       pulsout p13, leftSpeed		' Right Scorpion channel.
       pause 20
     next
  endif  
return
