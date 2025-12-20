@echo off

dotnet run

@if "%ERRORLEVEL%" == "0" goto success


:fail
	echo "This Application Has Failed..."
	echo "return value = %ERRORLEVEL%"
	goto end
:success
	echo "This Application Has Succeeded"
	echo "return value = %ERRORLEVEL%"
	goto end
:end

echo "Alllllllll Done."