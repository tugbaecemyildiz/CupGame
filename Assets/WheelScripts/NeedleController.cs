using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NeedleController : MonoBehaviour
{
    private bool isSpinning = true;
    [SerializeField] private PrizeController _prizeController;
    public ScoreManager scoreManager;

    public void SpinningEnded()
    {
        isSpinning = false;
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!isSpinning)
        {
            if (collision.CompareTag("Ayran"))
            {
                Debug.Log("Ayran Kazanýldý!");
                _prizeController.ShowPrize("Ayran");
                scoreManager.AddReward("Ayran");
            }
            if (collision.CompareTag("Midye"))
            {
                Debug.Log("Midye Kazanýldý!");
                _prizeController.ShowPrize("Midye");
                scoreManager.AddReward("Midye");
            }
            if (collision.CompareTag("Kokorec"))
            {
                Debug.Log("Kokoreç Kazanýldý!");
                _prizeController.ShowPrize("Kokorec");
                scoreManager.AddReward("Kokorec");
            }
            else if (collision.CompareTag("Loser"))
            {
                Debug.Log("Kaybettin!");
                _prizeController.ShowPrize("Loser");
                scoreManager.ResetScore();
            }
            
            isSpinning = true;

        }


    }

}
