using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class LevelGenerator : MonoBehaviour
{
    private List<GameObject> tileList;

    [SerializeField]
    private GameObject[] tiles;

    public float TileSize
    {
        get { return tiles[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }

    private void Start()
    {
        tileList = new List<GameObject>();
        GenerateLevel();
    }

    private void Update()
    {
        
    }

    private void GenerateLevel()
    {
        string[] levelData = ReadLevelMap();

        int levelX = levelData[0].ToCharArray().Length;

        int levelY = levelData.Length;

        Vector3 worldStart = new Vector3(-50.0f, 7.5f, 10.0f);

        for (int i = 0; i < levelY; i++)
        {
            char[] newTiles = levelData[i].ToCharArray();

            for (int j = 0; j < levelX; j++)
            {
                PlaceTile(newTiles[j].ToString(), j, i, worldStart);
            }
        }

        GameObject[] tileArray = tileList.ToArray();

        for (int i = 0; i < tileArray.Length; i++)
        {
            // Beginning with 1 - Outside Corner
            // If there are outside walls to your right and up
            if (!(i - 14 < 0) && tileArray[i].name.Contains("Outside Corner") && tileArray[i + 1].name.Contains("Outside Wall") && tileArray[i - 14].name.Contains("Outside Wall"))
                tileArray[i].transform.Rotate(0, 0, 90);
            // If there are outside walls to your left and up
            if (!(i - 14 < 0) && tileArray[i].name.Contains("Outside Corner") && tileArray[i - 1].name.Contains("Outside Wall") && tileArray[i - 14].name.Contains("Outside Wall"))
                tileArray[i].transform.Rotate(0, 0, 180);
            // If there are outside walls to your left and down
            if ((i + 14) <= tileArray.Length && !(i - 1 < 0) && tileArray[i].name.Contains("Outside Corner") && tileArray[i - 1].name.Contains("Outside Wall") && tileArray[i + 14].name.Contains("Outside Wall"))
                tileArray[i].transform.Rotate(0, 0, 270);

            // Now for 2 - Outside Wall
            // If there is an outside corner above you or below you
            if ((i + 14) <= tileArray.Length && !(i - 14 < 0) && tileArray[i].name.Contains("Outside Wall") && (tileArray[i - 14].name.Contains("Outside Corner") || tileArray[i + 14].name.Contains("Outside Corner")))
                tileArray[i].transform.Rotate(0, 0, 90);
            // If there is an outside wall above you or below you 
            if ((i + 14) <= tileArray.Length && !(i - 14 < 0) && tileArray[i].name.Contains("Outside Wall") && (tileArray[i - 14].name.Contains("Outside Wall") || tileArray[i + 14].name.Contains("Outside Wall")) && tileArray[i].transform.eulerAngles.z != 90)
                tileArray[i].transform.Rotate(0, 0, 90);

            // Now for 3 - Inside Corner
            // If there are inside walls to your right and up but not left
            if (!(i - 14 < 0) && tileArray[i].name.Contains("Inside Corner") && tileArray[i + 1].name.Contains("Inside Wall") && tileArray[i - 14].name.Contains("Inside Wall") && !(tileArray[i - 1].name.Contains("Inside Wall")))
                tileArray[i].transform.Rotate(0, 0, 90);
            // If there are inside walls to your left and up but not down or right
            if (!(i - 14 < 0) && tileArray[i].name.Contains("Inside Corner") && tileArray[i - 1].name.Contains("Inside Wall") && tileArray[i - 14].name.Contains("Inside Wall") && !(tileArray[i + 14].name.Contains("Inside Wall")) && !(tileArray[i + 1].name.Contains("Inside Wall")))
                tileArray[i].transform.Rotate(0, 0, 180);
            // If there are inside walls to your left and down but not up or right
            if ((i + 14) <= tileArray.Length && !(i - 1 < 0) && tileArray[i].name.Contains("Inside Corner") && tileArray[i - 1].name.Contains("Inside Wall") && tileArray[i + 14].name.Contains("Inside Wall") && !(tileArray[i - 14].name.Contains("Inside Wall")) && !(tileArray[i + 1].name.Contains("Inside Wall")))
                tileArray[i].transform.Rotate(0, 0, 270);
            // If there are inside walls to your left and up and also down and the left diagonal is horizontal 
            if (!(i - 14 < 0) && tileArray[i].name.Contains("Inside Corner") && tileArray[i - 1].name.Contains("Inside Wall") && tileArray[i - 14].name.Contains("Inside Wall") && tileArray[i + 14].name.Contains("Inside Wall") && (tileArray[i - 15].transform.eulerAngles.z == 0 || tileArray[i - 15].transform.eulerAngles.z == 180))
                tileArray[i].transform.Rotate(0, 0, 270);
            // If there is an inside corner above you and it and you are normal rotation and there is an inside wall to your right and right diagonal
            if (tileArray[i].name.Contains("Inside Corner") && tileArray[i - 14].name.Contains("Inside Corner") && tileArray[i].transform.eulerAngles.z == 0 && tileArray[i - 14].transform.eulerAngles.z == 0 && tileArray[i + 1].name.Contains("Inside Wall") && tileArray[i - 13].name.Contains("Inside Wall") && !(tileArray[i - 1].name.Contains("Inside Wall")))
                tileArray[i].transform.Rotate(0, 0, 90);
            // If there is an inside corner above you and it and you are normal rotation and there is an inside wall to your left but not right
            if (tileArray[i].name.Contains("Inside Corner") && tileArray[i - 14].name.Contains("Inside Corner") && tileArray[i].transform.eulerAngles.z == 0 && tileArray[i - 14].transform.eulerAngles.z == 0 && tileArray[i - 1].name.Contains("Inside Wall") && !(tileArray[i + 1].name.Contains("Inside Wall")))
            {
               tileArray[i].transform.Rotate(0, 0, 180);
                tileArray[i - 14].transform.Rotate(0, 0, 270);
            }
            // If there is an inside corner to your right and an inside wall below you
            if (tileArray[i].name.Contains("Inside Corner") && tileArray[i + 1].name.Contains("Inside Corner") && tileArray[i + 14].name.Contains("Inside Wall"))
                tileArray[i + 1].transform.Rotate(0, 0, 270);
            // If there is an inside corner to your right and an inside wall above you
            if (tileArray[i].name.Contains("Inside Corner") && tileArray[i + 1].name.Contains("Inside Corner") && tileArray[i - 14].name.Contains("Inside Wall"))
            {
                tileArray[i].transform.Rotate(0, 0, 90);
                tileArray[i + 1].transform.Rotate(0, 0, 180);
            }
            // If you are on the edge of the map and there is only an inner wall above you 
            if (((i == 13) || (i + 1) % 14 == 0) && tileArray[i].name.Contains("Inside Corner") && tileArray[i - 14].name.Contains("Inside Wall") && tileArray[i - 14].transform.eulerAngles.z != 0 && tileArray[i - 14].transform.eulerAngles.z != 180)
                tileArray[i].transform.Rotate(0, 0, 90);
            // If you part of a T complex
            if (tileArray[i].name.Contains("Inside Corner") && tileArray[i + 14].name.Contains("Inside Corner") && tileArray[i - 1].name.Contains("Inside Wall") && tileArray[i + 1].name.Contains("Inside Wall"))
                tileArray[i].transform.Rotate(0, 0, 90);
            
            // Now for 4 - Inside Wall
            // If there is an outside wall above you and below you 
            if ((i + 14) <= tileArray.Length && !(i - 14 < 0) && tileArray[i].name.Contains("Inside Wall") && tileArray[i - 14].name.Contains("Inside Wall") && tileArray[i + 14].name.Contains("Inside Wall") && (tileArray[i].transform.eulerAngles.z == 0 || tileArray[i].transform.eulerAngles.z != 180))
                tileArray[i].transform.Rotate(0, 0, 90);
            // If there is a T Junction above you with an euler angle of (0, 0, 0)
            if (!(i - 14 < 0) && tileArray[i].name.Contains("Inside Wall") && tileArray[i - 14].name.Contains("T Junction") && (tileArray[i].transform.eulerAngles.z == 0 || tileArray[i].transform.eulerAngles.z == 180))
                tileArray[i].transform.Rotate(0, 0, 90);
            // If there is an inside wall above and below you and you are not rotated and don't have a corner to your side
            if ((i + 14) <= tileArray.Length && !(i - 14 < 0) && tileArray[i].name.Contains("Inside Wall") && (tileArray[i - 14].name.Contains("Inside Wall") || tileArray[i + 14].name.Contains("Inside Wall")) && (tileArray[i].transform.eulerAngles.z == 0 || tileArray[i].transform.eulerAngles.z == 180) && !(tileArray[i + 1].name.Contains("Inside Corner") || tileArray[i - 1].name.Contains("Inside Corner")))
                tileArray[i].transform.Rotate(0, 0, 90);
            // If there is an inside wall left or right of you but not up or left and you are rotated 
            if (tileArray[i].name.Contains("Inside Wall") && tileArray[i - 1].name.Contains("Inside Wall") && tileArray[i + 1].name.Contains("Inside Wall") && (tileArray[i].transform.eulerAngles.z == 90 || tileArray[i].transform.eulerAngles.z == 270) && (!(tileArray[i + 14].name.Contains("Inside Wall")) || !(tileArray[i - 14].name.Contains("Inside Wall"))))
                tileArray[i].transform.Rotate(0, 0, 90); 
            // If there is an inside corner above or below you and you are not rotated
            if ((i + 14) <= tileArray.Length && !(i - 14 < 0) && tileArray[i].name.Contains("Inside Wall") && (tileArray[i - 14].name.Contains("Inside Corner") || tileArray[i + 14].name.Contains("Inside Corner")) && (tileArray[i].transform.eulerAngles.z == 0 || tileArray[i].transform.eulerAngles.z == 180) && !(tileArray[i - 1].name.Contains("Inside Wall")))
                tileArray[i].transform.Rotate(0, 0, 90);
            // If you have an inside wall above you and are on the bottom row
            if ((i + 14) > tileArray.Length && tileArray[i].name.Contains("Inside Wall") && tileArray[i - 14].name.Contains("Inside Wall"))
                tileArray[i].transform.Rotate(0, 0, 90);
        }
    }

    private void PlaceTile(string tileType, int x, int y, Vector3 worldStart)
    {
        int tileIndex = int.Parse(tileType);

        GameObject newTile = Instantiate(tiles[tileIndex]);

        tileList.Add(newTile);
        

        newTile.transform.position = new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y));
               
    }

    private string[] ReadLevelMap()
    {
        TextAsset bindData = Resources.Load("PacMan") as TextAsset;

        string data = bindData.text.Replace(Environment.NewLine, string.Empty);

        return data.Split('-');
    }
}
