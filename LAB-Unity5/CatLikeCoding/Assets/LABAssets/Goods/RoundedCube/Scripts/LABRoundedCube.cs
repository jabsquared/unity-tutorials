using UnityEngine;
using System.Collections;

[RequireComponent (typeof(MeshFilter), typeof(MeshRenderer))]
public class LABRoundedCube : MonoBehaviour
{
	public int xSize, ySize, zSize;
		
	public int roundness;
		
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
		int t = 0, v = 0;
		
		for (int y = 0; y < ySize; ++y, v++) {
			for (int q = 0; q < ~-ring; ++q, v++) {
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
		t = CreateBottomFace (triangles, t, ring);
		
		mesh.triangles = triangles;
		
	}

	private int CreateTopFace (int[] triangles, int t, int ring)
	{
		int v = ring * ySize;
		for (int x = 0; x < ~-xSize; ++x, v++) {
			t = SetQuad (triangles, t, v, -~v, ~-v + ring, v + ring);
		}
		t = SetQuad (triangles, t, v, -~v, ~-v + ring, v + 2);
		
		int vMin = ring * (ySize + 1) - 1;
		int vMid = vMin + 1;
		int vMax = v + 2;
		
		for (int z = 1; z < ~-zSize; ++z, vMin--, vMid++, vMax++) {
			t = SetQuad (triangles, t, vMin, vMid, ~-vMin, ~-vMid + xSize);
			for (int x = 1; x < ~-xSize; ++x, vMid++) {
				t = SetQuad (
					triangles, t,
					vMid, -~vMid, ~-vMid + xSize, vMid + xSize);
			}
			t = SetQuad (triangles, t, vMid, vMax, ~-vMid + xSize, -~vMax);
		}
		
		int vTop = vMin - 2;
		t = SetQuad (triangles, t, vMin, vMid, -~vTop, vTop);
		for (int x = 1; x < ~-xSize; ++x, vTop--, vMid++) {
			t = SetQuad (triangles, t, vMid, -~vMid, vTop, ~-vTop);
		}
		t = SetQuad (triangles, t, vMid, vTop - 2, vTop, ~-vTop);
		
		return t;
	}
	
	private int CreateBottomFace (int[] triangles, int t, int ring)
	{
		int v = 1;
		int vMid = vertices.Length - ~-xSize * ~-zSize;
		t = SetQuad (triangles, t, ~-ring, vMid, 0, 1);
		for (int x = 1; x < ~-xSize; ++x, v++, vMid++) {
			t = SetQuad (triangles, t, vMid, -~vMid, v, -~v);
		}
		t = SetQuad (triangles, t, vMid, v + 2, v, -~v);
		
		int vMin = ring - 2;
		vMid -= xSize - 2;
		int vMax = v + 2;
		
		for (int z = 1; z < ~-zSize; ++z, vMin--, vMid++, vMax++) {
			t = SetQuad (triangles, t, vMin, vMid + xSize - 1, vMin + 1, vMid);
			for (int x = 1; x < xSize - 1; x++, vMid++) {
				t = SetQuad (
					triangles, t,
					vMid + xSize - 1, vMid + xSize, vMid, vMid + 1);
			}
			t = SetQuad (triangles, t, vMid + xSize - 1, vMax + 1, vMid, vMax);
		}
		
		int vTop = vMin - 1;
		t = SetQuad (triangles, t, vTop + 1, vTop, vTop + 2, vMid);
		for (int x = 1; x < xSize - 1; ++x, vTop--, vMid++) {
			t = SetQuad (triangles, t, vTop, vTop - 1, vMid, vMid + 1);
		}
		t = SetQuad (triangles, t, vTop, vTop - 1, vMid, vTop - 2);
		
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
	
	private Vector3[] normals;
	
	private IEnumerator CreateVertices ()
	{
		WaitForSeconds wait = new WaitForSeconds (0.045f);
		
		int cornerVertices = 8;
		int edgeVertices = (xSize + ySize + zSize - 3) * 4;
		int faceVertices = (
			(~-xSize) * (~-ySize) +
			(~-xSize) * (~-zSize) +
			(~-ySize) * (~-zSize)) * 2;
		
		vertices = new Vector3[cornerVertices + edgeVertices + faceVertices];
		normals = new Vector3[vertices.Length];
		
		int v = 0;
		for (int y = 0; y<=ySize; ++y) {
			for (int x=0; x <= xSize; ++x) {
				SetVertex (v++, x, y, 0);
				if (vertexWillWait) {
					yield return wait;
				}
			}
			for (int z=1; z <= zSize; ++z) {
				SetVertex (v++, xSize, y, z);
				if (vertexWillWait) {
					yield return wait;
				}
			}
			for (int x=~-xSize; x >= 0; --x) {
				SetVertex (v++, x, y, zSize);
				if (vertexWillWait) {
					yield return wait;
				}
			}
			for (int z=~-zSize; z > 0; --z) {
				SetVertex (v++, 0, y, z);
				if (vertexWillWait) {
					yield return wait;
				}
			}
		}
		for (int z =1; z<zSize; ++z) {
			for (int x = 1; x<xSize; ++x) {
				SetVertex (v++, x, ySize, z);
				if (vertexWillWait) {
					yield return wait;
				}
			}
		}
		for (int z =1; z<zSize; ++z) {
			for (int x = 1; x<xSize; ++x) {
				SetVertex (v++, x, 0, z);
				if (vertexWillWait) {
					yield return wait;
				}
			}
		}
		mesh.vertices = vertices;
		mesh.normals = normals;
	}
	
	private void SetVertex (int i, int x, int y, int z)
	{
		vertices [i] = new Vector3 (x, y, z);
	}
	
	private void OnDrawGizmos ()
	{
		if (vertices == null) {
			return;
		}
		
		Gizmos.color = Color.black;
		
		for (int i =0; i< vertices.Length; ++i) {
			
			Gizmos.color = Color.black;
			
			Gizmos.DrawSphere (transform.TransformPoint (vertices [i]), 0.09f);
			
			Gizmos.color = Color.yellow;
			
			Gizmos.DrawRay (vertices [i], normals [i]);
		}	
	}
}
