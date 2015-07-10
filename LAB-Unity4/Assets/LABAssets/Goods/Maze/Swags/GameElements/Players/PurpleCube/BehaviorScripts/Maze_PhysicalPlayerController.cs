using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

public class Maze_PhysicalPlayerController : Maze_PlayerMovement
{

	private PhysicalTaggedInputAxis  movement, rotation;
	
	private void Awake ()
	{
		movement = new PhysicalTaggedInputAxis ("[Movement]");
		rotation = new PhysicalTaggedInputAxis ("[Rotation]");

	}

	private void Update ()
	{

		if (Move (movement.h, movement.v)) {
			//print("[Physical] Player Is Moving");
			
		}

		if (Rotate (rotation.h)) {
			//print("[Virtual] Player Is Moving");

		}
	}
}
