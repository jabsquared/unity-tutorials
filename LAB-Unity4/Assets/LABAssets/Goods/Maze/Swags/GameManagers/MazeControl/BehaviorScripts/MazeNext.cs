using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MazeNext : LAB_ButtonActivation <MazeNext>, IPointerDownHandler, IPointerUpHandler
{

	public void OnPointerDown (PointerEventData data)
	{
		Activate ();
	}

	public void OnPointerUp (PointerEventData data)
	{
		Deactivate ();
	}

	/*----------------------------------------------------------------------------------------*/
	// For Desktop Version
	// TODO: Comment when completing the build
	/*----------------------------------------------------------------------------------------*/

	/*private void Update(){
		if (Input.GetKeyDown(KeyCode.N))
			Activate();
		if (Input.GetKeyUp(KeyCode.N))
			Deactivate();
	}*/

}
