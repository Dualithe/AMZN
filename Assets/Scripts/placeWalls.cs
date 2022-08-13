using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placeWalls : MonoBehaviour
{
    [SerializeField] GameObject sampleWall;
    [SerializeField] public float wallWidth = 0.2f;

    void Start()
    {
        Vector2 wallPos;
        Vector2 wallSize;
        float camSize = Camera.main.orthographicSize;
        float camAspect = camSize * Camera.main.aspect;
        Vector2 camPos = Camera.main.transform.position;
        GameObject wall;
        wallSize = new Vector2(wallWidth, camSize * 2);
        wallPos = camPos + new Vector2(camAspect, 0);
        wall = Instantiate(sampleWall, wallPos, Quaternion.identity, gameObject.transform);
        wall.transform.localScale = wallSize;

        wallPos = camPos + new Vector2(-camAspect, 0);
        wall = Instantiate(sampleWall, wallPos, Quaternion.identity, gameObject.transform);
        wall.transform.localScale = wallSize;


        wallSize = new Vector2(camAspect * 2, wallWidth);
        wallPos = camPos + new Vector2(0, camSize);
        wall = Instantiate(sampleWall, wallPos, Quaternion.identity, gameObject.transform);
        wall.transform.localScale = wallSize;

        wallPos = camPos + new Vector2(0, -camSize);
        wall = Instantiate(sampleWall, wallPos, Quaternion.identity, gameObject.transform);
        wall.transform.localScale = wallSize;
    }


}
