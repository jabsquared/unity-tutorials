using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MazeUtilities : MonoBehaviour {

	public IntVector2 size;

	public MazeDoor doorPrefab;

	protected MazeCell[,] cells;

	/*----------------------------------------------------------------------------------------*/
	// Gell Cell Method
	// Return the current Cell object at the coordinate given
	// A Cell object has edges and all that stuffs
	/*----------------------------------------------------------------------------------------*/
	public MazeCell GetCell(IntVector2 coordinates){
		return cells [coordinates.x, coordinates.z];
	}

	/*----------------------------------------------------------------------------------------*/
	/// <summary>
	/// Get a random coordinate within the size of the maze
	/// </summary>
	public IntVector2 RandomCoordinates {
		get {
			return new IntVector2 (Random.Range(0, size.x), Random.Range(0,size.z));
		}
	}

	/*----------------------------------------------------------------------------------------*/
	/// <summary>
	/// Check if the Coordinates given is within the size  of the Maze
	/// </summary>
	public bool ContainsCoordinates (IntVector2 coordinates){
		return coordinates.x >= 0 && coordinates.z >= 0 &&
			coordinates.x < size.x && coordinates.z < size.z;
	}

	/*----------------------------------------------------------------------------------------*/
	// Cell Index Flavor
	// Choose the index based on a Flavor as noted below
	/*----------------------------------------------------------------------------------------*/
	public int CellFlavor (int flavor, int activeCellsCount){
		switch (flavor){
		case -9: // IS29: Random cell
			return Random.Range(0, activeCellsCount);
		case 0: // Simple choosing the first cell
			return 0;  
		default: //Divide by the flavor type || 1 = last Cell, 2 = middle Cell
			return activeCellsCount/Mathf.Abs(flavor);
		}	
	}

	/*----------------------------------------------------------------------------------------*/
	public MazeCell cellPrefab;
	/// <summary>
	/// Return the Created Cell at desired coordinate
	/// </summary>
	protected MazeCell CreatedCell (IntVector2 coordinates){
		MazeCell newCell = Instantiate (cellPrefab) as MazeCell;
		cells[coordinates.x,coordinates.z] = newCell;
		
		newCell.coordinates = coordinates;
		newCell.name = string.Format("Cell [{0}-{1}]",coordinates.x,coordinates.z);
		newCell.transform.parent = transform;
		newCell.transform.localPosition = new Vector3( 
		                                              coordinates.x - size.x * 0.5f + 0.5f, 
		                                              0f, 
		                                              coordinates.z - size.z * 0.5f + 0.5f);
		return newCell;
	}

	/*----------------------------------------------------------------------------------------*/
	public MazePassage passagePrefab;
	/// <summary>
	/// Creates passage if in same room.
	/// </summary>
	protected void CreatePassageInSameRoom (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
		MazePassage passage = Instantiate(passagePrefab) as MazePassage;
		passage.Initialize(cell, otherCell, direction);
		passage = Instantiate(passagePrefab) as MazePassage;
		passage.Initialize(otherCell, cell, direction.GetOpposite());
		if (cell.room != otherCell.room) {
			MazeRoom roomToAssimilate = otherCell.room;
			cell.room.Assimilate(roomToAssimilate);
			rooms.Remove(roomToAssimilate);
			Destroy(roomToAssimilate);
		}
	}
	/// <summary>
	/// Creates passage.
	/// </summary>
	protected void CreatePassage(MazeCell cell, MazeCell otherCell, MazeDirection direction){
		MazePassage prefab = Random.value < MazeDoorProbability.value ? doorPrefab : passagePrefab;
		MazePassage passage = Instantiate (prefab) as MazePassage;
		passage.Initialize (cell, otherCell, direction);
		passage = Instantiate (prefab) as MazePassage;
		if (passage is MazeDoor){
			otherCell.Initialize(CreatedRoom(cell.room.settingIndex));
		}
		else{
			otherCell.Initialize(cell.room);
		}
		passage.Initialize (otherCell, cell, direction.GetOpposite());
		
	}
	/*----------------------------------------------------------------------------------------*/
	public MazeWall[] wallPrefabs;
	/// <summary>
	/// Creates a Wall
	/// </summary>
	protected void CreateWall (MazeCell cell, MazeCell otherCell, MazeDirection direction){
		MazeWall wall = Instantiate (wallPrefabs [Random.Range(0, wallPrefabs.Length)]) as MazeWall;
		
		wall.Initialize (cell, otherCell, direction);
		
		if (otherCell != null){
			wall = Instantiate (wallPrefabs [Random.Range(0, wallPrefabs.Length)]) as MazeWall;
			wall.Initialize (otherCell, cell, direction.GetOpposite());
		}
	}
	/*----------------------------------------------------------------------------------------*/
	// Maze Room Init
	public MazeRoomSettings[] roomSettings;
	protected List<MazeRoom> rooms = new List<MazeRoom>();
	
	/// <summary>
	/// Return Created Room.
	/// </summary>
	protected MazeRoom CreatedRoom (int indexToExclude){
		MazeRoom newRoom = ScriptableObject.CreateInstance<MazeRoom>();
		newRoom.settingIndex = Random.Range (0, roomSettings.Length);
		if (newRoom.settingIndex == indexToExclude){
			newRoom.settingIndex = (newRoom.settingIndex + 1) % roomSettings.Length;
		}
		newRoom.settings = roomSettings[newRoom.settingIndex];
		rooms.Add(newRoom);
		return newRoom;
	}
}
