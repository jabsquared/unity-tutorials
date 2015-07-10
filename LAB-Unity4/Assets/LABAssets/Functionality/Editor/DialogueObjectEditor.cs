using UnityEditor;
using UnityEngine;
using System.Collections;


[CustomEditor(typeof(DialogueObject))]

public class DialogueObjectEditor : Editor
{
	
	private int dialogueID;
	
	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
		
		DialogueObject dialogueObject = (DialogueObject)target;
		
		
		EditorGUILayout.LabelField ("Dialogue");
		
		if (GUILayout.Button ("Create Dialogue")) {
			Debug.Log ("Added a Dialogue Game object to the scene");
			
			// make the object 
			this.AddNewDialogueObject (dialogueObject.CharacterName, dialogueID);
			dialogueID += 1;
		}
		
		EditorGUILayout.LabelField ("DialogueDecision");
	}
	
	private void AddNewDialogueObject (string characterName, int dialogueID)
	{
		int counter = 0;
		if (counter < 1) {
			new GameObject ("Dialogue : " + characterName + " " + dialogueID);
			
			counter++;
		}
	}

}