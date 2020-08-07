using UnityEngine;
using System.Collections;
//using UnityEditor;
using System.IO;
using System;
using XLua;

public class AssetsBundle : MonoBehaviour
{
    
    public Injection[] injections;

    internal static LuaEnv luaEnv = new LuaEnv(); //all lua behaviour shared one luaenv only!
    internal static float lastGCTime = 0;
    internal const float GCInterval = 1;//1 second 

    private Action luaStart;
    private Action luaUpdate;
    private Action luaOnDestroy;

    private LuaTable scriptEnv;

    public TextAsset luaTest;

    void Awake()
    {

        ImportAsset();
        scriptEnv = luaEnv.NewTable();

        LuaTable meta = luaEnv.NewTable();
        meta.Set("__index", luaEnv.Global);
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();

        scriptEnv.Set("self", GameObject.Find("Cube"));//把this放进self中，在lua中调用
        scriptEnv.Set("test", GameObject.Find("Sphere"));

        foreach (var injection in injections)
        {
            scriptEnv.Set(injection.name, injection.value);//在injection.value的gameobject放到injuection.name中，在lua中使用
        }


        //运行测试的lua
        LuaEnv testLua = new LuaEnv();
        testLua.DoString(luaTest.text);
        
       
        testLua.Global.Set("self", GameObject.Find("Capsule"));
        testLua.Global.Set("button", GameObject.Find("Button"));
        testLua.Global.Set("MainCamera", GameObject.Find("Main Camera"));
        testLua.Global.Get("start", out act);
        testLua.Global.Get("test", out test);
        testLua.Global.Get("test2", out test2);
        testLua.Global.Get("test3", out test3);
        testLua.Global.Get("test4", out test4);
        testLua.Global.Get("test5", out test5);
        act();
        test2();
        test();
        test4();
        test5();
    }
    Action act;
    Action test;
    Action test2;
    Action test3;
    Action test4;
    Action test5;

    // Use this for initialization
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

        if (test3 != null) {
            test3();
        }

