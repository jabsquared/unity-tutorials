using UnityEngine;
using System.Collections;

public class SlideShowCam : MonoBehaviour
{
	
	public Transform canvas;
	
	private Transform[] markers;
	
	private int cP;
	
	public float speed = 1.8f;
	
	private void Awake ()
	{
		markers = canvas.GetComponentsInChildren<Transform> ();
		
		cP = 1;
		
		LMTM ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.anyKeyDown) {
			if (Input.GetKeyDown (KeyCode.N)) {
				if (cP < ~-markers.Length) {
					++cP;	
				} else if (cP == ~-markers.Length) {
					cP = 1;
				}
			}
			if (Input.GetKeyDown (KeyCode.B)) {
				if (cP > 1) {
					--cP;
				} else if (cP == 1) {
					cP = markers.Length - 1;
				}
			}
			
			LMTM ();
		}
	}
	
	public void LMTM ()
	{
		StopAllCoroutines ();
		
		Debug.Log (cP);
		
		StartCoroutine (LAB_Transform.MoveObjectGlobal (transform, markers [cP].position, speed));
	}
}
