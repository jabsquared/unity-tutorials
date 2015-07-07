using UnityEngine;
using System.Collections;



public class LAB_CameraFollow : MonoBehaviour
{

	public Transform objectToFollow;		// Reference to the player's transform.	
	
	void Update ()
	{
		TrackPlayer ();
	}
	
	// Distance in each axis the player can move before the camera follows.
	public Vector2 margin = Vector2.zero;
	
	bool CheckXMargin ()
	{
		// Returns true if the distance between the camera and the player in the x axis is greater than the x margin.
		return margin.x > 0 && 
			Mathf.Abs (transform.position.x - objectToFollow.position.x) > margin.x;
	}
	
	bool CheckYMargin ()
	{
		// Returns true if the distance between the camera and the player in the y axis is greater than the y margin.
		return margin.y > 0 &&
			Mathf.Abs (transform.position.y - objectToFollow.position.y) > margin.y;
	}
	// How smoothly the camera catches up with it's target movement
	public Vector2 smooth = new Vector2 (18f, 18f);
		
	public Vector2 maxCam = new Vector2 (36.0f, 36.0f);		// The maximum x and y coordinates the camera can have.
	public Vector2 minCam = new Vector2 (-36.0f, -36.0f);		// The minimum x and y coordinates the camera can have.
	
	void TrackPlayer ()
	{
		// By default the target x and y coordinates of the camera are it's current x and y coordinates.
		float targetX = CheckXMargin () ? 
			Mathf.Lerp (transform.position.x, objectToFollow.position.x, smooth.x * Time.smoothDeltaTime) :
				transform.position.x;
			
		float targetY = CheckYMargin () ?
			Mathf.Lerp (transform.position.y, objectToFollow.position.y, smooth.y * Time.smoothDeltaTime) :
				transform.position.y;
		
		// The target x and y coordinates should not be larger than the maximum or smaller than the minimum.
		if (maxCam.x > 0)
			targetX = Mathf.Clamp (targetX, minCam.x, maxCam.x);
		
		if (maxCam.y > 0)
			targetY = Mathf.Clamp (targetY, minCam.y, maxCam.y);
		
		// Set the camera's position to the target position with the same z component.
		transform.position = new Vector3 (targetX, targetY, transform.position.z);
	}
}
//TODO: Change this to 2D