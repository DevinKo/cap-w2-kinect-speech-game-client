using UnityEngine;
using System.Collections;

namespace GameMenuKit
{
	public class GUILevelManager : MonoBehaviour
	{
		public string Key;
		public bool Available = true;
		public bool PassedToUnlock = true;
		public GUILevelManager NeedCompleted;
		public SaveLevel[] Levels;
		private GUILevelBadge[] levelBadges;

		void Awake ()
		{
			
			for (int i = 0; i < Levels.Length; i++) {
				Levels [i].Key = Key;
			}

		}

		void OnEnable ()
		{
			if (GMK.LevelManager)
				GMK.LevelManager.ReloadLevels (this);
		}

		void Start ()
		{
			levelBadges = (GUILevelBadge[])FindObjectsOfType (typeof(GUILevelBadge));

			if (levelBadges.Length == Levels.Length) {
				for (int i = 0; i < Levels.Length; i++) {
					if (Levels [i] != null) {
						for (int k = 0; k < levelBadges.Length; k++) {
							if (Levels [i].ID == levelBadges [k].ID) {
								levelBadges [k].Level = Levels [i];
							}
						}
					}
				}
			}
			if (GMK.LevelManager)
				GMK.LevelManager.ReloadLevels (this);

		}

		public void UpdateLevel (ref SaveLevel[] levels)
		{
			if (NeedCompleted != null) {
				if (NeedCompleted.IsAllLevelsCompleted ()) {
					Available = true;
				}
			}
			for (int i = 0; i < levels.Length; i++) {
				if (levels [i] != null) {
					levels [i].IsLocked = PassedToUnlock;
				}
			}


			if (levels.Length > 0 && Available) {
				levels [0].IsLocked = false;
			}

			if (PassedToUnlock) {
				for (int i = 0; i < levels.Length; i++) {
					if (levels [i] != null) {
						if (levels [i].IsPassed) {
							levels [i].IsLocked = false;
							if (i + 1 < levels.Length) {
								levels [i + 1].IsLocked = false;
							}
						}
					}
				}
			}
		}

		public void LateUpdate ()
		{
			if (this.gameObject.activeSelf) {
				UpdateLevel (ref Levels);
			}
		}

		public void SaveLevel ()
		{
			if (GMK.LevelManager == null)
				return;

			GMK.LevelManager.SaveData (Key, ref Levels);
			
		}

		public void LoadLevel ()
		{
			if (GMK.LevelManager == null)
				return;

			GMK.LevelManager.LoadData (Key, ref Levels);
			
		}

		public bool IsAllLevelsCompleted ()
		{
			for (int i = 0; i < Levels.Length; i++) {
				if (!Levels [i].IsPassed) {
					return false;
				}
			}
			return true;
		}
	}
}