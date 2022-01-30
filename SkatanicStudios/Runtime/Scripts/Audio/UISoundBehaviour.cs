using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISoundBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public bool isControlledByTweener;
    public DynSFXEvent enableSound;
    public DynSFXEvent disableSound;
    public DynSFXEvent mouseEnterSound;
    public DynSFXEvent mouseExitSound;
    public DynSFXEvent mouseClickSound;


    public void OnEnable()
    {
        if (isControlledByTweener) { return; }
        if(enableSound != null)
        {
            enableSound.Play();
        }
    }

    public void OnDisable()
    {
        if (isControlledByTweener) { return; }
        if(disableSound != null)
        {
            disableSound.Play();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (mouseClickSound != null)
        {
            mouseClickSound.Play();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (mouseEnterSound != null)
        {
            mouseEnterSound.Play();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (mouseExitSound != null)
        {
            mouseExitSound.Play();
        }
    }
}
