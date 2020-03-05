using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Mesh generator.
/// Generates a flat plane
/// http://kobolds-keep.net/downloads/ProceduralTerrain/ProceduralTerrain.cs
/// This code is released under the Creative Commons 0 License. https://creativecommons.org/publicdomain/zero/1.0/
/// Its an interesting read if you have the time and enthusiasm
/// </summary>
public class MeshGenerator : MonoBehaviour {

	public int width = 10;
	public float spacing = 1f;
	public MeshFilter meshFilter = null;

    
	public Mesh GenerateMesh ()
	{
		float start_time = Time.time;

		List<Vector3[]> verts = new List<Vector3[]>();
		List<int> tris = new List<int>();
		List<Vector2> uvs = new List<Vector2>();

		// Generate everything.
		for (int z = 0; z < width; z++)
		{
			verts.Add(new Vector3[width]);
			for (int x = 0; x < width; x++)
			{
				Vector3 current_point = new Vector3();
				current_point.x = (x * spacing) - (width/2f*spacing);
				current_point.z = z * spacing - (width/2f*spacing);
				// Triangular grid offset
				int offset = z % 2;
				if (offset == 1)
				{
					current_point.x -= spacing * 0.5f;
				}

				current_point.y = 0;

				verts[z][x] = current_point;
				uvs.Add(new Vector2(x,z)); // TODO Add a variable to scale UVs.

				// TODO The edges of the grid aren't right here, but as long as we're not wrapping back and making underside faces it should be okay.

				// Don't generate a triangle if it would be out of bounds.
				int current_x = x + (1-offset);
				if (current_x-1 <= 0 || z <= 0 || current_x >= width)
				{
					continue;
				}
				// Generate the triangle north of you.
				tris.Add(x + z*width);
				tris.Add(current_x + (z-1)*width);
				tris.Add((current_x-1) + (z-1)*width);

				// Generate the triangle northwest of you.
				if (x-1 <= 0 || z <= 0)
				{
					continue;
				}
				tris.Add(x + z*width);
				tris.Add((current_x-1) + (z-1)*width);
				tris.Add((x-1) + z*width);
			}
		}

		// Unfold the 2d array of verticies into a 1d array.
		Vector3[] unfolded_verts = new Vector3[width*width];
		int i = 0;
		foreach (Vector3[] v in verts)
		{
			v.CopyTo(unfolded_verts, i * width);
			i++;
		}

		// Generate the mesh object.
		Mesh ret = new Mesh();
		ret.vertices = unfolded_verts;
		ret.triangles = tris.ToArray();
		ret.uv = uvs.ToArray();

		// Assign the mesh object and update it.
		ret.RecalculateBounds();
		ret.RecalculateNormals();
		meshFilter.mesh = ret;

		float diff = Time.time - start_time;
		Debug.Log("ProceduralTerrain was generated in " + diff + " seconds.");

		return ret;
	}

	//retrieves the neighbours of the mesh vertex at the index
	public static List<int> GetNeighbors(Mesh meshX, int index){
		List<int> verts = new List<int>();
		for(int i =0 ; i < meshX.triangles.Length / 3; i++){
			// see if the triangle contains the index
			bool found = false;
			for(int j = 0; j<3; j++){
				int cur = meshX.triangles[i * 3 + j];
				if(cur == index) found = true;
			}
			// if we found the index in the triangle, append the others.
			if(found){
				for(int j = 0; j<3; j++){
					int cur = meshX.triangles[i * 3 + j];
					if(verts.IndexOf(cur) == -1 && cur != index){
						verts.Add(cur);
					}
				}
			}
		}
		return verts;
	}
}
