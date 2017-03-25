using UnityEngine;
using System.Collections;

namespace GameMenuKit
{
	public class CameraAimer : MonoBehaviour
	{

		public float Distance = 100;
		public Camera CameraControl;
		void Start ()
		{
			if(CameraControl == null && this.GetComponent<Camera>())
				CameraControl = this.GetComponent<Camera>();
		}

		void LateUpdate ()
		{
			if(CameraControl == null)
			return;

			RaycastHit hit;
			Ray ray = CameraControl.ScreenPointToRay (Input.mousePosition);

			this.transform.LookAt (ray.origin + Vector3.forward * Distance);
			
		}
	}
}