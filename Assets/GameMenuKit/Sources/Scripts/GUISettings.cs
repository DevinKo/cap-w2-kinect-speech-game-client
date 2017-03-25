using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace GameMenuKit
{
	public class GUISettings : MonoBehaviour
	{
		public Dropdown ScreenDropDownList;
		public Dropdown QualityDropDownList;
		public Slider SoundControl;
		public Slider SFXControl;
		public Slider MusicControl;
		public Toggle FullScreen;
		public Toggle Vsync;


		void Start ()
		{

			string[] quality = QualitySettings.names;
			List<Dropdown.OptionData> screens = new List<Dropdown.OptionData> ();
			List<Dropdown.OptionData> qualitys = new List<Dropdown.OptionData> ();

			foreach (string qua in quality) {
				Dropdown.OptionData data = new Dropdown.OptionData ();
				data.text = qua;
				qualitys.Add (data);
			}

			foreach (Resolution res in GMKGameSettings.Resolutions) {
				Dropdown.OptionData data = new Dropdown.OptionData ();
				data.text = res.width + " x " + res.height;
				screens.Add (data);
			}

			if (ScreenDropDownList) {
				ScreenDropDownList.AddOptions (screens);
			}

			if (QualityDropDownList) {
				QualityDropDownList.AddOptions (qualitys);
			}

			SetValues ();
		}


		void GetValues ()
		{
			if (FullScreen)
				GMKGameSettings.FullScreen = FullScreen.isOn;

			if (Vsync)
				GMKGameSettings.VSYNC = Vsync.isOn;

			if (SoundControl)
				GMKGameSettings.SoundsVolume = SoundControl.value;

			if (MusicControl)
				GMKGameSettings.MusicsVolume = MusicControl.value;

			if (SFXControl)
				GMKGameSettings.SFXVolume = SFXControl.value;

			if (ScreenDropDownList) {
				GMKGameSettings.ScreenSize = ScreenDropDownList.value;
			}

			if (QualityDropDownList) {
				GMKGameSettings.QualityLevel = QualityDropDownList.value;
			}

		}

		void SetValues ()
		{
			if (FullScreen)
				FullScreen.isOn = GMKGameSettings.FullScreen;

			if (Vsync)
				Vsync.isOn = GMKGameSettings.VSYNC;

			if (SoundControl)
				SoundControl.value = GMKGameSettings.SoundsVolume;

			if (MusicControl)
				MusicControl.value = GMKGameSettings.MusicsVolume;

			if (SFXControl)
				SFXControl.value = GMKGameSettings.SFXVolume;

			if (ScreenDropDownList) {
				ScreenDropDownList.value = GMKGameSettings.ScreenSize;
			}

			if (QualityDropDownList) {
				QualityDropDownList.value = GMKGameSettings.QualityLevel;
			}

		}



		public void Apply ()
		{
			GetValues ();
			GMKGameSettings.SaveSetting ("User");
			GMKGameSettings.UseSetting ();
		}

		public void Defult ()
		{
			GMKGameSettings.LoadSetting ("Default");
			SetValues ();
		}

	
	}

}