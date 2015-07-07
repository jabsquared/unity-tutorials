using System.Collections.Generic;

public class LAB_Queue <LAB>
{
	
	LinkedList<LAB> list;
	
	public LAB_Queue ()
	{
		list = new LinkedList<LAB> ();
	}
	
	public void Enqueue (LAB t)
	{
		list.AddLast (t);
	}
	
	public LAB Dequeue ()
	{
		var result = list.First.Value;
		list.RemoveFirst ();
		return result;
	}
	
	public LAB Peek ()
	{
		return list.First.Value;
	}
	
	public bool Remove (LAB t)
	{
		return list.Remove (t);
	}
	
	public int Count { get { return list.Count; } }
}
