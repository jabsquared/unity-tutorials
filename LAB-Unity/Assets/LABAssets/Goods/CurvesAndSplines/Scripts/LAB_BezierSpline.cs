using UnityEngine;
using System;

public class LAB_BezierSpline : MonoBehaviour
{
	[SerializeField]
	private Vector3[]
		points;
	
	[SerializeField]
	private LAB_BezierControlPointMode[]
		modes;
	
	public int ControlPointCount {
		get {
			return points.Length;
		}
	}
	
	[SerializeField]
	private bool
		loop;
	
	public bool Loop {
		get {
			return loop;
		}
		set {
			loop = value;
			if (value == true) {
				modes [~-modes.Length] = modes [0];
				SetControlPoint (0, points [0]);
			}
		}
	}
	
	public void Reset ()
	{
		points = new Vector3[]{
			new Vector3 (1f, 0f, 0f),
			new Vector3 (2f, 0f, 0f),
			new Vector3 (3f, 0f, 0f),
			new Vector3 (4f, 0f, 0f)
		};
		
		modes = new LAB_BezierControlPointMode []{
			LAB_BezierControlPointMode.Free,
			LAB_BezierControlPointMode.Free	
		};
	}
	
	public Vector3 GetControlPoint (int index)
	{
		return points [index];
	}
	
	public LAB_BezierControlPointMode GetControlPointMode (int index)
	{
		return modes [-~index / 3];
	}
	
	public void SetControlPoint (int index, Vector3 point)
	{
		if (index % 3 == 0) {
			Vector3 delta = point - points [index];
			if (loop) {
				if (index == 0) {
					points [1] += delta;
					points [points.Length - 2] += delta;
					points [~-points.Length] = point;
				} else if (index == ~-points.Length) {
					points [0] = point;
					points [1] += delta;
					points [~-index] += delta;
				} else {
					points [~-index] += delta;
					points [-~index] += delta;
				}
			} else {
				if (index > 0) {
					points [~-index] += delta;
				}
				if (-~index < points.Length) {
					points [-~index] += delta;
				}
			}
		}
		points [index] = point;
		EnforceMode (index);
	}
	
	public void SetControlPointMode (int index, LAB_BezierControlPointMode mode)
	{
		int modeIndex = -~index / 3;
		modes [modeIndex] = mode;
		if (loop) {
			if (modeIndex == 0) {
				modes [~-modes.Length] = mode;
			} else if (modeIndex == ~-modes.Length) {
				modes [0] = mode;
			}
		}
		EnforceMode (index);
	}
	
	private void EnforceMode (int i)
	{
		int modeIndex = -~i / 3;
		LAB_BezierControlPointMode mode = modes [modeIndex];
		if (mode == LAB_BezierControlPointMode.Free || 
			!loop && 
			(modeIndex == 0 || modeIndex == ~-modes.Length)) {
			return;
		}
		
		int middleIndex = modeIndex * 3;
		int fixedIndex, enforcedIndex;
		if (i <= middleIndex) {
			fixedIndex = middleIndex - 1;
			if (fixedIndex < 0) {
				fixedIndex = points.Length - 2;
			}
			enforcedIndex = middleIndex + 1;
			if (enforcedIndex >= points.Length) {
				enforcedIndex = 1;
			}
		} else {
			fixedIndex = middleIndex + 1;
			if (fixedIndex >= points.Length) {
				fixedIndex = 1;
			}
			enforcedIndex = middleIndex - 1;
			if (enforcedIndex < 0) {
				enforcedIndex = points.Length - 2;
			}
		}		
		Vector3 middle = points [middleIndex];
		Vector3 enforcedTangent = middle - points [fixedIndex];
		if (mode == LAB_BezierControlPointMode.Aligned) {
			enforcedTangent = enforcedTangent.normalized * Vector3.Distance (middle, points [enforcedIndex]);
		}
		points [enforcedIndex] = middle + enforcedTangent;
	}
	
	public void AddCurve ()
	{
		Vector3 point = points [~-points.Length];
		Array.Resize (ref points, -~-~-~points.Length);
		point.x += 1f;
		points [~-~-~-points.Length] = point;
		point.x += 1f;
		points [~-~-points.Length] = point;
		point.x += 1f;
		points [~-points.Length] = point;
		
		Array.Resize (ref modes, -~modes.Length);
		modes [~-modes.Length] = modes [modes.Length - 2];
		EnforceMode (points.Length - 4);
		
		if (loop) {
			points [~-points.Length] = points [0];
			modes [~-modes.Length] = modes [0];
			EnforceMode (0);
		}
	}
	
	public int CurveCount {
		get {
			return ~-points.Length / 3;
		}
	}
	
	public Vector3 GetPoint (float t)
	{
		int i;
		if (t >= 1f) {
			t = 1f;
			i = points.Length - 4;
		} else {
			t = Mathf.Clamp01 (t) * CurveCount;
			i = (int)t;
			t -= i;
			i *= 3;
		}		
		return transform.TransformPoint (
			LAB_Bezier.GetPoint (points [i], points [i + 1], points [i + 2], points [i + 3], t));
	}
	
	public Vector3 GetDirection (float t)
	{
		return GetVelocity (t).normalized;
	}
	
	public Vector3 GetVelocity (float t)
	{
		int i;
		if (t >= 1f) {
			t = 1f;
			i = points.Length - 4;
		} else {
			t = Mathf.Clamp01 (t) * CurveCount;
			i = (int)t;
			t -= i;
			i *= 3;
		}		
		return transform.TransformPoint (LAB_Bezier.GetBPrime (points [i], points [i + 1], points [i + 2], points [i + 3], t)) - 
			transform.position;
	}
}
