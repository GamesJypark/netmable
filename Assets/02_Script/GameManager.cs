using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] Tabs = new GameObject[5]; //UI들 SetActive를 위한 게임오브젝트
    public Dictionary<int, GameObject> Cards; // 카드 데이터베이스
    public GameObject[] Formation = new GameObject[6]; // 현재 편성
    public GameObject[] FormationBox = new GameObject[6]; // 편성 후보 카드들
    public GameObject FormationCautionText; // 편성 미완료 경고 텍스트
    public static GameManager gameManager; // 게임매니저 싱글톤화
    public GameObject Fade; // 페이드 인-아웃 오브젝트
    public float fadeSpeed = 1.5f; // 페이드 속도
    // Start is called before the first frame update
    void Awake() // 기본 세팅
    {
        Cards = new Dictionary<int, GameObject>(); // 카드 데이터베이스 초기화
        DontDestroyOnLoad(this.gameObject);// 게임매니저 싱글톤화
        if(gameManager == null)
        {
            gameManager = this;
        }
        else if(gameManager != null)
        {
            Destroy(this.gameObject);
        }
    }
    public void FormationRegister(GameObject card) // 편성에 인풋된 카드 편성
    {
        int count = 0;
        for(int i = 0; i < Formation.Length; i++)
        {
            if(Formation[i] != card)
            {
                count += 1;
            }
        }
        if(count == Formation.Length)
        {
            for(int i = 0; i < Formation.Length; i++)
            {
                if(Formation[i] == null)
                {
                    Formation[i] = card;
                    FormationBoxSet(i, card.transform.GetChild(0).gameObject.GetComponent<Image>(), false);
                    break;
                }
            }
        }
    }
    public void FormationBoxSet(int index, Image image, bool isDelete) // 편성 박스 안 이미지 세팅
    {
        if(isDelete == false)
        {
            FormationBox[index].GetComponent<Image>().sprite = image.sprite;
        }
        else
        {
            FormationBox[index].GetComponent<Image>().sprite = null;
        }
    }
    public void loadTab(int index) // ui화면 전환
    {
        fade(index, 0);

    }
    public void FormationDelete() // 편성 전체삭제
    {
        for(int i = 0; i < Formation.Length; i++)
        {
            Formation[i] = null;
            FormationBox[i].GetComponent<Image>().sprite = null;
        }        
    }
    public void StageSetting(int index) // 스테이지 세팅 시작
    {
        int count = 0;
        for(int i = 0; i < Formation.Length; i++)
        {
            if(Formation[i] != null) // 편성이 완료되었는지 검사
            {
                count++;
            } 
        }
        if(count == Formation.Length)
        {
            fade(4, index);
        }
        else
        {
            StartCoroutine("formation_caution");
        }
    }
    public void fade(int index, int stage) // 페이트 인-아웃 실행 함수(0은 페이드 인, 1은 페이드 아웃)
    {
        StartCoroutine(FadeIn(index, stage));
    }
    IEnumerator formation_caution() // 편성이 완료되지 않았다고 알리는 메세지 출력
    {
        FormationCautionText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        FormationCautionText.SetActive(false);
    }
    IEnumerator FadeIn(int index, int stage)
    {
        Fade.SetActive(true);
        Color acolor = Fade.GetComponent<Image>().color;
        while(acolor.a < 1)
        {
            acolor.a += fadeSpeed * Time.deltaTime;
            Fade.GetComponent<Image>().color = acolor;
            yield return new WaitForSeconds(0.01f);
        }
        for(int i = 0; i < Tabs.Length; i++)
        {
            if(i == index)
            {
                Tabs[i].SetActive(true);
            }
            else
            {
                Tabs[i].SetActive(false);
            }
        }
        if(index == 4)
        {
            GameSystem.system.GameSetting(stage);
        }
        StartCoroutine("FadeOut");
    }
    IEnumerator FadeOut()
    {
        Color color = Fade.GetComponent<Image>().color;
        while(color.a > 0)
        {
            color.a -= fadeSpeed * Time.deltaTime;
            Fade.GetComponent<Image>().color = color;
            yield return new WaitForSeconds(0.01f);
        }
        Fade.SetActive(false);
    }
  
}
