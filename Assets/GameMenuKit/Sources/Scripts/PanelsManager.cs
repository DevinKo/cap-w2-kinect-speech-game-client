using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace GameMenuKit
{
	public class PanelsManager : MonoBehaviour
	{
		public PanelInstance[] Pages;
		[HideInInspector]
		public PanelInstance currentPanel;

		void Start ()
		{
			panelIndex = 0;
			for (int i = 0; i < Pages.Length; i++) {
				if (Pages [i].gameObject.GetComponent<PanelInstance> () == null) {
					Pages [i].gameObject.AddComponent<PanelInstance> ();
				}
			}

			if (Pages.Length <= 0)
				return;
			
		}
		public bool IsPanelExist (string panelname)
		{
			for (int i = 0; i < Pages.Length; i++) {
				if (Pages [i].name == panelname) {
					return true;
				}
			}
			return false;
		}

		public void CloseAllPanels ()
		{
			if (Pages.Length <= 0)
				return;

			for (int i = 0; i < Pages.Length; i++) {
				if (Pages [i].isActiveAndEnabled) {
					StartCoroutine (CloseDeleyed (Pages [i]));
				}
			}
		}

		public void CloseAllPanelsAndOpen (PanelInstance panel)
		{
			if (Pages.Length <= 0)
				return;

			bool closeOther = false;
			for (int i = 0; i < Pages.Length; i++) {
				if (Pages [i].isActiveAndEnabled && Pages [i] != panel) {
					closeOther = true;
					StartCoroutine (CloseAndOpenDeleyed (Pages [i], panel));
				}
			}

			if (panel != null && !closeOther) {
				panel.SetOpen ();
			}
		}

		public void OpenPanelInstantByName (string name)
		{
			PanelInstance page = null;
			for (int i = 0; i < Pages.Length; i++) {
				if (Pages [i].name == name) {
					page = Pages [i];
					break;
				}
			}
			if (page == null)
				return;
			
			page.PanelBefore = currentPanel;
			currentPanel = page;
			page.SetOpen ();
		}

		public void OpenPanelInstantByNameNoPreviousSave (string name)
		{
			PanelInstance page = null;
			for (int i = 0; i < Pages.Length; i++) {
				if (Pages [i].name == name) {
					page = Pages [i];
					break;
				}
			}
			if (page == null)
				return;

			currentPanel = page;
			page.SetOpen ();
		}

		public void OpenPanelByName (string name)
		{
			PanelInstance page = null;
			for (int i = 0; i < Pages.Length; i++) {
				if (Pages [i].name == name) {
					page = Pages [i];
					break;
				}
			}
			if (page == null)
				return;
			
			page.PanelBefore = currentPanel;
			currentPanel = page;
			CloseAllPanelsAndOpen (page);
		}

		public void OpenPanelByNameNoPreviousSave (string name)
		{
			PanelInstance page = null;
			for (int i = 0; i < Pages.Length; i++) {
				if (Pages [i].name == name) {
					page = Pages [i];
					break;
				}
			}
			if (page == null)
				return;

			currentPanel = page;
			CloseAllPanelsAndOpen (page);
		}

		public void OpenPreviousPanel ()
		{
			if (currentPanel && currentPanel.PanelBefore) {

				CloseAllPanelsAndOpen (currentPanel.PanelBefore);
			}
		}

		public void OpenPanelByNameToggle (string name)
		{
			PanelInstance page = null;
			for (int i = 0; i < Pages.Length; i++) {
				if (Pages [i].name == name) {
					page = Pages [i];
					break;
				}
			}
			if (page == null)
				return;
		
			if (currentPanel == page) {
				currentPanel = null;
				StartCoroutine (CloseDeleyed (page));
				return;
			} else {
				page.PanelBefore = currentPanel;
				currentPanel = page;
				CloseAllPanelsAndOpen (page);
				return;
			}
		
		}

		public void ClosePanelByName (string name)
		{
			PanelInstance page = null;
			for (int i = 0; i < Pages.Length; i++) {
				if (Pages [i].name == name) {
					page = Pages [i];
					break;
				}
			}
			if (page == null)
				return;
		
			currentPanel = null;
			StartCoroutine (CloseDeleyed (page));
		}

		public void CloseCurrent ()
		{
			if (currentPanel) {
				StartCoroutine (CloseDeleyed (currentPanel));
			}
		}

		public void ClosePanel (PanelInstance panel)
		{
			if (panel) {
				StartCoroutine (CloseDeleyed (panel));
			}
		}

		IEnumerator CloseDeleyed (PanelInstance page)
		{
			page.SetAnimation ("Open", false);

			while (!page.IsClosed ()) {
				yield return new WaitForEndOfFrame ();
			}
			page.SetClose ();
		}

		IEnumerator CloseAndOpenDeleyed (PanelInstance closepage, PanelInstance openpage)
		{
			if (closepage != null) {
				closepage.SetAnimation ("Open", false);

				while (!closepage.IsClosed ()) {
					yield return new WaitForEndOfFrame ();
				}

				closepage.SetClose ();
			}
			openpage.SetOpen ();
		}

		public bool IsPanelOpened (string name)
		{
			for (int i = 0; i < Pages.Length; i++) {
				if (Pages [i].name == name) {
					return Pages [i].gameObject.activeSelf;
				}
			}
			return false;
		}

		private int panelIndex = 0;

		public void NextPanel ()
		{

			for (int i = 0; i < Pages.Length; i++) {
				if (Pages[i]!=null) {
					if(Pages[i].gameObject.activeSelf){
						panelIndex = i;
						break;
					}
				}
			}

			panelIndex++;
			if (panelIndex >= Pages.Length) {
				panelIndex = Pages.Length - 1;
			}

			PanelInstance page = null;
			for (int i = 0; i < Pages.Length; i++) {
				if (i == panelIndex) {
					page = Pages [i];
					break;
				}
			}

			if (page == null)
				return;
			
			page.PanelBefore = currentPanel;
			currentPanel = page;
			CloseAllPanelsAndOpen (page);
		}

		public void PrevPanel ()
		{
			for (int i = 0; i < Pages.Length; i++) {
				if (Pages[i]!=null) {
					if(Pages[i].gameObject.activeSelf){
						panelIndex = i;
						break;
					}
				}
			}

			panelIndex--;
			if (panelIndex < 0) {
				panelIndex = 0;
			}

			PanelInstance page = null;
			for (int i = 0; i < Pages.Length; i++) {
				if (i == panelIndex) {
					page = Pages [i];
					break;
				}
			}

			if (page == null)
				return;
			
			page.PanelBefore = currentPanel;
			currentPanel = page;
			CloseAllPanelsAndOpen (page);
		}

	}
}