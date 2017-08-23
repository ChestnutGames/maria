
@echo off
set lua=".\..\3rd\sproto-Csharp\tools\lua.exe"
set lpeg=".\..\3rd\sproto-Csharp\tools\lpeg.so"
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
	copy .\lpeg.so %dst_path%
) else	(
	echo "exist lua"
)

lua.exe util.lua

pause