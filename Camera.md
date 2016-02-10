# Camera Options #
| Option | Requirements  | Problems | Resolution | Discussion |
|:-------|:--------------|:---------|:-----------|:-----------|
| Current Camera with Win CE | Win CE Firewire Driver <br> Win CE Camera Driver <table><thead><th>          </th><th> <font color='Orange'>Not Resolved</font><br> </th><th> <a href='http://groups.google.com/group/see3po/browse_thread/thread/d9f5e5ccae1f449e#'> discussion </a> </th></thead><tbody>
<tr><td> Current Camera with Win XP Embedded  </td><td> Win XP Embedded must be supported by our computer <br> Win XP Camera Driver<br> Win XP Firewire Driver </td><td>          </td><td> <font color='Red'> Resolved </font> <br> Win XP is not supported by our computer<br> </td><td> <a href='http://groups.google.com/group/see3po/browse_thread/thread/1b8b7c6f4bb47bc2'> discussion</a> </td></tr>
<tr><td> New USB Camera </td><td> Minimum Resolution: <br> Minimum Frame Rate: </td><td> Only USB 1.1 is supported by the Embedded PC <br> Least attractive option to Tyler </td><td> <font color='Orange'>Not Resolved</font></td><td>            </td></tr>
<tr><td> Replace Embedded PC with a Netbook</td><td> Expansion Slot for Firewire support <br> A Serial Port <br> Physical connection to the robot </td><td>          </td><td> <font color='Orange'>Not Resolved</font><br> </td><td> <a href='http://groups.google.com/group/see3po/browse_thread/thread/50f1e3def3161ad5#'> discussion</a></td></tr>
<tr><td> Add one Firewire port </td><td> Make sure camera works on current Firewire </td><td> Need to send the computer back to manufacture </td><td> <font color='Orange'>Not Resolved</font></td><td>            </td></tr></tbody></table>


<h2>Current camera manufacture Knowledge Base</h2>
<ul><li>Article 106: Imaging Products camera compatability with Windows CE .NET.<br>
SUMMARY:<br>
Windows CE .NET is a generalized embedded operating system for devices such as handhelds, thin clients, logic controllers, and advanced consumer electronics. This article briefly describes the compatability of PGR Imaging Products with this operating system.<br>
APPLICABLE PRODUCTS :<br>
Dragonfly (NANC) •  Scorpion •  Firefly2 (NANC) •  Flea (NANC) •<br>
ANSWER:<br>
Windows CE .NET is a generalized embedded operating system for devices such as handhelds, thin clients, logic controllers, and advanced consumer electronics.</li></ul>

Although Point Grey Research does not support our products for use on this operating system, some customers have successfully used the Dragonfly on a Windows CE .NET 4.1 platform.<br>
<br>
One specific customer installed Windows CE .NET on a Pentium III clone PC/104 single-board computer (SBC), which supplies power to the Dragonfly.<br>
<br>
Using a hi-res (1024x768) Dragonfly, the customer changed the 1394 DCAM driver that comes with the operating system using Platform Builder. No additional information on the specific changes made is available.<br>
<br>
Using a lo-res (640x480) Dragonfly requires the appropriate registry keys to be added so that Plug and Play can recognize the camera; the registry keys are as follows:<br>
<br>
[HKEY_LOCAL_MACHINE\WDMDrivers\{7EA55E5C-F3A5-4ec2-AE0A-F9B7203C13F5}\0002]<br>
"MatchingDeviceId"="1394\\A02D&102"<br>
"Dll"="1394dcam.DLL"<br>
"FriendlyName"="Dragonfly Digital Camera 1.0.0.20"<br>
<br>
<br>
<h1>Solution</h1>

<blockquote>We have decided to replace the embedded computer on robot with a new one which must be able to run Windows XP embedded or Windows XP. The requirements of this replacing computer are as following:<br>
</blockquote><ol><li>2 serial ports (RS232)<br>
</li><li>WiFi connection<br>
</li><li>2 Firefire (IEEE 1394) ports<br>
</li><li>Windows XP embedded or Windows XP<br>
</li><li>on-board boot up device, such as  DOM (DiskOnModule), CFC (CompactFlash Card), PFD (PATA/IDE Flash Disk), SFD (SATA Flash Disk)<br>
</li><li>power by battery</li></ol>


