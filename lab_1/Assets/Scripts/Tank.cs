using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{

    float rotateSpeed = 90.0f;
    float speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float transAmount = speed * Time.deltaTime;
        float rotateAmount = rotateSpeed * Time.deltaTime;

        if (Input.GetKey("up"))
        {
            transform.Translate(0, 0, transAmount);
        }
        if (Input.GetKey("down"))
        {
            transform.Translate(0, 0, -transAmount);
        }
        if (Input.GetKey("left"))
        {
            transform.Rotate(0, -rotateAmount, 0);
        }
        if (Input.GetKey("right"))
        {
            transform.Rotate(0, rotateAmount, 0);
        }

    }
}

