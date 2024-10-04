using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.Rendering;
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
    public GameObject lightBrazier;

    public List<GameObject> interactableLocationsPrefabs;


    int xSize =10;
    int zSize = 10;

    int xCutoutSize = 5;
    int zCutoutSize = 5;

    int prefabSize = 5;
    float columnStandOut = 0.3f;

    bool[,] floorArrayOccupy;

    public Light mainLight;
    public Light mainLightTwo;


    int x;
    int z;

    Vector2 one = Vector2.zero;
    Vector2 two = Vector2.zero;
    Vector2 three = Vector2.zero;
    Vector2 four = Vector2.zero;

    Vector3 calculate;
    GameObject newObject;
    GameObject targetPrefab;
    bool complete = false;
    int targetPrefabIndex;

    private void Awake()
    {
     //   xSize = Random.Range(7, 9);
      //  zSize = Random.Range(7, 9);

        xCutoutSize = Random.Range(2, 4);
        zCutoutSize = Random.Range(2,4);

        Floors();
    //    SeeThroughWalls();
        Walls();
    //    ContentsOne();
    //    ContentsTwo();
     //   ContentsThree();

        mainLight.gameObject.SetActive(false);
       // mainLightTwo.gameObject.SetActive(false);

        DebugArray();
    }

    void Floors()
    {
        Vector3 targetPos;
        GameObject newObject;

        floorArrayOccupy = new bool[xSize, zSize];

        for (int x = xSize - xCutoutSize; x < xSize; x++)
        {
            for (int z = zSize - zCutoutSize; z < zSize; z++)
            {
                floorArrayOccupy[x, z] = true;
            }
        }

        for (int x = 0; x < floorArrayOccupy.GetLength(0); x++)
        {
            for (int z = 0; z < floorArrayOccupy.GetLength(1); z++)
            {
                if (!floorArrayOccupy[x, z])
                {
                    targetPos = new Vector3(x * prefabSize, 0, z * prefabSize);
                    newObject = Instantiate(floorPrefab, targetPos, Quaternion.identity);
                    newObject.transform.parent = floors;
                    newObject.transform.name = string.Format("{0},{1}", x,z);
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

        int x = floorArrayOccupy.GetLength(0) -1;
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

            if (floorArrayOccupy[x, z])
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
            if (!floorArrayOccupy[x, z])
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

    void ContentsOne()
    {
        x = Random.Range(1, xSize - xCutoutSize -1);
        calculate = new Vector3(x * prefabSize - 2.5f, 0, (zSize-1) * prefabSize + 2.5f);
        newObject = Instantiate(lightBrazier, calculate, Quaternion.identity);
        newObject.transform.parent = other;
        floorArrayOccupy[x,zSize-1] = true;
        newObject.transform.localEulerAngles = new Vector3(0, Random.Range(0, 59), 0);

        /*
        while (!complete)
        {
            newX = Random.Range(1, xSize - xCutoutSize - 1);
            newZ = Random.Range(1, zSize - zCutoutSize - 1);
            if(!(newX == x && newZ == z))
            {
                complete = true;
            }
        }
  

        targetPrefab = interactableLocationsPrefabs[Random.Range(0, interactableLocationsPrefabs.Count)];
        calculate = new Vector3(newX * prefabSize - 2.5f, 0, newZ * prefabSize + 2.5f);

        newObject = Instantiate(targetPrefab, calculate, Quaternion.identity);
        newObject.transform.localEulerAngles = new Vector3(0, Random.Range(0, 59), 0);
        floorArrayOccupy[newX, newZ] = true;
              */


        // ---------



        // ---------    third section



    }

    void ContentsTwo()
    {
        z = Random.Range(1, zSize - zCutoutSize);
        calculate = new Vector3((xSize - 1) * prefabSize - 2.5f, 0, z * prefabSize + 2.5f);
        newObject = Instantiate(lightBrazier, calculate, Quaternion.identity);
        newObject.transform.parent = other;
        floorArrayOccupy[xSize - 1, z] = true;
        newObject.transform.localEulerAngles = new Vector3(0, Random.Range(0, 59), 0);
        one = new Vector2(xSize - 1, z);

               
        complete = false;
        while (!complete)
        {
            z = Random.Range(1, zSize - zCutoutSize);
            x = Random.Range(xSize - xCutoutSize, xSize);
            two = new Vector2(x, z);

            if (one != two)
            {
                complete = true;
            }
        }

        calculate = new Vector3(x * prefabSize - 2.5f, 0, z * prefabSize + 2.5f);

        List<GameObject> availablePrefabs = new List<GameObject>(interactableLocationsPrefabs);
        targetPrefabIndex = Random.Range(0, availablePrefabs.Count);
        targetPrefab = availablePrefabs[targetPrefabIndex];
        availablePrefabs.RemoveAt(targetPrefabIndex);

        newObject = Instantiate(targetPrefab, calculate, Quaternion.identity);
        newObject.transform.localEulerAngles = new Vector3(0, Random.Range(0, 59), 0);
        floorArrayOccupy[x, z] = true;
        newObject.transform.parent = other;


        complete = false;
        while (!complete)
        {
            z = Random.Range(1, zSize - zCutoutSize);
            x = Random.Range(xSize - xCutoutSize, xSize);
            three = new Vector2(x, z);

            if (one != two && one != three)
            {
                complete = true;
            }
        }

        calculate = new Vector3(x * prefabSize - 2.5f, 0, z * prefabSize + 2.5f);

        targetPrefabIndex = Random.Range(0, availablePrefabs.Count);
        targetPrefab = availablePrefabs[targetPrefabIndex];
      //  availablePrefabs.RemoveAt(targetPrefabIndex);

        newObject = Instantiate(targetPrefab, calculate, Quaternion.identity);
        newObject.transform.localEulerAngles = new Vector3(0, Random.Range(0, 59), 0);
        floorArrayOccupy[x, z] = true;
        newObject.transform.parent = other;
     
    }

    void ContentsThree()
    {
        x = Random.Range(1, xSize - xCutoutSize - 1);
        z = Random.Range(1, zSize - zCutoutSize - 1);
        calculate = new Vector3(x * prefabSize - 2.5f, 0, z * prefabSize + 2.5f);
        newObject = Instantiate(lightBrazier, calculate, Quaternion.identity);
        floorArrayOccupy[x, z] = true;
        newObject.transform.localEulerAngles = new Vector3(0, Random.Range(0, 59), 0);
        newObject.transform.parent = other;
        one = new Vector2(x, z);

        complete = false;
        while (!complete)
        {
            x = Random.Range(1, xSize - xCutoutSize - 1);
            z = Random.Range(1, zSize - zCutoutSize - 1);
            two = new Vector2(x, z);

            if (one != two)
            {
                complete = true;
            }
        }

        calculate = new Vector3(x * prefabSize - 2.5f, 0, z * prefabSize + 2.5f);

        List<GameObject> availablePrefabs = new List<GameObject>(interactableLocationsPrefabs);
        targetPrefabIndex = Random.Range(0, availablePrefabs.Count);
        targetPrefab = availablePrefabs[targetPrefabIndex];
        availablePrefabs.RemoveAt(targetPrefabIndex);

        newObject = Instantiate(targetPrefab, calculate, Quaternion.identity);
        newObject.transform.localEulerAngles = new Vector3(0, Random.Range(0, 59), 0);
        floorArrayOccupy[x, z] = true;
        newObject.transform.parent = other;

        complete = false;
        while (!complete)
        {
            x = Random.Range(1, xSize - xCutoutSize - 1);
            z = Random.Range(1, zSize - zCutoutSize - 1);

            three = new Vector2(x, z);

            if (three != one && three != two)
            {
                complete = true;
            }
        }

        calculate = new Vector3(x * prefabSize - 2.5f, 0, z * prefabSize + 2.5f);

        targetPrefabIndex = Random.Range(0, availablePrefabs.Count);
        targetPrefab = availablePrefabs[targetPrefabIndex];
        availablePrefabs.RemoveAt(targetPrefabIndex);

        newObject = Instantiate(targetPrefab, calculate, Quaternion.identity);
        newObject.transform.localEulerAngles = new Vector3(0, Random.Range(0, 59), 0);
        floorArrayOccupy[x,z] = true;
        newObject.transform.parent = other;

        complete = false;
        while (!complete)
        {
            x = Random.Range(1, xSize - xCutoutSize - 1);
            z = Random.Range(1, zSize - zCutoutSize - 1);

            four = new Vector2(x, z);

            if (four != one && four != two && four != three)
            {
                complete = true;
            }
        }

        calculate = new Vector3(x * prefabSize - 2.5f, 0, z * prefabSize + 2.5f);

        targetPrefabIndex = Random.Range(0, availablePrefabs.Count);
        targetPrefab = availablePrefabs[targetPrefabIndex];

        newObject = Instantiate(targetPrefab, calculate, Quaternion.identity);
        newObject.transform.localEulerAngles = new Vector3(0, Random.Range(0, 59), 0);
        floorArrayOccupy[x, z] = true;
        newObject.transform.parent = other;


    }
    void DebugArray()
    {
        for (int j = 0; j < floorArrayOccupy.GetLength(1); j++)
        {
            for (int i = 0; i < floorArrayOccupy.GetLength(0); i++)
            {
                var msg = "[" + i.ToString() + ", " + j.ToString() + "] = " + floorArrayOccupy[i, j].ToString();
              //  Debug.Log(msg);
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