using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWalls : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Wall bricks")
        {
            Destroy(col.gameObject);
        }
    }
}