<h1>New embedded computer choices</h1>
<ol><li>advantech - PCM-9562<br>
<ul><li><font color='Red'><a href='http://www.advantech.com/products/PCM-9562/mod_1-E0EHC7.aspx'>PCM-9562</a> main board<br>
</li><li><a href='http://www.advantech.com/products/PCM-3115/mod_1-2JKGQH.aspx'>PCM-3115</a> 2-slot Card Bus Module to plug in our 2 Firewire ports adapter with PCMCIA interface that we have now.<br>
</li><li>Pre-install Windows XPe<br>
</li><li>Wiring kit for PCM-9562 (Part# PCM-10586-9562E)<br>
</li><li>CompactFlash 50-pin to IDE 44-pin adapter (Part# CF-HDD-ADP)<br>
</li><li>AT Cable 4P x 2/4200-H-4P 15 cm (Part# 170304015K)</font>
</li><li><a href='http://www.google.com/products/catalog?q=mini-card+wireless&oe=utf-8&client=firefox-a&cid=1666255041516663562&sa=title#cond=1'>Dell Wireless 3945</a> PCI-E Mini-Card Network adapter<br>
</li><li>2G DDR2 SDRAM (667 MHz, 200-pin SODIMM)<br>
</li><li>1 CompactFlach Card TYPE I/II (at least 4Gb)<br>
</li><li>12V battery 13W 1.2A<br>
</li></ul></li><li>Liantec - EMB-5740<br>
<ul><li><font color='Red'><a href='http://www.liantec.com/product/emboard/EMB-5740.htm'>EMB-5740</a> main board with 512MB on-board SDRAM<br>
</li><li><a href='http://www.liantec.com/product/tbm/TBM-1260.htm'>TBM-1260</a> Tiny-Bus IEEE1394 Firewire Module with 3 External IEEE1394 Firewire Ports and Mini-PCI Type-IIIA/B Socket</font>
</li><li>Mini-PCI Type-III wireless 802.11 a/b/g(WLAN)<br>
</li><li>1G DDR2 SDRAM<br>
</li><li>1 CompactFlach Card TYPE I/II (at least 4Gb)<br>
</li><li>12V battery<br>
</li></ul></li><li>GlobalAmerican Inc - LS-571<br>
<ul><li><font color='Red'><a href='http://www.globalamericaninc.com/p3307830/3307830_-_5.25%22_Embedded_Controller_with_Socket_P_for_Intel_Core_2_Duo_processor/product_info.html'>LS-571</a> 3307830F - 5.25" Embedded Controller with Socket P for Intel Core 2 Duo processor and 1 GB DDR2 on-board SDRAM<br>
</li><li><a href='http://www.globalamericaninc.com/Mini_PCI_Modules-IEEE_1394/c55_432/p1807613/1807613_-_Mini-PCI_IEEE_1394A_Firewire_400_Module/product_info.html'>MP-323</a> 1807613 Mini-PCI IEEE 1394A Firewire 400 Module<br>
</li><li><a href='http://www.globalamericaninc.com/Mini_PCI_Modules-LAN_/_Wireless_LAN/c55_430/p1507631/1507631_-_Mini-PCI_Wireless_LAN_Card/product_info.html'>MP-2501</a> 1507631 Mini-PCI Wireless LAN Card<br>
</li><li><a href='http://www.globalamericaninc.com/p3704008/IDE_Disk_On_Module_-_Choose_Your_Options/product_info.html'>Disk On Module</a> - 8G </font>
</li><li>12V battery  (at least 90W)<br>
</li></ul></li><li>AAEON Tech - EPIC-9457 Rev. B<br>
<ul><li><font color='Red'><a href='http://www.aaeon.com/PD_Products_Detail_DCCAA0A1DF3344B7BB_C8064EACC7F34610B4_7345A4463FCD402DBC_TW_UTF-8.html'>EPIC-9457 Rev. B</a> main board with onboard DDRII 400/533 Memory 1 GB<br>
</li><li><a href='http://www.aaeon.com/PD_Products_Detail_DCCAA0A1DF3344B7BB_D57323E1E0A7458C95_E7CA10C0F83D41C3A9_TW_UTF-8.html'>PCM-3115C</a> PC/104 2-slot PCMCIA Module to plug in our 2 Firewire ports adapter with PCMCIA interface that we have now.</font>
</li><li><a href='http://www.google.com/products/catalog?q=mini-card+wireless&oe=utf-8&client=firefox-a&cid=1666255041516663562&sa=title#cond=1'>Dell Wireless 3945</a> PCI-E Mini-Card Network adapter<br>
</li><li>1 CompactFlach Card TYPE I/II (at least 4Gb)<br>
</li><li>+8.5~19V battery<br>
</li><li>Touch Screen Supports 4/5/8-wire (optional)</li></ul></li></ol>

<h1>We have chosen</h1>
<ul><li>Logic Supply <a href='http://www.logicsupply.com/products/j7f2we_1g'>Jetway J7F2WE-1G Mini-ITX Mainboard</a> with 1GB memory ($132+$39 = $171)<br>
</li><li>Logic Supply <a href='http://www.logicsupply.com/products/picopsu_80'>PicoPSU-80 DC-DC Power Converter</a> ($25)<br>
</li><li>Logic Supply <a href='http://www.logicsupply.com/products/fdm80sqi4g'>Emphase 40-pin Industrial Flash Disk Module 4 GB</a> ($72)<br>
</li><li>Logic Supply <a href='http://www.logicsupply.com/products/g01_1394_1p'>IEEE 1394 (FireWire) Pin Header Cable</a> ($8.5)<br>
</li><li>USB wireless adapter - <a href='http://www.amazon.com/gp/offer-listing/B000CRFI8A/ref=dp_olp_new_map?ie=UTF8&condition=new'>Linksys by Cisco WUSB54GC Compact Wireless-G USB Adapter</a> ($38.57)<br>
</li><li>12V/5A battery - <a href='http://www.all-battery.com/12v5000mahflatnimhbatterypack11637.aspx'>12V 5000mAh Flat NiMH Battery</a> ($77.84)<br>
</li><li>Battery charger - <a href='http://www.all-battery.com/smartuniversalbatterypackcharger12v-168vcurrentselection.aspx'>Smart Universal Battery Pack Charger: 12V - 16.8V</a> ($22.99)</li></ul>

Therefore, the total cost is about $410