using UnityEngine;
using System.Collections;

public class LABShortcut : MonoBehaviour
{
	public bool quit;
	
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
		if (quit) {
			QuitKey ();
		}
	}
	
	void QuitKey ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (Application.loadedLevel != 0) {
				Application.LoadLevel (0);
			} else {
				Application.Quit ();
			}
		}
	}
}
