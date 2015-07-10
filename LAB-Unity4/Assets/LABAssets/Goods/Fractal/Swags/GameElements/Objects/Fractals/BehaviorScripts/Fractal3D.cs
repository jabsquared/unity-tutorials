using UnityEngine;
using System.Collections;

public class Fractal3D : MonoBehaviour
{

	// TODO: Make a bool to either random the child direction. Then Random these.

	private static FractalDirection[] childDirections = {
		new FractalDirection (Vector3.up, Quaternion.identity),
		new FractalDirection (Vector3.right, Quaternion.Euler (0f, 0f, -90f)),
		new FractalDirection (Vector3.left, Quaternion.Euler (0f, 0f, 90f)),
		new FractalDirection (Vector3.forward, Quaternion.Euler (90f, 0f, 0f)),
		new FractalDirection (Vector3.back, Quaternion.Euler (-90f, 0f, 0f))
	};

	public Mesh[] meshes;
	public Material material;

	[Range(1,5)]
	public int
		maxDepth;
	private int depth;

	[Range(0,90)]
	public float
		maxRotationSpeed;
	private float rotationSpeed;

	[Range(0,90)]
	public float
		maxTwist;

	/*----------------------------------------------------------------------------------------*/
	//Core Methods
	//.... Start: Init the Fractal and Its Components
	//.... Update: Motion of the Fractal 
	/*----------------------------------------------------------------------------------------*/

	private void Start ()
	{

		if (InitializeMaterials ()) {
			//print("Initialized Materials...");
		}

		rotationSpeed = Random.Range (-maxRotationSpeed, maxRotationSpeed);
		transform.Rotate (Random.Range (-maxTwist, maxTwist), 0f, 0f);

		gameObject.AddComponent<MeshFilter> ().mesh = meshes [Random.Range (0, meshes.Length)];

		gameObject.AddComponent<MeshRenderer> ().material = materials [depth, Random.Range (0, materials.GetLength (1))];

		if (depth < maxDepth /*&& isVisible*/) {
			// Make a blank GameObject, add this Fractal Script, while calling the initalize method
			StartCoroutine (CreateChildren ());
		}
	}

	//TODO: Make a Random Direction of Rotation
	private void Update ()
	{
		transform.Rotate (0f, rotationSpeed * Time.deltaTime, 0f);
	}

	/*----------------------------------------------------------------------------------------*/
	/*----------------------------------------------------------------------------------------*/

	/*private bool isVisible;

	private void OnBecameInvisible ()
	{
			isVisible = enabled = false;
	}

	private void OnBecameVisible ()
	{
			isVisible = enabled = true;
	}*/


	/*----------------------------------------------------------------------------------------*/
	//Material Initilization Methods
	//.... Used to Generate Material array for each Depth, It runs only if the 
	//current node is a root.
	/*----------------------------------------------------------------------------------------*/

	private Material[,] materials;
	
	private bool InitializeMaterials ()
	{
		if (materials == null) {
			materials = new Material[maxDepth + 1, Random.Range (1, 8)];
			for (int i = 0; i <= maxDepth; ++i) {
				float t = i / (maxDepth - 1f);
				t *= t;
				for (int j = 0; j < materials.GetLength(1); ++j) {
					materials [i, j] = new Material (material);
					materials [i, j].color = Color.Lerp (Color.white, LAB_Color.RandomRGB, t);
				}
			}
			for (int j = 0; j < materials.GetLength(1); ++j) {
				materials [maxDepth, j].color = LAB_Color.RandomRGB;
			}
			return true;
		} else {
			return false;
		}
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
	
	private void InitializeFractal (Fractal3D parentFractal, int childIndex)
	{

		meshes = parentFractal.meshes;

		materials = parentFractal.materials;

		// WHY NOT ++? || It's for a child, ++ will increment the parent. 
		// The other child will have conflict since 1 parent many children.
		depth = parentFractal.depth + 1; // To avoid Depth conflict while creating many branches
		
		maxDepth = parentFractal.maxDepth;
		
		transform.parent = parentFractal.transform;
		
		childScale = parentFractal.childScale; 

		spawnRandomly = parentFractal.spawnRandomly;

		spawnProbability = parentFractal.spawnProbability;

		maxRotationSpeed = parentFractal.maxRotationSpeed;

		transform.localScale = Vector3.one * childScale;
		// 0.5 is half the size of parent, plus half of child because we move
		// the center of the child.
		transform.localPosition = 
			childDirections [childIndex].GetDirection () * (0.5f + 0.5f * childScale);
		transform.localRotation = childDirections [childIndex].GetOrientation ();
		
	}


	/*----------------------------------------------------------------------------------------*/
	//Create Children Method
	/*----------------------------------------------------------------------------------------*/
	[Header("- Create Children -")]

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
					AddComponent<Fractal3D> ().
						InitializeFractal (this, i);
			}
		}
	}
}
