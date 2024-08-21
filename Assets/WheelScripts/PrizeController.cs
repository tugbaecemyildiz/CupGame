using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq.Expressions;
using System;
using DG.Tweening;

public class PrizeController : MonoBehaviour
{
    [SerializeField] private GameObject _prizePanel;
    [SerializeField] private Image _prizeImage;
    [SerializeField] private TMP_Text _prizeText;

    [SerializeField] private Sprite _ayranSprite;
    [SerializeField] private Sprite _midyeSprite;
    [SerializeField] private Sprite _kokorecSprite;
    [SerializeField] private Sprite _loseAllSprite;

    public void ShowPrize(string prizeType)
    {
        switch (prizeType)
        {
            case "Ayran":
                _prizeImage.sprite = _ayranSprite;
                _prizeText.text = "Tebrikler, Ayran Kazandýnýz!";
                break;
            case "Kokorec":
                _prizeImage.sprite = _kokorecSprite;
                _prizeText.text = "Tebrikler, Kokoreç Kazandýnýz!";
                break;
            case "Midye":
                _prizeImage.sprite = _midyeSprite;
                _prizeText.text = "Tebrikler, Midye Kazandýnýz!";
                break;
            case "Loser":
                _prizeImage.sprite = _loseAllSprite;
                _prizeText.text = "Her Þeyi Kaybettiniz!";
                break;
        }
        _prizePanel.transform.localScale = Vector3.zero;
        _prizePanel.SetActive(true);

        _prizePanel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
    }
    public void BackButton()
    {
        _prizePanel.SetActive(false);
    }

}
