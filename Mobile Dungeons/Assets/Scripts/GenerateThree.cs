using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GenerateThree : MonoBehaviour
{
    public GameObject floorPrefab;
    float floorSize = 5;

    public GameObject wallPrefab;
    public GameObject columnPrefab;
    public GameObject playerPrefab;

    int[,] floorArray;
    Contents[,] contentsArray;

    float columnRegular = 0.4f;
    float columnDoors = 0.2f;
    float columnCorner = 0.6f;

    float corner = 0.35f;
    float wall = 0.2f;
    float door = 0.25f;
    /*

    MeshRenderer[,,] xCutWallRenderersArray;
    MeshRenderer[,] xWallRenderersArray;
    float[,] xWallTransition;

    MeshRenderer[,,] yCutWallRenderersArray;
    MeshRenderer[,] yWallRendersArray;
    float[,] yWallTransition;
    */

    Player player;
    public AnimationCurve curve;

    public Vector2 testVector = Vector2.zero;
    

    // Start is called before the first frame update
    void Awake()
    {
        GenerateArray(40,41);
        GenerateFloor();
        GenerateWalls();
        GenerateColumns();
     //   DebugColumnsArray();

        GameObject playerObject = Instantiate(playerPrefab, new Vector3(1 * floorSize -2.5f, 0, 1 * floorSize +2.5f), Quaternion.identity);
        player = playerObject.GetComponent<Player>();
        player.InitialiseCoordinate(new Vector2(1,1));
    }

    // Update is called once per frame

    private void Update()
    {
        Vector2 playerCoordinate = player.currentCoordinate;
        int x = (int)playerCoordinate.x;
        int y = (int)playerCoordinate.y;
        UpdateContents();

    }

    void UpdateContents()
    {
        for (int i = 0; i < contentsArray.GetLength(0); i++)
        {
            for(int j = 0; j < contentsArray.GetLength(1); j++)
            {
                Contents targetContents = contentsArray[i,j];
           //     bool boolean = false;
                if(testVector.x > i)
                {
                  //  targetContents.xWall 
                }


            }
        }
    }

  //  void SetAlphaOfMaterial(MeshRenderer rend, float value)
   // {
        //rendersList[i].material.SetFloat("_Float", shaderEvaluate * 0.05f);
    //    rend.material.SetFloat("Alpha", value);
   // }

    void GenerateArray(int sizeX, int sizeY)
    {
        floorArray = new int[sizeX, sizeY];
        contentsArray = new Contents[sizeX+1, sizeY+1];
     //   xCutWallRenderersArray = new MeshRenderer[21,21,2];
     //   xWallTransition = new float[21, 21]; 

        //  xWallRenderersArray = new MeshRenderer[21,21];
        //  yWallTransition = new float[21, 21];

        for (int x = 1; x < 5; x++)
        {
            for (int y = 1; y < 5; y++)
            {
                floorArray[x, y] = 2;
            }
        }

        for(int x = 10; x < 15; x++)
        {
            for(int y = 10; y < 15; y++)
            {
                floorArray[x, y] = 2;
            }
        }

        //----------

        for(int y = 5; y < 12; y++)
        {
            floorArray[3, y] = 1;   
        }

        for(int x = 3; x < 10; x++)
        {
            floorArray[x, 12] = 1;
        }

        //----------

        for(int y = 9; y > 5;  y--)
        {
            floorArray[11,y] = 1;
        }

        //-----

        for(int x = 15; x < 18;  x++)
        {
            floorArray[x, 13] = 1;
        }

        for(int y = 15; y < 18; y++)
        {
            floorArray[13, y] = 1;
        }

        for(int x = 12; x < 16; x++)
        {
            floorArray[x, 6] = 1;
        }

        for(int x = 16; x < 20; x++)
        {
            for(int y = 4; y < 9; y++)
            {
                floorArray[x, y] = 5;
            }
        }

        // -------

        for(int y = 13; y < 17; y++)
        {
            floorArray[17,y] = 1;
        }

        for(int x = 16; x < 20; x++)
        {
            for(int y = 17; y < 21; y++)
            {
                floorArray[x,y] = 5;
            }
        }

        // --------

        for(int x = 13; x > 7; x--)
        {
            floorArray[x,17] = 1;
        }


        for(int y = 18; y > 14;  y--)
        {
            for (int x = 7; x > 3; x--)
            {
                floorArray[x, y] = 5;
            }
        }
    }

    void InitialiseContentsArray(int x, int y)
    {
        contentsArray[x, y] = new Contents();
    }

    void CheckAndInitialiseContentsArray(int x, int y)
    {
        if(contentsArray[x, y] == null)
        {
            contentsArray[x, y] = new Contents();
        }
    }

    void UpdateContentsColumn(int x, int y, GameObject targetObject)
    {
        Contents targetContents = contentsArray[x, y];
        targetContents.column = targetObject.transform.GetChild(1).GetComponent<MeshRenderer>();

    }

    void CheckAndUpdateContentsColumn(int x, int y, GameObject targetObject)
    {
        Contents targetContents = contentsArray[x, y];
        if(targetContents.column == null)
        {
            targetContents.column = targetObject.GetComponent<MeshRenderer>();
        }

    }


    void GenerateFloor()
    {
        for(int x = 0; x < floorArray.GetLength(0); x++)
        {
            for(int y = 0; y < floorArray.GetLength(1); y++)
            {
                if(floorArray[x, y] != 0)
                {
                    Vector3 calculate = new Vector3(x * floorSize, 0, y * floorSize);
                    GameObject newObj = Instantiate(floorPrefab, calculate, Quaternion.identity);
                    newObj.transform.name = x + ":" + y;

                    contentsArray[x, y] = new Contents();
                }
            }
        }
    }

    void GenerateWalls()
    {
        Vector3 calculate;
        GameObject newObj;
        for (int x = 0; x < floorArray.GetLength(0); x++)
        {
            for (int y = 0; y < floorArray.GetLength(1); y++)
            {
                if (floorArray[x, y] != 0)
                {
                    InitialiseContentsArray(x, y);

                    if (floorArray[x - 1, y] == 0)
                    {
                        calculate = new Vector3(x * floorSize - 5, 0, y * floorSize);
                        newObj = Instantiate(wallPrefab, calculate, Quaternion.identity);
                        calculate = new Vector3(0, 90, 0);
                        newObj.transform.eulerAngles = calculate;
                        newObj.transform.name = x + ":" + y;

                        contentsArray[x, y].xWall = newObj.transform.GetChild(0).GetComponent<MeshRenderer>();
                        MeshRenderer[] meshRends = new MeshRenderer[2];
                        meshRends[0] = newObj.transform.GetChild(1).GetComponent<MeshRenderer>();
                        meshRends[1] = newObj.transform.GetChild(2).GetComponent<MeshRenderer>();
                        contentsArray[x, y].xCutWalls = meshRends;
                    }

                    if (floorArray[x, y - 1] == 0)
                    {
                        calculate = new Vector3(x * floorSize, 0, y * floorSize);
                        newObj = Instantiate(wallPrefab, calculate, Quaternion.identity);
                        newObj.transform.name = x + ":" + y;

                        InitialiseContentsArray(x, y);

                        contentsArray[x, y].yWall = newObj.transform.GetChild(0).GetComponent<MeshRenderer>();
                        MeshRenderer[] meshRends = new MeshRenderer[2];
                        meshRends[0] = newObj.transform.GetChild(1).GetComponent<MeshRenderer>();
                        meshRends[1] = newObj.transform.GetChild(2).GetComponent<MeshRenderer>();
                        contentsArray[x, y].xCutWalls = meshRends;

                        // column
                        //corner

                    }

                    if (floorArray[x + 1, y] == 0)
                    {
                        calculate = new Vector3(x * floorSize, 0, y * floorSize + 5);
                        newObj = Instantiate(wallPrefab, calculate, Quaternion.identity);
                        calculate = new Vector3(0, -90, 0);
                        newObj.transform.eulerAngles = calculate;
                        newObj.transform.name = x + 1 + ":" + y;

                        InitialiseContentsArray(x + 1, y);

                        contentsArray[x + 1, y].xWall = newObj.transform.GetChild(0).GetComponent<MeshRenderer>();
                        MeshRenderer[] meshRends = new MeshRenderer[2];
                        meshRends[0] = newObj.transform.GetChild(1).GetComponent<MeshRenderer>();
                        meshRends[1] = newObj.transform.GetChild(2).GetComponent<MeshRenderer>();
                        contentsArray[x + 1, y].xCutWalls = meshRends;

                    }

                    if (floorArray[x, y + 1] == 0)
                    {
                        calculate = new Vector3(x * floorSize - 5, 0, y * floorSize + 5);
                        newObj = Instantiate(wallPrefab, calculate, Quaternion.identity);
                        calculate = new Vector3(0, 180, 0);
                        newObj.transform.eulerAngles = calculate;
                        newObj.transform.name = x + ":" + y;

                        InitialiseContentsArray(x, y + 1);

                        contentsArray[x, y + 1].xWall = newObj.transform.GetChild(0).GetComponent<MeshRenderer>();
                        MeshRenderer[] meshRends = new MeshRenderer[2];
                        meshRends[0] = newObj.transform.GetChild(1).GetComponent<MeshRenderer>();
                        meshRends[1] = newObj.transform.GetChild(2).GetComponent<MeshRenderer>();
                        contentsArray[x, y + 1].xCutWalls = meshRends;
                    }
                }
            }
        }
    }

    void GenerateColumns()
    {
        Vector3 calculate;
        GameObject newObj;
        for (int x = 0; x < floorArray.GetLength(0); x++)
        {
            for (int y = 0; y < floorArray.GetLength(1); y++)
            {
                if (floorArray[x, y] > 1)
                {
                    //   InitialiseContentsArray(x, y);

                    // entrance to tunnel columns


                    if (floorArray[x - 1, y] == 1)
                    {
                        InstantiateColumn(x, y + 1, 0, 0);
                        InstantiateColumn(x, y, 0, 0);
                    }
                    else if (floorArray[x + 1, y] == 1)
                    {
                        InstantiateColumn(x+1,y,0, 0);
                        InstantiateColumn(x+1, y+1, 0, 0);
                    }
                    else if (floorArray[x, y + 1] == 1)
                    {
                        InstantiateColumn(x, y + 1, 0, 0);
                        InstantiateColumn(x + 1, y + 1, 0, 0);
                        /*
                        calculate = new Vector3((x - 1) * floorSize, 0, (y + 1) * floorSize);
                        newObj = Instantiate(columnPrefab, calculate, Quaternion.identity);
                        newObj.transform.name = ((x) + ":" + (y + 1));
                        CheckAndInitialiseContentsArray(x, y + 1);
                        UpdateContentsColumn(x, y + 1, newObj);

                        calculate = new Vector3((x) * floorSize, 0, (y + 1) * floorSize);
                        newObj = Instantiate(columnPrefab, calculate, Quaternion.identity);
                        newObj.transform.name = ((x + 1) + ":" + (y + 1));
                        CheckAndInitialiseContentsArray(x + 1, y + 1);
                        UpdateContentsColumn(x + 1, y + 1, newObj);
                        */
                    }
                    else if (floorArray[x, y - 1] == 1)
                    {
                        InstantiateColumn(x,y,0,0);
                        InstantiateColumn(x+1,y,0,0);
                        /*

                        calculate = new Vector3((x - 1) * floorSize, 0, (y) * floorSize);
                        newObj = Instantiate(columnPrefab, calculate, Quaternion.identity);
                        newObj.transform.name = ((x) + ":" + (y));
                        CheckAndInitialiseContentsArray(x, y);
                        UpdateContentsColumn(x, y, newObj);

                        calculate = new Vector3((x) * floorSize, 0, (y) * floorSize);
                        newObj = Instantiate(columnPrefab, calculate, Quaternion.identity);
                        newObj.transform.name = ((x + 1) + ":" + (y));
                        */

                    }




                    // room corners
                    
                    if (floorArray[x+1,y+1] == 0 && floorArray[x+1,y] == 0 && floorArray[x,y+1] == 0)
                    {
                        //   calculate = new Vector3((x) * floorSize, 0, (y+1) * floorSize);
                        //    newObj = Instantiate(columnPrefab, calculate, Quaternion.identity);
                        //   newObj.transform.name = ((x + 1) + ":" + (y+1));
                        InstantiateColumn(x+1, y+1, 0, 0);
                    }
                    else if (floorArray[x+1, y-1] == 0 && floorArray[x+1,y] == 0 && floorArray[x,y-1] == 0)
                    {
                        //  calculate = new Vector3((x) * floorSize, 0, (y) * floorSize);
                        //    newObj = Instantiate(columnPrefab, calculate, Quaternion.identity);
                        //   newObj.transform.name = ((x + 1) + ":" + (y));
                        InstantiateColumn(x+1, y, 0, 0);
                    }
                    else if (floorArray[x-1, y+1] == 0 && floorArray[x-1,y] == 0 && floorArray[x,y+1] == 0)
                    {
                        InstantiateColumn(x, y+1, 0, 0);
                        //   calculate = new Vector3((x-1) * floorSize, 0, (y+1) * floorSize);
                        //   newObj = Instantiate(columnPrefab, calculate, Quaternion.identity);
                        //   newObj.transform.name = (((x) + ":" + (y + 1)));
                    }
                    else if (floorArray[x-1, y-1] == 0 && floorArray[x-1,y] == 0 && floorArray[x,y-1] == 0)
                    {
                        InstantiateColumn(x,y,0,0);
                    //    calculate = new Vector3((x - 1) * floorSize, 0, (y) * floorSize);
                     //   newObj = Instantiate(columnPrefab, calculate, Quaternion.identity);
                     //   newObj.transform.name = (x + ":" + y);
                    }

                    //---------------- regular
                    if (floorArray[x + 1, y] == 0)
                    {
                        InstantiateColumnTwo(x + 1, y, columnRegular, 0);
                    }

                    /*
                    if (floorArray[x,y-1] == 0)
                    {
                        InstantiateColumnTwo(x, y, 0, 0);
                    }
                    else if (floorArray[x-1,y] == 0)
                    {
                        InstantiateColumnTwo(x, y, 0, 0);
                    }
                    else if (floorArray[x,y+1] == 0)
                    {
                        InstantiateColumnTwo(x,y+1,0,0); 
                    }

                    */


                    /*
                    if(!southWest && west && south)  // corner
                    {
                        calculate = new Vector3((x - 1) * floorSize + corner, 0, y * floorSize + corner);
                        newObj = Instantiate(columnPrefab, calculate, Quaternion.identity);
                        newObj.transform.name = x + ":" + y + " COLUMN NEW";
                    }
                    else if(!south && southWest && west)
                    {
                        calculate = new Vector3((x - 1) * floorSize -door , 0, y * floorSize + door);
                        newObj = Instantiate(columnPrefab, calculate, Quaternion.identity);
                        newObj.transform.name = x + ":" + y + " COLUMN NEW";
                    }
                    else if(!south && !west)    //corner
                    {
                        calculate = new Vector3((x - 1) * floorSize + corner, 0, y * floorSize + corner);
                        newObj = Instantiate(columnPrefab, calculate, Quaternion.identity);
                        newObj.transform.name = x + ":" + y + " COLUMN NEW";
                    }
                    else if(!south)     // regular
                    {
                        calculate = new Vector3((x - 1) * floorSize, 0, y * floorSize + wall);
                        newObj = Instantiate(columnPrefab, calculate, Quaternion.identity);
                        newObj.transform.name = x + ":" + y + " COLUMN NEW";
                    }
                    */

                    /*


                    if (floorArray[x - 1, y - 1] == 0)
                    {
                        if (floorArray[x - 1, y] != 0 && floorArray[x, y - 1] != 0)
                        {
                            calculate = new Vector3((x - 1) * floorSize, 0, y * floorSize);
                            newObj = Instantiate(columnPrefab, calculate, Quaternion.identity);
                            newObj.transform.name = x + ":" + y + " COLUMN NEW";
                        }
                    }

                    */


                    /*

                    if (floorArray[x, y - 1] == 0)
                    {

                        if (floorArray[x - 1, y] == 0)
                        {
                            calculate = new Vector3((x - 1) * floorSize + columnCorner, 0, y * floorSize + columnCorner);
                            newObj = Instantiate(columnPrefab, calculate, Quaternion.identity);
                            newObj.transform.name = x + ":" + y + " COLUMN";
                        }

                        if (floorArray[x + 1, y] == 0)
                        {
                            calculate = new Vector3(x * floorSize - columnChange, 0, y * floorSize + columnCorner);
                            newObj = Instantiate(columnPrefab, calculate, Quaternion.identity);
                            newObj.transform.name = (x + 1) + ":" + y + " COLUMN";
                        }
                        

                    }


                    if (floorArray[x, y + 1] == 0)
                    {
                        if (floorArray[x - 1, y] == 0)
                        {
                            calculate = new Vector3((x - 1) * floorSize + columnCorner, 0, (y + 1) * floorSize - columnCorner);
                            newObj = Instantiate(columnPrefab, calculate, Quaternion.identity);
                            newObj.transform.name = x + ":" + (y + 1) + " COLUMN";

                            //   newObj.transform.eulerAngles = new Vector3(0, 90, 0);
                        }

                        if (floorArray[x + 1, y] == 0)
                        {
                            calculate = new Vector3(x * floorSize - columnCorner, 0, (y + 1) * floorSize - columnCorner);
                            newObj = Instantiate(columnPrefab, calculate, Quaternion.identity);
                            newObj.transform.name = (x + 1) + ":" + (y + 1) + " COLUMN";
                            //     newObj.transform.eulerAngles = new Vector3(0, 90, 0);
                        }
                    }

                    if (floorArray[x - 1, y + 1] == 0)
                    {
                        if (floorArray[x - 1, y] != 0 && floorArray[x, y + 1] != 0)
                        {
                            calculate = new Vector3((x - 1) * floorSize, 0, (y + 1) * floorSize);
                            newObj = Instantiate(columnPrefab, calculate, Quaternion.identity);
                            newObj.transform.name = x + ":" + (y + 1) + " COLUMN NEW";
                        }
                    }

                    if (floorArray[x + 1, y + 1] == 0)
                    {
                        if (floorArray[x, y + 1] != 0 && floorArray[x + 1, y] != 0)
                        {
                            calculate = new Vector3((x) * floorSize, 0, (y + 1) * floorSize);
                            newObj = Instantiate(columnPrefab, calculate, Quaternion.identity);
                            newObj.transform.name = (x + 1) + ":" + (y + 1) + "COLUMN NEW";
                        }
                    }

                    if (floorArray[x + 1, y - 1] == 0)
                    {
                        if (floorArray[x, y - 1] != 0 && floorArray[x + 1, y] != 0)
                        {
                            calculate = new Vector3(x * floorSize, 0, y * floorSize);
                            newObj = Instantiate(columnPrefab, calculate, Quaternion.identity);
                            newObj.transform.name = (x + 1) + ":" + y + "COLUMN NEW";
                        }
                    }

               
                    if (floorArray[x - 1, y - 1] == 0)
                    {
                        if (floorArray[x - 1, y] != 0 && floorArray[x, y - 1] != 0)
                        {
                            calculate = new Vector3((x - 1) * floorSize, 0, y * floorSize);
                            newObj = Instantiate(columnPrefab, calculate, Quaternion.identity);
                            newObj.transform.name = x + ":" + y + " COLUMN NEW";
                        }
                    }
                    */

                }
            }
        }
    }

    void InstantiateColumn(int x, int y, float xDifference, float yDifference)
    {
        Vector3 calculate = new Vector3((x-1) * floorSize + xDifference, 0, y * floorSize + yDifference);
        GameObject newObj = Instantiate(columnPrefab, calculate, Quaternion.identity);
        newObj.transform.name = (x + ":" + y + " COLUMN");
        CheckAndInitialiseContentsArray(x,y);
        UpdateContentsColumn(x,y,newObj);
    }

    void InstantiateColumnTwo(int x, int y, float xDifference, float yDifference)
    {
        Contents targetContents = contentsArray[x,y];
        if(targetContents == null)
        {
            Debug.Log(Time.time);
            contentsArray[x, y] = new Contents();
            Vector3 calculate = new Vector3((x - 1) * floorSize + xDifference, 0, y * floorSize + yDifference);
            GameObject newObj = Instantiate(columnPrefab, calculate, Quaternion.identity);
            newObj.transform.name = (x + ":" + y + " COLUMN");
            UpdateContentsColumn(x, y, newObj);
        }
        else
        {
            MeshRenderer meshRenderer = contentsArray[x, y].column;
            Debug.Log(meshRenderer);
            if(meshRenderer == null)
            {
                Vector3 calculate = new Vector3((x - 1) * floorSize + xDifference, 0, y * floorSize + yDifference);
                GameObject newObj = Instantiate(columnPrefab, calculate, Quaternion.identity);
                newObj.transform.name = (x + ":" + y + " COLUMN");
                UpdateContentsColumn(x, y, newObj);
            }
        }
    }

    void DebugArray(int[,] floorArray)
    {
        string text = "";
        string textTwo = "";

        //   Debug.Log(floorArray.GetLength(0));
        //   Debug.Log(floorArray.GetLength(1));

        for (int x = 0; x < floorArray.GetLength(0); x++)
        {
            textTwo = "";
            for (int y = 0; y < floorArray.GetLength(1); y++)
            {
                //   Debug.Log(Time.time);
                if (y == 0)
                {
                    //   Debug.Log(Time.time);
                    textTwo = textTwo += "Y:" + x + " ";
                }
                textTwo += string.Format("[{0}] ", floorArray[x, y]);
            }
            textTwo += "\n";
            text += text;
        }

        Debug.Log(text);

    }

    void DebugArrayTwo(int[,] floorArray)
    {
        string finalText = "";
        string tempText;
        for (int y = floorArray.GetLength(1) - 1; y > -1; y--)
        {
            tempText = "";
            for (int x = 0; x < floorArray.GetLength(0); x++)
            {
                tempText += floorArray[x, y].ToString();
            }
            finalText += tempText;
            finalText += "\n";
        }

        //   tempText = "x: ";
        //   for (int x = 0; x < floorArray.GetLength(0); x++)
        //  {
        //     tempText += x + " ";
        //  }

        // finalText += tempText;

        Debug.Log(finalText);

    }

    void DebugColumnsArray()
    {
        string finalText = "";
        string tempText;
        for (int y = contentsArray.GetLength(1) - 1; y > -1; y--)
        {
            tempText = "";
            for (int x = 0; x < contentsArray.GetLength(0); x++)
            {
                Contents targetContents = contentsArray[x, y];
                if(targetContents == null)
                {
                    tempText += 0;
                }
                else
                {
                    if(targetContents.column != null)
                    {
                        tempText += 2;
                    }
                    else
                    {
                        tempText += 1;
                    }
                }
             //   tempText += floorArray[x, y].ToString();
            }
            finalText += tempText;
            finalText += "\n";
        }

        //   tempText = "x: ";
        //   for (int x = 0; x < floorArray.GetLength(0); x++)
        //  {
        //     tempText += x + " ";
        //  }

        // finalText += tempText;

        Debug.Log(finalText);
    }
}

        /*
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
        */