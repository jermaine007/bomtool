@echo off
set PROJECT_DIR=%~dp0..\
cd /d %PROJECT_DIR%
dotnet publish^
 -c Release^
 -f netcoreapp3.0^
 -r linux-x64^
 --self-contained^
 -o bin\Publish\Linux^
 /p:PublishSingleFile=true^
 /p:BuiltFor=Linux

pause