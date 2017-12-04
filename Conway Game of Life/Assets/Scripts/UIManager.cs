using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	//public static UIManager instance;
	public InputField inputXGridSize, inputYGridSize;

	public void ActionSetGridSize ()
	{
		inputXGridSize.contentType = InputField.ContentType.IntegerNumber;

		int x = int.Parse (inputXGridSize.text);
		int y = int.Parse (inputYGridSize.text);

	

		GameManager.gm.InitGrid (x, y);
	}

	public void ActionStartSimulation (Text btnText)
	{
		if (!GameManager.isGameStarted) {
			GameManager.gm.StartSimulation ();
			btnText.text = "Stop";

		} else {
			GameManager.isGameStarted = false;
			btnText.text = "Start";
		}

	
	}

	public void ActionSetRandomCellState ()
	{
		GameManager.gm.RandomCellState ();	
	}

	public void ActionResetSimulation ()
	{
		GameManager.gm.ResetAllCells ();
	}

}
