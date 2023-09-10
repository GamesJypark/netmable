using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnText : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameSystem.system.turn == 0)
        {
            this.GetComponent<Text>().text = "아군의 턴";
        }
        if(GameSystem.system.turn == 1)
        {
            this.GetComponent<Text>().text = "아군의 턴";
        }
        if(GameSystem.system.turn == 2)
        {
            this.GetComponent<Text>().text = "적의 턴";
        }
    }
}
