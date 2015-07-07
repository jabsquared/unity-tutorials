using UnityEngine;
using System.Collections;

public class LAB_Shortcut : MonoBehaviour
{
	void Awake ()
	{
		DontDestroyOnLoad (gameObject);
		if (FindObjectsOfType (GetType ()).Length > 1) {
			Destroy (gameObject);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (Application.loadedLevel != 0) {
				Application.LoadLevelAsync (0);
			} else {
				Application.Quit ();
			}
		}
	}
}
