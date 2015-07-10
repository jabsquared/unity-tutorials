using UnityEngine;
using System.Collections;

[RequireComponent (typeof(MeshFilter), typeof(MeshRenderer))]
public class LABCube : MonoBehaviour
{
	public int xSize, ySize, zSize;
	public bool willWait;
	
	private Mesh mesh;
	private Vector3[] vertices;

	private void Awake ()
	{
		StartCoroutine (Generate ());
	}
	
	private IEnumerator Generate ()
	{
		WaitForSeconds wait = new WaitForSeconds (0.045f);
		
		GetComponent<MeshFilter> ().mesh = mesh = new Mesh ();
		mesh.name = "Procedral Cube";
		
		int cornerVertices = 8;
		int edgeVertices = (xSize + ySize + zSize - 3) * 4;
		int faceVertices = (
			(~-xSize) * (~-ySize) +
			(~-xSize) * (~-zSize) +
			(~-ySize) * (~-ySize)) * 2;
		
		vertices = new Vector3[cornerVertices + edgeVertices + faceVertices];
		
		int v = 0;
		for (int x=0; x < xSize; ++x) {
			vertices [v++] = new Vector3 (x, 0, 0);
			if (willWait) {
				yield return wait;
			}
		}
	}
	
	private void OnDrawGizmos ()
	{
		if (vertices == null) {
			return;
		}
		Gizmos.color = Color.black;
		
		for (int i =0; i< vertices.Length; ++i) {
			Gizmos.DrawSphere (vertices [i], 0.09f);
		}
		
	}
	
	
	
	
	
	
	
	
	
	
	
}
