using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : BaseSingleton<GameMain>
{
    public override void Awake()
    {
        base.Awake();
        //初始化游戏组件：场景中释放各种管理器

        //附加xLua管理器
        gameObject.AddComponent<xLuaMgr>();
    }

    //检查是否有更新
    IEnumerator CheckHotUpdate()
    {
        yield return null;
    }

    //开始游戏
    IEnumerator StartGame()
    {
        //待检查更新结束
        yield return StartCoroutine(CheckHotUpdate());

        //由xLua管理器利用Lua虚拟机运行Lua的逻辑代码
        xLuaMgr.Instance.EnterGame();
    }

    private void Start()
    {
        StartCoroutine(StartGame());
    }


}
