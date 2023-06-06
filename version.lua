json = require("lua_modules.JSON")

config_file_name = "Config/version.json"

function sleep (a) 
    local sec = tonumber(os.clock() + a); 
    while (os.clock() < sec) do 
    end 
end



version = {
	version_data = {
		major = 0,
		minor = 1,
		path = 2,
		util = ""
	},
	message = ""
}

function version:load_version()
	local file = io.open(config_file_name, "r")

	text = file:read("*a")

	self.version_data = json:decode(text)

	io.close(file)
end

function version:upload_version()
	local file = io.open(config_file_name, "w")

	file:write(json:encode(self.version_data))

	io.close(file)
end

function version:format()
	if self.version_data.util == "" then
		return "v" .. self.version_data.major .. "." .. self.version_data.minor .. "." .. self.version_data.path
	else
		return "v" .. self.version_data.major .. "." .. self.version_data.minor .. "." .. self.version_data.path .. "-" .. self.version_data.util
	end
end

function version:git_commit()
	os.execute("git add .")
	os.execute("git commit -m \"pre-version commit " .. self:format() .. "\"")
end

function version:git_tag()
	os.execute("git tag -a " .. self:format() .. " -m \"" .. self.message .. "\"")
end

function version:git_show_info()
	os.execute("git show " .. self:format())
end

function version:git_push()
	os.execute("git push origin " .. self:format())
end



function main()
	version:load_version()

	while true do
		io.write("Current version is: " .. version:format() .. "\n")

		io.write("Type next move [C/c]hange / [U/u]til / [I/i]ncrement / [G/g]it / [E/e]xit: ")

		local move = string.lower(io.read())

		if move == "c" then
			io.write("Change [A]ll / [M/m]ajor / [Mi/mi]nor / [P/p]ath: ")
			local switch = string.lower(io.read())

			if switch == "a" then
				io.write("major: ")
				version.version_data.major = io.read()

				io.write("minor: ")
				version.version_data.minor = io.read()

				io.write("path: ")
				version.version_data.path = io.read()
			elseif switch == "m" then
				io.write("major: ")
				version.version_data.major = io.read()
			elseif switch == "mi" then
				io.write("minor: ")
				version.version_data.minor = io.read()
			elseif switch == "p" then
				io.write("path: ")
				version.version_data.path = io.read()
			end
		elseif move == "u" then
			io.write("[A/a]dd / [C/c]hange / [D/d]elete: ")

			local switch = io.read()

			if switch == "a" then
				io.write("Type util: ")
				version.version_data.util = io.read()
			elseif switch == "c" then
				local file = io.open("util_tmp.txt", "w")
				file:write(version.version_data.util)
				io.close(file)
				
				os.execute("util_tmp.txt")

				file = io.open("util_tmp.txt", "r")
				repeat
					sleep(2)
					file:seek("set", 0)
				until file:read("*a") ~= version.version_data.util
				
				file:seek("set", 0)

				version.version_data.util = file:read("*a")
				
				io.close(file)
				os.remove("util_tmp.txt")
			elseif switch == "d" then
				version.version_data.util = ""
			end
		elseif move == "i" then
			io.write("[M/m]ajor / [Mi/mi]nor / [P/p]ath: ")

			local switch = string.lower(io.read())

			if switch == "m" then
				version.version_data.major = version.version_data.major + 1
			elseif switch == "mi" then
				version.version_data.minor = version.version_data.minor + 1
			elseif switch == "p" then
				version.version_data.path = version.version_data.path + 1
			end
		elseif move == "g" then
			io.write("Type the message: ")
			version.message = io.read()

			version:git_commit()
			version:git_tag()
			version:git_push()
		elseif move == "e" then
			break
		end
	end

	version:upload_version()
end

main()
