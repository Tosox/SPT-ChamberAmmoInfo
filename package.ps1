param(
  [string]$PluginName = "Tosox.ChamberAmmoInfo",
  [string]$DllName = "Tosox.ChamberAmmoInfo.dll"
)

$repoRoot = $PSScriptRoot
$bin = Join-Path $repoRoot "bin\Release"
$dll = Join-Path $bin $DllName
if (-not (Test-Path $dll)) {
  throw "DLL not found. Build Release first."
}

$ver = (Get-Item $dll).VersionInfo.FileVersion
$packRoot = Join-Path $repoRoot "packed"

# Create layout
$dstPlugin = Join-Path $packRoot "BepInEx\plugins"
New-Item $dstPlugin -ItemType Directory -Force | Out-Null

# Copy DLL
Copy-Item $dll $dstPlugin

# Zip
$zipName = "$PluginName-v$ver.zip"
$zipPath = Join-Path $repoRoot $zipName
if (Test-Path $zipPath) { Remove-Item $zipPath -Force }
Compress-Archive -Path (Join-Path $packRoot "*") -DestinationPath $zipPath

# Clean up
Remove-Item $packRoot -Recurse -Force -ErrorAction SilentlyContinue | Out-Null

Write-Host "Packaged -> $zipPath"