        if (Time.time - LuaBehaviour.lastGCTime > GCInterval)
        {
            luaEnv.Tick();
            LuaBehaviour.lastGCTime = Time.time;
        }
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
        scriptEnv.Dispose();
        injections = null;
    }

    /// <summary> /// 自动打包资源（设置了Assetbundle Name的资源） /// </summary> 
   /* [MenuItem("AssetBundle/Build AssetBundles 01")] //设置编辑器菜单选项 
    static void BuildAllAssetBundles()
    { //打包资源的路径 
        string targetPath = Application.dataPath + "/StreamingAssets"; //打包资源 
        BuildPipeline.BuildAssetBundles(targetPath, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows); //刷新编辑器 
        AssetDatabase.Refresh();
    }

    // <summary> /// 查看所有的Assetbundle名称（设置Assetbundle Name的对象） /// </summary> 
    [MenuItem("AssetBundle/Get AssetBundle names")]
    static void GetNames()
    {
        var names = AssetDatabase.GetAllAssetBundleNames(); //获取所有设置的AssetBundle 
        foreach (var name in names)
            Debug.Log("AssetBundle: " + name);
    }

    void Export()
    {
        string basePath = "";
        string assetName = "";
        string assetVarian = "";
        AssetImporter importer = AssetImporter.GetAtPath(basePath);
        importer.assetBundleName = assetName;
        importer.assetBundleVariant = assetVarian;
    }*/

    void ImportAsset()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "test.test");
        AssetBundle ass = AssetBundle.LoadFromFile(path);
        UnityEngine.Object ob = ass.LoadAsset("New Material");
        Material mat = Instantiate(ob) as Material;
        GameObject.Find("Sphere").GetComponent<MeshRenderer>().material = mat;

        UnityEngine.Object oc = ass.LoadAsset("GeometryMat");
        Material ma = Instantiate(oc) as Material;
        GameObject.Find("Cube").GetComponent<MeshRenderer>().material = ma;


        string knight = Path.Combine(Application.streamingAssetsPath, "knight.test");
        AssetBundle kni = AssetBundle.LoadFromFile(knight);
        UnityEngine.Object kn = kni.LoadAsset("Knight");
        GameObject obj2 = Instantiate(kn) as GameObject;

        string kn12 = Path.Combine(Application.streamingAssetsPath, "luatest.test");
        AssetBundle kn00 = AssetBundle.LoadFromFile(kn12);
        Debug.Log(kn00.LoadAllAssets()[0]);
        UnityEngine.Object k = kni.LoadAsset("Capsule(1)");
        Debug.Log(k);

        mUpdateFilesPath = "file://" + Application.streamingAssetsPath + "/" + "LuaTest.lua.txt";
        Debug.Log(mUpdateFilesPath);
        StartCoroutine("Download");

    }

   
   /* IEnumerator GetModels(string path)
    {
        WWW getdata = new WWW(path);
        yield return getdata;
        UnityEngine.Object[] models = getdata.assetBundle.LoadAllAssets();
        for (int i = 0; i < models.Length; i++)
        {
            GameObject model = Instantiate((GameObject)models[i]);
            model.transform.localPosition = new Vector3(0, 0, 0);
            model.transform.localScale = new Vector3(1, 1, 1);
            model.GetComponent<Animation>().Stop();
        }
    }*/

    string mUpdateFilesPath;
    string luaText;
    Action luaTestOutPut;
    /// <summary>
    /// 下载lua文件
    /// </summary>
    /// <returns></returns>
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
        
        LuaEnv testLua = new LuaEnv();
        testLua.DoString(luaText);


        testLua.Global.Set("self", GameObject.Find("Capsule"));
        testLua.Global.Set("test", GameObject.Find("Button"));

        testLua.Global.Get("start",out luaStart);
        testLua.Global.Get("update",out luaUpdate);
        testLua.Global.Get("ondestroy",out luaOnDestroy);
        testLua.Global.Get("luaTest",out luaTestOutPut);
        luaTestOutPut();
        Debug.Log("完成下载lua");
        /*  luaEnv.DoString(luaText, "LuaBehaviour", scriptEnv);//运行luaScript.text的lua


          Action luaAwake = scriptEnv.Get<Action>("awake");
          scriptEnv.Get("start", out luaStart);
          scriptEnv.Get("update", out luaUpdate);
          scriptEnv.Get("ondestroy", out luaOnDestroy);

          if (luaAwake != null)
          {
              luaAwake();
          }*/
    }

    /// <summary>
    /// 从网上下载文件
    /// </summary>
    /// <returns></returns>
    IEnumerator ImportWWW()
    {
        string URL = "";
        WWW _www = new WWW(URL);
        yield return _www;

        if (_www.isDone) {
            byte[] model = _www.bytes;
            int len = model.Length;

            //把网上下载的文件放在xxx文件夹
            Stream str;
            FileInfo file = new FileInfo(Application.persistentDataPath + "/" + "xxx");

            if (!file.Exists)
            {

                str = file.Create();
                str.Write(model, 0, len);
                str.Close();
                str.Dispose();

                //加载文件
                WWW _modelPath = new WWW("文件位置");
                yield return _modelPath;
                if (_modelPath.isDone)
                {
                    Instantiate(_modelPath.assetBundle.mainAsset);
                    _modelPath.assetBundle.Unload(false);
                    Resources.UnloadUnusedAssets();
                }
            }

            

           
        }
        
    }


    /// <summary>
    /// 下载并保存资源到本地
    /// </summary>
    /// <param name="url"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static IEnumerator DownloadAndSave(string url, string name, Action<bool, string> Finish = null)
    {
        url = Uri.EscapeUriString(url);
        string Loading = string.Empty;
        bool b = false;
        WWW www = new WWW(url);
        if (www.error != null)
        {
            print("error:" + www.error);
        }
        while (!www.isDone)
        {
            Loading = (((int)(www.progress * 100)) % 100) + "%";
            if (Finish != null)
            {
                Finish(b, Loading);
            }
            yield return 1;
        }
        if (www.isDone)
        {
            Loading = "100%";
            byte[] bytes = www.bytes;
            b = SaveAssets(Application.persistentDataPath, name, bytes);
            if (Finish != null)
            {
                Finish(b, Loading);
            }
        }
    }

    /// <summary>
    /// 保存资源到本地(
    /// </summary>
    /// <param name="path"></param>
    /// <param name="name"></param>
    /// <param name="info"></param>
    /// <param name="length"></param>
    public static bool SaveAssets(string path, string name, byte[] bytes)
    {
        Stream sw;
        FileInfo t = new FileInfo(path + "//" + name);
        if (!t.Exists)
        {
            try
            {
                sw = t.Create();
                sw.Write(bytes, 0, bytes.Length);
                sw.Close();
                sw.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }
        else
        {
            return true;
        }
    }

    
}
