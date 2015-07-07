using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LAB_Title_MKI : MonoBehaviour
{
	public LAB_Title_LevelButton buttonPrefab;
	private LAB_Title_LevelButton[] buttonInstances;
	
	private int[] indexX = new int[] {-1, 1, 1,-1};
	private int[] indexY = new int[] { 1, 1,-1,-1};
	void Start ()
	{
		int levelCount = Application.levelCount;
		buttonInstances = new LAB_Title_LevelButton[levelCount];
		
		Vector2 buttonSize = buttonPrefab.GetComponent<RectTransform> ().sizeDelta;
		Vector2 initialPosition = new Vector2 (buttonSize.x / 2f, buttonSize.x / 2f);	
		Debug.Log (initialPosition);
		
		for (int i = 1; i < levelCount; i++) {
			buttonInstances [i] = Instantiate (buttonPrefab) as LAB_Title_LevelButton;
			buttonInstances [i].transform.SetParent (transform, false);
			buttonInstances [i].name.Replace ("(Clone)", i.ToString ());
			buttonInstances [i].Level = i;
			buttonInstances [i].transform.localPosition = 
				new Vector2 (initialPosition.x * indexX [i - 1],
				            initialPosition.y * indexY [i - 1]);
		}
		// i = 1 => x(1)  = -1
		// i = 2 => x(2)  =  1
		// i = 3 => x(3)  = -2
		// i = 4 => x(4)  =  2
		// 2. x^3-14.5 x^2+31.5 x-20.
		// ai + bi
		
		//
		
		// 2 * i - 3
		
		// -> a = 2 => 2*2 + b = 1 => b = -3
		
	}
	
}
