using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardUse : MonoBehaviour, IPointerDownHandler
{
    public int cardIndex;
    public int useActPoint;
    // Start is called before the first frame update
    void Awake()
    {
        useActPoint = GameManager.gameManager.Formation[cardIndex].GetComponent<Cards>().UseActPoint;
    }
    void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        Instantiate(GameSystem.system.SBF);
        if(GameSystem.system.controlMode != 0)
        {        
            GameSystem.system.controlMode = 2;
        }
        GameSystem.system.selectCard = this.gameObject;
    }
}
