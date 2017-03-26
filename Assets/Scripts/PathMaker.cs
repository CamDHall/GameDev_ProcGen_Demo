using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMaker : MonoBehaviour {

    public Transform pathMakerSpherePrefab;
    int counter = 0, counterLimit, prefabSelection = 0;
    float leftChanceLimit, rightChanceLimit, spawnChanceLowLimit;
    Vector2 center;

    public Transform pyramid, rock, nile;
    Transform newFloorTile;

    List<Transform> floorList = new List<Transform>();


	void Start () {
        counterLimit = Random.Range(50, 75);
        leftChanceLimit = Random.Range(0.01f, 0.07f);
        rightChanceLimit = leftChanceLimit + Random.Range(0.01f, 0.07f);
        spawnChanceLowLimit = Random.Range(0.97f, 0.985f);

	}
	
	void Update () {
		if(counter < counterLimit && GameManager.globalTileCount < 500)
        {
            float randomNum = Random.Range(0.0f, 1.0f);

            if(randomNum < leftChanceLimit)
            {
                transform.Rotate(0, 90, 0);
            } else if(randomNum < rightChanceLimit)
            {
                transform.Rotate(0, -90, 0);
            } else if(randomNum >= spawnChanceLowLimit && randomNum <= 1.0f)
            {
                Instantiate(pathMakerSpherePrefab, transform.position, Quaternion.identity);
            }

            prefabSelection = Random.Range(0, 2);

            if(prefabSelection == 0)
            {
                newFloorTile = (Transform)Instantiate(pyramid, transform.position, Quaternion.identity);
            }

            if (prefabSelection == 1)
            {
                newFloorTile = (Transform)Instantiate(rock, transform.position, Quaternion.identity);
            }

            if(prefabSelection > 1)
            {
                newFloorTile = (Transform)Instantiate(nile, transform.position, Quaternion.identity);
            }

            floorList.Add(newFloorTile);
            GameManager.globalTileCount++;
            transform.Translate(Vector3.forward * 5);
            counter++;

            if(newFloorTile.transform.position.x < GameManager.farLeft)
            {
                GameManager.farLeft = newFloorTile.transform.position.x;
            }

            if(newFloorTile.transform.position.x > GameManager.farRight)
            {
                GameManager.farRight = newFloorTile.transform.position.x;
            }

            if(newFloorTile.transform.position.z < GameManager.bottom)
            {
                GameManager.bottom = newFloorTile.transform.position.z;
            }

            if(newFloorTile.transform.position.z > GameManager.top)
            {
                GameManager.top = newFloorTile.transform.position.z;
            }

            ResizeCamera();
        } else
        {
            Destroy(this.gameObject);
        }
    }

    void ResizeCamera()
    {

        center = new Vector2(((GameManager.farRight - Mathf.Abs(GameManager.farLeft)) / 2), (GameManager.top - Mathf.Abs(GameManager.bottom)) / 2);
        Camera.main.transform.position = new Vector3(center.x, 60f + GameManager.globalTileCount/2, center.y);
    }
}
