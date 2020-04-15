@echo off
cd /d %~dp0

dotnet publish^
 -c Release^
 -f netcoreapp3.0^
 -r osx-x64^
 --self-contained^
 -o bin\Publish\Osx^
 /p:PublishSingleFile=true^
 /p:BuiltFor=Osx

pause