using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboard : MonoBehaviour
{
    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(transform.position + GameObject.FindGameObjectWithTag("MainCamera").transform.forward);
    }

}
