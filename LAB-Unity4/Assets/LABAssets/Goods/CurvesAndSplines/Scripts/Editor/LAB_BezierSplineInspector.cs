using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(LAB_BezierSpline))]
public class LAB_BezierSplineInspector : Editor
{
	private LAB_BezierSpline spline;
	private Transform handleTransform;
	private Quaternion handleRotation;

	public override void OnInspectorGUI ()
	{
		spline = target as LAB_BezierSpline;
		
		EditorGUI.BeginChangeCheck ();
		bool loop = EditorGUILayout.Toggle ("Loop", spline.Loop);
		if (EditorGUI.EndChangeCheck ()) {
			Undo.RecordObject (spline, "Toggle Loop");
			EditorUtility.SetDirty (spline);
			spline.Loop = loop;
		}
		
		if (selectedIndex >= 0 && selectedIndex < spline.ControlPointCount) {
			DrawSelectedPointInspector ();
		}
			
		if (GUILayout.Button ("Add Curve")) {
			Undo.RecordObject (spline, "Add Curve");
			spline.AddCurve ();
			EditorUtility.SetDirty (spline);
		}
	}

	private void DrawSelectedPointInspector ()
	{
		GUILayout.Label ("Selected Point");
		EditorGUI.BeginChangeCheck ();
		Vector3 p = EditorGUILayout.Vector3Field ("Position", spline.GetControlPoint (selectedIndex));
		
		if (EditorGUI.EndChangeCheck ()) {
			Undo.RecordObject (spline, "Move Point");
			EditorUtility.SetDirty (spline);
			spline.SetControlPoint (selectedIndex, p);
		}
		
		EditorGUI.BeginChangeCheck ();
		LAB_BezierControlPointMode mode = (LAB_BezierControlPointMode)
			EditorGUILayout.EnumPopup ("Mode", spline.GetControlPointMode (selectedIndex));
		if (EditorGUI.EndChangeCheck ()) {
			Undo.RecordObject (spline, "Change Point Mode");
			spline.SetControlPointMode (selectedIndex, mode);
			EditorUtility.SetDirty (spline);
		}
	}
	
	private const int smoothness = 9;

	private float directionScale = 0.54f;
	
	private void OnSceneGUI ()
	{
		SetupReferences ();
		
		Vector3[] p = InitPointArray ();
		
		DrawBasicLines (p);
				
		DrawDirection ();
		
		//DrawSpeed ();
		
		DrawCurves (p);
	}
	
	private delegate Vector3 NextPoint (Vector3 lineStart,int i);
	
	private void DrawDirection ()
	{
		Handles.color = Color.green;
		
		Vector3 lineStart = spline.GetPoint (0f);
		
		Handles.DrawLine (lineStart, lineStart + spline.GetDirection (0f) * directionScale);
		
		Draw (lineStart, DirectionNextPoint);
	}
	
	private Vector3 DirectionNextPoint (Vector3 lineStart, int i)
	{
		Vector3 lineEnd = spline.GetPoint (i / (float)smoothness);
		
		Handles.DrawLine (lineEnd, lineEnd + spline.GetDirection (i / (float)smoothness) * directionScale);
		
		return lineEnd;
	}
	
	private void DrawSpeed ()
	{
		Handles.color = Color.green;
		
		Vector3 lineStart = spline.GetPoint (0f);
		
		Handles.DrawLine (lineStart, lineStart + spline.GetVelocity (0f));
		
		Draw (lineStart, SpeedNextPoint);
	}
	
	private Vector3 SpeedNextPoint (Vector3 lineStart, int i)
	{
		Vector3 lineEnd = spline.GetPoint (i / (float)smoothness);
		
		Handles.DrawLine (lineEnd, lineEnd + spline.GetVelocity (i / (float)smoothness));
		
		return lineEnd;
	}
	
	private Vector3 CurveNextPoint (Vector3 lineStart, int i)
	{
		Vector3 lineEnd = spline.GetPoint (i / (float)smoothness);
		
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
	
	private void DrawCurves (Vector3[] p)
	{
		for (int i = 0; i<spline.ControlPointCount-1; i+=3) {
			Handles.DrawBezier (p [i], p [i + 3], p [i + 1], p [i + 2], Color.white, null, 2f);
		}
	}
	
	private void SetupReferences ()
	{
		spline = target as LAB_BezierSpline;
		
		handleTransform = spline.transform;
		
		handleRotation = Tools.pivotRotation == PivotRotation.Local ?
			handleTransform.rotation : Quaternion.identity;
	}
	
	private Vector3[] InitPointArray ()
	{
		Vector3[] p = new Vector3[spline.ControlPointCount];
		
		for (int i = 0; i < spline.ControlPointCount; ++i) {
			p [i] = CheckPoint (i);
		}
		
		return p;
	}
	
	private const float handleSize = 0.054f;
	private const float pickSize = 0.072f;
	
	private int selectedIndex = -1;
	
	private static Color[] modeColors = {
		Color.white,
		Color.yellow,
		Color.cyan
	};
	
	private Vector3 CheckPoint (int i)
	{
		Vector3 p = handleTransform.TransformPoint (spline.GetControlPoint (i));
		
		Handles.color = modeColors [(int)spline.GetControlPointMode (i)];
	
		float size = HandleUtility.GetHandleSize (p);
			
		if (i == 0) {
			size *= 2f;
		}
			
		if (Handles.Button (p, handleRotation, size * handleSize, size * pickSize, Handles.DotCap)) {
			selectedIndex = i;
			Repaint ();
		}
		if (selectedIndex == i) {
			EditorGUI.BeginChangeCheck ();
		
			p = Handles.DoPositionHandle (p, handleRotation);
		
			if (EditorGUI.EndChangeCheck ()) {
				Undo.RecordObject (spline, "Move Point");
				EditorUtility.SetDirty (spline);
				
				spline.SetControlPoint (i, handleTransform.InverseTransformPoint (p));
			}
		}
		return p;
	}
	
}
