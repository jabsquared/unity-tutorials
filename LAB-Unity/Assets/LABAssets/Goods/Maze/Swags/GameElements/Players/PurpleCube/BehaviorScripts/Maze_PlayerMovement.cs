using UnityEngine;
using System.Collections;

public class Maze_PlayerMovement : MonoBehaviour
{

	private MazeCell currentCell;
	private MazeDirection currentDirection;

	private bool isMoveAxisInUse;
	private bool isRotateAxisInUse;

	protected bool Move (float h, float v)
	{
		if ((h != 0 || v != 0) && (!this.isMoveAxisInUse)) {
			if (Mathf.Abs (h) < 0.999f) {
				if (v > 0.999f) {
					Navigate (currentDirection);
				} else if (v < -0.999f) {
					Navigate (currentDirection.GetOpposite ());
				}
			} else if (Mathf.Abs (v) < 0.999f) {
				if (h > 0.999f) {
					Navigate (currentDirection.GetNextClockwise ());
				} else if (h < -0.999f) {
					Navigate (currentDirection.GetNextCounterclockwise ());
				}
			}
			return this.isMoveAxisInUse;
		} 
		/*if (this.isMoveAxisInUse) StartCoroutine (LAB_Transform.CoolDown());
			return this.isMoveAxisInUse;
		} else {
			return (this.isMoveAxisInUse = false);
		}*/
		 else if (h == 0f && v == 0f) {
			return (this.isMoveAxisInUse = false);
		} else {
			return false;
		}
	}

	private void Navigate (MazeDirection direction)
	{

		this.isMoveAxisInUse = true;

		//print(direction.ToString());

		MazeCellEdge edge = currentCell.GetEdge (direction);
		
		if (edge is MazePassage) { // MovementTakePlace
			StartCoroutine (SetLocation (edge.otherCel));	
		}
	}

	protected bool Rotate (float h)
	{
		if (h != 0 && (!this.isRotateAxisInUse)) {
			if (h > 0.63f) {
				Face (currentDirection.GetNextClockwise ());
			} else if (h < -0.63f) {
				Face (currentDirection.GetNextCounterclockwise ());
			} 
			//if (this.isRotateAxisInUse) StartCoroutine (LAB_Transform.CoolDown());
			return this.isRotateAxisInUse;
		} /*else
			return (this.isRotateAxisInUse = false);*/
		else if (h == 0) {
			return (this.isRotateAxisInUse = false);
		} else { 
			return false;
		}
	}

	[Range(0,4.5f)]
	public float
		rotateSpeed = 0.9f;

	private void Face (MazeDirection direction)
	{
		isRotateAxisInUse = true;

		currentDirection = direction;

		StartCoroutine (LAB_Transform.RotateObjectLocal (transform, direction.ToRotation (), rotateSpeed));
	}

	[Range(0,4.5f)]
	public float
		moveSpeed = 1.8f;

	public IEnumerator SetLocation (MazeCell cell)
	{
		yield return StartCoroutine (LAB_Transform.MoveObjectLocal (transform, cell.transform.localPosition, moveSpeed));
		//cell.OnPlayerEntered ();
		if (currentCell != null) {
			currentCell.OnPlayerExited ();
		}
		currentCell = cell;
		currentCell.OnPlayerEntered ();
	}
}
