using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour
{

	public enum CELLSTATE
	{
		alive,
		dead,
	};

	public CELLSTATE cellState;

	public int aliveNeighbourCount{ get; set; }

	public Material matAlive;
	public Material matDead;

	MeshRenderer mrenderer;

	public void Awake ()
	{
		mrenderer = GetComponent <MeshRenderer> ();
	}

	public void RandomCellState ()
	{
		cellState = (Random.Range (0, 2) == 0) ? CELLSTATE.alive : CELLSTATE.dead;
		ChangeCellColor ();
	}

	public void ChangeCellColor ()
	{
		mrenderer.material = (cellState == CELLSTATE.alive) ? matAlive : matDead;
	}

	public void ChangeCellState ()
	{

		cellState = (cellState == CELLSTATE.alive) ? CELLSTATE.dead : CELLSTATE.alive;
		ChangeCellColor ();
	}

	void OnMouseUpAsButton ()
	{
		ChangeCellState ();
	}
}
