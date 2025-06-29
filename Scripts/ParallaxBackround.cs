using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackround : MonoBehaviour
{

    private GameObject cam;

    [SerializeField] private float parallaxEffect;

    private float xPosition;

    private float length;

    void Start()
    {
        cam = GameObject.Find("Main Camera");

        length = GetComponent<SpriteRenderer>().bounds.size.x;

        xPosition = transform.position.x;
    }

    void Update()
    {
        float distanceMoverd =cam.transform.position.x * (1 - parallaxEffect);

        float distanceToMove = cam.transform.position.x * parallaxEffect;

        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);


        if(distanceMoverd > xPosition + length)
            xPosition = xPosition + length;
        else if (distanceMoverd < xPosition - length)
            xPosition = xPosition - length;
    }
}
