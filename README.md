# SharpBypassUAC
C# tool for UAC bypasses

# Usage
SharpBypassUAC currently supports the **eventvwr**, **fodhelper**, **sdclt**, **slui**, and **DiskCleanup** UAC bypasses.

SharpBypassUAC accepts a base64 encoded windows command to be executed in high integrity. The command is base64 encoded to be easily used in tools such as Covenant's "Assembly" task

## Parameters
  -b, --bypass=VALUE         Bypass to execute: eventvwr, fodhelper, sdclt, slui, diskcleanup
  
  -e, --encodedCommand=VALUE Base64 encoded command to execute

### Example usage for eventvwr bypass to launch calc.exe
SharpBypassUAC.exe -b eventvwr -e Y21kIC9jIHN0YXJ0IGNhbGMuZXhl

### Example usage for fodhelper bypass to launch calc.exe
SharpBypassUAC.exe -b fodhelper -e Y21kIC9jIHN0YXJ0IGNhbGMuZXhl

### Example usage for sdclt bypass to launch calc.exe
SharpBypassUAC.exe -b sdclt -e Y21kIC9jIHN0YXJ0IGNhbGMuZXhl
- Note: this appears to only work on Windows 10 in my testing

### Example usage for slui bypass to launch calc.exe
SharpBypassUAC.exe -b slui -e Y21kIC9jIHN0YXJ0IGNhbGMuZXhl

### Example usage for DiskCleanup bypass to launch calc.exe
SharpBypassUAC.exe -b dikcleanup -e Y21kIC9jIHN0YXJ0IGNhbGMuZXhlICYmIFJFTQ==
- Note: The command you execute will need to end in "&& REM"

## Detections
Most of these bypasses rely on modifying registry keys in the HKCU hive, specifically keys under HKCU\Software\Classes\. HKCU\Software\Classes\ should be monitored for any new keys or modification to existing keys. If this is too noisy in your environment, the specific keys used for each technique are listed below.

### Eventvwr
Registry modifications to:
- HKCU\Software\Classes\mscfile\Shell\Open\command
  - Modifies the "(default)" value with the command to execute

### Fodhelper
Registry modifications to:
- HKCU\Software\Classes\ms-settings\Shell\Open\command
  - Modifies the "(default)" value with the command to execute
  - Modifies the "DelegateExecute" value with an empty value

### SDCLT
Registry modifications to:
- HKCU\Software\Classes\Folder\shell\open\command
  - Modifies the "(default)" value with the command to execute
  - Modifies the "DelegateExecute" value with an empty value

### SLUI
Registry modifications to:
- HKCU\Software\Classes\exefile\Shell\Open\command
  - Modifies the "(default)" value with the command to execute

### DiskCleanup
Registry modifications to:
- HKCU\Environment
  - Modifies the "windir" value with the command to execute
Starts the "\Microsoft\Windows\DiskCleanup\SilentCleanup" scheduled task. Example:

schtasks /Run /TN \\Microsoft\\Windows\\DiskCleanup\\SilentCleanup /I

# Credits
**eventvwr**:
[enigma0x3's](https://github.com/enigma0x3) [Invoke-EventVwrBypass.ps1 script](https://github.com/enigma0x3/Misc-PowerShell-Stuff/blob/master/Invoke-EventVwrBypass.ps1)

**fodhelper**:
[winscripting.blog's](https://github.com/winscripting) [FodhelperBypass.ps1 script](https://github.com/winscripting/UAC-bypass)

**sdclt**: [Emeric Nasi's](https://twitter.com/emericnasi?lang=en) [blog post](http://blog.sevagas.com/?Yet-another-sdclt-UAC-bypass)

**slui**: [bytecode77's](https://github.com/bytecode77) [slui file handler hijack tool](https://github.com/bytecode77/slui-file-handler-hijack-privilege-escalation)

**DiskCleanup**: [enigma0x3's](https://github.com/enigma0x3) Bypassing UAC on Windows 10 using Disk Cleanup [blog post](https://enigma0x3.net/2016/07/22/bypassing-uac-on-windows-10-using-disk-cleanup/) and [gushmazuko's](https://github.com/gushmazuko) [DiskCleanupBypass_direct.ps1 script](https://github.com/gushmazuko/WinBypass/blob/master/DiskCleanupBypass_direct.ps1)

Many of these were discovered by going through the **UACME** project found on [github](https://github.com/hfiref0x/UACME/).
