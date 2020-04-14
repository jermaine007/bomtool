@echo off
cd /d %~dp0
dotnet publish -c Release -f netcoreapp3.0 -r win-x64 --self-contained -o bin\publish /p:PublishSingleFile=true /p:PublishTrimmed=true /p:PublishReadyToRun=true /p:PublishReadyToRunShowWarnings=true

call "%VS140COMNTOOLS%vsvars32.bat"
editbin.exe /subsystem:windows "%~dp0bin\publish\BomTool.exe"

pause