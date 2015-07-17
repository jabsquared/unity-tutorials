using UnityEngine;
using System.Collections;

public class PhysicalCameraMovement : MonoBehaviour {

	private PhysicalTaggedInputAxis  movement, rotation;

	void Awake() {
		movement = new PhysicalTaggedInputAxis  ("[Movement]");
		rotation = new PhysicalTaggedInputAxis ("[Rotation]");
	}

	[System.Serializable]
	public struct InvertDirection {
		public bool horizontal, vertical;
	}
	public InvertDirection movementInverted, rotationInverted;

	// Update is called once per frame
	void Update () {

		// Get
		if (Move (movement.h * (movementInverted.horizontal ? -1.0f : 1.0f), 
		          movement.v * (movementInverted.vertical ? -1.0f : 1.0f ))){
			//print("Camera Is Moving");
			
		}
		if (Rotate (rotation.h * (rotationInverted.horizontal ? -1.0f : 1.0f), 
		            rotation.v * (rotationInverted.vertical ? -1.0f : 1.0f))){
			//print ("Camera Is Rotating");
		} 
	}

	[Range(0,1)]
	public float moveSpeedScale = 0.09f;
	
	private bool Move(float h, float v)	{
		if (h!=0 || v!=0){ // Check if these Axises change at all
			transform.position += 
				transform.forward * v * moveSpeedScale 
					+ transform.right * h * moveSpeedScale;

			return true;
		}
		else {
			return false;
			//Make a wait time here, if too long, 
			//tell the player how to move the camera;
		}
	}

	[Range(0,1)]
	public float rotateSpeedScale = 0.63f;

	private bool Rotate(float h, float v) {

		if (h!=0 || v!=0){ // Check if these Axises change at all
			Vector3 newAngle = transform.eulerAngles + ((new Vector3 (-v,h,0))) * rotateSpeedScale;

			transform.localEulerAngles = newAngle;

			return true;
		}
		else{

			return false;
		}
	}

}