@ECHO OFF

:: Administrative permission check
net session >nul 2>nul
IF ERRORLEVEL 1 (
	color 4F
	echo ���Ҽ�����Ա����.
	echo.
	pause & break
	echo.
	cls
)

sc delete autopppoe-service
echo ���Թرմ�����
pause