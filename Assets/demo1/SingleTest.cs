using UnityEngine;
using System.Collections;

public class SingleTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Queue queue = new Queue();
        queue.Enqueue(1);
        queue.Enqueue(2);
        object obj;// = queue.Dequeue();//获取并移除元素，取最先添加的元素
        obj = queue.Peek();//获取但不移除元素，取最先添加的元素
        int i = (int)obj;
        Debug.Log(obj);


        Stack stack = new Stack();
        stack.Push(1);
        obj = stack.Pop();//获取并移除元素，取最后添加的元素
        //obj = stack.Peek();//获取但不移除元素，取最先添加的元素
        Debug.Log((int)obj);

        Hashtable hashtable = new Hashtable();
        hashtable.Add(1, "Test01");
        hashtable.Add("2", "Test02");
        hashtable.Remove(1);
        int cout = hashtable.Count;
        hashtable.Clear();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
