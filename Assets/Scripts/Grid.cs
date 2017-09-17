using UnityEngine;

public class Grid : MonoBehaviour
{
	[SerializeField] private Square _squarePrefab;
	public Square[,] squareArray;
	private int _width;
	private int _height;
	public bool gameDone;
	
	private void Start()
	{
		GenerateGrid(16,30,99);
	}
	
	private void GenerateGrid (int yCount, int xCount, int bombCount) 
	{
		squareArray = new Square[xCount,yCount];
		_width = xCount;
		_height = yCount;
		
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
		PlaceBombs(bombCount,xCount,yCount);
	}

	private void PlaceBombs(int bombCount,int numberAmount,int rowAmount)
	{
		var bombAmount = 0;
		var currentRow = -1;
		var currentNumber = -1;
		//while there are less then the given amount of bombs
		while (bombAmount < bombCount)
		{
			//move to next tile.
			currentNumber++;
			currentRow++;	
			if (currentNumber > numberAmount - 1)
			{
				currentNumber = 0;
				currentRow++;
			}
			if (currentRow > rowAmount -1)
				currentRow = 0;
			//Decide to make a bomb, if true make it a bomb.
			if (Random.Range(0, 100) >= 20 || squareArray[currentNumber,currentRow].isBomb) continue;
			squareArray[currentNumber,currentRow].isBomb = true;
			bombAmount++;
		}
	}
	
	public void FloodFilling(int x, int y)
	{
		if (x < 0 || x > _width || y < 0 || y > _height || squareArray[x,y].isChecked)
			return;
		
		squareArray[x, y].ChooseSprite(CheckArea(x,y));

		if (CheckArea(x, y) > 0)
			return;
		
		squareArray[x, y].isChecked = true;
		for (var i = -1; i < 2; i++)
		{
			for (var j = -1; j < 2; j++)
			{
				if (i == 0 && j == 0) return;
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
				if (i == 0 && j == 0) continue;
				if (CheckTile(x + i, y + j)) ++bombCount;
			}
		}

		return bombCount;
	}
	
	private bool CheckTile(int x, int y)
	{
		if (x < 0 || x > _width || y < 0 || y > _height) return false;
		return squareArray[x, y].isBomb;
	}
	
	public void RevealBombs() {
		foreach (var tile in squareArray)
			if (tile.isBomb) tile.ChooseSprite(0);
	}
}