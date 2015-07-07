using UnityEngine;
using System.Collections;

public class Maze_GameManager : MonoBehaviour
{

	public Maze mazePrefab; // Get Assigned in Editor
	private Maze mazeInstance; // Used throughout as An Instance

	public Maze_VirtualPlayerController playerPrefab;
	private Maze_VirtualPlayerController playerInstance;

	//For Desktop version
	//public Maze_PhysicalPlayerController playerPrefab;
	//private Maze_PhysicalPlayerController playerInstance;

	/*----------------------------------------------------------------------------------------*/
	//Core Methods
	//.... Start: 
	//.... Update:  
	/*----------------------------------------------------------------------------------------*/
	private void Start ()
	{
		//StartCoroutine(BeginGame());
		BeginGameDirect ();
	}
	
	private void Update ()
	{
		if (MazeReset.IsActivated || Input.GetKeyDown (KeyCode.Space)) {
			//print("Clicked Maze Reset");
			RestartGame ();
			MazeReset.Deactivate ();
		}
	}

	private void BeginGameDirect ()
	{
		Camera.main.clearFlags = CameraClearFlags.Skybox;
		Camera.main.rect = new Rect (0f, 0f, 1f, 1f);
		mazeInstance = Instantiate (mazePrefab) as Maze;
		mazeInstance.name = "Maze";
		mazeInstance.transform.parent = GameObject.Find ("MazeSystem").transform;
		
		mazeInstance.Generate ();
		
		playerInstance = Instantiate (playerPrefab) as Maze_VirtualPlayerController;
		
		//playerInstance = Instantiate(playerPrefab) as Maze_PhysicalPlayerController;
		
		playerInstance.name = "Player";
		
		StartCoroutine (playerInstance.SetLocation (mazeInstance.GetCell (mazeInstance.RandomCoordinates)));
		
		Camera.main.clearFlags = CameraClearFlags.Depth;
		Camera.main.rect = new Rect (0f, 0f, 0.5f, 0.5f);
	}

	/*----------------------------------------------------------------------------------------*/
	//Begin Game
	//....
	/*----------------------------------------------------------------------------------------*/

	private IEnumerator BeginGame ()
	{
		Camera.main.clearFlags = CameraClearFlags.Skybox;
		Camera.main.rect = new Rect (0f, 0f, 1f, 1f);
		mazeInstance = Instantiate (mazePrefab) as Maze;
		mazeInstance.name = "Maze";
		mazeInstance.transform.parent = GameObject.Find ("MazeSystem").transform;

		yield return StartCoroutine (mazeInstance.GenerateWithDelay ());

		playerInstance = Instantiate (playerPrefab) as Maze_VirtualPlayerController;

		//playerInstance = Instantiate(playerPrefab) as Maze_PhysicalPlayerController;

		playerInstance.name = "Player";
		
		yield return StartCoroutine (playerInstance.SetLocation (mazeInstance.GetCell (mazeInstance.RandomCoordinates)));
		
		Camera.main.clearFlags = CameraClearFlags.Depth;
		Camera.main.rect = new Rect (0f, 0f, 0.5f, 0.5f);
	}

	/*----------------------------------------------------------------------------------------*/
	//Restart Game
	//....
	/*----------------------------------------------------------------------------------------*/
	
	private void RestartGame ()
	{
		StopAllCoroutines (); // We can Restart even when the maze is being generated
		Destroy (mazeInstance.gameObject);
		if (playerInstance != null) {
			Destroy (playerInstance.gameObject);
		}
		if (MazeDirect.isEnabled) {
			BeginGameDirect ();
		} else {
			StartCoroutine (BeginGame ());
		}
	}
}
