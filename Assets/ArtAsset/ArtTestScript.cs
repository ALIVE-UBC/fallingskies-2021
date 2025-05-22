using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtTestScript : MonoBehaviour
{
    public Mesh mesh;
    public Material material;


    void Start()
    {
        Mesh cubeMesh = Resources.GetBuiltinResource<Mesh>("Cube.fbx");
        Graphics.DrawMesh(cubeMesh, new Vector3(0, 1, 0), Quaternion.identity, material, 0);
    }
}
