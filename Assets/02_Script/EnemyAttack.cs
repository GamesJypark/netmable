using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Vector2[] AttackRange; // 공격 범위

    public void EnemyAtc() // 적의 공격
    {
        FindAnyObjectByType<FightEft>().GetComponent<FightEft>().MoveToObj(transform);
        Debug.Log(gameObject);
        for(int i = 0; i < AttackRange.Length; i++)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(this.transform.position, AttackRange[i], 0); // 콜라이더 박스 소환
            foreach(Collider2D col in colliders) // 콜라이더 박스에 감지된 타일 검출
            {
                if(col.gameObject.tag == "tile")
                {
                    col.gameObject.GetComponent<Tile>().isTileHot(false); // 감지된 타일에 공격 신호 발신
                }
                if(col.gameObject.tag == "Unit" || col.gameObject.tag == "Player")
                {
                    col.gameObject.GetComponent<Units>().Hit(transform.GetComponentInParent<Enemy>().AttackPoint, transform.GetComponentInParent<Enemy>().AtkEft);
                }
            }
        }
    }
}
