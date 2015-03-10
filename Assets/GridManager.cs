using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridManager : MonoBehaviour {

	private Dictionary< Tuple, GameObject> gameBoard;	// grid of tiles - access by passign tuple of int coordinates to get tile gameobject back


	// Use this for initialization
	void Start () {
		gameBoard = new Dictionary< Tuple, GameObject> ();
		gameBoard.Add (new Tuple(2,3),new GameObject());
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}

public class Tuple
{
	public int Item1 { get; set; }
	public int Item2 { get; set; }
	
	public Tuple(int Item1, int Item2)
	{
		this.Item1 = Item1;
		this.Item2 = Item2;
	}
}


