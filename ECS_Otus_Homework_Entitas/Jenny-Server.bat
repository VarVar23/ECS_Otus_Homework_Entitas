@echo off
pushd %~dp0
echo Jenny server on port 3333. Keep this window open.
dotnet Jenny\Jenny.Generator.Cli.dll server
popd
pause
