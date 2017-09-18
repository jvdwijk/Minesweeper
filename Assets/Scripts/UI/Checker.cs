using UnityEngine;

namespace UI
{
    public class Checker : MonoBehaviour
    {
        private Grid _grid;
	
        private void Start()
        {
            _grid = FindObjectOfType<Grid>().GetComponent<Grid>();
        }
	
        private void OnMouseUpAsButton () {
            _grid.CheckFlags();
        }
    }
}