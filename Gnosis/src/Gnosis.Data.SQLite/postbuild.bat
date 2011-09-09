@echo off
 
Set RegQry=HKLM\Hardware\Description\System\CentralProcessor\0
 
REG.exe Query %RegQry% > checkOS.txt
 
Find /i "x86" < CheckOS.txt > StringCheck.txt
 
If %ERRORLEVEL% == 0 (
    copy System.Data.SQLite32.DLL System.Data.SQLite.DLL
) ELSE (
    copy System.Data.SQLite64.DLL System.Data.SQLite.DLL
)