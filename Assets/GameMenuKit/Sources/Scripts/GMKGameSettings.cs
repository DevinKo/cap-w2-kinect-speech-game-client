using UnityEngine;
using System.Collections;

namespace GameMenuKit
{
	public static class GMKGameSettings
	{
		public static float SoundsVolume = 1;
		public static float MusicsVolume = 1;
		public static float SFXVolume = 1;
		public static bool FullScreen = false;
		public static bool VSYNC = true;
		public static int QualityLevel = 0;
		public static int ScreenSize = 0;
		public static Resolution[] Resolutions;

		public static void Initialize ()
		{
			Resolutions = Screen.resolutions;
			SaveSetting ("Default");
			LoadSetting ("User");
		}

		public static void SaveSetting (string key)
		{
			PlayerPrefs.SetInt (key + "_QUALITY", GMKGameSettings.QualityLevel);
			PlayerPrefs.SetInt (key + "_SCREEN", GMKGameSettings.ScreenSize);

			if (GMKGameSettings.FullScreen) {
				PlayerPrefs.SetInt (key + "_FULLSCREEN", 1);
			} else {
				PlayerPrefs.SetInt (key + "_FULLSCREEN", 0);
			}
			if (GMKGameSettings.VSYNC) {
				PlayerPrefs.SetInt (key + "_VSYNC", 1);
			} else {
				PlayerPrefs.SetInt (key + "_VSYNC", 0);
			}

			PlayerPrefs.SetFloat (key + "_SOUND", GMKGameSettings.SoundsVolume);
			PlayerPrefs.SetFloat (key + "_MUSIC", GMKGameSettings.MusicsVolume);
			PlayerPrefs.SetFloat (key + "_SFX", GMKGameSettings.SFXVolume);
			PlayerPrefs.SetInt (key + "_HAS", 1);
		}

		public static void LoadSetting (string key)
		{
			if (PlayerPrefs.HasKey (key + "_HAS")) {
				GMKGameSettings.QualityLevel = PlayerPrefs.GetInt (key + "_QUALITY");
				GMKGameSettings.ScreenSize = PlayerPrefs.GetInt (key + "_SCREEN");

				if (PlayerPrefs.GetInt (key + "_FULLSCREEN") == 1) {
					GMKGameSettings.FullScreen = true;
				} else {
					GMKGameSettings.FullScreen = false;
				}
				if (PlayerPrefs.GetInt (key + "_VSYNC") == 1) {
					GMKGameSettings.VSYNC = true;
				} else {
					GMKGameSettings.VSYNC = false;
				}

				GMKGameSettings.SoundsVolume = PlayerPrefs.GetFloat (key + "_SOUND");
				GMKGameSettings.MusicsVolume = PlayerPrefs.GetFloat (key + "_MUSIC");
				GMKGameSettings.SFXVolume = PlayerPrefs.GetFloat (key + "_SFX");


				UseSetting ();
			}
		}

		public static void UseSetting ()
		{
			if (Resolutions.Length <= 0)
				Resolutions = Screen.resolutions;

			if (Resolutions.Length > GMKGameSettings.ScreenSize) {
				Screen.SetResolution (Resolutions [GMKGameSettings.ScreenSize].width, Resolutions [GMKGameSettings.ScreenSize].height, GMKGameSettings.FullScreen);
			}
			QualitySettings.SetQualityLevel (GMKGameSettings.QualityLevel);
		}
	}

}