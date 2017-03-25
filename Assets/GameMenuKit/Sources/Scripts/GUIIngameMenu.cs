using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace GameMenuKit
{
	public class GUIIngameMenu : MonoBehaviour
	{
		void Start ()
		{
	
		}

		void Update ()
		{
	
		}

		public void LoadScene (string scenename)
		{
			SceneManager.LoadScene (scenename);
		}
	}
}