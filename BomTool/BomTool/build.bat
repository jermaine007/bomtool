@echo off
cd /d %~dp0
dotnet publish -c Release -f netcoreapp3.0 -r win-x64 --self-contained -o bin\publish /p:PublishSingleFile=true /p:PublishTrimmed=true /p:PublishReadyToRun=true

pause