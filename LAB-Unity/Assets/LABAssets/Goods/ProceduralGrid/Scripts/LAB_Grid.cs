using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class LAB_Grid : MonoBehaviour
{
	public int xSize, ySize;

	public bool willWait;
		
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
				if (willWait) {
					yield return wait;
				}
			}
		}
		
		mesh.vertices = vertices;
		
		int[] triangles = new int[xSize * ySize * 6];
		for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++) {
			for (int x = 0; x < xSize; x++, ti+=6, vi++) {
				triangles [ti] = vi;
				triangles [ti + 3] = triangles [ti + 2] = -~vi;
				triangles [ti + 4] = triangles [ti + 1] = -~vi + xSize;
				triangles [ti + 5] = vi + xSize + 2;
				
				if (willWait) {
					yield return wait;
					mesh.triangles = triangles;
				}
			}
		}
		
		if (!willWait)
			mesh.triangles = triangles;
		
		mesh.RecalculateNormals ();
		
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
