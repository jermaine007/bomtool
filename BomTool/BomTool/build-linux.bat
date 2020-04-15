@echo off
cd /d %~dp0
dotnet publish^
 -c Release^
 -f netcoreapp3.0^
 -r linux-x64^
 --self-contained^
 -o bin\Publish\Linux^
 /p:PublishSingleFile=true^
 /p:BuiltFor=Linux

pause