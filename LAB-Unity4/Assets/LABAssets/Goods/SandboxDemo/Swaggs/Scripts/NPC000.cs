using UnityEngine;
using System.Collections;

public class NPC000 : MonoBehaviour
{
	
	void OnCollisionEnter (Collision other)
	{
		foreach (ContactPoint contact in  other.contacts) {
			Debug.DrawRay (contact.point, contact.normal, Color.white);
		}
	}
	
	void OnTriggerEnter (Collider other)
	{
		if (other.tag.Equals ("Player")) {
			DebugDisplay.Log ("In conversation");
		}	
	}
}