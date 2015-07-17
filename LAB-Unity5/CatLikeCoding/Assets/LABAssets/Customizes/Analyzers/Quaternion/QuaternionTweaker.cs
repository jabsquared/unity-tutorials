using UnityEngine;
using System.Collections;

public class QuaternionTweaker : MonoBehaviour {

	[Range(0,9)]
	public float x, y, z, w;

	// Update is called once per frame
	void Update () {
		transform.rotation = new Quaternion (x,y,z,w);
	}
}
