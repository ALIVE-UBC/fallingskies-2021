using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    public float Height = 100f;

    private Transform _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Vector3 pos = new Vector3(_player.position.x, Height, _player.position.z);
        this.transform.position = pos;
    }
}
