using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
	[SerializeField] private Text _timerUi;
	private float _seconds = 0;
	private float _minutes;
	
	private void Start ()
	{
		_timerUi.text = "0:00";
		InvokeRepeating("TimerUpdate", 1, 1);
	}

	private void TimerUpdate()
	{
		_seconds++;
		if (_seconds > 60)
		{
			_minutes++;
			_seconds = 0;
		}
		_timerUi.text = _seconds < 10 ? _minutes + ":0" + _seconds : _minutes + ":" + _seconds;
	}
}