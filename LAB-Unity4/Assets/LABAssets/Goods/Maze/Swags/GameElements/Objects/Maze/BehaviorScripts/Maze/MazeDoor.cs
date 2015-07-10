using UnityEngine;
using System.Collections;

public class MazeDoor : MazePassage
{

	public Transform hinge;

	private bool isMirrored;
	private static Quaternion 
		normalRotation = Quaternion.Euler (0f, -90f, 0f),
		mirroredRotation = Quaternion.Euler (0f, 90f, 0f); 

	private MazeDoor OtherSideOfDoor {
		get {
			return otherCel.GetEdge (direction.GetOpposite ()) as MazeDoor;
		}
	}
	
	//private bool openned;
	public override void OnPlayerEntered ()
	{
		OtherSideOfDoor.cell.room.Show ();

//		if (!openned && !OtherSideOfDoor.openned) {
			
		Quaternion rotateDirection = isMirrored ? mirroredRotation : normalRotation;
		StartCoroutine (LAB_Transform.RotateObjectLocal (hinge, rotateDirection, 0.45f));
		StartCoroutine (LAB_Transform.RotateObjectLocal (OtherSideOfDoor.hinge, rotateDirection, 0.45f));
		//openned = true;
//		} else if (OtherSideOfDoor.openned && !openned) {
//			OtherSideOfDoor.openned = false;
		//	openned = true;
		//}
	}

	
	public override void OnPlayerExited ()
	{
		//if (openned && !OtherSideOfDoor.openned) {
		OtherSideOfDoor.hinge.localRotation = hinge.localRotation = Quaternion.identity;
		//			StartCoroutine (LAB_Transform.RotateObjectLocal (OtherSideOfDoor.hinge, Quaternion.identity, 2.7f));
		//			StartCoroutine (LAB_Transform.RotateObjectLocal (hinge, Quaternion.identity, 2.7f));
		//openned = OtherSideOfDoor.openned = false;
		OtherSideOfDoor.cell.room.Hide ();
		//}
	}

	public override void Initialize (MazeCell primary, MazeCell otherCell, MazeDirection direction)
	{
		base.Initialize (primary, otherCell, direction);
		if (OtherSideOfDoor != null) {
			isMirrored = true;
			hinge.localScale = new Vector3 (-1f, 1f, 1f);
			Vector3 p = hinge.localPosition;
			p.x = -p.x;
			hinge.localPosition = p;
		}
		for (int i = 0; i < transform.childCount; i++) {
			Transform child = transform.GetChild (i);
			if (child != hinge) {
				child.GetComponent<Renderer> ().material = cell.room.settings.wallMaterial;
			}
		}
	}
}




