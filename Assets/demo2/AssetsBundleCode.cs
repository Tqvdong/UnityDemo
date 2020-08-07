using UnityEngine;
using System.Collections;
using UnityEditor;

public class AssetsBundleCode : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    [MenuItem("AssetBundle/Build (Single)")]
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
    }

   /* [MenuItem("AssetBundle/Build (Single)")]
    static void Build_AssetBundle()
    {
        BuildPipeline.BuildAssetBundles(Application.dataPath + "/Test_AssetBundle", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
        //刷新
        AssetDatabase.Refresh();
    }

    [MenuItem("AssetBundle/Build (Collection)")]
    static void Build_AssetBundle_Collection()
    {
        AssetBundleBuild[] buildMap = new AssetBundleBuild[1];
        //打包出来的资源包名字
        buildMap[0].assetBundleName = "enemybundle";

        //在Project视图中，选择要打包的对象 
        Object[] selects = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        string[] enemyAsset = new string[selects.Length];
        for (int i = 0; i < selects.Length; i++)
        {
            //获得选择 对象的路径
            enemyAsset[i] = AssetDatabase.GetAssetPath(selects[i]);
        }
        buildMap[0].assetNames = enemyAsset;

        BuildPipeline.BuildAssetBundles(Application.dataPath + "/Test_AssetBundle", buildMap, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
        //刷新
        AssetDatabase.Refresh();
    }*/

}
