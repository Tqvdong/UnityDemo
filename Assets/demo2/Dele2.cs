using UnityEngine;
using System.Collections;


//单例模式
public class Dele2 : MonoBehaviour {

	// Use this for initialization
	void Start () {

        
      
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    private static Dele2 _instance = null;

    public static Dele2 instance {

        get {
            if (_instance == null) _instance = new Dele2();
            return _instance;
        }

    }
}



///工厂模式
///基类，虚方法
///子类，继承
///工厂类，负责生产子类
///管理类,调用工厂类来生产


///观察者模式
///


///建造者模式
///怪物
///怪物管理器
///声效管理器，特效管理器，血条管理器
///抽象建造类
///建造类继承抽象建造类