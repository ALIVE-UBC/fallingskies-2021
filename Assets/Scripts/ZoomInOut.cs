using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomInOut : MonoBehaviour
{
    Camera minimapcamera2;
    public float zoomAMT = 60f;

    // Start is called before the first frame update
    void Start()
    {
        minimapcamera2 = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        minimapcamera2.orthographicSize = zoomAMT;

    }

    public void sliderZoom(float zoom)
    {
        zoomAMT = zoom;
    }
}
