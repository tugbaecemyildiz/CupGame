using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class TrailerUI : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip defaultClip;
    public VideoClip[] videoClips;
    public GameObject goToGameButton;
    private int currentGameIndex = -1;

    void Start()
    {
        PlayDefaultVideo();
    }

    private void PlayDefaultVideo()
    {
        videoPlayer.clip = defaultClip;
        videoPlayer.Play();
        goToGameButton.SetActive(false);
    }

    public void PlayTrailer(int index)
    {
        videoPlayer.clip = videoClips[index];
        videoPlayer.Play();

        currentGameIndex = index;

        goToGameButton.SetActive(true);
    }

    public void OpenGame()
    {
        switch (currentGameIndex)
        {
            case 0:
                SceneManager.LoadScene(1);
                break;
            case 1:
                SceneManager.LoadScene(2);
                break;
            case 2:
                SceneManager.LoadScene(3);
                break;
            case 3:
                SceneManager.LoadScene(4);
                break;
            case 4:
                SceneManager.LoadScene(5);
                break;
            default:
                Debug.LogWarning("Geçersiz oyun indeksi: " + currentGameIndex);
                break;
        }
    }
}
