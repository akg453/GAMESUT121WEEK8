using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public GameObject collectible;

    private List<MyGridCell> gridCellList;



    // Start is called before the first frame update
    void Start()
    {
        gridCellList = GetComponent<MyPathSystem>().gridCellList;

        Instantiate(collectible, gridCellList[Random.Range(2, 9)].location, Quaternion.identity).name = "inky";
        Instantiate(collectible, gridCellList[Random.Range(10, 18)].location, Quaternion.identity).name = "pinky";
        Instantiate(collectible, gridCellList[Random.Range(19, 27)].location, Quaternion.identity).name = "blinky";
        Instantiate(collectible, gridCellList[Random.Range(28, 30)].location, Quaternion.identity).name = "clyde";
    }

   

    // Update is called once per frame
    void Update()
    {
        
    }
}
