using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Assets.Toolbox;

public class SfxManager : MonoBehaviour {

    public AudioClip[] SfxSamples = new AudioClip[4];

    public enum SFX
    {
        OBJECT_FOUND,
        ZONE_COMPLETE,
        DESCRIBE_COMPLETE,
        LEVEL_COMPLETE
    }

    private Toolbox _toolbox;

    // Unity Start method
    private void Start()
    {
        _toolbox = FindObjectOfType<Toolbox>();

        // assign sounds to events in EventHub
        _toolbox.EventHub.SpyScene.ZoneActivated += PlayZoneActivatedSound;
        _toolbox.EventHub.SpyScene.ZoneComplete += PlayZoneCompleteSound;
        _toolbox.EventHub.SpyScene.DescribeComplete += PlayLevelCompleteSound;
    }

    private void OnDestroy()
    {
        // assign sounds to events in EventHub
        _toolbox.EventHub.SpyScene.ZoneActivated -= PlayZoneActivatedSound;
        _toolbox.EventHub.SpyScene.ZoneComplete -= PlayZoneCompleteSound;
        _toolbox.EventHub.SpyScene.DescribeComplete -= PlayLevelCompleteSound;
    }

    public void PlayZoneActivatedSound(object sender, EventArgs e)
    {
        GetComponent<AudioSource>().PlayOneShot(SfxSamples[(int)SFX.OBJECT_FOUND]);
    }

    public void PlayZoneCompleteSound(object sender, EventArgs e)
    {
        GetComponent<AudioSource>().PlayOneShot(SfxSamples[(int)SFX.ZONE_COMPLETE]);
    }

    public void PlayDescribeCompleteSound(object sender, EventArgs e)
    {
        GetComponent<AudioSource>().PlayOneShot(SfxSamples[(int)SFX.DESCRIBE_COMPLETE]);
    }

    public void PlayLevelCompleteSound(object sender, EventArgs e)
    {
        GetComponent<AudioSource>().PlayOneShot(SfxSamples[(int)SFX.LEVEL_COMPLETE]);
    }
}
