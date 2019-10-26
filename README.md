# SharpBypassUAC
C# tool for UAC bypasses

# Usage
SharpBypassUAC currently supports the **eventvwr** and **fodhelper** UAC bypasses.

SharpBypassUAC accepts a base64 encoded windows command to be executed in high integrity. The command is base64 encoded to be easily used in tools such as Covenant's "Assembly" task

## Parameters
  -b, --bypass=VALUE         Bypass to execute: eventvwr, fodhelper
  
  -e, --encodedCommand=VALUE Base64 encoded command to execute

### Example usage for eventvwr bypass to launch calc.exe
SharpBypassUAC.exe -b eventvwr -e Y21kIC9jIHN0YXJ0IGNhbGMuZXhl

### Example usage for fodhelper bypass to launch calc.exe
SharpBypassUAC.exe -b eventvwr -e Y21kIC9jIHN0YXJ0IGNhbGMuZXhl

# Credits
These bypasses were based on the work done by [enigma0x3's](https://github.com/enigma0x3) [Invoke-EventVwrBypass.ps1 script](https://github.com/enigma0x3/Misc-PowerShell-Stuff/blob/master/Invoke-EventVwrBypass.ps1) and [winscripting.blog's](https://github.com/winscripting) [FodhelperBypass.ps1 script](https://github.com/winscripting/UAC-bypass)
