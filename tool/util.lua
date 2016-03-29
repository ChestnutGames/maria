package.path = "./../sprotodump/?.lua;" .. package.path

os.execute("cd ./../sprotodump && ./../tool/lua sprotodump.lua -cs ./../tool/c2s.sproto -d ./../tool -namespace c2s")