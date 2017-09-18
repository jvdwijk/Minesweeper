using UnityEngine;
using UnityEngine.UI;

	public class Square : MonoBehaviour
	{
		//the sprites used
		[SerializeField] private Sprite[] _numberSprites;
		[SerializeField] private Sprite _bombSprite;
		[SerializeField] private Sprite _flagSprite;	
		//Booleans used
		public bool isFlagged = false;
		public bool isChecked;
		public bool isBomb;
		//The main Grid.
		private Grid _grid;
	
		public int number; 	//x
		public int row;		//y
	
		private void Start()
		{
			_grid = FindObjectOfType<Grid>().GetComponent<Grid>();
		}
		
		private void OnMouseUpAsButton()
		{
			if(_grid.gameDone) return;
			if (Input.GetKey(KeyCode.LeftShift))
			{
				print(isFlagged);
				isFlagged = !isFlagged;
				ChangeSprite(isFlagged ? _flagSprite : _numberSprites[9]);
				return;
			}
			if(isFlagged) return;
			if (isBomb)
			{
				EndGame();
				return;
			}
			
			ChangeSprite(_numberSprites[_grid.CheckArea(number, row)]);
			_grid.FloodFilling(number, row);
		}
	
		private void ChangeSprite(Sprite newSprite)
		{
			var spriteRenderer = GetComponent<SpriteRenderer>();
			
			spriteRenderer.sprite = newSprite;
		}
	
		public void ChooseSprite(int spriteNumber)
		{
			ChangeSprite(isBomb ? _bombSprite : _numberSprites[spriteNumber]);
		}

		private void EndGame()
		{
			_grid.gameDone = true;
			_grid.FindBombs(true);
		}
	}