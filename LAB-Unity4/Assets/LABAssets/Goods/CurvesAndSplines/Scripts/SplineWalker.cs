using UnityEngine;
using System.Collections;

public class SplineWalker : MonoBehaviour
{
	
	public enum SplineWalkerMode
	{
		Once,
		Loop,
		Pingpong
	}
	public SplineWalkerMode mode;
	
	private bool goingForward = true;

	public LAB_BezierSpline spline;
	
	public float duration;
	
	private float progress;
	
	// Use this for initialization
	void Start ()
	{
		
	}
	
	public bool lookForward;
	
	// Update is called once per frame
	void Update ()
	{
		if (goingForward) {
			progress += Time.deltaTime / duration;
			if (progress > 1f) {
				if (mode.Equals (SplineWalkerMode.Once)) {
					progress = 1f;
				} else if (mode.Equals (SplineWalkerMode.Loop)) {
					progress -= 1f;
				} else {
					progress = 2f - progress;
					goingForward = false;
				}
			}
		} else {
			progress -= Time.deltaTime / duration;
			if (progress < 0f) {
				progress *= -1;
				goingForward = true;
			}
		}
		
		Vector3 position = spline.GetPoint (progress);
		
		transform.localPosition = position;
		
		if (lookForward) {
			transform.LookAt (position + spline.GetDirection (progress));
		}
	}
}
