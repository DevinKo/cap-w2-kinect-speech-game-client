using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace GameMenuKit
{
	public class GUICasualGame : MonoBehaviour
	{
		public int Score = 0;
		public string Optional = "3";

		public Text HighScore;
		public Text HighRate;

		void Start ()
		{
			Score = Random.Range (0, 10000);
			Optional = Random.Range (1, 4).ToString ();
		}

		void Update ()
		{
			if (GMK.LevelManager == null)
				return;

			// you can get currentlevel info via GMK.LevelManager.CurrentLevel
			if (HighScore != null) {
				HighScore.text = GMK.LevelManager.CurrentLevel.Score.ToString ();
			}
			if (HighRate != null) {
				HighRate.text = GMK.LevelManager.CurrentLevel.Optional.ToString ();
			}
		}

		// this is just a sample to show you how it complete and collect score and rating.
		// so you can custom this function as any you want
		public void LevelComplete ()
		{
			// update score and option info to the level
			GMK.LevelManager.CompleteLevel (Score, Optional);
			GMK.GameMenuKitManager.LoadScene ("mainmenu_casual",new string[]{"LevelSelect"});

		}

	}
}
