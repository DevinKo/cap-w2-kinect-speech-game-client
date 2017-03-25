using UnityEngine;
using System.Collections;

namespace GameMenuKit
{
	[RequireComponent (typeof(Camera))]
	public class CameraMover : MonoBehaviour
	{
		private Vector3 positionTarget;
		private Quaternion roationTarget;
		private float fovTarget;
		public float Speed = 30;
		private Camera cam;


		public void SetCameraTransform(Camera target){
			if(target == null)
				return;

			positionTarget = target.transform.position;
			roationTarget = target.transform.rotation;
			fovTarget = target.fieldOfView;
		}


		void Start ()
		{
			cam = this.GetComponent<Camera>();
			positionTarget = this.transform.position;
			roationTarget = this.transform.rotation;
			fovTarget = cam.fieldOfView;
		}
	

		void LateUpdate ()
		{
			this.transform.position = Vector3.Lerp(this.transform.position,positionTarget,Speed * Time.deltaTime);
			this.transform.rotation = Quaternion.Slerp(this.transform.rotation,roationTarget,Speed * Time.deltaTime);
			cam.fieldOfView = Mathf.Lerp(cam.fieldOfView,fovTarget,Speed * Time.deltaTime);
		}
	}
}