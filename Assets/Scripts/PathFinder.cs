using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Path finder.
/// A simple tutorial pathfinding class with emphasis on "build it in 2 hours".
/// Takes 2 points and maps the best path it can to it on a graph generated from a mesh
/// Known issues :- lack of backtracking, optimization, visibility
/// </summary>
public class PathFinder : MonoBehaviour {

	/// <summary>
	/// Pathnode.
	/// A class that holds all the necessary variables to be a point on the path graph
	/// </summary>
	public class Pathnode
	{
		public Vector3 Position;
		public float BaseHeuristic;
		public List<Pathnode> NeighbourNodes = new List<Pathnode>();

		public Pathnode()
		{
			Position = Vector3.zero;
			BaseHeuristic = 0;
		}

		public Pathnode(Vector3 position, float baseHeuristic)
		{
			Position = position;
			BaseHeuristic = baseHeuristic;
		}

		//TODO :- make this safe
		public List<Pathnode> GetNeighbours()
		{
			return NeighbourNodes;
		}
	}

	/// <summary>
	/// Path.
	/// Helper class, made to organize the code
	/// </summary>
	private class Path
	{
		public List<Pathnode> NodeList = new List<Pathnode> ();
		public float Heuristic = 0;

		public void Clear()
		{
			NodeList.Clear ();
			Heuristic = 0;
		}
	}

	//a list of all the pathNodes in the scene/environment
	private List<Pathnode> _nodes = new List<Pathnode>();

	//the path that will be used to calculate the path
	private Path _path = new Path();
	private Pathnode _start;
	private Pathnode _end;

	public delegate float HeuristicSetter(Pathnode node);


	/// <summary>
	/// Initializes the pathfinder.
	/// Made to instill structure, always init your classes before you set about 
	/// using it. It may seem useless now, but when your code grows enough you will 
	/// want to have somewhere to initialize everything you need, in lieu of initializing things 
	/// on the fly.
	/// </summary>
	private void InitializePathfinder()
	{
		_nodes.Clear ();
		_path.Clear ();
	}
		
	/// <summary>
	/// Adds the mesh vertices as nodes on our path graph.
	/// </summary>
	/// <param name="terrain">Terrain.</param>
	/// <param name="baseHeuristic">Base heuristic.</param>
	/// <param name="setterCallback">Setter callback.</param>
	public void AddNodes(Mesh terrain,float baseHeuristic = 0,HeuristicSetter setterCallback = null)
	{
		//retrieve nodes
		Vector3[] nodes = terrain.vertices;
		Pathnode temp;

		//add them to our graph
		for (int i = 0 ; i < nodes.Length; ++i) {
			Vector3 nodePos = nodes [i];
			if (_nodes.Find (x => x.Position == nodePos) == null) {
				temp = new Pathnode (nodePos,baseHeuristic);
				_nodes.Add (temp);
			}
		}

		//assign neighbours for each node. THIS METHOD IS NOT OPTIMIZED. O(n) = n^3!
		for (int i = 0; i < nodes.Length; ++i) {
			List<Pathnode> neighbourNodes = new List<Pathnode> ();
			List<int> neighbours = new List<int> (MeshGenerator.GetNeighbors (terrain, i));
			for (int j = 0; j < neighbours.Count; ++j) {
				neighbourNodes.Add (_nodes.Find ((x) => x.Position == terrain.vertices [neighbours [j]]));	
			}
			_nodes[i].NeighbourNodes = neighbourNodes;
			_nodes[i].BaseHeuristic = (setterCallback == null ? baseHeuristic : setterCallback.Invoke (_nodes [i]));

		}

		//remove all unlinked nodes
		_nodes.RemoveAll ((x) => x.NeighbourNodes.Count == 0);

		Debug.Log (_nodes.Count + " " + terrain.vertices.Length);
	}

