local filename = "./../../cat/proto.lua"
local fd = io.open(filename, "r")
local buffer = fd:read("a")
fd:close()
local c = {}
local x = 1
local c2s = string.gsub(buffer, ".+%[%[([%w%.%*%s%_]+)%]%]?.+%[%[([%w%.%*%s%_]+)%]%].+", function ( seg )
	-- body
	print(seg)
	if x == 1 then
		fd = io.open("c2s.sproto", "w")
		fd.write(seg)
		fd:close()
	elseif x == 2 then
		fd = io.open("s2c.sproto", "w")
		fd.write(seg)
		fd:close()
	end
	x = x + 1
end)

