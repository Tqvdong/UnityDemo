using UnityEngine;
using System.Collections;
using XLua;

public class NewBehaviourScript : MonoBehaviour {

    XLua.LuaEnv luaenv = new XLua.LuaEnv();
    public float a = 9;
	// Use this for initialization
	void Start () {

        int i = Random.Range(0, 10);
        Debug.Log(i);
        luaenv.DoString("CS.UnityEngine.Debug.Log('hello')");
        luaenv.DoString("print('lua hello world')");
        luaenv.DoString("require'main'");

        int a = luaenv.Global.Get<int>("a");
        Debug.Log(a);

        
        Table t = luaenv.Global.Get<Table>("t");
        Debug.Log(t.a);
        
        XLua.LuaFunction addfun = luaenv.Global.Get<XLua.LuaFunction>("addfun");
        object[] c = addfun.Call(1, 2);
        int cint = System.Convert.ToInt32(c[0]);
        Debug.Log(cint);

        XLua.LuaFunction update = luaenv.Global.Get<XLua.LuaFunction>("update");
        update.Call();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Test(int i) {
        Debug.Log("xxrrrrr" + i);


    }
}

 public class Table
{
   public int a;
   public int b;
}