using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Time.timeScale = Time.timeScale * 2;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Time.timeScale = Time.timeScale / 2;
        }
    }
}
