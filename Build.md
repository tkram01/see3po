# Prerequisites #
  1. Code is written in C#, and requires Microsoft Visual Studio to compile
  1. The code requires [.Net Framework 3.5](http://www.microsoft.com/downloads/details.aspx?FamilyId=333325FD-AE52-4E35-B531-508D977D32A6&displaylang=en) to run
  1. The Host PC must be wireless networking and its IP address must be known.

## Host PC Interface ##
  1. Download all latest code from [Google Code](http://code.google.com/p/see3po/source/browse/trunk#trunk/SourceCode/GUI)
  1. Check that all files are labeled with the release number
  1. Open See3PO.sln in Visual Studio
  1. Compile See3PO

## Robot Interface ##
> ### How to upload/update files onto robot's embedded computer ###
    1. [Download ActiveSync](http://www.microsoft.com/windowsmobile/en-us/downloads/microsoft/activesync-download.mspx) application and then install.
    1. Connect Host and embedded computer with USB cable.
    1. Run [ActiveSync](http://www.microsoft.com/windowsmobile/en-us/downloads/microsoft/activesync-download.mspx) application.
    1. Click on explore bottom.
    1. The folders and files should show up in an explore window.
    1. Drag and drop the file needs to be uploaded.

> ### How to upload/update programs onto ATOM microchip ###
    1. Download [BasicATOMPro ](http://basicmicro.com/ViewPage.aspx?ContentCode=d_basicatompro) and then install.
    1. Connect ATOM and PC with serial cable (COM port).
    1. Run BasicATOMPro program.
    1. Open the source file that need to be uploaded.
    1. Build the program then the compiled bin file will be upload to ATOM.
