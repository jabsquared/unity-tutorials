using UnityEngine;
using System.Collections;

public class Fractal2D : MonoBehaviour
{
	
	private static FractalDirection[] childDirections = {
		new FractalDirection (Vector3.up, Quaternion.identity),
		new FractalDirection (Vector3.right, Quaternion.Euler (0f, 0f, -90f)),
		new FractalDirection (Vector3.left, Quaternion.Euler (0f, 0f, 90f)),
		new FractalDirection (Vector3.down, Quaternion.Euler (0f, 0f, 0f))

	};

	public Sprite[] sprites;

	[Range(1,5)]
	public int
		maxDepth;
	private int depth;
	
	[Range(0,90)]
	public float
		maxRotationSpeed;
	private float rotationSpeed;

	/*----------------------------------------------------------------------------------------*/
	//Core Methods
	//.... Start: Init the Fractal and Its Components
	//.... Update: Motion of the Fractal 
	/*----------------------------------------------------------------------------------------*/

	private void Start ()
	{

		rotationSpeed = Random.Range (-maxRotationSpeed, maxRotationSpeed);

		gameObject.AddComponent<SpriteRenderer> ().sprite = sprites [Random.Range (0, sprites.Length)];
		if (depth < maxDepth /*&& isVisible*/) {
			// Make a blank GameObject, add this Fractal Script, while calling the initalize method
			StartCoroutine (CreateChildren ());
		}
	}
	/*----------------------------------------------------------------------------------------*/
	/*----------------------------------------------------------------------------------------*/
/*	private bool isVisible;
	
	private void OnBecameInvisible ()
	{
		isVisible = enabled = false;
	}

	private void OnBecameVisible ()
	{
		isVisible = enabled = true;
	}*/

	/*----------------------------------------------------------------------------------------*/


	[Tooltip("0 to disable")]
	public Vector3
		rotationDirection;

	private void Update ()
	{
		transform.Rotate (rotationDirection * (rotationSpeed * Time.deltaTime));

		//transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);

	}

	/*----------------------------------------------------------------------------------------*/
	//Fractal Initilization Method
	//.... Need to reassign the children's variable as they were blank when Init
	/*----------------------------------------------------------------------------------------*/
	
	[Header("- Init Fractal -")]
	[Tooltip("How much to Scale the Children")]
	[Range(0,1)]
	// Limit Scale from 0 to 1
	public float
		childScale; 

	private void InitializeFractal (Fractal2D parentFractal, int childIndex)
	{

		sprites = parentFractal.sprites;

		depth = parentFractal.depth + 1; // To avoid Depth conflict while creating many branches
		
		maxDepth = parentFractal.maxDepth;
		
		transform.parent = parentFractal.transform;
		
		childScale = parentFractal.childScale; 

		spawnRandomly = parentFractal.spawnRandomly;
		
		spawnProbability = parentFractal.spawnProbability;
		
		maxRotationSpeed = parentFractal.maxRotationSpeed;

		rotationDirection = parentFractal.rotationDirection;

		transform.localScale = Vector3.one * childScale;
		// 0.5 is half the size of parent, plus half of child because we are moving
		// the center of the child.
		transform.localPosition = 
			childDirections [childIndex].GetDirection () * (0.5f + 0.5f * childScale);
		transform.localRotation = childDirections [childIndex].GetOrientation ();
		
	}

	/*----------------------------------------------------------------------------------------*/
	//Create Children Method
	/*----------------------------------------------------------------------------------------*/
	[Header(" - Create Children - ")]

	[Range(0,4.5f)]
	public float
		waitTime;

	public bool spawnRandomly;

	[Range(0,1)]
	public float
		spawnProbability = 1.0f;

	private IEnumerator CreateChildren ()
	{
		for (int i = 0; i < childDirections.Length; ++i) {
			if (spawnRandomly ? (Random.value < Random.value) : (Random.value < spawnProbability)) {
				yield return new WaitForSeconds (Random.Range (0.1f, waitTime));
				new GameObject ("Fractal Child").
					AddComponent<Fractal2D> ().
						InitializeFractal (this, i);
			}
		}
	}
}
