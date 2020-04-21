@echo off
set PROJECT_DIR=%~dp0..\
cd /d %PROJECT_DIR%
dotnet publish^
 -c Release^
 -f netcoreapp3.0^
 -r osx-x64^
 --self-contained^
 -o bin\Publish\Osx^
 /p:PublishSingleFile=true^
 /p:BuiltFor=Osx

pause