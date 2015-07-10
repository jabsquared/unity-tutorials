using UnityEngine;
using System.Collections;

public class Maze_VirtualPlayerController : Maze_PlayerMovement {

	private VirtualTaggedInputAxis  movement, rotation;

	private void Awake(){
		movement = new VirtualTaggedInputAxis ("[Movement]");
		rotation = new VirtualTaggedInputAxis ("[Rotation]");
		
	}

	private void Update () {
		//print(movement.ToString());
		if (Move (movement.h, movement.v)){
			//print("[Physical] Player Is Moving");
		}
		//print(rotation.ToString());
		if (Rotate (rotation.h)){
			//print("[Virtual] Player Is Moving");
			
		}
	}
}
