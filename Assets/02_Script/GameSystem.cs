using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.Burst.Intrinsics;
using Unity.Mathematics;
using Unity.VisualScripting;
using System.Reflection;

public class GameSystem : MonoBehaviour
{
    public int ActPoint;
    public static GameSystem system; // 게임 시스템 싱글톤
    public GameObject player;
    public GameObject tile; // 게임오브젝트 타일
    public GameObject[] enemyPrefab; // 적 게임오브젝트들
    public GameObject[] enemys; // 생성된 적들
    public GameObject[] cards = new GameObject[6]; // 게임 내부 카드 이미지 로딩용 게임오브젝트
    public GameObject selectCard; // 선택된 카드
    public GameObject[] cloneUnit = new GameObject[6]; // 복제된 유닛
    public Vector2 sizeMin, sizeMax; // 맵 최소~최대 사이즈
    public int turn; // 턴 구분(1은 플레이어, 2는 적)
    public int controlMode; // 컨트롤 하는 대상(1은 캐릭터, 2는 카드)
    public Vector3 inputPos; // 입력받은 위치
    public Text REtext; // 남아있는 적 알림 텍스트
    public Text ActPointText; // 남은 행동력 표시 텍스트
    public GameObject ti; // 현재 턴 알리미
    public GameObject tcButton; // 턴 전환 버튼
    public GameObject SBF; // 소환 미리보기 오브젝트
    public GameObject A_WARNNING; // 행동력 부족 경고 텍스트
    int attackCount = 0; // 소환된 유닛 중 몇명이 행동했는가
    int attackCount_enemy = 0; // 소환된 적 중 몇명이 행동했는가
    int amountUnit_enemy = 0; // 스테이지 소환된 유닛
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject); // 게임시스템 싱글톤화 
        if(system == null)
        {
            system = this;
        }
        else if(system != null)
        {
            Destroy(this.gameObject);
        }
        ActPoint = 0;
    }
    public void GameSetting(int stage) // 로컬 변수의 값을 스위치로 스테이지 세팅
    {
        switch(stage)
        {
            case 1:
                mapSetting();
                cardSetting();
                EnemySpwan(enemyPrefab[0], new Vector2(3.5f, 2));
                EnemySpwan(enemyPrefab[1], new Vector2(3.5f, 0));
                PlayerSpwan(new Vector2(-2, 0), player);
                gameStart();
                amountUnit_enemy = 2;
                ActPoint = 5;
                ActPointText.text = "행동력 : " + ActPoint;
                break;
        }

    }
    private void cardSetting() // 카드 이미지 세팅
    {
        for(int i = 0; i < cards.Length; i++)
        {
            cards[i].transform.GetChild(0).GetComponent<Image>().sprite = GameManager.gameManager.Formation[i].transform.GetChild(0).GetComponent<Image>().sprite;
        }
    }
    private void mapSetting() // 맵 세팅
    {
        for(float i = sizeMin.y; i <= sizeMax.y; i++)
        {
            for(float k = sizeMin.x; k <= sizeMax.x; k++)
            {
                Instantiate(tile, new Vector2(k, i), Quaternion.identity);
            }
        }          
    }
    public void EnemySpwan(GameObject enemy, Vector2 pos) // 적 스폰 세팅
    {
        for(int i = 0; i <enemys.Length; i++)
        {
            if(enemys[i] == null)
            {
                enemys[i] = Instantiate(enemy, pos, Quaternion.identity);
                break;
            }
        }
    }
    public void PlayerSpwan(Vector2 pos, GameObject player) // 플레이어 스폰
    {
        player.SetActive(true);
    }
    public void gameStart() // 게임 시작 시 실행되는 메소드
    {
        ti.SetActive(true);
        ti.GetComponent<T_I>().TI();
        EnemyCheck();
        Invoke("turnChangeNow", 1.5f);
    }
    public void turnChange_button()
    {
        turnChange();
        tcButton.SetActive(false);
    }
    public void turnChange() // 턴 변경 시 실행되는 메소드
    {
        EnemyCheck();
        player.GetComponent<PlayerAttack>().playerAttack();
        PlayerAtk(); // 플레이어 공격 후 턴 체인지 함수 실행
        EnemyCheck();
        controlMode = 0;
    }
    public void turnChangeNow()
    {
        EnemyCheck();
        switch(turn)
        {
            case 0:
                turn = 1;
                controlMode = 1;
                break;
            case 1:
                turn = 2; // 유효 턴 값을 "적"으로 변경
                controlMode = 0; // 플레이어 컨트롤 불가 설정
                enemyControl(); // 적 행동
                break;
            case 2:
                turn = 1;// 유효 턴 값을 플레이어로 설정
                controlMode = 1;
                tcButton.SetActive(true);
                break;
        }
        EnemyCheck();
        ti.GetComponent<T_I>().TI();
        ActPoint = 5;
        ActPointText.text = "행동력 : " + ActPoint;
    }
    public void input(Vector2 pos) // 타일 위치 받기
    {
        inputPos = pos;
    }
    public void controlCount()
    {
        if(inputPos.x >= sizeMin.x && inputPos.y >= sizeMin.y && inputPos.x <= sizeMax.x && inputPos.y <= sizeMax.y)
        {
            switch(turn)
            {
                case 1:
                    if(controlMode == 1) // 플레이어 이동
                    {
                        if(player.transform.position != inputPos) player.GetComponent<Player>().PlayerMove(inputPos);
                    }
                    else if(controlMode == 2) // 유닛 공격
                    {
                        Summons();
                    }
                    break;
                case 2:
                    break;
            }
        }
    }
    public void Summons()
    {
        if(ActPoint >= selectCard.GetComponent<CardUse>().useActPoint)
        {
            ActPoint -= selectCard.GetComponent<CardUse>().useActPoint;
            Instantiate(GameManager.gameManager.Formation[(selectCard.GetComponent<CardUse>().cardIndex)].GetComponent<Cards>().cardUnit, inputPos, Quaternion.identity);
            selectCard = null;
        }
        else
        {
            A_WARNNING.SetActive(true);
        }
        ActPointText.text = "행동력 : " + ActPoint;
        selectCard = null;
    }
    public void enemyControl() // 적 행동
    {
        
        if(amountUnit_enemy > attackCount_enemy)
        {
            if(enemys[attackCount_enemy] != null && enemys[attackCount_enemy].activeSelf != false)
            {
                enemys[attackCount_enemy].GetComponent<Enemy>().EnemyMove(player.transform, enemys[attackCount_enemy].transform);
                enemys[attackCount_enemy].transform.GetChild(0).GetComponent<EnemyAttack>().EnemyAtc();            
                attackCount_enemy++;
                EnemyCheck();
                Invoke("enemyControl", 1);
            }
            else
            {
                attackCount_enemy++;
                EnemyCheck();
                enemyControl();
            }
        }
        else
        {
            attackCount_enemy = 0;
            EnemyCheck();
            Invoke("turnChangeNow", 2f);
        }
    }
    public void PlayerAtk() // 플레이어 공격
    {
        int amountUnit = 0;
        foreach(GameObject unit in cloneUnit)
        {
            if(unit != null) amountUnit++;
        }
        if(amountUnit > attackCount)
        {
            if(cloneUnit[attackCount] != null && cloneUnit[attackCount].activeSelf != false)
            {
                cloneUnit[attackCount].transform.GetChild(0).GetComponent<PlayerAttack>().playerAttack();
                attackCount++;
                EnemyCheck();
                Invoke("PlayerAtk", 1);
            }
            else
            {
                attackCount++;
                EnemyCheck();
                PlayerAtk();                
            }
        }
        else
        {
            attackCount = 0;
            EnemyCheck();
            Invoke("turnChangeNow", 2f);
        }
    }
    public void EnemyCheck()
    {
        int count = 0;
        foreach(GameObject enemy in enemys)
        {
            if(enemy != null) count++;
        }
        REtext.text = "남은 적 : " + count;
    }
}
