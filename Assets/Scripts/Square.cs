	using UnityEngine;
	
	public class Square : MonoBehaviour
	{
		//the sprites used
		[SerializeField] private Sprite[] _numberSprites;
		[SerializeField] private Sprite _bombSprite;
		[SerializeField] private Sprite _flagSprite;
		[SerializeField] private Sprite _basicSprite;
		//Booleans used
		private bool _isFlagged;
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
				print(_isFlagged);
				_isFlagged = _isFlagged ? false : true;
				ChangeSprite(_isFlagged ? _basicSprite : _flagSprite);
				return;
			}
			if(_isFlagged) return;
			if (isBomb)
			{
				_grid.gameDone = true;
				_grid.RevealBombs();
				ChangeSprite(_bombSprite);
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
	}
