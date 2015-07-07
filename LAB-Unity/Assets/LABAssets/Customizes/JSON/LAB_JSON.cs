using UnityEngine;
using System.Collections;

public abstract class LAB_JSON : MonoBehaviour
{
	protected WWW data;
	
	protected  IEnumerator FetchData (string url)
	{
		WWW www = new WWW (url);
		
		yield return www;
		
		data = www;
	}	
}
