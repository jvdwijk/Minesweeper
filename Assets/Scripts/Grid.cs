using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
	[SerializeField] 
	private Square _squarePrefab;
	private List<Square> _squarelist = new List<Square>();
	
	private void Start()
	{
		//will probably put this somewhere else.
		GenerateGrid(16,30,99);
	}
	
	private void GenerateGrid (int yCount, int xCount, int bombCount) 
	{
		for (float x = 0; x < xCount; x++)
		{
			for (float y = 0; y < yCount; y++)
			{
				//Create the tile.
				var tile = Instantiate(_squarePrefab, new Vector2(x,y), transform.rotation);
				tile.row = y;
				tile.number = x;
				tile.transform.position = new Vector3(x*0.7f,y*0.7f, 0);
				//put square in the list
				_squarelist[(int)(y * x) + (int) x] = tile;
			}
		}
		PlaceBombs(bombCount);
	}

	private void PlaceBombs(int bombCount)
	{
		var bombAmount = 0;
		var tileNumber = -1;
		while (bombAmount < bombCount)
		{
			tileNumber++;
			if (tileNumber > _squarelist.Count)
				tileNumber = 0;
			//Decide to make a bomb, if true make it a bomb.
			if (Random.Range(0, 100) >= 20) continue;
			_squarelist[tileNumber].isBomb = true;
			bombAmount++;
		}
		print(bombAmount);
	}
}
