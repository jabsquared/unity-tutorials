using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof (Text))]
public class FPSDisplay : MonoBehaviour {

	[Range(0,4.5f)]
	public float updateInterval = 1.8f;

	private float accumulation;
	private int frames;
	private float timeLeft;

	private Text fpsCounter;

	private void Start () {
		fpsCounter = gameObject.GetComponent<Text>();
		ResetCounter();
	}
	[System.Serializable]
	public class QualityIndicator {
		public Color good, medium, bad;
	}

	public QualityIndicator fpsIndicator;

	private void Update () {
		timeLeft -= Time.deltaTime;
		accumulation += Time.timeScale / Time.deltaTime;
		++ frames;

		if (timeLeft <= 0){
			float fps = accumulation/frames;
				string fpsFormated = string.Format(" [FPS: {0:F2} ] ", fps);
			fpsCounter.text = fpsFormated;

			if (fps > 30 ){
				fpsCounter.color  = LAB_Color.LerpTo(fpsCounter.color, fpsIndicator.good);
			}
			else if (fps >10){
				fpsCounter.color = LAB_Color.LerpTo(fpsCounter.color, fpsIndicator.medium);
			}
			else {
				fpsCounter.color = LAB_Color.LerpTo(fpsCounter.color, fpsIndicator.bad);
			}
			ResetCounter();
		}
	}

	private void ResetCounter(){
		timeLeft = updateInterval;
		accumulation = 0;
		frames = 0;
	}
}
