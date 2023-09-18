using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Vector2[] AttackRange;
    public Vector2[] TagetPos;
    public void playerAttack()
    {
        for(int i = 0; i < TagetPos.Length; i++)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x + TagetPos[i].x, transform.position.y + TagetPos[i].y), AttackRange[i], 0);
            foreach(Collider2D col in colliders)
            {
                if(col.gameObject.tag == "tile")
                {
                    col.gameObject.GetComponent<Tile>().isTileHot(true);
                }
                if(col.gameObject.tag == "Enemy")
                {
                    col.gameObject.GetComponent<Enemy>().Hit(transform.GetComponentInParent<Units>().AttackPoint, transform.GetComponentInParent<Units>().AtkEft);
                }
            }
        }
    }
}