	/// <summary>
	/// Gets the node closest to a given position.
	/// </summary>
	/// <returns>The node closest to.</returns>
	/// <param name="pos">Position.</param>
	public Pathnode GetNodeClosestTo(Vector3 pos)
	{
		if (_nodes == null || _nodes.Count <= 0)
			return null;
		else {
			float shortestDistance = float.MaxValue;
			float dist = 0;
			Pathnode closestNode = null;
			for (int i = 0; i < _nodes.Count; ++i) {
				Pathnode node = _nodes [i];
				dist = Vector3.Distance (pos, node.Position);
				if (dist < shortestDistance) {
					shortestDistance = dist;
					closestNode = node;
				}
			}
			return closestNode;
		}
	}


	/// <summary>
	/// Gets the shortest path between 2 points.
	/// </summary>
	/// <returns>The shortest path.</returns>
	/// <param name="start">Start.</param>
	/// <param name="end">End.</param>
	private List<Pathnode> GetShortestPath(Vector3 start, Vector3 end)
	{
		if (_start == null)
			_start = new Pathnode ();
		_start.Position = start;
		_start.BaseHeuristic = 0;

		Pathnode nodeClosestToStart = GetNodeClosestTo (start);
		_start.NeighbourNodes.Add(nodeClosestToStart);

		//ensure you remove this later
		nodeClosestToStart.NeighbourNodes.Add (_start);

		if (_end == null)
			_end = new Pathnode ();
		_end.Position = end;
		_end.BaseHeuristic = 0;

		Pathnode nodeClosestToEnd = GetNodeClosestTo (end);
		_end.NeighbourNodes.Add (nodeClosestToEnd);

		//ensure you remove this later
		nodeClosestToEnd.NeighbourNodes.Add (_end);

		if (_end == _start)
			return null;

		//_end = GetNodeClosestTo (end);
		Pathnode currentNode = _start;
		HashSet<Pathnode> pathNodes = new HashSet<Pathnode> ();
		int iter = 0;
		do {
			iter ++;
			float shortestDist = float.MaxValue;
			float dist = 0;
			Pathnode closestNode = null;
			List<Pathnode> neighbours = currentNode.NeighbourNodes;
			for (int i = 0; i < neighbours.Count; ++i) {
				dist = (neighbours [i].Position - _end.Position).magnitude;
				dist += neighbours[i].BaseHeuristic;
				if (dist < shortestDist && !pathNodes.Contains(neighbours[i])) {
					shortestDist = dist;
					closestNode = neighbours [i];
				}
			}
			pathNodes.Add(currentNode);
			_path.NodeList.Add (currentNode);
			currentNode = closestNode;
		} while(currentNode != _end && iter < 1000);
		_path.NodeList.Add (currentNode);

		//remove the extra node
		nodeClosestToStart.NeighbourNodes.Remove (_start);
		//remove the extra node
		nodeClosestToStart.NeighbourNodes.Remove (_start);

		return _path.NodeList;
	}


	/// <summary>
	/// Gets the shortest path.
	/// channel function. You may need to create additional shortestPath methods within the
	/// path finder. Use this method to channel the calls
	/// </summary>
	/// <returns>The path.</returns>
	/// <param name="start">Start.</param>
	/// <param name="end">End.</param>
	public List<Pathnode> GetPath(Vector3 start, Vector3 end)
	{
		return GetShortestPath (start, end);
	}

	/// <summary>
	/// VISUALIZATION!!
	/// </summary>
	public void OnDrawGizmos()
	{
		foreach (Pathnode node in _nodes) {
			Gizmos.color = Color.white;
			Gizmos.DrawSphere (node.Position,0.1f);
			foreach (Pathnode neighbour in node.NeighbourNodes) {
				Gizmos.DrawLine (node.Position, neighbour.Position);
			}
		}
		if(_path != null && _path.NodeList != null && _path.NodeList.Count > 0)
		for (int i = 0 ; i < _path.NodeList.Count; ++i) {
			Gizmos.color = Color.red;
				Gizmos.DrawSphere ( _path.NodeList[i].Position,0.2f);
			if (i < _path.NodeList.Count - 1) {
				Gizmos.DrawLine (_path.NodeList [i].Position, _path.NodeList [i + 1].Position);
			}
		}

		Gizmos.color = Color.blue;
		if(_start != null)
			Gizmos.DrawSphere (_start.Position, 0.3f);
		if(_end != null)
			Gizmos.DrawSphere (_end.Position, 0.3f);
	}

}
