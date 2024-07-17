# Skriptname: ShowMessage.ps1

# Anzeige des Textes
Write-Host "Install DesktopOK wird gestartet..."

winget search desktopok

Write-Host "File Gedownloade111 Drücke Taste"
# Warte auf eine Tasteneingabe
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")


