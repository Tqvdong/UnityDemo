using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//委托，观察者模式
public class Dele : MonoBehaviour {

	// Use this for initialization
	void Start () {

        TestDelegate test = new TestDelegate();
       /* test.OnDelegate = new DelegateMethod(delegate (int i, int maxValue) {

            
        });*/

        test.OnDelegate+= new DelegateMethod(delegate (int i, int maxValue) {

         
            //操作
        });

      
        test.DoDelegateMethod();
        Dictionary<string, string> cc = new Dictionary<string, string>();

        Set("test", test);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Set<TKey, TValue>(TKey key, TValue value) {
        
        Debug.Log(key);

    }
}

public delegate void DelegateMethod(int positon, int maxValue);

public class TestDelegate {

    public DelegateMethod OnDelegate;

    public void DoDelegateMethod() {

        int maxValue = 100;
        for (int i = 0; i < maxValue; i++) {

            if (this.OnDelegate != null) {

                this.OnDelegate(i, maxValue);
            }

        }
    }

    public void ClearDelegate() {

        while (this.OnDelegate != null) {

            this.OnDelegate -= this.OnDelegate;
        }

    }


}
