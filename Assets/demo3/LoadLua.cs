using UnityEngine;
using System.Collections;
using System.IO;
using XLua;
using System;
using System.Collections.Generic;

public class LoadLua : MonoBehaviour
{

    private Action luaStart;
    private Action luaUpdate;
    private Action luaOnDestroy;

    string mUpdateFilesPath;
    string luaText;
    Action luaTestOutPut;
    LuaEnv testLua;
    // Use this for initialization
    void Awake()
    {
        mUpdateFilesPath = "file://" + Application.streamingAssetsPath + "/" + "LuaTest.lua.txt";
        StartCoroutine("Download");
    }



    IEnumerator Download()
    {
        //从本地加载Lua脚本更新文件，假设文件已经从服务器下载下来  
        WWW _WWW = new WWW(mUpdateFilesPath);
        Debug.Log(mUpdateFilesPath);
        yield return _WWW;
        //读取服务器版本
        luaText = _WWW.text;
        //比较本地的luaText和网络端版本号，版本号不一致则下载新lua
        Debug.Log("获得lua文件");
        Debug.Log(_WWW.text);

       testLua = new LuaEnv();
        testLua.DoString(luaText);


        testLua.Global.Set("self", GameObject.Find("Capsule"));
        testLua.Global.Set("test", GameObject.Find("Button"));

        testLua.Global.Get("start", out luaStart);
        testLua.Global.Get("update", out luaUpdate);
        testLua.Global.Get("ondestroy", out luaOnDestroy);
        testLua.Global.Get("luaTest", out luaTestOutPut);
        luaTestOutPut();
        Debug.Log("完成下载lua");
       
    }

    void Start()
    {
        if (luaStart != null)
        {
            luaStart();
        }
    }

    void Update()
    {
        if (luaUpdate != null)
        {
            luaUpdate();
        }

     if (Input.GetKeyDown(KeyCode.F)) {
            PrintTest();
           
        }
    }

    void PrintTest() {


        int speed = testLua.Global.Get<int>("speed1");
       // Debug.Log(speed);

        Person p = testLua.Global.Get<Person>("Person");
       // Debug.Log(p.name);
       // Debug.Log(p.age);

        Person_1 p1 = testLua.Global.Get<Person_1>("Person1");
       // Debug.Log(p1.name);
        p1.eat("org");


        Dictionary<string, object> dict = testLua.Global.Get<Dictionary<string, object>>("Person2");
        foreach (string key in dict.Keys) {
            print("key:" + key + "   value:" + dict[key]);

        }

       /* List<object> dic = testLua.Global.Get<List<object>>("Person2");
        foreach (string key in dic) {
           

        }*/

        Add add = testLua.Global.Get<Add>("add");
        int res1 = 0;int res2 = 0;
        int res = add(3, 4,out res1,out res2);
        Debug.Log(res);
        Debug.Log(res1);
    
    }

    void OnDestroy()
    {
        if (luaOnDestroy != null)
        {
            luaOnDestroy();
        }
        luaOnDestroy = null;
        luaUpdate = null;
        luaStart = null;
       
    }

    class Person
    {
        public string name;
        public int age;
    }

    [CSharpCallLua]
    interface Person_1 {

        string name { get; set; }
        int age { get; set; }
        void eat(string str);
    }

    [CSharpCallLua]
    delegate int Add(int a, int b, out int res1, out int res2);
    
}
