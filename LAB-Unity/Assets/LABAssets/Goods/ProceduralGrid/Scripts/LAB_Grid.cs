using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class LAB_Grid : MonoBehaviour
{
	public int xSize, ySize;
	
	private void Awake ()
	{
		StartCoroutine (Generate ());
	}
	
	private Vector3[] vertices;
	
	private Mesh mesh;
	
	private IEnumerator Generate ()
	{
		WaitForSeconds wait = new WaitForSeconds (0.05f);
		
		GetComponent<MeshFilter> ().mesh = mesh = new Mesh ();
		
		mesh.name = "Procedural Grid";
		
		vertices = new Vector3[(-~xSize * -~ySize)];
		for (int i = 0 , y = 0; y <-~ySize; ++y) {
			for (int x = 0; x <-~xSize; ++x,++i) {
				// Local Position of Gizmos:
				//transform.TransformPoint (vertices [i]) = new Vector3 (x, y);
				vertices [i] = new Vector3 (x, y);
				
				yield return wait;
			}
		}
		
		mesh.vertices = vertices;
		
		int[] triangles = {0, -~xSize,1, 1, -~xSize,xSize + 2};
		
		mesh.triangles = triangles;
		
	}
	
	private void OnDrawGizmos ()
	{
		if (vertices == null) {
			return;
		}
	
		Gizmos.color = Color.black;
		
		for (int i= 0; i < vertices.Length; ++i) {
			Gizmos.DrawSphere (vertices [i], 0.1f);
		}
	}
	
	
}
