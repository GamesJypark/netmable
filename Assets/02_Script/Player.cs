using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnMouseDown() {
        if(GameSystem.system.controlMode != 0)
        {
            GameSystem.system.controlMode = 1;
        }
    }
    public void PlayerMove(Vector2 dirPos)
    {
        if(GameSystem.system.controlMode != 0)
        {
            if(Vector2.Distance(transform.position, dirPos) == 1)
            {
                transform.position = dirPos;
            }
        }
    }

}
