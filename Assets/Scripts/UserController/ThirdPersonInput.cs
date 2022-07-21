using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;




public class ThirdPersonInput : MonoBehaviour
{
    public FixedJoystick LeftJoystick;
    public FixedButton Button;
    public FixedTouchField TouchField;
    protected ThirdPersonUserControl Control;

    public GameObject playerCamera;

    
    public  Vector3  CameraPosition = new Vector3(0, 3, 4);
    public float CameraHeight = 2f;
    protected float CameraAngle;
    protected float CameraAngleSpeed = 0.2f;

    // Use this for initialization
    void Start()
    {
        Control = GetComponent<ThirdPersonUserControl>();

    }

    // Update is called once per frame
    void Update()
    {
        //Control.m_Jump = Button.Pressed;
        Control.Hinput = LeftJoystick.Direction.x;
        Control.Vinput = LeftJoystick.Direction.y;

        // Keyboard controls for PC testing
#if UNITY_STANDALONE
        if (Control.Hinput == 0f)
        {
            Control.Hinput = Input.GetAxis("Horizontal");
        }
        if (Control.Vinput == 0f)
        {
            Control.Vinput = Input.GetAxis("Vertical");
        }
#endif

        CameraAngle += TouchField.TouchDist.x * CameraAngleSpeed;

        playerCamera.transform.position = transform.position + Quaternion.AngleAxis(CameraAngle, Vector3.up) * CameraPosition;
        playerCamera.transform.rotation = Quaternion.LookRotation(transform.position + Vector3.up * CameraHeight - playerCamera.transform.position, Vector3.up);

    }
}
