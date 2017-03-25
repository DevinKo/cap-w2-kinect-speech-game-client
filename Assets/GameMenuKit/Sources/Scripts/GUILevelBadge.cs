using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace GameMenuKit
{
	public class GUILevelBadge : MonoBehaviour
	{
		public string ID;
		public Text GUILabel;
		public Button GUIButton;
		public GUIRating Rating;
		public GameObject LockedIcon;
		[HideInInspector]
		public SaveLevel Level;


		void Start ()
		{
			

		}

		public void Select ()
		{
			if(GMK.GameMenuKitManager)
				GMK.GameMenuKitManager.LoadScene(Level.SceneName);
			if(GMK.LevelManager)
				GMK.LevelManager.StartThisLevel(Level);
		}

		void LateUpdate ()
		{
			if (Level == null)
				return;

			if (GUILabel != null) {
				GUILabel.text = ID;
			}
			if (Rating != null) {
				int.TryParse (Level.Optional, out Rating.Rate);
			}
			if (LockedIcon != null) {
				LockedIcon.SetActive (Level.IsLocked);
			}
			if (GUIButton != null) {
				GUIButton.interactable = !Level.IsLocked;
			}
		}
	}
}