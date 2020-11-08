using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCamera : MonoBehaviour
{
    public Transform[] mapPoints;
    private Transform target;
    private int count = 0;

    private void Start()
    {
        target = mapPoints[count];
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime);

        if (transform.position == target.position)
        {
            count++;
            if (count >= mapPoints.Length)
            {
                count = 0;
            }
            target = mapPoints[count];
        }
    }
}
