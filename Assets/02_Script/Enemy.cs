using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject overlapIcon; // 오버랩된 적 아이콘
    public int Hp;
    public int AttackPoint;
    public GameObject AtkEft;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void EnemyMove(Transform playerPos, Transform enemy)
    {
        int i = Random.Range(1, 3); // 위치 변경 난수값
        switch(i) // 플레이어 추격, 랜덤 위치 변경
        {
            case 1:
                if(playerPos.position.x > enemy.position.x)
                {
                    enemy.Translate(new Vector2(1, 0));
                }
                else if(playerPos.position.x < enemy.position.x)
                {
                    enemy.Translate(new Vector2(-1, 0));
                }
                break;
            case 2:
                if(playerPos.position.y > enemy.position.y)
                {
                    enemy.Translate(new Vector2(0, 1));
                }
                else if(playerPos.position.y < enemy.position.y)
                {
                    enemy.Translate(new Vector2(0, -1));
                }
                break;
        }
    }
    void OnTriggerEnter2D(Collider2D other) // 적과 겹쳤을 때 겹침 기호 출력
    {
        if(other.gameObject.tag == "Enemy")
        {
            Instantiate(overlapIcon, gameObject.transform.position, Quaternion.identity);
        }
    }
    void OnTriggerExit2D(Collider2D other) // 적과 떨어졌을 때 겹침 기호 삭제
    {
        if(other.gameObject.tag == "Enemy")
        {
            Destroy(overlapIcon);
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
