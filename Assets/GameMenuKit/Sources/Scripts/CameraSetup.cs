using UnityEngine;
using System.Collections;

namespace GameMenuKit
{
	[RequireComponent (typeof(Camera))]
	public class CameraSetup : MonoBehaviour
	{
		private CameraMover moveCamera;
		private Camera cam;
		void Start ()
		{
			cam = this.GetComponent<Camera>();
			if(cam != null){
				cam.enabled = false;
			}
		}

		public void SetEnable(){
			OnEnable();
		}

		void OnEnable(){
			moveCamera = (CameraMover)GameObject.FindObjectOfType(typeof(CameraMover));
			cam = this.GetComponent<Camera>();

			if(moveCamera!=null && cam != null){
				moveCamera.SetCameraTransform(cam);
			}

		}
	}
}