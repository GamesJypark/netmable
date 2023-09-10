using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Units : MonoBehaviour
{
    public int Hp;
    public int AttackPoint;
    public GameObject AtkEft;
    // Start is called before the first frame update
    void Awake()
    {
        if(gameObject.tag != "Player")
        {
            for(int i = 0; i < GameSystem.system.cloneUnit.Length; i++) // 게임시스템에 소환(복제)된 오브젝트 등록
            {
                if(GameSystem.system.cloneUnit[i] == null)
                {
                    GameSystem.system.cloneUnit[i] = this.gameObject;
                    break;
                }
            }
        }
    }
    public void Hit(int damage, GameObject Atkeft)
    {
        Instantiate(Atkeft, transform.position, Quaternion.identity);
        Hp -= damage;
        if(Hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
