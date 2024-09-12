using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GenerationOne : MonoBehaviour
{
    public Transform floors;
    public Transform walls;
    public Transform columns;
    public Transform seeThroughWalls;
    public Transform other;

    public GameObject floorPrefab;
    public GameObject wallPrefab;
    public GameObject columnPrefab;
    public GameObject seeThroughWallPrefab;
    public GameObject gatePrefab;


    public int xSize = 6;
    public int zSize = 6;

    public int xCutoutSize = 2;
    public int zCutoutSize = 2;

    int prefabSize = 5;
    float columnStandOut = 0.3f;

    bool[,] floorArrayCutout;

 //   public GameObject emptyPrefab;
    // Start is called before the first frame update
    private void Awake()
    {
        Floors();
        SeeThroughWalls();
        Walls();
    }

    // Update is called once per frame

    void Floors()
    {
        Vector3 targetPos;
        GameObject newObject;

        floorArrayCutout = new bool[xSize, zSize];

        for (int x = xSize - xCutoutSize; x < xSize; x++)
        {
            for (int z = zSize - zCutoutSize; z < zSize; z++)
            {
                floorArrayCutout[x, z] = true;
            }
        }

        for (int x = 0; x < floorArrayCutout.GetLength(0); x++)
        {
            for (int z = 0; z < floorArrayCutout.GetLength(1); z++)
            {
                if (!floorArrayCutout[x, z])
                {
                    targetPos = new Vector3(x * prefabSize, 0, z * prefabSize);
                    newObject = Instantiate(floorPrefab, targetPos, Quaternion.identity);
                    newObject.transform.parent = floors;
                }
            }
        }
    }

    void Walls()
    {
        int gateXWall = Random.Range(0, 2);
        int coordinate;
        if(gateXWall == 0)
        {
            coordinate = Random.Range(1, xSize -1);
        }
        else
        {
            coordinate = Random.Range(1, zSize -1);
        }

        GameObject newObject;
        Vector3 calculate;

        // wall x
        Vector3 currentPos = Vector3.zero;
        int increment = 0;

        while(increment != xSize)
        {
            if(gateXWall == 0 && increment == coordinate)
            {
                increment++;
                newObject = Instantiate(gatePrefab, currentPos, Quaternion.identity);
                newObject.transform.localEulerAngles = new Vector3(0, 270, 0);
                newObject.transform.parent = other;
            }
            else
            {
                increment++;
                newObject = Instantiate(wallPrefab, currentPos, Quaternion.identity);
                newObject.transform.parent = walls;
            }

            calculate = currentPos;
            calculate.z = columnStandOut;
            newObject = Instantiate(columnPrefab, calculate, Quaternion.identity);
            newObject.transform.parent = columns;

            currentPos.x += 5;
        }

        // wall z
        currentPos = new Vector3(-5, 0, 0);
        increment = 0;
        while(increment != zSize)
        {
            if(gateXWall == 1 && increment == coordinate)
            {
                increment++;
                newObject = Instantiate(gatePrefab, currentPos, Quaternion.identity);
                newObject.transform.parent = other;
            }
            else
            {
                increment++;
                newObject = Instantiate(wallPrefab, currentPos, Quaternion.identity);
                newObject.transform.localEulerAngles = new Vector3(0, 90, 0);
                newObject.transform.parent = walls;
            }

            currentPos.z += 5;

            calculate = currentPos;
            calculate.x += columnStandOut;
            newObject = Instantiate(columnPrefab, calculate, Quaternion.identity);
            newObject.transform.parent = columns;
        }

        calculate = new Vector3(-5+columnStandOut, 0, columnStandOut);
        newObject = Instantiate(columnPrefab, calculate, Quaternion.identity);
        newObject.transform.parent = columns;

    }

    void SeeThroughWalls()
    {
        Vector3 currentPos = new Vector3(xSize * prefabSize -5, 0, 0);
        Vector3 finalPos = new Vector3(-5, 0, zSize * prefabSize);
        GameObject newObject;

        int x = floorArrayCutout.GetLength(0) -1;
        int z = 0;

        // wall z
        bool complete = false;            
        while(!complete)
        {
            newObject = Instantiate(seeThroughWallPrefab, currentPos, Quaternion.identity);
            newObject.transform.localEulerAngles = new Vector3(0, -90, 0);
            currentPos.z += 2.5f;
            newObject.transform.parent = seeThroughWalls;

            newObject = Instantiate(seeThroughWallPrefab, currentPos, Quaternion.identity);
            newObject.transform.localEulerAngles = new Vector3(0, -90, 0);
            currentPos.z += 2.5f;
            newObject.transform.parent = seeThroughWalls;

            z += 1;

            if (floorArrayCutout[x, z])
            {
                complete = true;
            }
        }

        // wall x
        complete = false;
        while (!complete)
        {
            currentPos.x -= 2.5f;
            newObject = Instantiate(seeThroughWallPrefab, currentPos, Quaternion.identity);
            newObject.transform.parent = seeThroughWalls;

            currentPos.x -= 2.5f;
            newObject = Instantiate(seeThroughWallPrefab, currentPos, Quaternion.identity);
            newObject.transform.parent = seeThroughWalls;

            x -= 1;
            if (!floorArrayCutout[x, z])
            {
                complete = true;
            }
        }

        // wall z
        complete = false;
        while (!complete)
        {
            newObject = Instantiate(seeThroughWallPrefab, currentPos, Quaternion.identity);
            newObject.transform.localEulerAngles = new Vector3(0, -90, 0);
            newObject.transform.parent = seeThroughWalls;
            currentPos.z += 2.5f;

            newObject = Instantiate(seeThroughWallPrefab, currentPos, Quaternion.identity);
            newObject.transform.localEulerAngles = new Vector3(0, -90, 0);
            newObject.transform.parent = seeThroughWalls;
            currentPos.z += 2.5f;

            z += 1;

            if (z == zSize)
            {
                complete = true;
            }
        }

        // wall x
        complete = false;
        while (!complete)
        {
            currentPos.x -= 2.5f;
            newObject = Instantiate(seeThroughWallPrefab, currentPos, Quaternion.identity);
            newObject.transform.parent = seeThroughWalls;

            currentPos.x -= 2.5f;
            newObject = Instantiate(seeThroughWallPrefab, currentPos, Quaternion.identity);
            newObject.transform.parent = seeThroughWalls;

            x -= 1;
            if (x == -1)
            {
                complete = true;
            }
        }
    }
}





