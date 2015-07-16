using UnityEngine;
using UnityEngine.UI;
using System.Collections;


[RequireComponent(typeof (Slider))]
public abstract class LAB_FloatSlider <LAB> : MonoBehaviour {
	
	public static float value;
	
	private Text text;
	private Slider slider;

	private void Awake(){
		text = transform.FindChild("Value").GetComponent<Text>();
		slider = GetComponent<Slider>();
	}
	
	public void ChangeValue(){
		value = slider.value;
		text.text = value.ToString();
	}

	protected void SetToZero(){
		SetValue (0);
	}

	protected void SetToMid(){
		SetValue((slider.maxValue + slider.minValue)/2);
	}

	private void SetValue(float v){
		value = v;
		slider.value = value;
		text.text = value.ToString();
	}

}
