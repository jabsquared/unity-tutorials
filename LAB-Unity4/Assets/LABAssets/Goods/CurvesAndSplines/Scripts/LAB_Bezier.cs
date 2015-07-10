using UnityEngine;

public enum LAB_BezierControlPointMode
{
	Free,
	Aligned,
	Mirrored
}

public static class LAB_Bezier
{	
	public static Vector3 GetPoint (Vector3 p0, Vector3 p1, Vector3 p2, float t)
	{
		t = Mathf.Clamp01 (t);
	
		return
			--t * t * p0 -
			2f * t * ++t * p1 +
			t * t * p2;
	}
	
	public static Vector3 GetBPrime (Vector3 p0, Vector3 p1, Vector3 p2, float t)
	{
		return 2f * (--t * (p0 - p1) - ++t * (p1 - p2));
	}
	
	public static Vector3 GetPoint (Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
	{
		t = Mathf.Clamp01 (t);
		
		return
			--t * t * -t * p0 +
			3f * t * t * ++t * p1 -
			3f * --t * ++t * t * p2 +
			t * t * t * p3;
	}
	
	public static Vector3 GetBPrime (Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
	{
		t = Mathf.Clamp01 (t);
		
		return 
			3f * --t * -t * (p0 - p1) +
			6f * t * ++t * (p1 - p2) -
			3f * t * t * (p2 - p3);
	}
	
}
