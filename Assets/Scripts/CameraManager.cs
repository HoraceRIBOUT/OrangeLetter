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

    [Header("CameraPlacement")]
    [Tooltip("X = up / Y = Mid / Z = down")]
    public Vector3 camRadius = new Vector3(-5, -10, -6);
    public Vector3 camHeight = new Vector3(4, 0, -3);
    public Vector3 camRot = new Vector3(-20, -5, 30);
    public List<Transform> circles;
    public List<Transform> cameraVirtuals;


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
        ReplaceCameraFromRadius();
    }

    // Update is called once per frame
    void Update()
    {

        GlobalPlacement();


        InputManagement();
    }

    public void GlobalPlacement()
    {
        this.transform.position = this.transform.position * (1 - clampSpeed) + target.transform.position * (clampSpeed);
    }

    public void InputManagement()
    {
        //if (Input.GetMouseButton(1))
        //{
        //    Vector2 joystickMove = new Vector2(
        //        Input.GetAxis("Mouse X"),
        //        -Input.GetAxis("Mouse Y")
        //    );

        //    Vector3 finalRotation = Vector3.zero;
        //    finalRotation.y = rotationSpeed.x * joystickMove.x;
        //    if(!blockXRotation)
        //        finalRotation.x = rotationSpeed.y * joystickMove.y;
        //    //Vector2 joystickMove = new Vector2(
        //    //    Input.GetAxis("VerticalCam"),
        //    //    Input.GetAxis("HorizontalCam")
        //    //    );

        //    //ok value inputed, rotate it
        //    mainCamera.transform.Rotate(finalRotation * Time.deltaTime);
        //}

        if (Input.GetMouseButtonUp(1))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetMouseButtonDown(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void UpdateCamPosition(float xAxis, float yAxis)
    {
        xAxis = xAxis % 1;//to overcome overflow 
        xAxis -= 0.5f;
        foreach (Transform trans in circles)
        {
            trans.localRotation = Quaternion.Euler(0, 360 * xAxis, 0);
        }
        Transform secondCam = cameraVirtuals[0];
        if (yAxis < 0)//mean that we are going between midCam and downCam and also need to take the inverse.
        {
            secondCam = cameraVirtuals[2];
            yAxis = -yAxis;
        }
        Debug.Log("yAxis = " + yAxis);
        mainCamera.transform.position = Vector3.Lerp(cameraVirtuals[1].position, secondCam.position, yAxis);
        mainCamera.transform.rotation = Quaternion.Lerp(cameraVirtuals[1].rotation, secondCam.rotation, yAxis);
    }

    public void ReplaceCameraFromRadius()
    {
        for (int i = 0; i < 3; i++)
        {
            cameraVirtuals[i].localPosition = new Vector3(0, 0, camRadius[i]);
            cameraVirtuals[i].localRotation = Quaternion.Euler(camRot[i], 0, 0);
            circles[i].localPosition = new Vector3(0, camHeight[i], 0);
        }
    }                               
                                    

    public Camera GetCurrentCamera()
    {
        return mainCamera;
    }
}
