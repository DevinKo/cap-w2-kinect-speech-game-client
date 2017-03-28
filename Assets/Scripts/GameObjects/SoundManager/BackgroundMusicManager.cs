using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Assets.Toolbox;

public class BackgroundMusicManager : MonoBehaviour {

    private Toolbox _toolbox;
    private AudioSource _music;

    // Use this for initialization
    void Start () {

        _toolbox = FindObjectOfType<Toolbox>();
        _music = GetComponent<AudioSource>();

        _toolbox.EventHub.SpyScene.LoadComplete += StartSpyMusic;
        _toolbox.EventHub.SpyScene.DescribeComplete += StopSpyMusic;

    }

    private void OnDestroy()
    {
        _toolbox.EventHub.SpyScene.LoadComplete -= StartSpyMusic;
        _toolbox.EventHub.SpyScene.DescribeComplete -= StopSpyMusic;

    }

    public void StartSpyMusic(object sender, EventArgs e)
    {
        _music.Stop();
        _music.Play();
    }
	
    public void StopSpyMusic(object sender, EventArgs e)
    {
        _music.Stop();
    }

}
