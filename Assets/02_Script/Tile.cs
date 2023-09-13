using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isAtk = false; // 이 타일이 공격중인지 여부
    public bool isPlayeratk = false; // 이 타일을 플레이어가 공격중인지 여부
    // Start is called before the first frame update
    private void Update() {
        if(GameSystem.system.selectCard != null)
        {
            if(Input.GetMouseButtonUp(0))
            {
                GameSystem.system.controlCount();
            }
        }
    }
    private void OnMouseEnter() { // 클릭했을 때 클릭된 오브젝트 위치 반환
        GameSystem.system.input(new Vector3(this.transform.position.x, this.transform.position.y, 0));
    }
    private void OnMouseExit() {
        GameSystem.system.input(new Vector3(100, 100, 0));
    }
    public void isTileHot(bool isPlayerAtk) // 타일 공격 절차 시작
    {
        isPlayeratk = isPlayerAtk;
        StartCoroutine("TileHot");
    }
    IEnumerator TileHot() // 타일 공격 이펙트 출력
    {
        Color color = gameObject.GetComponent<SpriteRenderer>().color;
        float speed = 15f;
        float t = 0;
        if(isPlayeratk == true)
        {
            while(color != Color.red)
            {
                color = Color.Lerp(Color.white, Color.red, t);
                gameObject.GetComponent<SpriteRenderer>().color = color;
                t += speed * Time.deltaTime;
                yield return new WaitForSeconds(0.01f);                    
            }
            isAtk = true;
            StartCoroutine("TileCool");
        }
        else
        {
            while(color != Color.green)
            {
                color = Color.Lerp(Color.white, Color.green, t);
                gameObject.GetComponent<SpriteRenderer>().color = color;
                t += speed * Time.deltaTime;
                yield return new WaitForSeconds(0.01f);                    
            }
            isAtk = true;
            StartCoroutine("TileCool");           
        }
    }
    IEnumerator TileCool()
    {
        Color color = gameObject.GetComponent<SpriteRenderer>().color;
        float speed = 15f;
        float t = 0;
        while(color != Color.white)
        {
            color = Color.Lerp(color, Color.white, t);
            gameObject.GetComponent<SpriteRenderer>().color = color;
            t += speed * Time.deltaTime;
            yield return new WaitForSeconds(0.01f);                    
        }
        isAtk = false;
    }
}
