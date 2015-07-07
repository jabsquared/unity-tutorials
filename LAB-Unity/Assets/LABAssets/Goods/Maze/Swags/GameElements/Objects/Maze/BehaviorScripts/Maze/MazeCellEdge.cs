using UnityEngine;
using System.Collections;

public abstract class MazeCellEdge : MonoBehaviour {

	[HideInInspector]
	public MazeCell cell, otherCel;

	[HideInInspector]
	public MazeDirection direction;

	/// <summary>
	/// Initialize the specified cell, otherCell and direction.
	/// </summary>
	/// <param name="cell">Cell.</param>
	/// <param name="otherCell">Other cell.</param>
	/// <param name="direction">Direction.</param>

	public virtual void Initialize (MazeCell cell, MazeCell otherCell, MazeDirection direction){
		this.cell = cell;
		this.otherCel = otherCell;
		this.direction = direction;

		cell.SetEdge(direction, this);
		transform.parent = cell.transform;
		transform.localPosition = Vector3.zero;
		transform.localRotation = direction.ToRotation();
	}

	public virtual void OnPlayerEntered() { }

	public virtual void OnPlayerExited() { }

}
