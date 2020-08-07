using UnityEngine;
using System.Collections;

public class DelegateUse : MonoBehaviour {

	// Use this for initialization
	void Start () {

        MyDelegate md = new MyDelegate(MyDelegateFunc);
        md("aaa");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public delegate void MyDelegate(string name);

    public static void MyDelegateFunc(string name) {

        Debug.Log(name);
    }
}
