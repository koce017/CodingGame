using UnityEngine;

public class RobotTarget
{
	private bool moved;
	private bool rotated;

	private Vector2Int tile;
	private Vector3 position;
	private Quaternion rotation;

	private readonly RobotInMazeLevel robot;
	private readonly LevelLoader mazeLoader;

	internal RobotTarget(RobotInMazeLevel robot, Vector2Int tile, LevelLoader levelLoader)
	{
		this.tile = tile;
		this.robot = robot;
		this.mazeLoader = levelLoader;
	}

	internal int Col()
	{
		return tile.x;
	}

	internal int Row()
	{
		return tile.y;
	}

	internal void Calculate()
	{
		position = mazeLoader.GetTilePosition(tile.y, tile.x, robot.transform.position.y);
		rotation = Quaternion.LookRotation(position - robot.transform.position);
	}

	internal bool RotateRobot()
	{
		if (rotated) return true;
		if (Quaternion.Angle(robot.transform.rotation, rotation) < 0.001f) rotated = true;
		robot.transform.rotation = Quaternion.RotateTowards(robot.transform.rotation, rotation, robot.RotationSpeed * Time.deltaTime);
		return rotated;
	}

	internal bool MoveRobot()
	{
		if (moved) return true;
		if (Vector3.Distance(robot.transform.position, position) < 0.001f) moved = true;
		robot.transform.position = Vector3.MoveTowards(robot.transform.position, position, robot.MoveSpeed * Time.deltaTime);
		return moved;
	}
}
