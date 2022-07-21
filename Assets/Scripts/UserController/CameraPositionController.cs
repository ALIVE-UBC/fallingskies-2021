using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraPositionController : MonoBehaviour
{
    public Vector3 defaultPos = new Vector3(0,3,4);

    public Vector3 targetPosition;

    public float defaultHeight = 2f;
    public float targetHeight;

    public float transTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag( "House") ) 
        {
            DOTween.To(() => GetComponent<ThirdPersonInput>().CameraPosition, x => GetComponent<ThirdPersonInput>().CameraPosition = x, targetPosition, transTime);
            DOTween.To(() => GetComponent<ThirdPersonInput>().CameraHeight, x => GetComponent<ThirdPersonInput>().CameraHeight = x, targetHeight, transTime);
        }
        if (other.CompareTag("SpaceShip"))
        {
            DOTween.To(() => GetComponent<ThirdPersonInput>().CameraHeight, x => GetComponent<ThirdPersonInput>().CameraHeight = x, 1f, transTime);
            DOTween.To(() => GetComponent<ThirdPersonInput>().CameraPosition, x => GetComponent<ThirdPersonInput>().CameraPosition = x, new Vector3(0,1.5f,1.5f), transTime);


        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("House"))
        {
            DOTween.To(() => GetComponent<ThirdPersonInput>().CameraHeight, x => GetComponent<ThirdPersonInput>().CameraHeight = x, defaultHeight, transTime);
            DOTween.To(() => GetComponent<ThirdPersonInput>().CameraPosition, x => GetComponent<ThirdPersonInput>().CameraPosition = x, defaultPos, transTime);

        }

        if (other.CompareTag("SpaceShip"))
        {
            DOTween.To(() => GetComponent<ThirdPersonInput>().CameraHeight, x => GetComponent<ThirdPersonInput>().CameraHeight = x, defaultHeight, transTime);
            DOTween.To(() => GetComponent<ThirdPersonInput>().CameraPosition, x => GetComponent<ThirdPersonInput>().CameraPosition = x, defaultPos, transTime);


        }
    }
}
