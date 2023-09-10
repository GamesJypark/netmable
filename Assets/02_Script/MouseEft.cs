using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class MouseEft : MonoBehaviour
{
    public GameObject clickEft;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(clickEft, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        }
    }
}
