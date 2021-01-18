using UnityEngine;
using UnityEngine.EventSystems;

public class AttackButtonController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private HeroScriptToAnimate hero;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }

        hero.Attack();
    }
}