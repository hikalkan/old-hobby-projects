# Oyunu Free Pascal ile Windows icin derler ve win\ klasorune calisir hale getirir.
# Gereksinim: Free Pascal 3.2+ (ornegin: scoop install freepascal)
$ErrorActionPreference = 'Stop'
Set-Location $PSScriptRoot

$fpc = (Get-Command fpc -ErrorAction Stop).Source
if ($fpc -like '*\scoop\shims\*') { $fpcRoot = "$env:USERPROFILE\scoop\apps\freepascal\current" }
else { $fpcRoot = Split-Path (Split-Path (Split-Path $fpc)) }
$units = Get-ChildItem "$fpcRoot\units" -Directory | Select-Object -First 1

$obj = Join-Path $env:TEMP 'tsl-obj'
New-Item -ItemType Directory -Force win, $obj | Out-Null
$common = @("-Fu$($units.FullName)\*", "-FU$obj", '-FEwin', '-vew')

fpc @common TSL.PAS
if ($LASTEXITCODE -ne 0) { throw 'TSL.PAS derlenemedi' }
fpc @common TSLCONV.PAS
if ($LASTEXITCODE -ne 0) { throw 'TSLCONV.PAS derlenemedi' }

# Veri dosyalari: Real (6 bayt) iceren dosyalar cevrilir, digerleri kopyalanir.
# win\ icinde zaten varsa (devam eden oyun) dokunulmaz.
if (-not (Test-Path win\ILKVERI.TSL)) {
    & .\win\TSLCONV.exe . win
    if ($LASTEXITCODE -ne 0) { Remove-Item win\ILKVERI.TSL -ErrorAction SilentlyContinue; throw 'Veri dosyalari cevrilemedi' }
    Copy-Item FIKS.TSL, OYNKAYIT.TSL win\
}
Remove-Item win\*.o, win\link*.res -ErrorAction SilentlyContinue
Write-Host 'Tamam: win\TSL.exe'
