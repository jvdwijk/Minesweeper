using System.Collections.Generic;
using System.Xml.Xsl;
using UnityEngine;

public class Square : MonoBehaviour
{
	public float row;
	public float number;
	public bool isBomb;

	public void OnClick()
	{
		if (isBomb)
		{
			print("game over.");
		}
	}
}
