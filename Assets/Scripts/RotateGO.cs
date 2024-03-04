using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGO : MonoBehaviour
{
    public Transform tr;

    public Vector3 value;

    // Update is called once per frame
    void Update()
    {
        tr.Rotate(value * Time.deltaTime);
    }
}
