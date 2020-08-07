using UnityEngine;
using System.Collections;
using XLua;
using System.IO;
using System.Linq;

public class XLuaManager : MonoBehaviour {

    static XLuaManager m_instance;

    public static XLuaManager instance {
        get { return m_instance ?? (m_instance = new XLuaManager()); }

    }

    public LuaEnv LuaEnv {

        private set;
        get;
    }

    XLuaManager() {

        LuaEnv = new LuaEnv();
        LuaEnv.CustomLoader loader = OriginalLuaLoader;
        LuaEnv.AddLoader(loader);
    }

    byte[] OriginalLuaLoader(ref string filepath) {

        if (string.IsNullOrEmpty(filepath))
        {
            Debug.LogError("Load original lua failed, because filepath is empty");
            return null;
        }

        string luaName = filepath + ".lua";
        //lua文件的存放路径
        string folder = string.Format("{0}/Resources", Application.dataPath);
       
        string[] files = Directory.GetFiles(folder, "*.txt", SearchOption.AllDirectories);
        string rightFile = files.FirstOrDefault(f => {
            string n = Path.GetFileNameWithoutExtension(f);
            return n == luaName;
        });
        
        if (string.IsNullOrEmpty(rightFile))
        {
            Debug.LogError("Load original lua failed, because lua file not exist, file name: " + luaName);
            return null;
        }

        filepath = rightFile;
        return File.ReadAllBytes(rightFile);


    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (LuaEnv != null) {

            LuaEnv.Tick();
        }
	}

    public void LoadLuaTable(string name) {
        if (string.IsNullOrEmpty(name))
        {
            Debug.LogError("string.IsNullOrEmpty(name)");
            return;
        }

        string code = string.Format("require '{0}'", name);
        LuaEnv.DoString(code);

        
    }


    public LuaTable GetLuaTable(string name) {

        if (string.IsNullOrEmpty(name)) {
            return null;
        }
        
        LuaTable table = LuaEnv.Global.Get<LuaTable>(name);
        if (table == null) {
            LoadLuaTable(name);
            table = LuaEnv.Global.Get<LuaTable>(name);

        }
        return table;

    }

    public void Dispose() {

        Dispose(true);

    }

    protected void Dispose(bool disposing) {

        if (disposing) {

            if (LuaEnv!=null) {

                LuaEnv.GC();
                LuaEnv.Dispose();
            }

        }

    }

    ~XLuaManager() {

        Dispose(false);
    }
}
