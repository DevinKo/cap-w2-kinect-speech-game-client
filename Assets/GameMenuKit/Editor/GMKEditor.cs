using UnityEngine;
using UnityEditor;
using System.Collections;
using GameMenuKit;

namespace GameMenuKit
{
	public class GMKEditor : MonoBehaviour
	{
	
		[MenuItem ("Window/Game Menu Kit/Template/Create Mainmenu")]
		static void CreatetTmpMainmenu ()
		{
			GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath ("Assets/GameMenuKit/Editor/Mainmenu_Template.prefab", typeof(GameObject));
			GameObject ob = Instantiate (prefab, Vector3.zero, Quaternion.identity) as GameObject;
			ob.name = "Mainmenu";
			ob.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, 0);

			GameObject prefab2 = (GameObject)AssetDatabase.LoadAssetAtPath ("Assets/GameMenuKit/Editor/SceneLoader.prefab", typeof(GameObject));
			GameObject ob2 = Instantiate (prefab2, Vector3.zero, Quaternion.identity) as GameObject;
			ob2.name = "SceneLoader";
			ob2.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, 0);
		}

		[MenuItem ("Window/Game Menu Kit/Template/Create In Game Mainmenu")]
		static void CreatetTmpInMainmenu ()
		{
			GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath ("Assets/GameMenuKit/Editor/InGameMenu_Template.prefab", typeof(GameObject));
			GameObject ob = Instantiate (prefab, Vector3.zero, Quaternion.identity) as GameObject;
			ob.name = "InGameMainmenu";
			ob.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, 0);
		}


		[MenuItem ("Window/Game Menu Kit/Prefab/Create Panel Manager")]
		static void CreatePanelManager ()
		{
			if (Selection.activeGameObject != null) {
				GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath ("Assets/GameMenuKit/Editor/PanelManager.prefab", typeof(GameObject));
				GameObject ob = Instantiate (prefab, Vector3.zero, Quaternion.identity) as GameObject;
				ob.name = "PanelManager";
				ob.gameObject.transform.parent = Selection.activeGameObject.transform;
				ob.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, 0);
			}
		}

		[MenuItem ("Window/Game Menu Kit/Prefab/Create Panel")]
		static void CreatePanel ()
		{
			if (Selection.activeGameObject != null) {

				if (Selection.activeGameObject.GetComponent<PanelsManager> () != null) {
					PanelsManager panelmanager = Selection.activeGameObject.GetComponent<PanelsManager> ();
					GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath ("Assets/GameMenuKit/Editor/Panel.prefab", typeof(GameObject));
					GameObject ob = Instantiate (prefab, Vector3.zero, Quaternion.identity) as GameObject;
					ob.name = "Panel" + panelmanager.Pages.Length;
					ob.gameObject.transform.parent = Selection.activeGameObject.transform;
					ob.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, 0);


					System.Array.Resize (ref panelmanager.Pages, panelmanager.Pages.Length + 1);
					panelmanager.Pages [panelmanager.Pages.Length - 1] = ob.GetComponent<PanelInstance> ();

				}
			}
		}

		[MenuItem ("Window/Game Menu Kit/Prefab/Create Scene Loader")]
		static void CreateLoader ()
		{
			GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath ("Assets/GameMenuKit/Editor/SceneLoader.prefab", typeof(GameObject));
			GameObject ob = Instantiate (prefab, Vector3.zero, Quaternion.identity) as GameObject;
			ob.name = "SceneLoader";
			ob.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, 0);
		}

		[MenuItem ("Window/Game Menu Kit/Prefab/Create Game Option")]
		static void CreateOption ()
		{
			if (Selection.activeGameObject != null) {
				GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath ("Assets/GameMenuKit/Editor/Option.prefab", typeof(GameObject));
				GameObject ob = Instantiate (prefab, Vector3.zero, Quaternion.identity) as GameObject;

				ob.name = "Option";
				ob.gameObject.transform.parent = Selection.activeGameObject.transform;
				ob.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, 0);
			}
		}

		[MenuItem ("Window/Game Menu Kit/Prefab/Create Game Credit")]
		static void CreateCreditPref ()
		{
			if (Selection.activeGameObject != null) {
				GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath ("Assets/GameMenuKit/Editor/Credit.prefab", typeof(GameObject));
				GameObject ob = Instantiate (prefab, Vector3.zero, Quaternion.identity) as GameObject;

				ob.name = "Credit";
				ob.gameObject.transform.parent = Selection.activeGameObject.transform;
				ob.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, 0);
			}
		}

		[MenuItem ("Window/Game Menu Kit/Prefab/Create Level Manager")]
		static void CreateLevelManager ()
		{
		
			GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath ("Assets/GameMenuKit/Editor/LevelManager.prefab", typeof(GameObject));
			GameObject ob = Instantiate (prefab, Vector3.zero, Quaternion.identity) as GameObject;
			ob.name = "LevelManager";

		}

		[MenuItem ("Window/Game Menu Kit/Prefab/Create Level Badge")]
		static void CreateLevelBadge ()
		{
			if (Selection.activeGameObject != null) {
				GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath ("Assets/GameMenuKit/Editor/LevelBadge.prefab", typeof(GameObject));
				GameObject ob = Instantiate (prefab, Vector3.zero, Quaternion.identity) as GameObject;

				ob.name = "LevelBadge";
				ob.gameObject.transform.parent = Selection.activeGameObject.transform;
				ob.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, 0);
			}
		}

		[MenuItem ("Window/Game Menu Kit/Component/Panel Manager")]
		static void AddPanelManager ()
		{
			if (Selection.activeGameObject != null) {
				if (Selection.activeGameObject.GetComponent<PanelsManager> () == null)
					Selection.activeGameObject.AddComponent<PanelsManager> ();
			}
		}

		[MenuItem ("Window/Game Menu Kit/Component/Panel Instance")]
		static void AddPanel ()
		{
			if (Selection.activeGameObject != null) {
				if (Selection.activeGameObject.GetComponent<PanelInstance> () == null)
					Selection.activeGameObject.AddComponent<PanelInstance> ();
			}
		}

		[MenuItem ("Window/Game Menu Kit/Component/Credit")]
		static void CreateCredits ()
		{
			if (Selection.activeGameObject != null) {

				if (Selection.activeGameObject.GetComponent<GUICreditRenderer> () == null)
					Selection.activeGameObject.AddComponent<GUICreditRenderer> ();

				if (Selection.activeGameObject.GetComponent<GUICreditRenderer> () != null) {
					GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath ("Assets/GameMenuKit/Editor/CreditPanel.prefab", typeof(GameObject));
					GameObject ob = Instantiate (prefab, Vector3.zero, Quaternion.identity) as GameObject;
					ob.name = "CreditPanel";
					ob.gameObject.transform.parent = Selection.activeGameObject.transform;
					ob.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, -1500);
					Selection.activeGameObject.GetComponent<GUICreditRenderer> ().Credit = ob.GetComponent<RectTransform> ();
				}
			}
		}

		[MenuItem ("Window/Game Menu Kit/Component/Key Press")]
		static void CreateKeyPress ()
		{
			if (Selection.activeGameObject != null) {
				if (Selection.activeGameObject.GetComponent<GUIKeyPress> () == null)
					Selection.activeGameObject.AddComponent<GUIKeyPress> ();
			}
		}

		[MenuItem ("Window/Game Menu Kit/Component/GUI Level Manager")]
		static void CreateGUIlevelManager ()
		{
			if (Selection.activeGameObject != null) {
				if (Selection.activeGameObject.GetComponent<GUILevelManager> () == null)
					Selection.activeGameObject.AddComponent<GUILevelManager> ();
			}
		}

		[MenuItem ("Window/Game Menu Kit/Component/GUI Level Start")]
		static void CreateGUIlevelStart ()
		{
			if (Selection.activeGameObject != null) {
				if (Selection.activeGameObject.GetComponent<GUILevelStart> () == null)
					Selection.activeGameObject.AddComponent<GUILevelStart> ();
			}
		}

		void Start ()
		{
	
		}

	}
}