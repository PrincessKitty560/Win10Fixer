# Silent Install Firefox 
$Installer = "$env:temp\firefox_installer.exe"
$url = "https://download.mozilla.org/?product=firefox-latest&os=win64&lang=en-US"
Invoke-WebRequest -Uri $url -OutFile $Installer -UseBasicParsing
Start-Process -FilePath $Installer -Args "/silent /install" -Wait
Remove-Item -Path $Installer

If (Test-Path -Path "${Env:ProgramFiles(x86)}\Mozilla Firefox\firefox.exe") 
{
	$FFPath = "${Env:ProgramFiles(x86)}\Mozilla Firefox\firefox.exe"
} Else {
	$FFPath = "${Env:ProgramFiles}\Mozilla Firefox\firefox.exe"
}
$FFApp = "firefox.exe"
$FFCommandArgs = "-silent -nosplash -setDefaultBrowser"
& "$FFPath$FFApp" $FFCommandArgs

exit