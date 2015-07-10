using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MazePrevious : LAB_ButtonActivation <MazePrevious> , IPointerDownHandler, IPointerUpHandler {
	
	public void OnPointerDown (PointerEventData data){
		Activate();
	}
	
	public void OnPointerUp (PointerEventData data){
		Deactivate();
	}
	
	/*----------------------------------------------------------------------------------------*/
	// For Desktop Version
	// TODO: Comment when completing the build
	/*----------------------------------------------------------------------------------------*/
	
	/*private void Update(){
		if (Input.GetKeyDown(KeyCode.B))
			Activate();
		if (Input.GetKeyUp(KeyCode.B))
			Deactivate();
	}*/


}
