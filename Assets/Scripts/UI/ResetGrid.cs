using UnityEngine;

public class ResetGrid : MonoBehaviour
{
	private Grid _grid;
	
	private void Start()
	{
		_grid = FindObjectOfType<Grid>().GetComponent<Grid>();
	}
	
	private void OnMouseUpAsButton () {
		_grid.Reset();
	}
}
