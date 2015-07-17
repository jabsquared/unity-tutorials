using UnityEngine;
using System.Collections;

public class LAB_Color
{

	public static Color LerpTo (Color baseColor, Color finishColor)
	{
		return (baseColor == finishColor) ? 
			(finishColor) : (Color.Lerp (baseColor, finishColor, Time.deltaTime * 4.5f));
	}
	
	public static Color HalfA (Color baseColor)
	{
		return new Color (baseColor.r, baseColor.g, baseColor.b, 0.54f);
	}

	public static Color RandomA (Color baseColor)
	{
		return new Color (baseColor.r, baseColor.g, baseColor.b, Random.value);
	}

	public static Color RandomRGB {
		get {
			return new Color (Random.value, Random.value, Random.value, 0.99f);
		}
	}

	public static Color RandomRGBA {
		get {
			return new Color (Random.value, Random.value, Random.value, Random.value);
		}
	}
}
