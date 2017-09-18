using UnityEngine;

public class Grid : MonoBehaviour
{
	[SerializeField] private Square _squarePrefab;
	public Square[,] squareArray;
	private int _width;
	private int _height;
	private int _initialBombCount;
	public bool gameDone;
	
	private void Start()
	{
		GenerateGrid(16,30,99);
	}
	
	public void GenerateGrid (int yCount, int xCount, int bombCount) 
	{
		squareArray = new Square[xCount,yCount];
		_width = xCount;
		_height = yCount;
		_initialBombCount = bombCount;
		
		for (float x = 0; x < xCount; x++)
		{
			for (float y = 0; y < yCount; y++)
			{
				//Create the tile.
				var tile = Instantiate(_squarePrefab, new Vector2(x, y), transform.rotation);
				tile.transform.position = new Vector3(x * 0.7f, y * 0.7f, 0);
				//put square in the list
				tile.number = (int) x;
				tile.row = (int) y;
				squareArray[(int)x, (int)y] = tile;
			}
		}
		PlaceBombs();
	}

	private void PlaceBombs()
	{
		var bombAmount = 0;
		var currentRow = -1;
		var currentNumber = -1;
		//while there are less then the given amount of bombs
		while (bombAmount < _initialBombCount)
		{
			//move to next tile.
			currentNumber++;
			currentRow++;	
			if (currentNumber > _width - 1)
			{
				currentNumber = 0;
				currentRow++;
			}
			if (currentRow > _height -1)
				currentRow = 0;
			//Decide to make a bomb, if true make it a bomb.
			if (Random.Range(0, 100) >= 20 || squareArray[currentNumber,currentRow].isBomb) continue;
			squareArray[currentNumber,currentRow].isBomb = true;
			bombAmount++;
		}
	}
	
	public void FloodFilling(int x, int y)
	{
		if (x < 0 || x >= _width || y < 0 || y >= _height || squareArray[x,y].isChecked)
			return;
		
		squareArray[x, y].ChooseSprite(CheckArea(x,y));

		if (CheckArea(x, y) > 0)
			return;
		
		squareArray[x, y].isChecked = true;
		for (var i = -1; i < 2; i++)
		{
			for (var j = -1; j < 2; j++)
			{
				if (i == 0 && j == 0) continue;
				FloodFilling(x + i, y + j);
			}
		}
	}
	
	public int CheckArea(int x, int y) {
		var bombCount = 0;

		for (var i = -1; i < 2; i++)
		{
			for (var j = -1; j < 2; j++)
			{
				if (!CheckTile(x + i, y + j)) continue;
				bombCount++;
				if (i == 0 && j == 0) return 9;
			}
		}
		return bombCount;
	}
	
	private bool CheckTile(int x, int y)
	{
		if (x < 0 || x >= _width || y < 0 || y >= _height || squareArray[x, y].isChecked) return false;
		return squareArray[x, y].isBomb;
	}
	
	public int FindBombs(bool reveal)
	{
		var bombCount = 0;
		foreach (var tile in squareArray)
		{
			if (!tile.isBomb) continue;
			bombCount++;
			if(reveal) tile.ChooseSprite(0);
		}
		return bombCount;
	}

	public void Reset()
	{
		gameDone = false;
		foreach (var tile in squareArray)
		{
			tile.isBomb = false;
			tile.ChooseSprite(9);
		}
		PlaceBombs();
	}

	public void CheckFlags()
	{
		var bombCount = 0;
		foreach (var tile in squareArray)
		{
			if(!tile.isFlagged) continue;
			if (tile.isBomb) bombCount++;
			else
			{
				gameDone = true;
				FindBombs(true);
			}
		}
		if (bombCount >= _initialBombCount) return;
		gameDone = true;
		FindBombs(true);
	}
}