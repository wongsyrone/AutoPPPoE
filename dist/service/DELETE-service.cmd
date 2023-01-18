@ECHO OFF
title 删除auto pppoe服务

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

echo 按任意键卸载auto pppoe服务
echo.
pause

sc delete autopppoe-service
echo 可以关闭窗口了
pause