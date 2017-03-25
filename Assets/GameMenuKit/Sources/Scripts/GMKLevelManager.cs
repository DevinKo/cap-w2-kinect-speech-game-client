using UnityEngine;
using System.Collections;

namespace GameMenuKit
{
	public class GMKLevelManager : MonoBehaviour
	{
		public SaveLevel CurrentLevel;

		void Start ()
		{
			GMK.LevelManager = this;
			DontDestroyOnLoad (this.gameObject);
		}

		public void ReloadLevels (GUILevelManager guiLevelManager)
		{
			if (guiLevelManager.Levels != null) {
				LoadData (guiLevelManager.Key, ref guiLevelManager.Levels);
			}
		}


		public void CompleteLevel (int score, string optional)
		{
			CurrentLevel.IsPassed = true;
			CurrentLevel.Optional = optional;
			CurrentLevel.Score = score;
			WriteLevel (CurrentLevel.Key, CurrentLevel);
		}


		public void StartThisLevel (SaveLevel level)
		{
			CurrentLevel = level;
		}


		public void SaveData (string Key, ref SaveLevel[] Levels)
		{
			for (int i = 0; i < Levels.Length; i++) {
				WriteLevel (Key, Levels [i]);
			}
		}


		public void LoadData (string Key, ref SaveLevel[] Levels)
		{
			for (int i = 0; i < Levels.Length; i++) {
				ReadLevel (Key, ref Levels [i]);
			}
		}

		public void WriteLevel (string Key, SaveLevel Level)
		{

			PlayerPrefs.SetInt (Key + "_Score_" + Level.ID, Level.Score);
			PlayerPrefs.SetString (Key + "_Optional_" + Level.ID, Level.Optional);
			PlayerPrefs.SetInt (Key + "_IsPassed_" + Level.ID, BoolToInt (Level.IsPassed));
		}

		public void ReadLevel (string Key, ref SaveLevel Level)
		{
			Level.Score = PlayerPrefs.GetInt (Key + "_Score_" + Level.ID);
			Level.Optional = PlayerPrefs.GetString (Key + "_Optional_" + Level.ID);
			Level.IsPassed = IntToBool (PlayerPrefs.GetInt (Key + "_IsPassed_" + Level.ID)); 
		}

		public void ClearSave (string Key, SaveLevel[] Levels)
		{
			for (int i = 0; i < Levels.Length; i++) {
				PlayerPrefs.DeleteKey (Key + "_Score_" + Levels [i].ID);
				PlayerPrefs.DeleteKey (Key + "_Optional_" + Levels [i].ID);
				PlayerPrefs.DeleteKey (Key + "_IsPassed_" + Levels [i].ID);
			}
		}

		public void ClearAllSave ()
		{
			PlayerPrefs.DeleteAll ();
		}

		public bool IntToBool (int num)
		{
			if (num == 1) {
				return true;
			}
			return false;
		}

		public int BoolToInt (bool val)
		{
			if (val) {
				return 1;
			}
			return 0;
		}
	}


	[System.Serializable]
	public class SaveLevel
	{
		public string ID;
		public string SceneName;
		public int Score;
		public string Optional;
		public bool IsPassed;
		public bool IsLocked;

		[HideInInspector]
		public string Key;
	}

}