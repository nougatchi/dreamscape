local key = "" -- this is the key that people will use to enter your Server
-- if the key specified here is not the same as the key people use to join (e.g. the key is bivkoi and the user is joining with a key of "")
-- then the connection will be denied, and if the key is blank and someone joins with a key the connection will be denied aswell.
-- you can manually change how it accepts requests by changing the function peerhandle
local game = DataModel:C("Factories"):C("GameLibrary")
local characterfactory = DataModel:C("Factories"):C("CharacterFactory")
local math = game.math
local random = game:GenerateRandom(game:FindEntropy())
resolution = {433, 426}

function peerhandle(request)
	request:AcceptIfKey(key)
end

function peerdisconnect(peer, info, player)
	player:Destroy()
end

function peerconnect(peer, player)
	game:Debug("New Connection")
	player = characterfactory:NewPlayer("newpeer",peer)
	char = player:GetChar()
	char:Fill(0,0,0)
	player:SetGameTitle("Dreamscape")
	char:LogEvent("<head><style>body { font-family: arial; font-size: 8; font-style: italics; } .s { font-style: italics; }</style></head>")
	char:LogEvent("<p class='s'>Welcome to Dreamscape!</p>")
	char:AddBrush(0,0,0) -- brush 0 black
	char:AddBrush(255,0,0) -- brush 1 red
	char:AddBrush(0,255,0) -- brush 2 green
	char:AddBrush(0,0,255) -- brush 3 blue
	char:AddBrush(255,0,255) -- brush 4 red+blue
	char:AddBrush(255,255,0) -- brush 5 red+green
	char:AddBrush(0,255,255) -- brush 6 green+blue
	char:AddBrush(255,255,255) -- brush 7 white
	char:AddBrush(127,127,127) -- brush 8 gray
	char:DrawText(7,216,70,"DREAMSCAPE")
end 

function act(action,peer,player)
	player:GetChar():LogEvent("<p class='chat'>" + action + "</p>")
end

function getinfo()
	return "<head><style>body { font-family: arial; }</style></head><body><h1>Generic Dreamscape Server</h1></body>"
end