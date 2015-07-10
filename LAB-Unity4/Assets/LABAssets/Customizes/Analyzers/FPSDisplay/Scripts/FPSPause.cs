using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class FPSPause : MonoBehaviour, IPointerClickHandler {

	private bool activate;
	
	private void Awake(){
		activate = GetComponent<FPSDisplay>().enabled;
	}

	public void OnPointerClick (PointerEventData data){
		activate = !activate;

		if (activate){
			gameObject.GetComponent<FPSDisplay>().enabled = true;
			gameObject.GetComponent<Text>().CrossFadeAlpha(0.99f,0.9f,true);
		}
		else{
			gameObject.GetComponent<FPSDisplay>().enabled = false;
			gameObject.GetComponent<Text>().CrossFadeAlpha(0.09f,0.9f,true);
		}
	}
}
