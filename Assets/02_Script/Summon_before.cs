using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class Summon_before : MonoBehaviour
{
    SpriteRenderer sp;
    public Vector2[] AttackRange;
    public Vector2[] TagetPos;
    // Start is called before the first frame update
    private void Start() {
        sp = GetComponent<SpriteRenderer>();
        int length = 0;
        foreach(Vector2 vec in GameManager.gameManager.Formation[(GameSystem.system.selectCard.GetComponent<CardUse>().cardIndex)].GetComponent<Cards>().cardUnit.GetComponentInChildren<PlayerAttack>().AttackRange)
        {
            AttackRange[length] = vec;
            length++;
        }
        length = 0;
        foreach(Vector2 vec in GameManager.gameManager.Formation[(GameSystem.system.selectCard.GetComponent<CardUse>().cardIndex)].GetComponent<Cards>().cardUnit.GetComponentInChildren<PlayerAttack>().TagetPos)
        {
            TagetPos[length] = vec;
            length++;
        }
        sp.sprite = GameManager.gameManager.Formation[(GameSystem.system.selectCard.GetComponent<CardUse>().cardIndex)].GetComponent<Cards>().cardUnit.GetComponent<SpriteRenderer>().sprite;
        for(int i = 0; i < TagetPos.Length; i++)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x + TagetPos[i].x, transform.position.y + TagetPos[i].y), AttackRange[i], 0);
            foreach(Collider2D col in colliders)
            {
                if(col.gameObject.tag == "dummyRange"){ 
                    col.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 127);
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        if(Input.GetMouseButtonUp(0))
        {
            Destroy(this.gameObject);
        }
    }
}
