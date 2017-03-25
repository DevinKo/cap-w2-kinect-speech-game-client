using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

namespace GameMenuKit
{
	public class GUICreditRenderer : MonoBehaviour
	{
		public float Speed = 30.0f;
		public RectTransform Credit;
		public bool IsPlaying = false;
		public UnityEvent AfterEnded;
		private Vector2 positionTemp;

		void Start(){

		}

		void Awake ()
		{
			if (Credit != null)
				positionTemp = Credit.anchoredPosition;

			IsPlaying = true;
		}
		public void Restart(){
			OnEnable ();
		}

		void OnEnable ()
		{
			if (Credit != null)
				Credit.anchoredPosition = positionTemp;

			IsPlaying = true;
		}

		void Update ()
		{
			if (Credit == null || !IsPlaying)
				return;

			if (Credit.anchoredPosition.y > 0) {
				IsPlaying = false;
				Ended ();
			} else {
				IsPlaying = true;
				Credit.anchoredPosition += Vector2.up * Speed * Time.deltaTime;
			}
		}

		void Ended ()
		{
			if (AfterEnded != null) {
				AfterEnded.Invoke ();
			}
		}
	}
}