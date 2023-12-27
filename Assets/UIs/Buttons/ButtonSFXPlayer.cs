using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSFXPlayer : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IPointerClickHandler, ISubmitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundsManager.Instance.PlaySFX(SoundsManager.TypesOfSFX.HoverButton, 0.1f);
    }

    public void OnSelect(BaseEventData eventData)
    {
        SoundsManager.Instance.PlaySFX(SoundsManager.TypesOfSFX.HoverButton, 0.1f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SoundsManager.Instance.PlaySFX(SoundsManager.TypesOfSFX.ButtonPressed, 0.15f);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        SoundsManager.Instance.PlaySFX(SoundsManager.TypesOfSFX.ButtonPressed, 0.15f);
    }
}