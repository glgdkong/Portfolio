using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrollingBackground : MonoBehaviour
{
    [SerializeField] private GameObject CameraObject;

    [SerializeField] private Transform[] backgroundPoint;
    [SerializeField] private Transform[] groundPoint;
    [SerializeField] private Transform[] cameraPoints;

    private float groundHorizontalSpace = 0f, backgroundHorizontalSpace = 0f, groundVerticalSpace = 0f, backgroundVerticalSpace = 0f;

    void Start()
    {
        //float camera_width = camera_leftPoint.position.x - camera_rightPoint.position.x;
        float cameraHorizontal = cameraPoints[0].position.x - cameraPoints[1].position.x;
        float cameraVertical = cameraPoints[2].position.y - cameraPoints[3].position.y;

        //ground_sideSpace = ground_rightPoint.position.x - ground_leftPoint.position.x;
        groundHorizontalSpace = groundPoint[1].position.x - groundPoint[0].position.x;
        groundVerticalSpace = groundPoint[3].position.y - groundPoint[2].position.y;

        //background_sideSpace = background_leftPoint.position.x - background_rightPoint.position.x - camera_width * 0.5f;
        backgroundHorizontalSpace = backgroundPoint[0].position.x - backgroundPoint[1].position.x - (cameraHorizontal * 0.5f);
        backgroundVerticalSpace = backgroundPoint[2].position.y - backgroundPoint[3].position.y - (cameraVertical * 0.5f);
    }

    void Update()
    {
        SetPosition();
    }

    void SetPosition()
    {
        //float background_xPos = camera_object.transform.position.x + ((camera_object.transform.position.x - ground_leftPoint.position.x) / ground_sideSpace - 0.5f) * background_sideSpace;
        float backgroundXPos = CameraObject.transform.position.x + ((CameraObject.transform.position.x - groundPoint[0].position.x) / groundHorizontalSpace - 0.5f) * backgroundHorizontalSpace;
        float backgroundYPos = CameraObject.transform.position.y + ((CameraObject.transform.position.y - groundPoint[2].position.y) / groundVerticalSpace - 0.5f) * backgroundVerticalSpace;

        //transform.position = new Vector2(background_xPos, transform.position.y);
        transform.position = new Vector2(backgroundXPos, backgroundYPos);
    }
}
