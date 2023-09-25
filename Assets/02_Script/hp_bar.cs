using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Video;

public class hp_bar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void hit(float MaxHp, float remainHp)
    {
        float rotation = (remainHp / MaxHp) * 100;
        transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, 360 - ((float)360 / (float)100) * rotation);
    }
}
