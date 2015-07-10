using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MazeReset : LAB_ButtonActivation <MazeReset>, IPointerClickHandler
{
	public void OnPointerClick (PointerEventData data)
	{
		Activate ();
	}
}
