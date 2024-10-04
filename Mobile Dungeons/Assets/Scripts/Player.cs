using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 currentCoordinate;
    // Start is called before the first frame update

    public void InitialiseCoordinate(Vector2 currentCoordinate)
    {
        this.currentCoordinate = currentCoordinate;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            currentCoordinate.x -= 1;
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            currentCoordinate.y -= 1;
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            currentCoordinate.x += 1;
        }
        else if(Input.GetKeyDown(KeyCode.W))
        {
            currentCoordinate.y += 1;
        }

        Vector3 calculate = new Vector3(currentCoordinate.x * 5 - 2.5f, 0, currentCoordinate.y * 5 + 2.5f);
        transform.position = calculate;
            
      //  transform.position 
    }
}
//(1 * floorSize -2.5f, 0, 1 * floorSize +2.5f)