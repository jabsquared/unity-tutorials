using UnityEngine;
using System.Collections;

public class PaintingRandomized : MonoBehaviour {

	public Material[] paintings;
	
	// -0.27 -> 0.09
	// -0.27 -> 0.27
	// Use this for initialization
	void Start () {
		renderer.material = paintings[Random.Range(0, paintings.Length)];

		transform.localPosition += new Vector3 ( Random.Range (-0.27f,0.27f),
		                                        Random.Range (-0.27f, 0.09f), 0);

		if (Random.value < 0.5f)
			transform.localScale = new Vector3 (0.45f, 0.45f, 0.009f);

		transform.Rotate ( new Vector3(0, 0, Random.Range(0, 360f)));

		enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

	/*----------------------------------------------------------------------------------------*/
	private void OnBecameInvisible(){
		enabled = true;
	}
	
	private void OnBecameVisible(){
		if (Camera.main.name != "WatcherCamera"){
			enabled = true;
		}
	}

}
