@ECHO OFF
title ɾ��auto pppoe����

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

echo �������ж��auto pppoe����
echo.
pause

sc delete autopppoe-service
echo ���Թرմ�����
pause