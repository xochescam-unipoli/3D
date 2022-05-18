using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    CharacterController characterController;

    //Movements variables
    public float walk = 5.0f,
                 jump = 7.0f,
                 gravity = 22.0f;

    //Camera variables
    public Camera cam;
    float h_mouse, v_mouse;
    public float mouseHor = 3.0f,
                 mouseVer = 2.0f,
                 minRot = -65.0f,
                 maxRot = 60.0f;

    //Init vector 3D with x,y,z = 0
    private Vector3 move = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        //Assign component to variable
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        rotation();
        movements();
    }

    public void rotation()
    {
        //Values of mouse * input value
        h_mouse = mouseHor * Input.GetAxis("Mouse X");
        v_mouse += mouseVer * Input.GetAxis("Mouse Y");

        //Delimit the rotation in x or vertically
        v_mouse = Mathf.Clamp(v_mouse, minRot, maxRot);

        //Rotate the camera
        cam.transform.localEulerAngles = new Vector3(-v_mouse, 0, 0);
        transform.Rotate(0, h_mouse, 0);
    }

    public void movements()
    {
        //Verify if the character is in the ground or not
        if (characterController.isGrounded)
        {
            //Give values to vector3 in x, y, z
            //Input.GetAxis("Horizontal") = WD || Arrow keys < >
            move = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

            //Move the direction of the character according with the camera direction and the keys
            move = transform.TransformDirection(move) * walk;

            jumping();
        }

        //Getting the move.y value and increasing the value per time
        move.y -= gravity * Time.deltaTime;

        //Delimit the user movements with collisions
        characterController.Move(move * Time.deltaTime);
    }

    public void jumping()
    {
        if (Input.GetKey(KeyCode.Space))
            move.y = jump;
    }
}
