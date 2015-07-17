using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Toggle))]
public class LAB_Toggle : MonoBehaviour
{

	public static bool isEnabled;

	public void Toggled ()
	{
		isEnabled = GetComponent<Toggle> ().isOn;
	}

}
