using UnityEngine;
using System.Collections;

namespace GameMenuKit
{
	public class GUIRating : MonoBehaviour
	{
		public GameObject[] Stars;
		public int Rate;

		void Start ()
		{
	
		}

		void LateUpdate ()
		{
			for (int i = 0; i < Stars.Length; i++) {
				Stars [i].SetActive (false);
			}

			for (int i = 0; i < Stars.Length; i++) {
				if (Rate > i) {
					Stars [i].SetActive (true);
				}
			}
		}
	}
}
