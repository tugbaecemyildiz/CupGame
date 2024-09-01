using UnityEngine;

public class NeedleController : MonoBehaviour
{
    private bool isSpinning = true;
    [SerializeField] private PrizeController _prizeController;
    public ScoreManager scoreManager;
    

    private void Start()
    {
        
    }
    public void SpinningEnded()
    {
        isSpinning = false;
    }
    public bool GetSpinning()
    {
        return isSpinning;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isSpinning)
        {
            // �d�l i�lemleri
            if (collision.CompareTag("Ayran"))
            {
                _prizeController.ShowPrize("Ayran");
                scoreManager.AddPrize("Ayran");
            }
            else if (collision.CompareTag("Midye"))
            {
                _prizeController.ShowPrize("Midye");
                scoreManager.AddPrize("Midye");
            }
            else if (collision.CompareTag("Kokorec"))
            {
                _prizeController.ShowPrize("Kokorec");
                scoreManager.AddPrize("Kokorec");
            }
            else if (collision.CompareTag("Loser"))
            {
                _prizeController.ShowPrize("Loser");
                scoreManager.ResetScore();
            }
            isSpinning = true; // �d�l i�lemi tamamland�ktan sonra yeniden d�nmeye haz�r
        }
    }
}
