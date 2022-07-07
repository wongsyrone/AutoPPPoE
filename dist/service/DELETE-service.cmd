@ECHO OFF

:: Administrative permission check
net session >nul 2>nul
IF ERRORLEVEL 1 (
	color 4F
	echo 请右键管理员运行.
	echo.
	pause & break
	echo.
	cls
)

sc delete autopppoe-service
echo 可以关闭窗口了
pause