// if (floorArrayCutout[x - 1, z +1])
//  {
//   complete = true;
//     }
//  }




//   newObject = Instantiate(seeThroughWallPrefab, currentPos, Quaternion.identity);
//   currentPos.x -= 2.5f;








//    while(currentPos != finalPos)
//    {
/*
bool complete = false;
while(currentPos != finalPos)
{
    while (!complete)
    {
        if (wallZ)
        {
            GameObject newObject = Instantiate(seeThroughWallPrefab, currentPos, Quaternion.identity);
            newObject.transform.localEulerAngles = new Vector3(0, -90, 0);
            currentPos.z += 2.5f;

            newObject = Instantiate(seeThroughWallPrefab, currentPos, Quaternion.identity);
            newObject.transform.localEulerAngles = new Vector3(0, -90, 0);
            currentPos.z += 2.5f;

            z += 1;

        }
        else
        {
            GameObject newObject = Instantiate(seeThroughWallPrefab, currentPos, Quaternion.identity);
            currentPos.x += 2.5f;

            newObject = Instantiate(seeThroughWallPrefab, currentPos, Quaternion.identity);
            currentPos.x += 2.5f;

            x += 1;
        }

        if (floorArrayCutout[x-1, z])
        {
            complete = true;
            wallZ = !wallZ;
        }

    }
}
*/




/*
while (currentPos != finalPos)
{
    bool complete = false;
    while (!complete)
    {
        if (wallX)
        {
            if (x == floorArrayCutout.GetLength(0))
            {
                break;
            }
            else if (floorArrayCutout[x, z])
            {
                break;
            }
            currentPos.x += 5;
            x++;
            wallX = !wallX;
        }
        else
        {

        }
    }
}
}
}
*/
//    while(currentPos != finalPos)
//   {
/*
       bool complete = false;
       while(!complete)
       {
           if(wallX)
           {
               if (x == floorArrayCutout.GetLength(0))
               {
                   break;
               }
               else if (floorArrayCutout[x, z])
               {
                   break;
               }
               currentPos.x += 5;
               x++;
               wallX = !wallX;
           }
           else
           {

           }

  //     }


   }

   Debug.Log(currentPos);
*/

//  bool complete = false;
//  targetPos = Vector3.zero;
//   int x = 0;
//  while (!complete)
//  {

//  }
//    Instantiate(seeThroughWallPrefab, targetPos, Quaternion.identity);



/*
for (int x = 0; x < xSize; x++)
{
    // x wall
    targetPos = new Vector3(x * prefabSize, 0, 0);
    newObject = Instantiate(wallPrefab, targetPos, Quaternion.identity);
    newObject.transform.parent = walls;

    // x column
    targetPos = new Vector3(x * prefabSize, 0, 0.25f);
    newObject = Instantiate(columnPrefab, targetPos, Quaternion.identity);
    newObject.transform.parent = columns;

    for (int z = 0; z < zSize; z++)
    {                                                       // floors
        targetPos = new Vector3(x * prefabSize, 0, z * prefabSize);
        newObject = Instantiate(floorPrefab, targetPos, Quaternion.identity);
        newObject.transform.parent = floors;

        if(x == 0)  // y wall
        {
            targetPos.x = -5;
            newObject = Instantiate(wallPrefab, targetPos, Quaternion.identity);
            newObject.transform.localEulerAngles = new Vector3(0,90,0); 
            newObject.transform.parent = walls;

            // y column
            targetPos.x = -4.75f;
            if(z == 0)                      // first column
            {
                targetPos.z = 0.25f;
            }

            newObject = Instantiate(columnPrefab, targetPos, Quaternion.identity);
            newObject.transform.parent = columns;

            if (z == zSize - 1)         // final column
            {
                targetPos.z += prefabSize;
                newObject = Instantiate(columnPrefab, targetPos, Quaternion.identity);
                newObject.transform.parent = columns;
            }
        }
    }
}

for(int x = 0; x < xSize*2;  x++)
{
    targetPos = new Vector3(x * (prefabSize * 0.5f) -5, 0, zSize * prefabSize);
    newObject = Instantiate(seeThroughWallPrefab, targetPos, Quaternion.identity);
    newObject.transform.parent = seeThroughWalls;
}


for (int z = 0; z < zSize * 2; z++)
{
    targetPos = new Vector3(xSize * prefabSize - 5f, 0, z * (prefabSize * 0.5f) + 2.5f);
    newObject = Instantiate(seeThroughWallPrefab, targetPos, Quaternion.identity);
    newObject.transform.localEulerAngles = new Vector3(0, 90, 0);
    newObject.transform.parent = seeThroughWalls;
}

}
}
*/