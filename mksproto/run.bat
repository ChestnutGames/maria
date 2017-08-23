
@echo off
set lua=".\..\3rd\sproto-Csharp\tools\lua.exe"
set lpeg=".\..\3rd\sproto-Csharp\tools\lpeg.dll"
set lua53=".\..\3rd\sproto-Csharp\tools\lua53.dll"
set pthread=".\..\3rd\sproto-Csharp\tools\pthreadVC2.dll"
set dst_path=".\..\3rd\sproto-Csharp\tools"

rem 判断lua文件是否存在
if not exist %lua% (
	echo "not exist lua"
	copy .\lua.exe %dst_path%
) else (
	echo "exist lua"
)

rem 判断lpeg文件是否存在
if not exist %lpeg% (
	echo "not exist lpeg"
	copy .\lpeg.dll %dst_path%
) else	(
	echo "exist lua"
)

rem 判断lpeg文件是否存在
if not exist %lua53% (
	echo "not exist lua53"
	copy .\lua53.dll %dst_path%
) else	(
	echo "exist lua53"
)

rem 判断lpeg文件是否存在
if not exist %pthread% (
	echo "not exist pthreadVC2"
	copy .\pthreadVC2.dll %dst_path%
) else	(
	echo "exist pthreadVC2"
)

lua.exe mk_sproto.lua

cd ..\3rd\sproto-Csharp\tools
lua.exe sprotodump.lua -cs ..\..\..\mksproto\c2s.sproto -d ..\..\..\mksproto\ -p c2s
lua.exe sprotodump.lua -cs ..\..\..\mksproto\s2c.sproto -d ..\..\..\mksproto\ -p s2c

cd ..\..\..\mksproto
pause