using UnityEngine;
using System.Collections;

public class LAB_SceneLoader : MonoBehaviour
{
	public string levelName;
	public Object scene;
	public void LoadLevel ()
	{
		Application.LoadLevel (levelName);
	}
}
