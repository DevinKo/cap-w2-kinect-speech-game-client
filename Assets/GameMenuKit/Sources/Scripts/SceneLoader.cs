using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace GameMenuKit
{
	public class SceneLoader: MonoBehaviour
	{
		public AsyncOperation sceneSync;
		public Image FadeScreen;
		public RectTransform LoadingScreen;
		public float FadeInDuration = 2;
		public float FadeOutDuration = 1;
		public float TimeLoadingBase = 2;

		public void Start ()
		{
			GMK.SceneLoader = this;
			if (FadeScreen) {
				FadeScreen.gameObject.SetActive (true);
				FadeScreen.CrossFadeColor (new Color (0, 0, 0, 0), FadeInDuration, true, true);
			}

			if (LoadingScreen) {
				LoadingScreen.gameObject.SetActive (false);
			}

			DontDestroyOnLoad (this.gameObject);
		}

		public void StartLoadScene (string sceneName)
		{
			StartCoroutine (LoadScene (sceneName));
		}


		public IEnumerator LoadScene (string sceneName)
		{
			if (FadeScreen)
				FadeScreen.CrossFadeColor (new Color (0, 0, 0, 1), FadeInDuration, true, true);

			yield return new WaitForSeconds (FadeInDuration);

			if (FadeScreen)
				FadeScreen.CrossFadeColor (new Color (0, 0, 0, 0), 0.5f, true, true);

			if (LoadingScreen) {
				LoadingScreen.gameObject.SetActive (true);
			}

			sceneSync = SceneManager.LoadSceneAsync (sceneName);

			while (!sceneSync.isDone) {
				yield return new WaitForEndOfFrame ();
			}

			yield return new WaitForSeconds (TimeLoadingBase);

			if (FadeScreen)
				FadeScreen.CrossFadeColor (new Color (0, 0, 0, 1), 1, true, true);

			yield return new WaitForSeconds (1);

			if (LoadingScreen) {
				LoadingScreen.gameObject.SetActive (false);
			}

			if (FadeScreen)
				FadeScreen.CrossFadeColor (new Color (0, 0, 0, 0), FadeOutDuration, true, true);
		}

		public IEnumerator LoadScene (string sceneName, string[] andopenpanel)
		{
			if (FadeScreen)
				FadeScreen.CrossFadeColor (new Color (0, 0, 0, 1), FadeInDuration, true, true);

			yield return new WaitForSeconds (FadeInDuration);

			if (FadeScreen)
				FadeScreen.CrossFadeColor (new Color (0, 0, 0, 0), 0.5f, true, true);

			if (LoadingScreen) {
				LoadingScreen.gameObject.SetActive (true);
			}

			sceneSync = SceneManager.LoadSceneAsync (sceneName);

			while (!sceneSync.isDone) {
				yield return new WaitForEndOfFrame ();
			}

			yield return new WaitForSeconds (TimeLoadingBase);

			if (FadeScreen)
				FadeScreen.CrossFadeColor (new Color (0, 0, 0, 1), 1, true, true);

			yield return new WaitForSeconds (1);

			PanelsManager[] panel = (PanelsManager[])GameObject.FindObjectsOfType (typeof(PanelsManager));

			for (int i = 0; i < panel.Length; i++) {
				for (int j = 0; j < andopenpanel.Length; j++) {
					if (panel [i].IsPanelExist (andopenpanel [j])) {
						panel [i].OpenPanelByName (andopenpanel [j]);
					}
				}
			}

			if (LoadingScreen) {
				LoadingScreen.gameObject.SetActive (false);
			}

			if (FadeScreen)
				FadeScreen.CrossFadeColor (new Color (0, 0, 0, 0), FadeOutDuration, true, true);
		}

		public void StartLoadScene (string sceneName, string[] andopenpanel)
		{
			StartCoroutine (LoadScene (sceneName, andopenpanel));
		}
	}
}
