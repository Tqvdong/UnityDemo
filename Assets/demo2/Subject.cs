using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Subject : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void Initialized() {

        UnitySubject unitySubject = new UnitySubject();
        IObserver haiLanZero = new HaiLanZero();
        IObserver haiLanOne = new HaiLanOne();

        unitySubject.RegisterObserver(haiLanOne);
        unitySubject.RegisterObserver(haiLanZero);

        unitySubject.observerEvent += haiLanZero.Update;
        unitySubject.observerEvent += haiLanOne.Update;

        unitySubject.Tag = "频道更新";
        unitySubject.NotifyObservers();
    }


    
}

public interface ISubject
{
    void NotifyObservers();
    void RegisterObserver(IObserver observer);

}

public interface IObserver
{

    void Update(string tag);
}

//频道
public class UnitySubject : ISubject {

    private List<IObserver> observerList = new List<IObserver>();
    public event Action<string> observerEvent;
    private string tag = "";
    public string Tag {

        get { return tag; }
        set { tag = value; }

    }

    public void NotifyObservers() {

        for (int i = 0; i < observerList.Count; i++) {

            observerList[i].Update(tag);
        }
    }

    public void _NotifyObservers() {
       
        //observerEvent?.Invoke(Tag)
        //if(Tag!=null)
        observerEvent(Tag);
    }

    public void RegisterObserver(IObserver observer) {

        observerList.Add(observer);
    }

}

//观众1
public class HaiLanZero : IObserver {
    public void Update(string tag) {
        Debug.Log("收到" + tag);

    }


}

//观众2
public class HaiLanOne : IObserver {

    public void Update(string tag) {
        Debug.Log("收到" + tag);
        
    }

}
