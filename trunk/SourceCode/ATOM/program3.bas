'end of transmission
EOT con 0xEF

'in-packet types
PONG con 0x01
COMMAND con 0x10

'command classes
SYSTEM con 0x00
CONTINUOUS_DRIVE con 0x11

'commands for 0x1X's
HALT con 0x00
FORWARD con 0x01
FORWARD_LEFT con 0x02
FORWARD_RIGHT con 0x03
TURN_LEFT con 0x04
TURN_RIGHT con 0x05
BACKUP con 0x06
BACKUP_LEFT con 0x07
BACKUP_RIGHT con 0x08


inPacket var byte(6)
i var word
tempWord var word

leftSpeed var word
rightSpeed var word


leftSpeed = 3000
rightSpeed = 3000

sound p9, [200\500]

low p12
low p13

sound p9, [200\3000]


main:
  gosub drive
  gosub receive_command
goto main


receive_command:
  serin S_IN, i9600, ret, 350000, ret, [inPacket(0), inPacket(1), inPacket(2), inPacket(3), inPacket(4), inPacket(5)]

  if inPacket(0) = COMMAND then
    gosub parse_command
  endif
return


ret:
return


parse_command:
  if inPacket(1) = SYSTEM then
    if inPacket(2) = 1 then
      sound p9, [100\1000, 100\1000, 100\3000]
    elseif inPacket(2) = 2
      sound p9, [100\880, 100\988, 100\1046, 100\1175]
    elseif inPacket(2) = 3
      sound p9, [200\1000, 100\2000, 400\8000]
    elseif inPacket(2) = 4
      sound p9, [200\3000, 100\6000, 50\50, 100\6000]
    elseif inPacket(2) = 5
      sound p9, [300\2000, 10\50, 300\2000]
    endif
    return
  elseif inPacket(1) = CONTINUOUS_DRIVE
    'sound p9, [100\3000]

    tempWord.highbyte = inPacket(3)
    tempWord.lowbyte = inPacket(4)

    if inPacket(2) = HALT then
      rightSpeed = 3000
      leftSpeed = 3000
    elseif inPacket(2) = FORWARD
      rightSpeed = 3000 - tempWord
      leftSpeed = 3000 + tempWord
    elseif inPacket(2) = FORWARD_LEFT
      rightSpeed = 3000
      leftSpeed = 3000 + tempWord
    elseif inPacket(2) = FORWARD_RIGHT
      rightSpeed = 3000 - tempWord
      leftSpeed = 3000
    elseif inPacket(2) = TURN_LEFT
      rightSpeed = 3000 + tempWord
      leftSpeed = 3000 + tempWord
    elseif inPacket(2) = TURN_RIGHT
      rightSpeed = 3000 - tempWord
      leftSpeed = 3000 - tempWord
    elseif inPacket(2) = BACKUP
      rightSpeed = 3000 + tempWord
      leftSpeed = 3000 - tempWord
    elseif inPacket(2) = BACKUP_LEFT
      rightSpeed = 3000
      leftSpeed = 3000 - tempWord
    elseif inPacket(2) = BACKUP_RIGHT
      rightSpeed = 3000 + tempWord
      leftSpeed = 3000
    endif
  endif
return


drive:
  for i = 1 to 5
    pulsout p12, rightSpeed
    pulsout p13, leftSpeed
    pause 20
  next
return
