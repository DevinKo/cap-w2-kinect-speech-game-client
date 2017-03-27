using UnityEngine;
using System.Collections;

public class ExampleEvents : MonoBehaviour {

	void OnEnable()
	{
		GameTimer.TimeIsUp += TestFunction;
	}
	
	
	void OnDisable()
	{
		GameTimer.TimeIsUp -= TestFunction;
	}
	
	void TestFunction()
	{
		
		Debug.Log ("Time is up!");
		
	}
	
}
