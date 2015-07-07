using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(Text))]
public class DebugDisplay : MonoBehaviour
{		
	private Text debugText;
		
	private static List<string> debugList;
	
	// Use this for initialization
	void Start ()
	{
		debugText = gameObject.GetComponent<Text> ();
		
		debugList = new List<string> ();
	}
	
	public static void LogHit (ContactPoint contact)
	{
		Log (
			contact.thisCollider.name + " hit " + 
			contact.otherCollider.name);
	}
	
	public static void LogCollider (Collider other)
	{
		Log ("Entered " + other.name);
	}
	
	public static void Log (object msg)
	{
		string m = msg.ToString ();
		Debug.Log (msg);
		
		if (debugList != null) {
			if (debugList.Contains (m)) {
				debugList.Remove (m);
			} else if (debugList.Count >= 4) {
				debugList.RemoveAt (0);
			}
			debugList.Add (m);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{	
		if (debugText != null) {
			string debugMsg = " [Debug]\n";
			for (int i = 0; i <debugList.Count; ++i) {
				debugMsg += "   <size=18>" + debugList [i] + "</size>" + "\n";
			}
			debugText.text = debugMsg;
		}
	}
}