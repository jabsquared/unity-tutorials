using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(LAB_SceneLoader))] 
class LAB_SceneLoader_Editor : Editor
{
	string type = "(DefaultAsset)"; 
	
	//GetDragAndDropTitle returns a name + type, in this case LevelName(DefaultAsset), we want to remove the DefaultAsset part
	
	string path;
	
	public override void OnInspectorGUI ()
	{
		
	}
}
