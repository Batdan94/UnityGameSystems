using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circleMouse : MonoBehaviour
{







    void Update()
    {

        Vector3 mouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f - Camera.main.transform.position.z);
        mouse = Camera.main.ScreenToWorldPoint(mouse);

        this.transform.position = new Vector3(mouse.x, mouse.y, 0.0f);
    }
}