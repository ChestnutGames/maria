#!/bin/bash

lua="./../3rd/sproto-Csharp/tools/lua"
lpeg="./../3rd/sproto-Csharp/tools/lpeg.so"

# if [[ ! -d "./../../skynet" ]]; then
# 	#statements
# 	git submodule update --init
# fi

if [[ ! -f "$lua" ]]; then
	#statements	
	# if [[ ! -f "./../../skynet/3rd/lua/lua" ]]; then
	# 	#statements
	# 	make -C ./../../skynet/3rd/lua linux
	# fi
	cp ./lua $lua
fi

if [[ ! -f "$lepg" ]]; then
	#statements
	# if [[ ! -f "./../../skynet/3rd/lpeg/lpeg.so" ]]; then
	# 	#statements
	# 	make -C ./../../skynet/3rd/lpeg
	# fi
	cp ./lpeg.so $lpeg
fi
# $0 == test.sh
lua mk_sproto.lua
cd ./../3rd/sproto-Csharp/tools 
lua sprotodump.lua -cs ./../../../mksproto/c2s.sproto -d ./../../../mksproto/ -p c2s
lua sprotodump.lua -cs ./../../../mksproto/s2c.sproto -d ./../../../mksproto/ -p s2c
cd ./../../../mksproto/
