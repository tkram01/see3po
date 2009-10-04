i var byte
freq var word


for i = 1 to 5 step 1
freq = (i+5)*100
sound P9, [20*i\freq]
next

pause 1000

for i = 5 to 1 step -1
freq = (i+5)*100
sound P9, [20*i\freq]
next