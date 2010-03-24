' Lisence:
' $license$  

'end of transmission
EOT con 0xEF

'in-packet types
PONG con 0x01
COMMAND con 0x10

'out-packet types
PING con 0x02
DATA_PACKET con 0x10

'command classes
SYSTEM con 0x00
TIMED_DRIVE con 0x10
CONTINUOUS_DRIVE con 0x11

'commands for 0x1X's
HALT con 0x00
FORWARD con 0x01
FORWARD_LEFT con 0x02
FORWARD_RIGHT con 0x03
TURN_lEFT con 0x04
TURN_RIGHT con 0x05
BACKUP con 0x06
BACKUP_LEFT con 0x07
BACKUP_RIGHT con 0x08


inPacket var byte(7)
i var word
tempWord var word

queueIndex var byte
fillIndex var byte
leftSpeedBuffer var word(10)
rightSpeedBuffer var word(10)
durationBuffer var word(10)

leftSpeed var word
rightSpeed var word
duration var word
inQueue var byte

distance var word
battery var word


queueIndex = 0
fillIndex = 0
inQueue = 0
leftSpeed = 3000
rightSpeed = 3000
for i = 0 to 6
  inPacket(i) = 0x00
next


low p12
low p13


sound p9, [200\3000, 100\6000, 50\50, 100\6000]


main:
  'if inQueue = 0 then
    gosub drive
  'else
  '  gosub run_command
  'endif

  gosub read_sensors

  gosub send_data_packet
  gosub receive_command
goto main



read_sensors:
  adin p0, distance
  adin p12, battery
return

send_data_packet:
  inPacket(6) = 0x00
  serout S_OUT, i9600, 5, main, [DATA_PACKET, distance, battery, inPacket(0), inPacket(1), inPacket(2), inPacket(3), inPacket(4), inPacket(5), inPacket (6), EOT]
return

receive_command:
  serin S_IN, i9600, 30, main, [inPacket(0), inPacket(1), inPacket(2), inPacket(3), inPacket(4), inPacket(5), inPacket(6)]

  if inPacket(0) = COMMAND then
    gosub parse_command
  endif
return

parse_command:
gosub drive
  if inPacket(1) = SYSTEM then
    return
  elseif inPacket(1) = CONTINUOUS_DRIVE
    sound p9, [100\3000, 100\800]
    queueIndex = fillIndex
    inQueue = 0

    tempWord.highbyte = inPacket(3)
    tempWord.lowbyte = inPacket(4)

    leftSpeed = 3000
    rightSpeed = 3000

    if inPacket(2) = FORWARD then
    sound p9, [100\3000, 100\800]
      rightSpeed = 3000 + tempWord
      leftSpeed = 3000 - tempWord
    elseif inPacket(2) = FORWARD_LEFT
      leftSpeed = 3000 - tempWord
    elseif inPacket(2) = FORWARD_RIGHT
      rightSpeed = 3000 + tempWord
    elseif inPacket(2) = TURN_LEFT
      rightSpeed = 3000 + tempWord
      leftSpeed = 3000 + tempWord
    elseif inPacket(2) = TURN_RIGHT
      rightSpeed = 3000 - tempWord
      leftSpeed = 3000 - tempWord
    elseif inPacket(2) = BACKUP
      rightSpeed = 3000 - tempWord
      leftSpeed = 3000 + tempWord
    elseif inPacket(2) = BACKUP_LEFT
      rightSpeed = 3000 - tempWord
    elseif inPacket(2) = BACKUP_RIGHT
      leftSpeed = 3000 + tempWord
    endif
  elseif inPacket(1) = TIMED_DRIVE
    inQueue = 1
    
    tempWord.highbyte = inPacket(3)
    tempWord.lowbyte = inPacket(4)
    
    durationBuffer(fillIndex).highbyte = inPacket(5)
    durationBuffer(fillIndex).lowbyte = inPacket(6)
    
    leftSpeedBuffer(fillIndex) = 3000
    rightSpeedBuffer(fillIndex) = 3000

    if inPacket(2) = FORWARD then
      rightSpeedBuffer(fillIndex) = 3000 + tempWord
      leftSpeedBuffer(fillIndex) = 3000 - tempWord
    elseif inPacket(2) = FORWARD_LEFT
      rightSpeedBuffer(fillIndex) = 3000 + tempWord
    elseif inPacket(2) = FORWARD_RIGHT
      leftSpeedBuffer(fillIndex) = 3000 - tempWord
    elseif inPacket(2) = TURN_LEFT
      rightSpeedBuffer(fillIndex) = 3000 + tempWord
      leftSpeedBuffer(fillIndex) = 3000 + tempWord
    elseif inPacket(2) = TURN_RIGHT
      rightSpeedBuffer(fillIndex) = 3000 - tempWord
      leftSpeedBuffer(fillIndex) = 3000 - tempWord
    elseif inPacket(2) = BACKUP
      rightSpeedBuffer(fillIndex) = 3000 - tempWord
      leftSpeedBuffer(fillIndex) = 3000 + tempWord
    elseif inPacket(2) = BACKUP_LEFT
      rightSpeedBuffer(fillIndex) = 3000 - tempWord
    elseif inPacket(2) = BACKUP_RIGHT
      leftSpeedBuffer(fillIndex) = 3000 + tempWord
    endif
    
    fillIndex = fillIndex + 1
    if fillIndex = 10 then
      fillIndex = 0
    endif
  endif
return






run_command:
  gosub drive
  duration = duration - 1000

  if duration < 0 then
    gosub fetch_next_command
  endif

return


drive:
  for i = 1 to 20
    pulsout p12, 2500
    pulsout p13, 3500
    pause 20
  next
return


fetch_next_command:
  queueIndex = queueIndex + 1

  if queueIndex = 10 then
    queueIndex = 0
  endif

  if queueIndex = fillIndex then
    leftSpeed = 3000
    rightSpeed = 3000
    inQueue = 0
  else
    leftSpeed = leftSpeedBuffer(queueIndex)
    rightSpeed = rightSpeedBuffer(queueIndex)
    duration = durationBuffer(queueIndex)
  endif
return
