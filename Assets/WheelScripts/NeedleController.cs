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
                Debug.Log("Ayran Kazan�ld�!");
                _prizeController.ShowPrize("Ayran");
                scoreManager.AddReward("Ayran");
            }
            if (collision.CompareTag("Midye"))
            {
                Debug.Log("Midye Kazan�ld�!");
                _prizeController.ShowPrize("Midye");
                scoreManager.AddReward("Midye");
            }
            if (collision.CompareTag("Kokorec"))
            {
                Debug.Log("Kokore� Kazan�ld�!");
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
