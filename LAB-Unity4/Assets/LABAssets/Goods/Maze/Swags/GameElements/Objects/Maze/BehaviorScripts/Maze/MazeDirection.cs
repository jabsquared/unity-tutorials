using UnityEngine;
using System.Collections;

public enum MazeDirection {
	North, East, South, West
}

public static class MazeDirections {

	public const int Count = 4;

	private static IntVector2[] vectors = {
		new IntVector2 (0, 1),
		new IntVector2 (1, 0),
		new IntVector2 (0,-1),
		new IntVector2 (-1, 0)
	};
	
	public static IntVector2 ToIntVector2 (this MazeDirection direction){
		return vectors [(int) direction];
	}


	public static MazeDirection RandomValue {
		get {
			return (MazeDirection)Random.Range(0,Count);
		}
	}

	public static MazeDirection GetNextClockwise (this MazeDirection direction) {
		return (MazeDirection)(((int)direction + 1) % Count);
	}
	
	public static MazeDirection GetNextCounterclockwise (this MazeDirection direction) {
		return (MazeDirection)(((int)direction + Count - 1) % Count);
	}

	/// <summary>
	/// The rotations assigned for each directions.
	/// </summary>
	public static Quaternion[] rotations = {
		Quaternion.identity,
		Quaternion.Euler (0f, 90f, 0f),
		Quaternion.Euler (0f, 180f, 0f),
		Quaternion.Euler (0f, 270f, 0f)
	};

	public static Quaternion ToRotation (this MazeDirection direction){
		return rotations [(int)direction];
	}


	/// <summary>
	/// The opposites Directions.
	/// </summary>
	private static MazeDirection[] opposites = {
		MazeDirection.South,
		MazeDirection.West,
		MazeDirection.North,
		MazeDirection.East
	};
	
	public static MazeDirection GetOpposite (this MazeDirection direction){
		return opposites[(int) direction];
	}


}