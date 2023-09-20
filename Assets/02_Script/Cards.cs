using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cards : MonoBehaviour, IPointerClickHandler
{
    public int cardCode;
    public bool isUnlock;
    public bool Onfomation;
    public int UseActPoint;
    public GameObject cardUnit;
    // Start is called before the first frame update
    void Start()
    {
        Register();
    }
    public void Register() // 게임매니저 속 카드 데이터베이스에 카드 등록
    {
        GameManager.gameManager.Cards.Add(cardCode, this.gameObject);
    }
    void IPointerClickHandler.OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData) // 편성 화면에서 클릭 시 편성에 카드 등록
    {
        GameSystem.system.gameObject.GetComponent<SoundManager>().PlayerAudio(1);
        if(isUnlock == false && Onfomation == false)
        {
            GameManager.gameManager.FormationRegister(this.gameObject);
        }
    }
}
