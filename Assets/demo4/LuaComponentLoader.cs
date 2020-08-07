using UnityEngine;
using System.Collections;
using System;
using XLua;

public class LuaComponentLoader : MonoBehaviour {

    public string luaComponentName;

    Action m_updateFunc;
    Action m_lateUpdateFunc;

    public LuaTable luaTable {
        get;
        private set;
    }

    public bool Load() {

        if (string.IsNullOrEmpty(luaComponentName))
        {
            return false;
        }

        luaTable = XLuaManager.instance.GetLuaTable(luaComponentName);

        if (luaTable == null)
        {
            return false;
        }
        Debug.Log("ok");
        luaTable.Set<string, Transform>("transform", transform);
        luaTable.Set<string, GameObject>("gameObject", gameObject);

        m_updateFunc = luaTable.Get<Action>("Update");
        m_lateUpdateFunc = luaTable.Get<Action>("LateUpdate");

        return true;



    }


    void CallLuaFunction(string funcName) {

        if (string.IsNullOrEmpty(funcName))
        {
            Debug.LogError("argument error: funcName");
            return;
        }

        Action func = luaTable.Get<Action>(funcName);
        if (func != null)
        {
            func();
        }

    }

    void Awake() {
        if (Load())
        {

            CallLuaFunction("Awake");
        }
        else {
            if (!string.IsNullOrEmpty(luaComponentName)) {

                return;
            }

        }


    }

	// Use this for initialization
	void Start () {

        if (luaTable == null) {
            if (string.IsNullOrEmpty(luaComponentName)) {

                return;
            }

            if (!Load()) {

                return;
            }

        }
        CallLuaFunction("Start");

	}
	
	// Update is called once per frame
	void Update () {
        if (m_updateFunc != null) {
            m_updateFunc();
        }
	}

    void LateUpdate() {

        if (m_lateUpdateFunc != null) {
            m_lateUpdateFunc();

        }

    }

    void OnEnable() {
        CallLuaFunction("OnEnable");

    }

    void OnDisable() {
        CallLuaFunction("OnDisable");

    }

    void OnDestroy() {

        CallLuaFunction("OnDestroy");
        luaTable.Set<string, Transform>("transform", null);
        luaTable.Set<string, GameObject>("gameObject", null);

        m_updateFunc = null;
        m_lateUpdateFunc = null;

        luaTable.Dispose();
        luaTable = null;
    }


}
