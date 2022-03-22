using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public Transform character = null;
    public Camera mainCamera;
    public Rigidbody _rgbd;
    public Vector3 directionnalSpeed = new Vector3(1, 1, 1);
    public Vector3 jumpForce = new Vector3(0, 100, 0);
    public float gravityOnFall = 2;

    // Start is called before the first frame update
    void Start()
    {
        if (character == null)
            character = this.transform;
        if (_rgbd == null)
            _rgbd = this.GetComponent<Rigidbody>();
        mainCamera = GameManager.instance.cameraMng.GetCurrentCamera();
    }

    // Update is called once per frame
    void Update()
    {
        InputManagement();
    }

    public void InputManagement()
    {
        Vector3 joystickMove = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")
            );
        joystickMove.Normalize();

        //Need to take the camera direction in account !
        //And to apply it on the surface where we move (either ground or wall, might be both too)

        Vector3 finalMoveAdd = Vector3.zero;
        finalMoveAdd += joystickMove.x * mainCamera.transform.right * directionnalSpeed.x;
        Debug.Log("Value (x) = " + finalMoveAdd + "(" + joystickMove.x + ")" + mainCamera.transform.right);
        Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.right, Color.green);
        finalMoveAdd += joystickMove.y * mainCamera.transform.up * directionnalSpeed.y;
        Debug.Log("Value (0) = " + finalMoveAdd);
        finalMoveAdd += joystickMove.z * mainCamera.transform.forward *  directionnalSpeed.z;
        Debug.Log("Value (y) = " + finalMoveAdd + "(" + joystickMove.z + ")" + mainCamera.transform.forward);
        Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.forward, Color.blue);

        Vector3 newPos = this.transform.position + finalMoveAdd * Time.deltaTime;
        _rgbd.MovePosition(newPos);
        Debug.Log("Moved. " + finalMoveAdd * Time.deltaTime + " = "+ newPos);


        JumpManagement();
    }

    void JumpManagement()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rgbd.velocity = Vector3.zero;
            /*
             * probably will take the current velocity in account for jumping (like if inverse, cancel to jump ;
             *                                                              if 90°, a little bit of the speed, like half
             *                                                                 and if same direction, add the speed (clamped to a max)
             */
             //Also will probably code it a little to add the "floaty on top only" effect (like it goes from 0 to 0.9 in 0.1 then 1 at 0.5 then from 0.9 to 0 between 0.9 to 1)
            _rgbd.AddForce(jumpForce);
        }

        if(_rgbd.velocity.y < 0)
        {
            Debug.Log("ok.");
            _rgbd.AddForce(Physics.gravity * gravityOnFall, ForceMode.Acceleration);
        }
    }
}
