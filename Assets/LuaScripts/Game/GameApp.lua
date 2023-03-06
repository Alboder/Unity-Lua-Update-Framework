GameApp = {}

function EnterGameScene()
	print("Enter Game Scene")

	-- 释放游戏地图到场景

	-- 释放角色、怪物、道具、物品到场景
	local obj = CS.UnityEngine.GameObject("Player")
	obj:AddComponent(typeof(CS.UnityEngine.SpriteRenderer))
end

function GameApp.EnterGame()

	print("Enter Game")

	-- 进入游戏场景
	EnterGameScene()
end


return GameApp