using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace GameMenuKit
{
	public class GUIKeyPress : MonoBehaviour
	{
		public KeyCode KeyPress;
		public UnityEvent Pressed;

		void Start ()
		{
	
		}

		void Update(){
			if(Input.GetKeyDown(KeyPress)){
				if(Pressed!=null){
					Pressed.Invoke();
				}
			}
		}

	}
}
