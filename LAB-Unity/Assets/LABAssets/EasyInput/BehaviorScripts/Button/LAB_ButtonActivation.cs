using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public abstract class LAB_ButtonActivation <LAB> : MonoBehaviour {

	private static bool activated;
	
	public static bool IsActivated{
		get {
			return  activated;
		}
	}

	public static void Activate (){
		activated = true;
	}
	
	public static void Deactivate(){
		activated = false;
	}

	public static void Toggle(){
		activated = !activated;
	}
}
