using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MyPathSystem : MonoBehaviour
{

    public enum SeedType { RANDOM, CUSTOM }
    [Header("Random Data")]
    public SeedType seedType = SeedType.RANDOM;

    System.Random random;
    public int seed = 0;

    public GameObject cellPrefab;
    public GameObject something;

    public GameObject thingToSpawn;

    [Space]
    public bool animatedPath;
    public List<MyGridCell> gridCellList = new List<MyGridCell>();
    public int pathLength = 10;

    public Transform startLocation;

    [Range(1.0f, 7.0f)]
    public float cellSize = 1.0f;




    void SetSeed()
    {
        if (seedType == SeedType.RANDOM)
            random = new System.Random();
        else if (seedType == SeedType.CUSTOM)
            random = new System.Random(seed);
    }

    private void Start()
    {
        SetSeed();
        if (animatedPath)
        {
            StartCoroutine(CreatePathRoutine());
        }
        else
            CreatePath();

    }
    void CreatePath()
    {

        gridCellList.Clear();

        Vector2 currentPosition = startLocation.position;
        MyGridCell gc = new MyGridCell(currentPosition);
        gridCellList.Add(gc);


        
        GameObject c = Instantiate(cellPrefab, currentPosition, Quaternion.identity);

        c.AddComponent<BoxCollider2D>();

        for (int i = 0; i < pathLength; i++)
        {

            int n = random.Next(100);

            if (n > 0 && n < 49)
            {
                currentPosition = new Vector2(currentPosition.x + cellSize, currentPosition.y);
            }
            if (random.NextDouble() < 0.2)
            {
                Instantiate(cellPrefab, currentPosition, Quaternion.identity);
            }
            else
            {
                currentPosition = new Vector2(currentPosition.x, currentPosition.y + cellSize);
            }
            //make new grid and change position
            gridCellList.Add(new MyGridCell(currentPosition));

            int y = random.Next(100);
            if (y > 0 && y < 49)
            {
                Debug.Log("Spawning a Peg");
            }
            else
            {
                Debug.Log("Not Spawning");
            }


            GameObject go = Instantiate(new GameObject("Block"), currentPosition, Quaternion.identity);
            Instantiate(cellPrefab, currentPosition, Quaternion.identity);
        }
    }


    IEnumerator CreatePathRoutine()
    {
        gridCellList.Clear();
        Vector2 currentPosition = new Vector2(-15.0f, -9.0f);

        gridCellList.Add(new MyGridCell(currentPosition));

        for (int i = 0; i < pathLength; i++)
        {

            int n = random.Next(100);

            if (n > 0 && n < 49)
            {
                currentPosition = new Vector2(currentPosition.x + cellSize, currentPosition.y);
            }
            else
            {
                currentPosition = new Vector2(currentPosition.x, currentPosition.y + cellSize);
            }

            gridCellList.Add(new MyGridCell(currentPosition));
            yield return null;
        }
    }


    private void OnDrawGizmos()
    {
        for (int i = 0; i < gridCellList.Count; i++)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(gridCellList[i].location, Vector2.one * cellSize);
            Gizmos.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            Gizmos.DrawCube(gridCellList[i].location, Vector2.one * cellSize);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

}