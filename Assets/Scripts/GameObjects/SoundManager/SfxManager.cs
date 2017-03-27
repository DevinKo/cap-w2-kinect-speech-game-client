using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Assets.Toolbox;

public class SfxManager : MonoBehaviour {

    public AudioClip[] SfxSamples = new AudioClip[3];

    public enum SFX
    {
        OBJECT_FOUND,
        ZONE_COMPLETE
    }

    private Toolbox _toolbox;

    // Unity Start method
    private void Start()
    {
        _toolbox = FindObjectOfType<Toolbox>();

        // assign sounds to events in EventHub
        _toolbox.EventHub.SpyScene.ZoneActivated += PlayZoneActivatedSound;
        _toolbox.EventHub.SpyScene.ZoneComplete += PlayZoneCompleteSound;
    }

    public void PlayZoneActivatedSound(object sender, EventArgs e)
    {
        GetComponent<AudioSource>().PlayOneShot(SfxSamples[(int)SFX.OBJECT_FOUND]);
    }

    public void PlayZoneCompleteSound(object sender, EventArgs e)
    {
        GetComponent<AudioSource>().PlayOneShot(SfxSamples[(int)SFX.ZONE_COMPLETE]);
    }
}
