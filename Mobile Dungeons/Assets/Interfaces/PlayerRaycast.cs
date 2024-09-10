using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    public GameObject targetObject;
    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray
        {
            origin = Camera.main.transform.position,
            direction = Camera.main.transform.forward
        };

        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 100))
            {
                IClickable clickable = hitInfo.collider.GetComponent<IClickable>();
                targetObject = hitInfo.collider.gameObject; 
            }
        }
    }
}
