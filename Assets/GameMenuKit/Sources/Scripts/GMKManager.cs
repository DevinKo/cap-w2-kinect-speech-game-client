using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace GameMenuKit
{
	public class GMKManager : MonoBehaviour
	{
		private SceneLoader sceneLoader;

		void Start ()
		{
			
		}

		void Awake ()
		{
			GMK.GameMenuKitManager = this;

			SceneLoader[] sceneLoaders = (SceneLoader[])GameObject.FindObjectsOfType (typeof(SceneLoader));
			if (sceneLoaders.Length > 0) {
				// set sceneloader instance to this class
				sceneLoader = sceneLoaders [0];
				// remove other sceneloader we dont need a multiple sceneLoader
				// because a sceneloader had set in mainmenu with don't destroy onload, so if we back to mainmenu agin, it will douplicate
				for (int i = 0; i < sceneLoaders.Length; i++) {
					if (i > 0) {
						Destroy (sceneLoaders [i].gameObject);
					}
				}
			}
			GMKGameSettings.Initialize ();
			GMK.SceneLoader = sceneLoader;
			GMKLevelManager[] levelManagers = (GMKLevelManager[])GameObject.FindObjectsOfType (typeof(GMKLevelManager));
			if (levelManagers.Length > 0) {
				for (int i = 0; i < levelManagers.Length; i++) {
					if (i > 0) {
						Destroy (levelManagers [i].gameObject);
					}
				}
			}
		}

		public void LoadScene (string sceneName)
		{
			if (sceneLoader)
				sceneLoader.StartLoadScene (sceneName);
		}

		public void LoadScene (string sceneName,string[] openpanel)
		{
			if (sceneLoader)
				sceneLoader.StartLoadScene (sceneName,openpanel);
		}

		public void ExitGame ()
		{
			Application.Quit ();
		}
	}

	public static class GMK
	{
		public static GMKLevelManager LevelManager;
		public static GMKManager GameMenuKitManager;
		public static SceneLoader SceneLoader;
	}
}
