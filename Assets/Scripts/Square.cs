using UnityEngine;

public class Square : MonoBehaviour
{
	[SerializeField] 
	private Sprite[] _numberSprites;
	[SerializeField] 
	private Sprite _bombSprite;
	[SerializeField]
	private Sprite _flagSprite;
	private bool _isFlagged;
	public bool isBomb;

	private void OnMouseUpAsButton()
	{
		if (Input.GetKey(KeyCode.LeftShift))
		{
			ChangeSprite(_flagSprite);
		}
		if (isBomb)
		{
			print("gameover");
		}
	}

	private void ChangeSprite(Sprite newSprite)
	{
		var spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = newSprite;
	}

	private void FloodFilling()
	{
		
	}
}
