using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class LAB_BGMTester : MonoBehaviour
{
	
	public AudioClip[] bgmS;
	// Use this for initialization
	private int bgmIndex;
	
	[Range(0,180)]
	public float
		heightOffset;
	
	[Range(9,180)]
	public float
		buttonSize = 45f;
	
	void OnGUI ()
	{
		if (bgmS.Length > 1) {		
			if (GUI.Button (new Rect (
				Screen.width / 2 - buttonSize * 3 / 2, Screen.height - buttonSize - heightOffset, 
				buttonSize, buttonSize), "<")) {
				if (bgmIndex > 0) {
					loadAndPlay (bgmIndex -= 1);
				}
			}
			
			if (GUI.Button (new Rect (
				Screen.width / 2 - buttonSize / 2, Screen.height - buttonSize - heightOffset, 
				buttonSize, buttonSize), bgmIndex.ToString ())) {
				loadAndPlay (Random.Range (0, bgmS.Length));
			}
			
			if (GUI.Button (new Rect (
				Screen.width / 2 + buttonSize / 2, Screen.height - buttonSize - heightOffset, 
				buttonSize, buttonSize), ">")) {
				if (bgmIndex < bgmS.Length - 1) {
					loadAndPlay (bgmIndex += 1);
				}
			}
		}
	}
	
	void Awake ()
	{
		if (bgmS.Length > 0)
			loadAndPlay (Random.Range (0, bgmS.Length));
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!audio.isPlaying) {
			if (bgmS.Length > 0) {
				loadAndPlay (Random.Range (0, bgmS.Length));
			}
		}
	}
	
	void loadAndPlay (int index)
	{
		audio.clip = bgmS [bgmIndex = index];
		
		audio.Play ();
	}
}
