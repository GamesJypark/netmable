using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightEft : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void MoveToObj(Transform pos)
    {
        transform.position = new Vector3(pos.position.x, pos.position.y, -10);
    }
    public void MoveToZero()
    {
        transform.position = new Vector3(0, 0, -10);
    }
}
