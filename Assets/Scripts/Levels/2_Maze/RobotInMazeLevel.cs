using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RobotInMazeLevel : MonoBehaviour
{
	enum Move
	{
		Up,
		Down,
		Left,
		Right
	}

	internal readonly float MoveSpeed = 13f;
	internal readonly float RotationSpeed = 200f;

	internal int Row { get; private set; }
	internal int Col { get; private set; }

	private Animator animator;
	private LevelLoader mazeLoader;

	private readonly Queue<Move> moves = new();
	private RobotTarget currentTarget;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		mazeLoader = FindFirstObjectByType<LevelLoader>();
		animator.speed = 1.3f;
	}

	private void Update()
	{
		if (Col == mazeLoader.Maze.Exit.x && Row == mazeLoader.Maze.Exit.y)
		{
			Destroy(gameObject); 
			Success();
			return;
		}

		if (moves.Count == 0 && currentTarget == null)
			return;

		if (currentTarget == null)
		{
			var move = moves.Dequeue();
			currentTarget = new RobotTarget(this, NextPosition(move), mazeLoader);
			ValidateMove(currentTarget, move);
			currentTarget.Calculate();
		}

		if (animator.GetBool("Open_Anim") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.8f)
			return;

		animator.SetBool("Walk_Anim", true);

		if (!currentTarget.RotateRobot())
			return;

		if (!currentTarget.MoveRobot())
			return;

		Col = currentTarget.Col();
		Row = currentTarget.Row();
		currentTarget = null;
	}

	private void Success()
	{
		int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
		if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
			SceneManager.LoadScene(nextSceneIndex);
	}

	private Vector2Int NextPosition(Move move)
	{
		return move switch
		{
			Move.Up => new Vector2Int(Col, Row + 1),
			Move.Down => new Vector2Int(Col, Row - 1),
			Move.Left => new Vector2Int(Col - 1, Row),
			_ => new Vector2Int(Col + 1, Row),
		};
	}
	private void ValidateMove(RobotTarget robotTarget, Move move)
	{
		if (robotTarget.Col() < 0 || robotTarget.Col() >= mazeLoader.Maze.W || robotTarget.Row() < 0 || robotTarget.Row() >= mazeLoader.Maze.H || mazeLoader.Maze.Tiles[robotTarget.Row(), robotTarget.Col()] == "Wall")
			throw new RuntimeException($"Invalid move {move}");
	}

	public object C(object[] _)
	{
		return Col;
	}

	public object R(object[] _)
	{
		return Row;
	}

	public object MoveUp(object[] _)
	{
		moves.Enqueue(Move.Up);
		return null;
	}

	public object MoveDown(object[] _)
	{
		moves.Enqueue(Move.Down);
		return null;
	}

	public object MoveLeft(object[] _)
	{
		moves.Enqueue(Move.Left);
		return null;
	}

	public object MoveRight(object[] _)
	{
		moves.Enqueue(Move.Right);
		return null;
	}

	internal void SetPosition(int r, int c)
	{
		Row = r;
		Col = c;
	}
}
