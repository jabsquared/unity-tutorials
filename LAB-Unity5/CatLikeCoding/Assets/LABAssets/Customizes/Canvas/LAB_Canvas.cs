using UnityEngine;
using System.Collections;

public class LAB_Canvas : MonoBehaviour
{
	public static IEnumerator MoveUILocal (RectTransform objectTransform, Vector3 target, float overTime)
	{
		Vector3 source = objectTransform.anchoredPosition;
		float startTime = Time.time;
		while (Time.time < startTime + overTime) {
			objectTransform.anchoredPosition = Vector3.Lerp (source, target, (Time.time - startTime) / overTime);
			yield return null;
		}
		objectTransform.anchoredPosition = target;
	}
}
