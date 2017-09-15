using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
	[SerializeField] 
	private Square _squarePrefab;
	public Square[,] squareArray;
	
	private void Start()
	{
		//will probably put this somewhere else.
		GenerateGrid(16,30,99);
	}
	
	private void GenerateGrid (int yCount, int xCount, int bombCount) 
	{
		squareArray = new Square[xCount,yCount];
		
		for (float x = 0; x < xCount; x++)
		{
			for (float y = 0; y < yCount; y++)
			{
				//Create the tile.
				var tile = Instantiate(_squarePrefab, new Vector2(x, y), transform.rotation);
				//tile.row = y;
				//tile.number = x;
				tile.transform.position = new Vector3(x * 0.7f, y * 0.7f, 0);
				//put square in the list
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
}
