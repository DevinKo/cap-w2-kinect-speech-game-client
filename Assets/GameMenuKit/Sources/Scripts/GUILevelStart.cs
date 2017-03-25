using UnityEngine;
using System.Collections;

namespace GameMenuKit
{
	public class GUILevelStart : MonoBehaviour
	{


		void Start ()
		{
			Setup();
		}

		void Setup(){
			PanelsManager panelManager = this.GetComponent<PanelsManager> ();
			if (panelManager == null)
				return;

			for (int i = 0; i < panelManager.Pages.Length; i++) {
				GUILevelManager level = panelManager.Pages [i].GetComponent<GUILevelManager> ();
				if (level) {
					
					if (level.IsAllLevelsCompleted ()) {

						if (i + 1 < panelManager.Pages.Length) {
							panelManager.OpenPanelByName (panelManager.Pages [i + 1].name);
						}
					}
				}
			}
		}

		void OnEnable(){
			Setup();
		}
		
		void Update ()
		{
	
		}
	}
}