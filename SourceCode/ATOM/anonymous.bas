ir var long
go var byte
i var byte


low p12
low p13
go = 0


sound P9, [200\1000, 100\2000, 400\8000]


main:
if in4 = 0 then gosub pwr_toggle
if go = 0 then no_pwr
gosub check
if ir < 100 then
  for i = 1 to 5
    gosub drive
  next
else
  pause 500

  for i = 1 to 30
    gosub back_up
  next

  pause 500

  for i = 1 to 15
    gosub turn_left
  next

  pause 500

endif
goto main


no_pwr:
pulsout p12, 3000
pulsout p13, 3000
goto main


pwr_toggle:
if in4 = 0 then pwr_toggle
if go = 1 then
go = 0
else
go = 1
endif
return


check:
adin p0, ir
return


drive:
pulsout p12, 3300
pulsout p13, 2700
pause 20
return


back_up:
pulsout p12, 2700
pulsout p13, 3300
pause 20
return


turn_left:
pulsout p12, 3300
pulsout p13, 3300
pause 20
return


turn_right:
pulsout p13, 3300
pulsout p13, 3300
pause 20
return
