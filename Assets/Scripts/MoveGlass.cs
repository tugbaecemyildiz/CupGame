using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveGlass : MonoBehaviour
{
    [SerializeField] private GameObject _winUI;
    [SerializeField] private GameObject _loseUI;

    public void GlassDown()
    {
        transform.DOMoveY(1.447f, 1f);
    }

    public void GlassUp()
    {
        transform.DOMoveY(1.7f, 1f);
    }

    private void OnMouseDown()
    {
        if (transform.childCount>0)
        {
            transform.GetChild(0).transform.parent=null;
            StartCoroutine(ShowResultUI(true));
            
        }
        else
        {
            StartCoroutine(ShowResultUI(false));
        }
        GlassUp();
    }
    public IEnumerator ShowResultUI(bool isWin)
    {
        yield return new WaitForSeconds(1f);
        if (isWin)
        {
            _winUI.SetActive(true);
        }
        else
        {
            _loseUI.SetActive(true);
        }
        
    }
}
