using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera mainCamera;
    public Transform target = null;
    [Range(0, 1)]
    public float clampSpeed = 0.9f;

    public Vector2 rotationSpeed = Vector2.one;

    public bool blockXRotation = false;



    /// <summary>
    /// TO DO : 
    /// - put 3 "camera" on 3 circle.
    /// - the X rotation  (horizontal) will define where on the circle we are. (always loonking to the center)
    /// - the Y rotation will change a lerp value to lerp between the 3 
    /// - and that's it. May add some "see trough" effect for the material, but will be on the shader side of work.
    /// </summary>



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        this.transform.position = this.transform.position * (1 - clampSpeed) + target.transform.position * (clampSpeed);


        InputManagement();
    }

    public void InputManagement()
    {
        if (Input.GetMouseButton(1))
        {
            Vector2 joystickMove = new Vector2(
                Input.GetAxis("Mouse X"),
                -Input.GetAxis("Mouse Y")
            );

            Vector3 finalRotation = Vector3.zero;
            finalRotation.y = rotationSpeed.x * joystickMove.x;
            if(!blockXRotation)
                finalRotation.x = rotationSpeed.y * joystickMove.y;
            //Vector2 joystickMove = new Vector2(
            //    Input.GetAxis("VerticalCam"),
            //    Input.GetAxis("HorizontalCam")
            //    );

            //ok value inputed, rotate it
            mainCamera.transform.Rotate(finalRotation * Time.deltaTime);
        }

        if (Input.GetMouseButtonUp(1))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetMouseButtonDown(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }


    public Camera GetCurrentCamera()
    {
        return mainCamera;
    }
}
