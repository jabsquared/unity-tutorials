using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class MazePause : LAB_ButtonActivation <MazePause>, IPointerClickHandler {

	private Image buttonImage;

	public Sprite pause, play;

	private void Awake(){
		Activate();
		buttonImage = GetComponent<Image>();
	}

	public void OnPointerClick (PointerEventData data) {	
		TogglePauseState();
	}
	
	private void TogglePauseState(){
		Toggle();
		buttonImage.sprite = IsActivated ? (play) :	(pause);
	}

	/*----------------------------------------------------------------------------------------*/
	// For Desktop Version
	// TODO: Comment when completing the build
	/*----------------------------------------------------------------------------------------*/
	
	/*private void Update(){
		if ( Input.GetKeyDown (KeyCode.P)){
			TogglePauseState();
		}
	}*/

}
