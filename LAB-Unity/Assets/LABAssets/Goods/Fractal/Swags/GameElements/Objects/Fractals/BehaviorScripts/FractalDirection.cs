using UnityEngine;
using System.Collections;

public class FractalDirection {

	private Vector3 direction;
	private Quaternion orientation;
	
	public FractalDirection ( Vector3 direction, Quaternion orientation){
		this.direction = direction;
		this.orientation = orientation;	
	}
	
	public Vector3 GetDirection(){

		return direction;
	}
	
	public Quaternion GetOrientation(){

		return orientation;
	}
}
