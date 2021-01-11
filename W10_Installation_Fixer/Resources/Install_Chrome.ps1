#Install Chrome
$Installer = "$env:temp\chrome_installer.exe"
$url = 'http://dl.google.com/chrome/install/latest/chrome_installer.exe'
Invoke-WebRequest -Uri $url -OutFile $Installer -UseBasicParsing
Start-Process -FilePath $Installer -Args '/silent /install' -Wait
Remove-Item -Path $Installer

#Make chrome default browser
If (Test-Path -Path "${Env:ProgramFiles(x86)}\Google\Chrome\Application\")
{
	$chromePath = "${Env:ProgramFiles(x86)}\Google\Chrome\Application\"
} Else {
	$chromePath = "${Env:ProgramFiles}\Google\Chrome\Application\"
}
$chromeApp = "chrome.exe"
$chromeCommandArgs = "--make-default-browser"
& "$chromePath$chromeApp" $chromeCommandArgs

exit