using UnityEngine;
using System.Collections;

public class PlanetObject : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public int planetIndex { get; private set; }
    public bool SendedMergeSignal { get; private set; } 
    public float scale { get; private set; } 

    public bool isBonus = false;

    public void Prepare(Sprite sprite, int index, float scale, float colliderRadius)
    {
        spriteRenderer.sprite = sprite;
        planetIndex = index;
        transform.localScale = Vector3.one * scale;
        this.scale = scale;

        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        if (collider != null)
        {
            collider.radius = colliderRadius;
        }

        if (planetIndex == 8)
        {
            StartCoroutine(DestroyAfterDelay(1f));
        }
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager.Instance.UpdateLevel();
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("GameAreaBoundary"))
        {
            GameManager.Instance.GameOver();
            return;
        }

        PlanetObject planetObject = other.transform.GetComponent<PlanetObject>();
        if (planetObject == null) return;

        if (planetObject.planetIndex != planetIndex && !planetObject.isBonus) return;

        if (planetObject.SendedMergeSignal) return;

        SendedMergeSignal = true;
        GameManager.Instance.Merge(this, planetObject);
    }
}
