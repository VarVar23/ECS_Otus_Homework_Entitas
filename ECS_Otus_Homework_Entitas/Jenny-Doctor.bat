@echo off
pushd %~dp0
dotnet Jenny\Jenny.Generator.Cli.dll doctor
popd
pause
