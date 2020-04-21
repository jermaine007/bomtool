@echo off
set PROJECT_DIR=%~dp0..\
cd /d %PROJECT_DIR%
dotnet publish^
 -c Release^
 -f netcoreapp3.0^
 -r win-x64^
 --self-contained^
 -o bin\Publish\Windows^
 /p:PublishSingleFile=true^
 /p:PublishTrimmed=true^
 /p:PublishReadyToRun=true^
 /p:PublishReadyToRunShowWarnings=true^
 /p:BuiltFor=Windows

call "%VS140COMNTOOLS%vsvars32.bat"
editbin.exe /subsystem:windows "%PROJECT_DIR%bin\Publish\Windows\BomTool.exe"

pause