package.path = "./../sprotodump/?.lua;" .. package.path
dofile("mk_sproto.lua")
os.execute("cd ./../sprotodump && ./lua sprotodump.lua -cs ./../tool/c2s.sproto -d ./../ -p c2s")