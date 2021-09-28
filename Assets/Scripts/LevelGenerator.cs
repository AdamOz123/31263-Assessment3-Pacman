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
            if (tileArray[i].name.Contains("Outside Corner") && tileArray[i + 14].name.Contains("Outside Wall"))
                tileArray[i + 14].transform.Rotate(0, 0, 90);
            if (tileArray[i].name.Contains("Outside Wall") && tileArray[i + 14].name.Contains("Outside Wall"))
                tileArray[i + 14].transform.Rotate(0, 0, 90);
            if (tileArray[i].name.Contains("T Junction") && tileArray[i + 14].name.Contains("Inside Wall"))
                tileArray[i + 14].transform.Rotate(0, 0, 90);
            if ((i + 14) < tileArray.Length && tileArray[i].name.Contains("Inside Wall") && tileArray[i + 14].name.Contains("Inside Wall") && !(tileArray[i - 1].name.Contains("Inside Corner")) && !(tileArray[i + 1].name.Contains("Inside Corner")))
                tileArray[i + 14].transform.Rotate(0, 0, 90);
            if (tileArray[i].name.Contains("Inside Wall") && tileArray[i - 14].name.Contains("Inside Corner") && tileArray[i + 14].name.Contains("Inside Corner"))
                tileArray[i].transform.Rotate(0, 0, 90);
            if (tileArray[i].name.Contains("Inside Corner") && tileArray[i - 1].name.Contains("Inside Wall"))
                tileArray[i].transform.Rotate(0, 0, 270);
            if (tileArray[i].name.Contains("Inside Corner") && tileArray[i - 14].name.Contains("Inside Wall") && tileArray[i - 1].name.Contains("Inside Wall"))
                tileArray[i].transform.Rotate(0, 0, 270);
            if (tileArray[i].name.Contains("Inside Corner") && tileArray[i - 14].name.Contains("Inside Wall") && tileArray[i + 1].name.Contains("Inside Wall"))
                tileArray[i].transform.Rotate(0, 0, 90);
            if (tileArray[i].name.Contains("Inside Corner") && tileArray[i - 1].name.Contains("Inside Wall") && tileArray[i - 14].name.Contains("Inside Corner"))
                tileArray[i].transform.Rotate(0, 0, 270);
            if (tileArray[i].name.Contains("Inside Corner") && tileArray[i + 1].name.Contains("Inside Wall") && tileArray[i - 14].name.Contains("Inside Corner"))
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
