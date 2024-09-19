using System.Collections;
using UnityEngine;

public class Planet3DObject : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    public int planetIndex { get; private set; }
    public bool SendedMergeSignal { get; private set; }
    public float scale { get; private set; }
    public bool isBonus = false;

    public void PreparePlanet(int index,  float scale )
    {
       
        planetIndex = index;
        transform.localScale = Vector3.one * scale;
        this.scale = scale;

        if (planetIndex == 6)
        {
            StartCoroutine(DestroyPlanet(1f));
        }
    }

    private IEnumerator DestroyPlanet(float delay)
    {
        yield return new WaitForSeconds(delay);
        Planet3DManager.Instance.UpdateGameLevel();
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        Planet3DObject planet3DObject = other.transform.GetComponent<Planet3DObject>();
    if (planet3DObject == null) return;

    if (planet3DObject.planetIndex != planetIndex && !planet3DObject.isBonus) return;

    // E�er birle�tirme sinyali daha �nce g�nderilmi�se, birle�meyi engelle
    if (SendedMergeSignal || planet3DObject.SendedMergeSignal) return;

    // Birle�tirme sinyali g�nderildi ve birle�me i�lemi yap�l�r
    SendedMergeSignal = true;

    // �kinci birle�me olmas�n� �nlemek i�in nesneleri devre d��� b�rak
    planet3DObject.SendedMergeSignal = true;

    Planet3DManager.Instance.Merge3D(this, planet3DObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameAreaBoundary"))
        {
            Planet3DManager.Instance.GameLoseOver();
            return;
        }
    }
}
