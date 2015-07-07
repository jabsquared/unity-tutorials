using UnityEngine;
using UnityEditor;

using System.Diagnostics;

[CustomEditor(typeof(LAB_BezierCurve))]
public class LAB_BezierCurveInspector : Editor
{
	private LAB_BezierCurve curve;
	private Transform handleTransform;
	private Quaternion handleRotation;

	private const int smoothness = 9;
		
	private float directionScale = 4.5f;
	
	private void OnSceneGUI ()
	{
		SetupReferences ();
		
		Vector3[] p = InitPointArray ();
		
		Stopwatch sw = new Stopwatch ();
		
		DrawBasicLines (p);
				
		DrawDirection ();
		
		//DrawCurves ();
		
		Handles.DrawBezier (p [0], p [3], p [1], p [2], Color.white, null, 2f);
	}
	
	private delegate Vector3 NextPoint (Vector3 lineStart,int i);
	
	private void DrawDirection ()
	{
		Handles.color = Color.green;
		
		Vector3 lineStart = curve.GetPoint (0f);
		
		Handles.DrawLine (lineStart, lineStart + curve.GetDirection (0f) * directionScale);
		
		Draw (lineStart, DirectionNextPoint);
	}
	
	private Vector3 DirectionNextPoint (Vector3 lineStart, int i)
	{
		Vector3 lineEnd = curve.GetPoint (i / (float)smoothness);
		
		Handles.DrawLine (lineEnd, lineEnd + curve.GetDirection (i / (float)smoothness) * directionScale);
		
		return lineEnd;
	}
	
	private void DrawSpeed ()
	{
		Handles.color = Color.green;
		
		Vector3 lineStart = curve.GetPoint (0f);
		
		Handles.DrawLine (lineStart, lineStart + curve.GetVelocity (0f));
		
		Draw (lineStart, SpeedNextPoint);
	}
	
	private Vector3 SpeedNextPoint (Vector3 lineStart, int i)
	{
		Vector3 lineEnd = curve.GetPoint (i / (float)smoothness);
		
		Handles.DrawLine (lineEnd, lineEnd + curve.GetVelocity (i / (float)smoothness));
		
		return lineEnd;
	}
	
	private void DrawCurves ()
	{
		Handles.color = Color.white;
		
		Draw (curve.GetPoint (0f), CurveNextPoint);	
	}
	
	private Vector3 CurveNextPoint (Vector3 lineStart, int i)
	{
		Vector3 lineEnd = curve.GetPoint (i / (float)smoothness);
		
		Handles.DrawLine (lineStart, lineEnd);
		
		return lineEnd;
	}
	
	private void Draw (Vector3 lineStart, NextPoint n)
	{	
		for (int i = 0; i <= smoothness; ++i) {
			lineStart = n (lineStart, i);
		}
	}
	
	private void DrawBasicLines (Vector3[] p)
	{
		Handles.color = Color.gray;
		
		for (int i = 1; i < p.Length; ++i) {
			Handles.DrawLine (p [i - 1], p [i]);
		}
	}
	
	private void SetupReferences ()
	{
		curve = target as LAB_BezierCurve;
		
		handleTransform = curve.transform;
		
		handleRotation = Tools.pivotRotation == PivotRotation.Local ?
			handleTransform.rotation : Quaternion.identity;
		
	}
	
	private Vector3[] InitPointArray ()
	{
		Vector3[] p = new Vector3[curve.points.Length];
		
		for (int i = 0; i < curve.points.Length; ++i) {
			p [i] = CheckPoint (i);
		}
		
		return p;
	}
	
	private Vector3 CheckPoint (int i)
	{
		Vector3 p = handleTransform.TransformPoint (curve.points [i]);
		
		EditorGUI.BeginChangeCheck ();
		
		p = Handles.DoPositionHandle (p, handleRotation);
		
		if (EditorGUI.EndChangeCheck ()) {
			Undo.RecordObject (curve, "Move Point");
			EditorUtility.SetDirty (curve);
			
			curve.points [i] = handleTransform.InverseTransformPoint (p);
		}
		return p;
	}
	
}
