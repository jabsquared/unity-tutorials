using UnityEngine;
using System.Collections;

public class LAB_LevelSwitcher : MonoBehaviour
{	
	
	[Range(0,180)]
	public float
		heightOffset;
	
	[Range(9,180)]
	public float
		buttonSize = 45f;

	void OnGUI ()
	{
		if (!Application.isLoadingLevel) {
			int currentLevel = Application.loadedLevel;
			if (GUI.Button (new Rect (
			Screen.width / 2 - buttonSize * 3 / 2, Screen.height - buttonSize - heightOffset, 
			buttonSize, buttonSize), "<")) {
				if (currentLevel > 0) {
					Application.LoadLevelAsync (currentLevel - 1);
				}
			}
			
			if (GUI.Button (new Rect (
			Screen.width / 2 - buttonSize / 2, Screen.height - buttonSize - heightOffset, 
			buttonSize, buttonSize), currentLevel.ToString ())) {
				Application.LoadLevelAsync (currentLevel);
			}
			
			if (GUI.Button (new Rect (
				Screen.width / 2 + buttonSize / 2, Screen.height - buttonSize - heightOffset, 
				buttonSize, buttonSize), ">")) {
				if (currentLevel < Application.levelCount) {
					Application.LoadLevelAsync (currentLevel + 1);
				}
			}
		}
	}
}
