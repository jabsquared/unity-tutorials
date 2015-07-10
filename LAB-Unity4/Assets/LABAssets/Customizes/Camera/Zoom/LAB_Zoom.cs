using UnityEngine;
using System.Collections;

public class LAB_Zoom : MonoBehaviour
{
	public bool enablePinchZoom;
	
	public bool enableScrollZoom;
	
	[Range(0.9f, 9.0f)]
	public float
		perspectiveZoomSpeed = 0.9f,
		orthoZoomSpeed = 0.9f,
		orthoMin = 0.9f;
	
	void Update ()
	{
		if (enableScrollZoom) {
			Zoom (-Input.GetAxis ("Mouse ScrollWheel"));
		}
		if (enablePinchZoom) {
			if (Input.touchCount == 2) {
				Zoom (TouchRate);
			}
		}
	}
	
	private float TouchRate {
		get {
			// Store both touches.
			Touch touchZero = Input.GetTouch (0);
			Touch touchOne = Input.GetTouch (1);
			
			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
			
			// Find the magnitude of the vector (the distance) between the touches in each frame.
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
			
			// Find the difference in the distances between each frame.
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
			
			return deltaMagnitudeDiff;
		} 
	} 
	
	
	private void Zoom (float zoomRate)
	{
		if (camera.isOrthoGraphic) {
			// Change the orthographic size
			camera.orthographicSize -= orthoZoomSpeed * zoomRate;
			//
			camera.orthographicSize = Mathf.Max (camera.orthographicSize, orthoMin);
		} else {
			// Otherwise change the field of view based on the change in distance
			camera.fieldOfView += perspectiveZoomSpeed * zoomRate;
			// Clamp the field of view to make sure it's between 0 and 180.
			camera.fieldOfView = Mathf.Clamp (camera.fieldOfView, 0.1f, 179.9f);
		}
	}
}
