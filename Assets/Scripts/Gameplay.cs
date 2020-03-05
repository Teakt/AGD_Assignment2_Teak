using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gameplay.
/// A class created to maintain code flow.
/// MeshGenerator and Pathfinder are seperate entities that can be used by other scripts, such
/// as this one.
/// </summary>
public class Gameplay : MonoBehaviour {


	public MeshGenerator _gen;
	public PathFinder _pf;

	public void Start()
	{
		Mesh m = _gen.GenerateMesh ();

		_pf.AddNodes (m, 0, (PathFinder.Pathnode pos)=>{
			//go wild! create your own heuristic
			return 0;
		});
		_pf.GetPath (m.vertices [0] - Vector3.one * 2, m.vertices [Random.Range(1,m.vertices.Length-1)]);
	}
}
