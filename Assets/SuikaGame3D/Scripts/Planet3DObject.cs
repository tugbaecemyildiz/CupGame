using System.Collections;
using UnityEngine;

public class Planet3DObject : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    public int planetIndex { get; private set; }
    public bool SendedMergeSignal { get; private set; }
    public float scale { get; private set; }
    public bool isBonus = false;

    public void PreparePlanet(int index, Texture texture, float scale, float colliderRadius)
    {
        meshRenderer.material.mainTexture = texture;
        planetIndex = index;
        transform.localScale = Vector3.one * scale;
        this.scale = scale;

        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.mainTexture = texture;
        }
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

    private void OnCollisionStay(Collision other)
    {

        Planet3DObject planet3DObject = other.transform.GetComponent<Planet3DObject>();
        if (planet3DObject == null) return;

        if (planet3DObject.planetIndex != planetIndex && !planet3DObject.isBonus) return;

        if (planet3DObject.SendedMergeSignal) return;

        SendedMergeSignal = true;
        Planet3DManager.Instance.Merge3D(this, planet3DObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameAreaBoundary"))
        {
            Planet3DManager.Instance.GameOver();
            return;
        }
    }
}
