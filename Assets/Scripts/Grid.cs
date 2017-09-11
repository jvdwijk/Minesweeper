using UnityEngine;

public class Grid : MonoBehaviour
{
	[SerializeField] 
	private Square _squareObject;

	private void Start()
	{
		GenerateGrid(9,9);
	}
	
	public void GenerateGrid (int xCount, int yCount) 
	{
		for (var x = 0; x < xCount; x++)
		{
			for (var y = 0; y < yCount; y++)
			{
				var nani = Instantiate(_squareObject, new Vector2(x,y), transform.rotation);
				nani.row = y;
				nani.number = x;
			}
		}
	}
}
