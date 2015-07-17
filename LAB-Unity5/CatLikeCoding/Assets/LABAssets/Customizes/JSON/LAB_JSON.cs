using UnityEngine;
using System.Collections;

public abstract class LAB_JSON : MonoBehaviour
{
	protected WWW data;
	
	public string GetData (string url)
	{
		StartCoroutine (FetchData (url));
		return data.text;
	}
	
	private IEnumerator FetchData (string url)
	{
		WWW fetch = new WWW (url);
		
		while (!fetch.isDone && fetch.error == null) {
			yield return null;
		}
		data = fetch;
	}	
}
