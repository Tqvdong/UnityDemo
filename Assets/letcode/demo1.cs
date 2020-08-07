using UnityEngine;
using System.Collections;

public class demo1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /*   给出两个 非空 的链表用来表示两个非负的整数。其中，它们各自的位数是按照 逆序的方式存储的，并且它们的每个节点只能存储 一位数字。

   如果，我们将这两个数相加起来，则会返回一个新的链表来表示它们的和。

   您可以假设除了数字 0 之外，这两个数都不会以 0 开头。

        示例：

输入：(2 -> 4 -> 3) + (5 -> 6 -> 4)
输出：7 -> 0 -> 8
原因：342 + 465 = 807
   */


    public ListNode AddTwoNumbers(ListNode l1, ListNode l2) {

        ListNode resultNode = new ListNode(0);
        var preNode = resultNode;
        int val = 0;
        while (l1 != null || l2 != null) {
            val = (l1 == null ? 0 : l1.val) + (l2 == null ? 0 : l2.val) + val;
            preNode.next = new ListNode(val % 10);
            preNode = preNode.next;
            val = val / 10;
            if(l1!=null)
            l1 = l1.next;
            l2 = l2.next;
        }
        if (val != 0) preNode.next = new ListNode(val);
        return resultNode;
    }

    public int Reverse(int x)
    {
        long y= x % 10;
        while (x / 10 != 0)
        {
            x = x / 10;
            y = y * 10 + (x / 10) % 10;
        }

        return (int)y;
    }


    public ListNode MergeTwoLists(ListNode l1,ListNode l2) {

        ListNode newNode = new ListNode(0);
        while (l1 != null || l2 != null) {
            if (l1.val < l2.val)
            {

                newNode.val = l1.val;
                
                l1 = l1.next;
            }
            else {

                newNode.val = l2.val;
                l2 = l2.next;
            }
            newNode.next = new ListNode(0);
            newNode = newNode.next;
            

        }
        return newNode;
    }

}


//链表
public class ListNode
{
    public ListNode(int i) {
        val = i;
    }

    public int val;
    public ListNode next;
    public ListNode previous;
}