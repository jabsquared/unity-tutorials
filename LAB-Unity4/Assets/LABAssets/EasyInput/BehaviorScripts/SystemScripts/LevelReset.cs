using UnityEngine;
using UnityEngine.EventSystems;

public class LevelReset : MonoBehaviour , IPointerClickHandler {

	public void OnPointerClick (PointerEventData data) {
		Application.LoadLevelAsync (Application.loadedLevelName);
	}
}