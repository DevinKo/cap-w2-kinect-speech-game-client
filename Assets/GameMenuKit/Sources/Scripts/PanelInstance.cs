using UnityEngine;
using System.Collections;

namespace GameMenuKit
{
	public class PanelInstance : MonoBehaviour
	{
		public Animator[] animators;
		[HideInInspector]
		public PanelInstance PanelBefore;
		public CameraSetup CameraPoint;

		void Start ()
		{
			
		}

		void OnEnable(){
			if(CameraPoint){
				CameraPoint.SetEnable();
			}
		}

		public void SetAnimation (string parameter, bool value)
		{
			for (int i = 0; i < animators.Length; i++) {
				if (animators [i] != null && animators [i].isActiveAndEnabled) {
					animators [i].SetBool (parameter, value);
				}
			}

		}

		public void SetOpen ()
		{
			this.gameObject.SetActive (true);
			SetAnimation ("Open", true);
		}

		public void SetClose ()
		{
			this.gameObject.SetActive (false);
		}

		public bool IsClosed ()
		{

			for (int i = 0; i < animators.Length; i++) {
				if (animators [i] !=null && animators [i].gameObject.activeSelf && animators [i].enabled) {
					
					if (animators [i].IsInTransition (0) || !animators [i].GetCurrentAnimatorStateInfo (0).IsName ("Closing")
					    || (animators [i].GetCurrentAnimatorStateInfo (0).IsName ("Closing") && animators [i].GetCurrentAnimatorStateInfo (0).normalizedTime < 0.9f)) {
						return false;
					}

				}
			}
			return true;
		}
	}
}