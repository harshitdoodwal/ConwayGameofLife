using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager gm;
	
	internal int xSize;
	internal int ySize;

	public float updateInterval;
	public Cell cellPrefab;
	public Cell[,] cellList, cellListNew;

	public static bool isGameStarted = false;
	public bool isGridDrawn = false;

	void Awake ()
	{
		gm = this;
	}

	public void InitGrid (int x, int y)
	{
		//TODO dynamically create new array 
		if (isGridDrawn) {
			isGameStarted = false;
			AlterCellGrid (x, y);
		}

		//HACK Destroy Existing
		//		if (cellList != null) {
		//			for (int i = 0; i < xSize; i++) {
		//				for (int j = 0; j < ySize; j++) {
		//					GameObject.Destroy (cellList [i, j].gameObject);
		//				}
		//			}

		else {

			xSize = x;
			ySize = y;

			cellList = new Cell[xSize, ySize];
			for (int i = 0; i < xSize; i++) {
				for (int j = 0; j < ySize; j++) {
					Cell cell = Instantiate (cellPrefab, new Vector3 (i, j, 0), Quaternion.identity)as Cell;
					cell.cellState = Cell.CELLSTATE.dead;
					//HACK For making development easy
					cell.gameObject.name = "cell" + i + j;
					cellList [i, j] = cell;
				}
			}
			isGridDrawn = true;
		}
	}

	public void RandomCellState ()
	{
		for (int i = 0; i < xSize; i++) {
			for (int j = 0; j < ySize; j++) {
				cellList [i, j].RandomCellState ();
			}
		}
	}

	public void ResetAllCells ()
	{
		for (int i = 0; i < xSize; i++) {
			for (int j = 0; j < ySize; j++) {
				cellList [i, j].cellState = Cell.CELLSTATE.dead;
				cellList [i, j].ChangeCellColor ();
			}
		}
	}

	public void StartSimulation ()
	{
		isGameStarted = true;
		Debug.Log ("Simulation Started");
		StartCoroutine (UpdateCellCycle ());
	}

	#region Game of Life Algorithm

	IEnumerator UpdateCellCycle ()
	{

		while (isGameStarted) {
			
			for (int i = 0; i < xSize; i++) {
				for (int j = 0; j < ySize; j++) {
					cellList [i, j].aliveNeighbourCount = GetLivingNeighourCount (i, j);
				}
			}

			for (int i = 0; i < xSize; i++) {
				for (int j = 0; j < ySize; j++) {
					
					if (cellList [i, j].cellState == Cell.CELLSTATE.alive && cellList [i, j].aliveNeighbourCount < 2)
						cellList [i, j].cellState = Cell.CELLSTATE.dead;
					//
					if (cellList [i, j].cellState == Cell.CELLSTATE.alive && (cellList [i, j].aliveNeighbourCount == 2 || cellList [i, j].aliveNeighbourCount == 3))
						cellList [i, j].cellState = Cell.CELLSTATE.alive;
					//
					if (cellList [i, j].cellState == Cell.CELLSTATE.alive && cellList [i, j].aliveNeighbourCount > 3)
						cellList [i, j].cellState = Cell.CELLSTATE.dead;
					//
					if (cellList [i, j].cellState != Cell.CELLSTATE.alive && cellList [i, j].aliveNeighbourCount == 3)
						cellList [i, j].cellState = Cell.CELLSTATE.alive;


					cellList [i, j].ChangeCellColor ();
				}
			}
			yield return new WaitForSeconds (updateInterval);
		}
	}

	int GetLivingNeighourCount (int x, int y)
	{
		int count = 0;

		// Check cell on the right.
		if (x != xSize - 1)
		if (cellList [x + 1, y].cellState != Cell.CELLSTATE.dead)
			count++;

		// Check cell on the bottom right.
		if (x != xSize - 1 && y != ySize - 1)
		if (cellList [x + 1, y + 1].cellState != Cell.CELLSTATE.dead)
			count++;

		// Check cell on the bottom.
		if (y != xSize - 1)
		if (cellList [x, y + 1].cellState != Cell.CELLSTATE.dead)
			count++;

		// Check cell on the bottom left.
		if (x != 0 && y != ySize - 1)
		if (cellList [x - 1, y + 1].cellState != Cell.CELLSTATE.dead)
			count++;

		// Check cell on the left.
		if (x != 0)
		if (cellList [x - 1, y].cellState != Cell.CELLSTATE.dead)
			count++;

		// Check cell on the top left.
		if (x != 0 && y != 0)
		if (cellList [x - 1, y - 1].cellState != Cell.CELLSTATE.dead)
			count++;

		// Check cell on the top.
		if (y != 0)
		if (cellList [x, y - 1].cellState != Cell.CELLSTATE.dead)
			count++;

		// Check cell on the top right.
		if (x != ySize - 1 && y != 0)
		if (cellList [x + 1, y - 1].cellState != Cell.CELLSTATE.dead)
			count++;

		return count;

	}

	#endregion

	public void AlterCellGrid (int xNew, int yNew)
	{
		if (xNew > xSize || yNew > ySize) {
			cellListNew = new Cell[xNew, yNew];

			for (int i = 0; i < xNew; i++) {
				for (int j = 0; j < yNew; j++) {
					if (j > (ySize - 1) || i > (xSize - 1)) {
						//	Debug.Log (" NewCell " + i + " " + j);
						Cell cell = Instantiate (cellPrefab, new Vector3 (i, j, 0), Quaternion.identity)as Cell;
						cell.cellState = Cell.CELLSTATE.dead;
						cell.gameObject.name = "cell" + i + j;
						cellListNew [i, j] = cell;
					} else {
						//	Debug.Log (" Exists " + i + " " + j);
						cellListNew [i, j] = cellList [i, j];
					}
				}
			}
			cellList = new Cell[xNew, yNew];
			cellList = cellListNew;
		} else {
			
			for (int i = 0; i < xSize; i++) {
				for (int j = 0; j < ySize; j++) {
					if (j > (yNew - 1) || i > (xNew - 1)) {
						//	Debug.Log ("Destroy " + cellList [i, j].gameObject.name);
						Destroy (cellList [i, j].gameObject);
					}
				}
			}
		}
		xSize = xNew;
		ySize = yNew;
		StartSimulation ();
	}
}
