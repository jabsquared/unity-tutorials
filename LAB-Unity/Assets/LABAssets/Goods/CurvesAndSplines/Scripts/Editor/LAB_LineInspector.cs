using UnityEditor;
using UnityEngine;

[CustomEditor (typeof(LAB_Line))]
public class LAB_LineInspector : Editor
{
	private LAB_Line l;
	
	private Transform hT;
	
	private Quaternion hR;
	
	private void OnSceneGUI ()
	{
		l = target as LAB_Line;
		
		hT = l.transform;
		
		hR = Tools.pivotRotation == PivotRotation.Local ?
			hT.rotation : Quaternion.identity;
		
		Vector3[] p = new Vector3[l.points.Length];
		
		for (int i = 0; i < l.points.Length; ++i) {
			p [i] = hT.TransformPoint (l.points [i]);
		}
		
		Handles.color = Color.white;
		
		Handles.DrawLine (p [0], p [1]);
		
		for (int i = 0; i < l.points.Length; ++i) {
			CheckPoint (i, p [i]);
		}
		
		
	}
	
	/// <summary>
	/// Check for Editor change event,
	/// then set the point once done
	/// </summary>
	/// <param name="pI">Point Index</param>
	/// <param name="p">Point Vector</param>
	private void CheckPoint (int pI, Vector3 p)
	{
		EditorGUI.BeginChangeCheck ();
		
		p = Handles.DoPositionHandle (p, hR);
		
		if (EditorGUI.EndChangeCheck ()) {
			
			// Record to Undo and Set dirty to Save
			Undo.RecordObject (l, "Move Point");
			
			EditorUtility.SetDirty (l);
			
			l.points [pI] = hT.InverseTransformPoint (p);
		}
	}
}
