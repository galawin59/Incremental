using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class LevelMap : MonoBehaviour
{
    // Cells
    [SerializeField] GameObject groundTile;
    [SerializeField] GameObject wallTile;
    [SerializeField] GameObject doorTile;
    [SerializeField] GameObject chestTile;

    [SerializeField] GameObject enemyFactory;

    [SerializeField] GameObject casesParent;
    [SerializeField] GameObject enemiesParent;

    Room startRoom;
    bool isValid = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!Check())
        {
            Debug.LogError("Some tiles are missing !");
            return;
        }
        GenerateLevel();
        GenerateDisplay();
        isValid = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isValid)
        {
            return;
        }




    }

    bool IsCellFree(Vector2Int pos)
    {
        Case cellType = startRoom.grid[pos.y][pos.x];
        if (cellType == Case.Wall || cellType == Case.Chest)
        {
            return false;
        }
        if (cellType == Case.Door)
        {
            // Check if door is open
        }
        if (cellType == Case.Ground)
        {
            foreach (Enemy e in startRoom.enemies)
            {
                if (e.pos.x == pos.x && e.pos.y == pos.y)
                {
                    return false;
                }
            }
        }
        return true;
    }

    bool Check()
    {
        if (!casesParent || !enemiesParent || !groundTile || !wallTile || !doorTile || !chestTile)
        {
            return false;
        }
        return true;
    }

    void GenerateLevel()
    {
        startRoom = new Room();
        startRoom.size = new Vector2Int(15, 20);
        startRoom.grid = new Case[startRoom.size.y][];
        for (int y = 0; y < startRoom.size.y; y++)
        {
            startRoom.grid[y] = new Case[startRoom.size.x];
            for (int x = 0; x < startRoom.size.x; x++)
            {
                if (x == 0 || y == 0 || y == startRoom.size.y - 1 ||x == startRoom.size.x - 1)
                {
                    startRoom.grid[y][x] = Case.Wall;
                }
                else
                {
                    startRoom.grid[y][x] = Case.Ground;
                }
            }
        }
        startRoom.enemies = new List<Enemy>();
        for (int i = 0; i < 3; i++)
        {
            Enemy newEnemy = new Enemy();
            if (Random.value < 0.5f)
            {
                newEnemy.enemyName = "SWORDMAN";
            }
            else
            {
                newEnemy.enemyName = "ARCHER";
            }
            while (true)
            {
                Vector2Int testPos = new Vector2Int(Random.Range(0, startRoom.size.x - 1), Random.Range(0, startRoom.size.y) - 1);

                if (IsCellFree(testPos))
                {
                    newEnemy.pos = testPos;
                    break;
                }
            }

            startRoom.enemies.Add(newEnemy);
        }
    }

    void GenerateDisplay()
    {
        for (int y = 0; y < startRoom.size.y; y++)
        {
            for (int x = 0; x < startRoom.size.x; x++)
            {
                GameObject toCopy = null;
                switch ((int)startRoom.grid[y][x])
                {
                    case 0:
                        toCopy = groundTile;
                        break;
                    case 1:
                        toCopy = wallTile;
                        break;
                    case 2:
                        toCopy = doorTile;
                        break;
                    case 3:
                        toCopy = chestTile;
                        break;
                }
                if (!toCopy)
                {
                    Debug.LogError("Problem during generation");
                    return;
                }
                GameObject newCell = Instantiate(toCopy, casesParent.transform);
                newCell.SetActive(true);
                newCell.transform.position = new Vector3((float)x - 4.5f, (float)y - 4.5f, 1f);
            }
        }
    }
}

public class Room
{
    Room up;
    Room down;
    Room left;
    Room right;

    public Vector2Int size;
    public Case[][] grid;
    public List<Enemy> enemies;
}

public enum Case
{
    Ground=0,
    Wall=1,
    Door=2,
    Chest=3
}