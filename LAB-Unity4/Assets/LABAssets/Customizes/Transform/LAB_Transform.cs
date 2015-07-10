using UnityEngine;
using System.Collections;

public class LAB_Transform : MonoBehaviour {
	
	//Move is not Vibrate || Transpose between

	public static IEnumerator MoveObjectLocal(Transform objectTransform, Vector3 target, float overTime){
		Vector3 source = objectTransform.localPosition;
		float startTime = Time.time;
		while(Time.time < startTime + overTime) {
			objectTransform.localPosition = Vector3.Lerp(source, target, (Time.time - startTime)/overTime);
			yield return null;
		}
		objectTransform.localPosition = target;
	}
	
	public static IEnumerator RotateObjectLocal (Transform objectTransform, Quaternion target, float overTime){
		Quaternion source = objectTransform.localRotation;
		float startTime = Time.time;
		while (Time.time < startTime + overTime){
			objectTransform.localRotation = Quaternion.Lerp (source, target, (Time.time - startTime)/overTime);
			yield return null;
		}
		objectTransform.localRotation = target;
	}

	public static IEnumerator MoveObjectGlobal(Transform objectTransform, Vector3 target, float overTime){
		Vector3 source = objectTransform.position;
		float startTime = Time.time;
		while(Time.time < startTime + overTime) {
			objectTransform.position = Vector3.Lerp(source, target, (Time.time - startTime)/overTime);
			yield return null;
		}
		objectTransform.position = target;
	}

	public static IEnumerator RotateObjectGlobal (Transform objectTransform, Quaternion target, float overTime){
		Quaternion source = objectTransform.rotation;
		float startTime = Time.time;
		while (Time.time < startTime + overTime){
			objectTransform.rotation = Quaternion.Lerp (source, target, (Time.time - startTime)/overTime);
			yield return null;
		}
		objectTransform.rotation = target;
	}
}
