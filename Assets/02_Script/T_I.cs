using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class T_I : MonoBehaviour
{
    Text text1;
    Text text2;
    // Start is called before the first frame update
    void Awake()
    {
        text1 = transform.GetChild(0).GetComponent<Text>();
        text2 = transform.GetChild(1).GetComponent<Text>();
    }

    // Update is called once per frame
    public void TI()
    {
        if(GameSystem.system.turn == 0)
        {
            text1.text = "게임 시작!";
            text2.text = "적을 모두 섬멸하세요!";
        }
        if(GameSystem.system.turn == 1)
        {
            text1.text = "아군의 턴!";
            text2.text = "유닛을 배치하거나 요원을 이동시키세요!";
        }
        if(GameSystem.system.turn == 2)
        {
            text1.text = "적의 턴!";
            text2.text = "공격에 대비하세요!";
        }
    }
}
