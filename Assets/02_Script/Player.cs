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
            Debug.Log("ddd");
            GameSystem.system.controlMode = 1;
        }
    }
    private void OnMouseUp() {
        if(GameSystem.system.controlMode == 1)
        {
            GameSystem.system.controlCount();
        }
    }
    public void PlayerMove(Vector2 dirPos)
    {
        if(GameSystem.system.controlMode != 0)
        {
            if(Vector2.Distance(transform.position, dirPos) == 1)
            {
                transform.position = dirPos;
                GameSystem.system.turnChange_button();
            }
        }
    }

}
