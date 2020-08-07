using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SingleMode : MonoBehaviour {

    public delegate void test(int a);
    test all;

	// Use this for initialization
	void Start () {
        all = new test(setAll);
        all(2);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void setAll(int b) {


    }

    //单例模式
    private static SingleMode instance;
    public static SingleMode Instance {
        get {

            if (instance == null) {
                instance = new SingleMode();

            }
            return instance;
        }

    }
}
