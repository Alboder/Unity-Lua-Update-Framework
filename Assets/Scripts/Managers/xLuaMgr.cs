using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

public class xLuaMgr : BaseSingleton<xLuaMgr>
{
    private LuaEnv env = null;
    private bool isGameStarted = false;
    private const string luaScriptsFolder = "LuaScripts";

    //AssetBundle myAsset;

    public override void Awake()
    {
        base.Awake();

        InitLuaEnv();

    }

    private void Update()
    {
        //在Update中清除Lua的未手动释放的LuaBase对象
        if (env != null)
        {
            env.Tick();
        }

        if (isGameStarted)
        {

        }
    }

    private void FixedUpdate()
    {
        if (isGameStarted)
        {

        }
    }

    private void LateUpdate()
    {
        if (isGameStarted)
        {

        }
    }

    //销毁时调用Dispose
    private void OnDestroy()
    {
        env.Dispose();
    }

#pragma warning disable 162
    public byte[] LuaScriptLoader(ref string filePath)
    {
        string scriptPath = string.Empty;
        filePath = filePath.Replace(".", "/") + ".lua";
        byte[] data;
#if UNITY_EDITOR && !PUBLISHING_MODE
        scriptPath = Path.Combine(Path.Combine(Application.dataPath, luaScriptsFolder), filePath);
        data = GameUtility.SafeReadAllBytes(scriptPath);
        Debug.Log("Editor");
        return data;
#endif
        Debug.Log("Publishing");
        //发布模式
        //从StreamingAssets文件夹中获取AssetBndle里面地lua.bytes文件
        AssetBundle myAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "luascripts"));
        if (myAssetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            return null ;
        }

        string assetPath = Path.GetFileName(filePath) + ".bytes";
        data = myAssetBundle.LoadAsset<TextAsset>(assetPath).bytes;
        myAssetBundle.Unload(false);
        return data;
    }

    public byte[] TestLoader(ref string filePath)
    {
        filePath = filePath.Replace(".", "/");
        filePath = Path.GetFileName(filePath);
        AssetBundle m_asset = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "luascripts"));
        if (m_asset == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            return null;
        }
        var data = m_asset.LoadAsset<TextAsset>(filePath + ".lua.bytes");
        //m_asset.Unload(false);
        return data.bytes;
    }

    void InitLuaEnv()
    {
        env = new LuaEnv();
        env.AddLoader(LuaScriptLoader);
        isGameStarted = false;
    }

    //进入游戏，运行游戏代码
    public void EnterGame()
    {
        isGameStarted = true;

        env.DoString("require 'Main'");
        env.DoString("main.Init()");

        //AssetBundle m_asset = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "luascripts"));
        //Debug.Log(Application.streamingAssetsPath);
        //if (m_asset == null)
        //{
        //    Debug.Log("Failed to load AssetBundle!");
        //}

        //var data = m_asset.LoadAsset<TextAsset>("/LuaBytes/Main.lua.bytes");

        //Debug.Log(data.text);

        //m_asset.Unload(false);
    }


}
