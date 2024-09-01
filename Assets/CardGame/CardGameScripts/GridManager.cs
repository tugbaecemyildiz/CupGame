using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width = 5;
    public int height = 4;
    public Vector3 startPoint;
    public Vector3 endPoint;
    public GameObject cardPrefab;
    [SerializeField] private Transform _cardContainer;

    private List<GameObject> _cards = new List<GameObject>();

    private void Start()
    {
        CreateGrid();
    }
    private void CreateGrid()
    {
        float xSpacing = (endPoint.x - startPoint.x) / (width - 1);
        float ySpacing = (endPoint.y - startPoint.y) / (height - 1);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3 position = new Vector3(startPoint.x + i * xSpacing, startPoint.y + j * ySpacing, startPoint.z);
                GameObject card = Instantiate(cardPrefab, position, Quaternion.identity);
                card.transform.SetParent(transform);
                _cards.Add(card);
            }
        }
    }
    public List<GameObject> GetCards()
    {
        return _cards;
    }

}
