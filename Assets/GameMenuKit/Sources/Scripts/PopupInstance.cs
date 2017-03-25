using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace GameMenuKit
{
	public class PopupInstance : PanelInstance
	{
		public UnityEvent OnAccept;
		public UnityEvent OnCancel;

		void Start ()
		{
	
		}

		public void Accept ()
		{
			if (OnAccept != null) {
				OnAccept.Invoke ();
			}
		}

		public void Cancel ()
		{
			if (OnCancel != null) {
				OnCancel.Invoke ();
			}
		}

	}
}
