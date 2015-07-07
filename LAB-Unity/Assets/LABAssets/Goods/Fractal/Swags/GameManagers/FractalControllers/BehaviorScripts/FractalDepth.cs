using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class FractalDepth : LAB_IntSlider <FractalDepth>
{ 
	public Transform FractalSystem;
	
	private Transform[] fractals;
	
	private void Start ()
	{
		fractals = new Transform[FractalSystem.childCount];
		for (int i = 0; i < FractalSystem.childCount; ++i) {
			if (FractalSystem.GetChild (i).gameObject.activeSelf)
				fractals [i] = FractalSystem.GetChild (i);
		}
	}
}
