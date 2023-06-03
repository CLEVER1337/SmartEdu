json = require("lua_modules.JSON")

config_file_name = "Config/version.json"



version = {
	version_data = {
		major = 0,
		minor = 1,
		path = 2
	}
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

function main()
	version:upload_version()
end

main()