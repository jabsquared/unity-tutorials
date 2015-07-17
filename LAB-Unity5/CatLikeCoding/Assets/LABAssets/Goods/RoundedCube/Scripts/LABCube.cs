using UnityEngine;
using System.Collections;

[RequireComponent (typeof(MeshFilter), typeof(MeshRenderer))]
public class LABCube : MonoBehaviour
{
	public int xSize, ySize, zSize;
		
	public bool vertexWillWait;
	
	public bool triangleWillWait;
	
	private Mesh mesh;
	
	private Vector3[] vertices;

	private void Awake ()
	{
		StartCoroutine (Generate ());
	}
	
	private IEnumerator Generate ()
	{
		GetComponent<MeshFilter> ().mesh = mesh = new Mesh ();
		mesh.name = "Procedral Cube";
		
		yield return StartCoroutine (CreateVertices ());
		
		yield return StartCoroutine (CreateTriangles ());
	}
	
	private IEnumerator CreateTriangles ()
	{
		WaitForSeconds wait = new WaitForSeconds (0.045f);
		
		int quads = (xSize * ySize + xSize * zSize + ySize * zSize) * 2;
		int[] triangles = new int[quads * 6];
		
		int ring = (xSize + zSize) * 2;
		int t = 0, v = 1;
		for (int y = 0; y<ySize; ++y,++v) {
			for (int q = 0; q<~-ring; ++q,++v) {
				t = SetQuad (triangles, t, v, -~v, v + ring, -~v + ring);
				if (triangleWillWait) {
					yield return wait;
				}
				mesh.triangles = triangles;
			}
			t = SetQuad (triangles, t, v, -~v - ring, v + ring, -~v);
			if (triangleWillWait) {
				yield return wait;
			}
			mesh.triangles = triangles;
		}
		
		t = CreateTopFace (triangles, t, ring);
		mesh.triangles = triangles;
		
	}

	private int CreateTopFace (int[] triangles, int t, int ring)
	{
		int v = ring * ySize;
		
		for (int x=0; x<xSize; ++x,++v) {
			t = SetQuad (triangles, t, v, -~v, ~-v + ring, v + ring);
		}
		t = SetQuad (triangles, t, v, -~v, ~-v + ring, v + 2);
		
		int vMin = ring * (ySize);
		int vMid = -~vMin;
		t = SetQuad (triangles, t, vMin, vMid, vMin - 1, vMid + xSize * 2 - 2);
		return t;
	}
	
	private static int SetQuad (int[] triangles, int i, int v00, int v10, int v01, int v11)
	{
		triangles [i] = v00;
		triangles [-~i] = triangles [i + 4] = v01;
		triangles [i + 2] = triangles [i + 3] = v10;
		triangles [i + 5] = v11;
		return i + 6;
	}
	
	private IEnumerator CreateVertices ()
	{
		WaitForSeconds wait = new WaitForSeconds (0.045f);
		
		int cornerVertices = 8;
		int edgeVertices = (xSize + ySize + zSize) * 4;
		int faceVertices = (
			(~-xSize) * (~-ySize) +
			(~-xSize) * (~-zSize) +
			(~-ySize) * (~-ySize)) * 2;
		
		vertices = new Vector3[cornerVertices + edgeVertices + faceVertices];
		
		int v = 0;
		for (int y = 0; y<=ySize; ++y) {
			for (int x=0; x <= xSize; ++x) {
				vertices [++v] = new Vector3 (x, y, 0);
				if (vertexWillWait) {
					yield return wait;
				}
			}
			for (int z=1; z <= zSize; ++z) {
				vertices [++v] = new Vector3 (xSize, y, z);
				if (vertexWillWait) {
					yield return wait;
				}
			}
			
			for (int x=~-xSize; x >= 0; --x) {
				vertices [++v] = new Vector3 (x, y, zSize);
				if (vertexWillWait) {
					yield return wait;
				}
			}
			for (int z=~-zSize; z > 0; --z) {
				vertices [++v] = new Vector3 (0, y, z);
				if (vertexWillWait) {
					yield return wait;
				}
			}
		}
		
		for (int z =1; z<zSize; ++z) {
			for (int x = 1; x<xSize; ++x) {
				vertices [++v] = new Vector3 (x, ySize, z);
				if (vertexWillWait) {
					yield return wait;
				}
			}
			for (int x = 1; x<xSize; ++x) {
				vertices [++v] = new Vector3 (x, 0, z);
				if (vertexWillWait) {
					yield return wait;
				}
			}
		}
		
		mesh.vertices = vertices;
	}
	
	private void OnDrawGizmos ()
	{
		if (vertices == null) {
			return;
		}
		Gizmos.color = Color.black;
		
		for (int i =0; i< vertices.Length; ++i) {
			Gizmos.DrawSphere (transform.TransformPoint (vertices [i]), 0.09f);
		}
			
	}
	
	
	
	
	
	
	
	
	
	
	
}
