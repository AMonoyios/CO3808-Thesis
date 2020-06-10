using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingWall : MonoBehaviour
{
    [Range(1.0f,7.0f)]
    public float Distance = 1.0f;

    [Range(1.0f,10.0f)]
    public float Speed = 1.0f;

    public enum Directions
    {
        x,
        y,
        z
    }
    public Directions Direction;

    private Vector3 StartingWallPosition;

    void Awake() => StartingWallPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

    // Update is called once per frame
    void Update()
    {
        switch (Direction)
        {
            case Directions.x:
                {
                    transform.position = new Vector3(StartingWallPosition.x - Mathf.PingPong(Time.time * Speed, Distance), transform.position.y, transform.position.z);

                    break;
                }
            case Directions.y:
                {
                    transform.position = new Vector3(transform.position.x, StartingWallPosition.y - Mathf.PingPong(Time.time * Speed, Distance), transform.position.z);

                    break;
                }
            case Directions.z:
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, StartingWallPosition.z - Mathf.PingPong(Time.time * Speed, Distance));

                    break;
                }
            default:
                break;
        }
    }
}
