using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Windows.Kinect;

public class KinectUICursor : AbstractKinectUICursor
{
    public Color normalColor = new Color(1f, 1f, 1f, 0.5f);
    public Color hoverColor = new Color(1f, 1f, 1f, 1f);
    public Color clickColor = new Color(1f, 1f, 1f, 1f);
    public Vector3 clickScale = new Vector3(.8f, .8f, .8f);

    private Vector3 _initScale;
    private Vector3 offset;

    public override void Start()
    {
        base.Start();
        _initScale = transform.localScale;
        _image.color = new Color(1f, 1f, 1f, 0f);
        offset = new Vector3(681.5f, 296.5f);
    }

    public override void ProcessData()
    {
        // update pos
        transform.position = new Vector3(_data.GetHandScreenPosition().x, (2 * offset.y) - _data.GetHandScreenPosition().y, _data.GetHandScreenPosition().z);

        if (_data.IsPressing)
        {
            _image.color = clickColor;
            _image.transform.localScale = clickScale;
            return;
        }
        if (_data.IsHovering)
        {
            _image.color = hoverColor;
        }
        else
        {
            _image.color = normalColor;
        }
        _image.transform.localScale = _initScale;
    }
}
