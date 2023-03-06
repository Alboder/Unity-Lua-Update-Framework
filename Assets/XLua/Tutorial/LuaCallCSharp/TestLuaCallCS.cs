using System;
using System.Collections;
using System.Collections.Generic;
using Tutorial;
using UnityEngine;
using XLua;

public class MyBaseClass
{
    public int BCID = 1024;
    public static string BC = "my base class";

    public void BCIDFunc()
    {
        Debug.Log("BCID: " + BCID);
    }

    public static void BCFunc()
    {
        Debug.Log("BC:" + BC);
    }
}

public class MyClass : MyBaseClass
{
    public int IDC;

    public void IDFunc()
    {
        Debug.LogFormat("My ID is: {0}", IDC);
    }

    public static MyClass operator +(MyClass x, MyClass y)
    {
        MyClass result = new MyClass();
        result.IDC = x.IDC + y.IDC;
        return result;
    }
}

[LuaCallCSharp]
public static class MyClassExtensions
{
    public static int GetSomeData(this MyClass obj)
    {
        Debug.Log("GetSomeData ret = " + obj.IDC);
        return obj.IDC;
    }
}

public class TestLuaCallCS : MonoBehaviour
{
    LuaEnv env = null;

    string script = @"
print('Hello World')

local MyClass = CS.MyClass
local m_class = MyClass()
m_class.IDC = 1092
print(m_class:GetSomeData())

    ";

    void Start()
    {
        env = new LuaEnv();
        env.DoString(script);

    }

    
    void Update()
    {
        if(env != null)
        {
            env.Tick();
        }
    }


    private void OnDestroy()
    {
        env.Dispose();  
    }
}
