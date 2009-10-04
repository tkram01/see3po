command0 var byte
command1 var byte
arg1 var word
arg2 var word
eot var byte
left_speed var word
right_speed var word
i var word


oninterrupt timeraint, send_data_packet
TMA = (TMA & 0xF0) | 0x4
enable timeraint


low p12
low p13
command0 = 0


sound p9, [300\2000, 10\50, 300\2000]


main:

pulsout p12, 3000
pulsout p13, 3000
pause 20

goto main


execute_command:
if command0 = 16 then
  sound p9, [100\4000, 100\6000, 100\8000]
  if command1 = 1 then 
    gosub forward
  elseif command1 = 2
    gosub forward_left
  elseif command1 = 3 
    gosub forward_right
  elseif command1 = 4
    gosub turn_left
  elseif command1 = 5
    gosub turn_right
  elseif command1 = 6
    gosub back
  elseif command1 = 7
    gosub back_left
  elseif command1 = 8
    gosub back_right
  endif
endif

sound p9, [100\8000]

return


send_data_packet:
serout S_OUT, i9600, 50, end_int, ["asdsf"]
goto receive_command

receive_command:
  serin S_IN, i9600, 50, end_int, [command0, command1, arg1.highbyte, arg1.lowbyte, arg2.highbyte, arg2.lowbyte, eot]
  gosub execute_command

end_int:
  resume


forward:
  right_speed = 3000 + (arg1*10)
  left_speed = 3000 - (arg1*10)
  gosub run
return


forward_left:
  right_speed = 3000
  left_speed = 3000 - (arg1*10)
  gosub run
return


forward_right:
  right_speed = 3000 + (arg1*10)
  left_speed = 3000
  gosub run
return


turn_left:
  right_speed = 3000 + (arg1*10)
  left_speed = 3000 + (arg1*10)
  gosub run
return


turn_right:
  right_speed = 3000 - (arg1*10)
  left_speed = 3000 - (arg1*10)
  gosub run
return


back:
  right_speed = 3000 - (arg1*10)
  left_speed = 3000 + (arg1*10)
  gosub run
return


back_left:
  right_speed = 3000
  left_speed = 3000 + (arg1*10)
  gosub run
return


back_right:
  right_speed = 3000 - (arg1*10)
  left_speed = 3000
  gosub run
return


run:
for i = 1 to arg2
  gosub step
next
return


step:
  pulsout p12, right_speed
  pulsout p13, left_speed
  pause 20
return
