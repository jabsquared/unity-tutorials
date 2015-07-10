using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class LAB_Title_LevelButton : MonoBehaviour, IPointerClickHandler
{
	
	public Text textChild;

	private int level;
	
	public int Level {
		get {
			return level;
		}
		set {
			if (value <= Application.levelCount) { // Check if the level is lower han levl Count
				// Set the level to Value
				level = value;
				//Set the Text to level
				textChild.text = level.ToString ();
				//Change Alpha. TODO: Implement random color for fun?
				if (level % 2 == 0) {
					Color buttonColor = GetComponent<Image> ().color;
					GetComponent<Image> ().color = LAB_Color.HalfA (buttonColor);
				}
			}
		}
	}
	
	
	private bool isFocused;
	
	//TODO: An update for some catchy Animations?
	
	public void OnPointerClick (PointerEventData data)
	{
		//Debug.Log ("Load" + level.ToString ());
		
		Application.LoadLevelAsync (level);
	}
	
}
