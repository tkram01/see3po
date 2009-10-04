left_pwr var long
right_pwr var long
ir var long
i var byte


left_pwr = 3000
right_pwr = 3000

serout S_OUT, i57600, ["no no"]

low p0
low p1
input p12


if in0 = p0 then gosub sound1


'signal end of priming
sound P9, [100\16000]


'main
main:

if in4 = 0 then right
if in5 = 0 then left
if in6 = 0 then both

pulsout p0, left_pwr
pulsout p1, right_pwr

pause 20

goto main

right:
if in4 = 0 then right
left_pwr = left_pwr + 100
goto main

left:
if in5 = 0 then left
left_pwr = left_pwr - 100
goto main

both:
adin ax0, ir
serout S_OUT, i57600, ["asdf"]
if ir > 500 then gosub sound1
pause 200
goto main

sound1:
sound P9, [250\8000]
return