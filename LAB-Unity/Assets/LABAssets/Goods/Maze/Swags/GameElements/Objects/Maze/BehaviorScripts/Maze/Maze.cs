using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Maze : MazeUtilities {

	public static bool isGenerated;

	private RectTransform mazeButtons, mazeSliders, joySticks;

	[Range(0,1.8f)]
	public float guiSpeed = 0.45f;
	/*----------------------------------------------------------------------------------------*/
	//Core Methods
	//.... Start: 
	//.... Update:  
	/*----------------------------------------------------------------------------------------*/
	private void Awake () {
		mazeButtons = GameObject.FindGameObjectWithTag("MazeButtons").GetComponent<RectTransform>();
		mazeSliders = GameObject.FindGameObjectWithTag("MazeSliders").GetComponent<RectTransform>();
		joySticks = GameObject.FindGameObjectWithTag("Joysticks").GetComponent<RectTransform>();
	}

	/*private void Update () {

	}*/

	/*----------------------------------------------------------------------------------------*/
	//Maze Generators without Delay
	public void Generate (){
		cells = new MazeCell[size.x, size.z];
		List<MazeCell> activeCells =  new List<MazeCell>();
		DoFirstGenerationStep (activeCells);
		isGenerated = false;
		ShowGenUI();
		while (activeCells.Count > 0){
			DoNextGenerationStep(activeCells);
		}
		isGenerated = true;
		ShowPlayUI();
		for (int i = 0; i < rooms.Count; i++) {
			rooms[i].Hide();
		}
	}

	/*----------------------------------------------------------------------------------------*/
	//Maze Generators with Delay

	//[Range(0,0.09f)]
	//public float generationDelay;

	public IEnumerator GenerateWithDelay (){
		WaitForSeconds delay = new WaitForSeconds(MazeGenerationDelay.value);
		cells = new MazeCell[size.x, size.z];

		List<MazeCell> activeCells =  new List<MazeCell>();
		DoFirstGenerationStep (activeCells);

		isGenerated = false;

		ShowGenUI();

		while (activeCells.Count > 0){
			yield return delay;
			if (MazePause.IsActivated){
				if (MazeNext.IsActivated){
					DoNextGenerationStep(activeCells);
				}
			}
			else{
				DoNextGenerationStep(activeCells);
			}
		}

		isGenerated = true;

		ShowPlayUI();

		for (int i = 0; i < rooms.Count; i++) {
			rooms[i].Hide();
		}
			
	}
	
	/*----------------------------------------------------------------------------------------*/
	// Do First Generation Step
	// ... Choose a Random Cell to Integrate from
	private void DoFirstGenerationStep (List<MazeCell> activeCells){
		MazeCell newCell = CreatedCell (RandomCoordinates);
		newCell.Initialize(CreatedRoom(-9));
		activeCells.Add(newCell);
	}

	/*----------------------------------------------------------------------------------------*/
	private void TracePreviousGenerationStep(List<MazeCell> activeCells){

		//int currentIndex = cellIndexFlavor(MazeFlavor.value , activeCells.Count - 1);

		/*MazeCell currentCell = activeCells [currentIndex];

		Destroy(currentCell);*/

	}

	/*----------------------------------------------------------------------------------------*/
	// Do Next Generation Step
	// ... Generate the next Generation of the cell
	private void DoNextGenerationStep (List<MazeCell> activeCells){

		int currentIndex = CellFlavor(MazeFlavor.value , activeCells.Count - 1);
		MazeCell currentCell = activeCells [currentIndex];

		if (currentCell.IsFullyInitialized) {
			activeCells.RemoveAt(currentIndex);
			return;
		}

		MazeDirection direction = currentCell.RandomUninitializedDirection;
		IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();

		if (ContainsCoordinates(coordinates)){
			MazeCell neighbor = GetCell (coordinates);
			if (neighbor == null){
				neighbor = CreatedCell (coordinates);
				CreatePassage(currentCell, neighbor, direction);
				activeCells.Add(neighbor);
			}
			else if (currentCell.room.settingIndex == neighbor.room.settingIndex){
				CreatePassageInSameRoom(currentCell, neighbor, direction);
			}
			else {
				CreateWall (currentCell, neighbor, direction);
			}
		}
		else {
			CreateWall (currentCell, null, direction);
		}
	}

	void ShowGenUI(){
		StartCoroutine(LAB_Transform.MoveObjectLocal(joySticks, new Vector3 (0,-450,0), guiSpeed));
		StartCoroutine(LAB_Transform.MoveObjectLocal(mazeButtons, new Vector3 (0, 0, 0), guiSpeed));
		StartCoroutine(LAB_Transform.MoveObjectLocal(mazeSliders, new Vector3 (0, 0, 0), guiSpeed));
	}
	void ShowPlayUI(){
		StartCoroutine(LAB_Transform.MoveObjectLocal(joySticks, new Vector3 (0, 0, 0), guiSpeed));
		StartCoroutine(LAB_Transform.MoveObjectLocal(mazeButtons, new Vector3 (0, 270, 0), guiSpeed));
		StartCoroutine(LAB_Transform.MoveObjectLocal(mazeSliders, new Vector3 (0, -360, 0), guiSpeed));
	}
}
