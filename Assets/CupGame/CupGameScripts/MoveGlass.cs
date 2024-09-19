using DG.Tweening;
using System.Collections;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class MoveGlass : MonoBehaviour
{
    [SerializeField] private GameObject _winUI;
    [SerializeField] private GameObject _loseUI;
    [SerializeField] private ShuffleManager shuffleManager;

    public void GlassDown()
    {
        transform.DOMoveY(1.447f, 1f);
    }
    public void GlassUp()
    {
        transform.DOMoveY(transform.position.y + 0.3f, 1).SetEase(Ease.InOutQuad);
        transform.DORotate(new Vector3(-30, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z), 0.4f).SetEase(Ease.InOutQuad);
    }
    private void OnMouseDown()
    {
        if (shuffleManager.isShuffling)
        {
            return;
        }
        if (transform.childCount > 0)
        {
            transform.GetChild(0).transform.parent = null;
            StartCoroutine(ShowResultUI(true));
        }
        else
        {
            StartCoroutine(ShowResultUI(false));
        }
        GlassUp();
        shuffleManager.isShuffling = true;
    }
    public IEnumerator ShowResultUI(bool isWin)
    {
        yield return new WaitForSeconds(1.1f);
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