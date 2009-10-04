   LIST n=0
P9 equ 09h
_MHZ equ 01312D00h
_ADDRESSMASK equ 01FFh
_TYPEMASK equ 0E00h
_SIGNMASK equ 04000h
_INDEXMASK equ 07000h
_INDEXOFFSET equ 0Ch
_ARRAYMASK equ 08000h
_FLOATTYPE equ 0E00h
_SLONGTYPE equ 04A00h
_LONGTYPE equ 0A00h
_SWORDTYPE equ 04600h
_SBYTETYPE equ 04200h
_WORDTYPE equ 0600h
_BYTETYPE equ 0200h
_NIBTYPE equ 0400h
_BITTYPE equ 0800h
_BANK0_START equ 020h
_BANK0_END equ 070h
_BANK1_START equ 0A0h
_BANK1_END equ 0F0h
_BANK2_START equ 0110h
_BANK2_END equ 0170h
_BANK3_START equ 0190h
_BANK3_END equ 01F0h
_w_temp equ 07Fh
_status_temp equ 07Eh
_pclath_temp equ 07Dh
_fsr_temp equ 07Ch
_INTSTATEH equ 07Bh
_INTSTATE equ 07Ah
_DEBUGWORK5 equ 079h
_DEBUGWORK4 equ 078h
_DEBUGWORK3 equ 077h
_DEBUGWORK2 equ 076h
_DEBUGWORK equ 075h
_CMD5 equ 079h
_CMD4 equ 078h
_CMD3 equ 077h
_CMD2 equ 076h
_CMD equ 075h
_SP equ 074h
_PCSP equ 073h
_PCU equ 072h
_PCH equ 071h
_PC equ 070h
_WORK equ 020h
_WORK2 equ 021h
_WORK3 equ 022h
_WORK4 equ 023h
_ENHWORK equ 024h
_ENHWORK2 equ 025h
_ENHWORK3 equ 026h
_ENHWORK4 equ 027h
_ENHWORK5 equ 028h
_ENHWORK6 equ 029h
_ENHWORK7 equ 02Ah
_ENHWORK8 equ 02Bh
_STACK equ 02Ch
_PCSTACK equ 070h
#include "C:\PROGRA~1\BASICA~1\system\PBASIC~2.INC" ;pBASICATOM28.inc
      __CONFIG 0
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MB30AD~1.LIB" ;mbasic14\mbasicmac.lib
#include "C:\PROGRA~1\BASICA~1\system\sys.mac" ;sys.mac
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MB3A95~1.LIB" ;mbasic14\mbasicdef.lib
;********************************************************************************
;	Basic Micro MBasic generated assembly file
;********************************************************************************
		ORG     0x000             ; processor reset vector
        errorlevel -306
                nop                       ; ICD support
                movlw   (_PROCSETUP>>8)
                movwf   PCLATH
                goto    _PROCSETUP        ; go to beginning of program
        errorlevel +306

                ORG     0x004             ; interrupt vector location
                movwf   _w_temp           ; save off current W register contents
                movfw   STATUS            ; move status register into W register
                clrf    STATUS            ; Select bank0
                movwf   _status_temp      ; save off contents of STATUS register
                movfw   PCLATH            ; move PCLATH register into W register
                clrf    PCLATH            ; Select Page0
                movwf   _pclath_temp      ; save off contents of PCLATH register
                movfw   FSR               ; move FSR register into W register
                movwf   _fsr_temp         ; save off contents of FSR register
_ISRSTART                                 ; ISR Start
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MB5CDB~1.LIB" ;mbasic14\mbasicint.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MB7774~1.LIB" ;mbasic14\mbasicwork.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MBAF42~1.LIB" ;mbasic14\mbasicclrw.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\mbasicgo.lib" ;mbasic14\mbasicgo.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MB42DD~1.LIB" ;mbasic14\mbasicpin.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MB9126~1.LIB" ;mbasic14\mbasicfreq.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MB38C5~1.LIB" ;mbasic14\mbasicmem.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MBFB2D~1.LIB" ;mbasic14\mbasictable.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MB5B6C~1.LIB" ;mbasic14\mbasicmath.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MBF525~1.LIB" ;mbasic14\mbasiclogic.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MBE374~1.LIB" ;mbasic14\mbasicfloat.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MB54AB~1.LIB" ;mbasic14\mbasicenh.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MB1BE1~1.LIB" ;mbasic14\mbasicnummod.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MB1AF2~1.LIB" ;mbasic14\mbasicstrmod.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MB5D50~1.LIB" ;mbasic14\mbasictime.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MBF13A~1.LIB" ;mbasic14\mbasicfornext.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MBASIC~2.LIB" ;mbasic14\mbasicbranch.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MBASIC~3.LIB" ;mbasic14\mbasicbutton.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MBASIC~4.LIB" ;mbasic14\mbasicclear.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MB0D77~1.LIB" ;mbasic14\mbasiccount.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MB7D3E~1.LIB" ;mbasic14\mbasicdtmf.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MB0D47~1.LIB" ;mbasic14\mbasicsound.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MBF565~1.LIB" ;mbasic14\mbasicdtmf2.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MB7554~1.LIB" ;mbasic14\mbasiclook.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MB1C37~1.LIB" ;mbasic14\mbasicpulse.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MB6EC9~1.LIB" ;mbasic14\mbasicpwm.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MB32DD~1.LIB" ;mbasic14\mbasicram.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MB05FB~1.LIB" ;mbasic14\mbasicrctime.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MB38E5~1.LIB" ;mbasic14\mbasicser.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MB32D7~1.LIB" ;mbasic14\mbasicserdet.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MB0375~1.LIB" ;mbasic14\mbasicservo.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MBE47E~1.LIB" ;mbasic14\mbasicshift.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MB9B36~1.LIB" ;mbasic14\mbasicstep.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MB38E7~1.LIB" ;mbasic14\mbasiceeprom.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MB0820~1.LIB" ;mbasic14\mbasicflash.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MB32A1~1.LIB" ;mbasic14\mbasiclcd.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MBD793~1.LIB" ;mbasic14\mbasici2c.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MB38FF~1.LIB" ;mbasic14\mbasicwdt.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MB1537~1.LIB" ;mbasic14\mbasicowire.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MBD9E8~1.LIB" ;mbasic14\mbasicx10.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MBASIC~1.LIB" ;mbasic14\mbasicadin.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MB495C~1.LIB" ;mbasic14\mbasichard.lib
#include "C:\PROGRA~1\BASICA~1\system\mbasic14\MB4095~1.LIB" ;mbasic14\mbasiccmd.lib
   LIST n=0
   _@PACKEDBUFFER
_START
   _@DEBUGDATA 0x1
   _@CMD _CONBP
   _@ARGB 009h ;P9
   _@CMD _CLRWP1
   _@CMD _CONP
   _@ARGW 003e8h ;1000
   _@CMD _CONP
   _@ARGW 03e80h ;16000
   _@CMD _CLRW
   _@CMD _FREQOUT
   _@END
   end
