using UnityEngine;

public class Planet3DManager : MonoBehaviour
{
    public static Planet3DManager Instance { get; private set; }

    [SerializeField] private ScoreController scoreController;
    [SerializeField] private GameArea3D gameArea;
    [SerializeField] private Planet3DSettings planetSettings;
    [SerializeField] private GameUI gameUI;

    [SerializeField] private Transform spawnPoint3D;
    [SerializeField] private Transform spawnParent;

    [SerializeField] private LayerMask raycastLayerMask;

    [SerializeField] private int initialMoves = 50;
    [SerializeField] private int movesReductionPerLevel = 10;

    private Planet3DObject _preview3DPlanet;
    private Camera _camera;

    private int _next3DPlanetIndex;

    private bool _canSpawn3D = true;
    private bool _isBonusActive = false;

    private int _movesLeft;
    private int _lastMoves;
    private int _level = 1;
    private int _maxLevel = 5;

    private bool IsClick => Input.GetKeyDown(KeyCode.Space);

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _camera = Camera.main;
    }

    private void Start()
    {
        Time.timeScale = 1f;
        _next3DPlanetIndex = Random.Range(0, planetSettings.MeshCount - 1);
        SetPreview(_next3DPlanetIndex);
        SetMovesForLevel();

        gameUI.bonusButton.onClick.AddListener(ActivateBonusPlanet);
        gameUI.addMovesButton.onClick.AddListener(AddMoves);
    }

    private void Update()
    {
        if (IsClick)
        {
            OnClicked();
        }
        if (_preview3DPlanet != null)
        {
            _preview3DPlanet.transform.position = spawnPoint3D.position;
        }
    }

    private int MovesLeft
    {
        get => _movesLeft;
        set
        {
            _movesLeft = value;
            gameUI.UpdateTextMoves(_movesLeft);

            if (_movesLeft <= 0)
            {
                GameOver();
            }
        }
    }

    public void SetPreview(int index)
    {
        if (_preview3DPlanet != null)
        {
            Destroy(_preview3DPlanet.gameObject);
        }

        Planet3DObject prefab = planetSettings.SpawnObject;
        _preview3DPlanet = Instantiate(prefab, spawnPoint3D.position, Quaternion.identity).GetComponent<Planet3DObject>();
        _preview3DPlanet.PreparePlanet(
            index,
            planetSettings.GetTexture(index),
            planetSettings.GetScale(index),
            planetSettings.GetRadius(index)
        );
        _preview3DPlanet.GetComponent<Rigidbody>().isKinematic = true;
        _preview3DPlanet.GetComponent<Collider>().isTrigger = true;
        _preview3DPlanet.gameObject.name = "Preview";
    }

    private void SpawnPlanet3D(int index, Vector3 position)
    {
        Planet3DObject prefab = planetSettings.SpawnObject;

        Planet3DObject planetObject = Instantiate(prefab, position, Quaternion.identity);
        planetObject.transform.SetParent(spawnParent);

        Texture texture = planetSettings.GetTexture(index);
        float scale = planetSettings.GetScale(index);
        float radius = planetSettings.GetRadius(index);

        planetObject.PreparePlanet(index, texture, scale, radius);
    }

    private void SetBonusPreview()
    {
        if (_preview3DPlanet != null)
        {
            Destroy(_preview3DPlanet.gameObject);
        }

        Planet3DObject prefab = planetSettings.SpawnObject;
        _preview3DPlanet = Instantiate(prefab, spawnPoint3D.position, Quaternion.identity).GetComponent<Planet3DObject>();
        _preview3DPlanet.PreparePlanet(
            -1,
            planetSettings.GetBonusTexture(),
            planetSettings.GetBonusScale(),
            planetSettings.GetBonusRadius()
        );
        _preview3DPlanet.GetComponent<Rigidbody>().isKinematic = true;
        _preview3DPlanet.GetComponent<Collider>().isTrigger = true;
    }

    private void SpawnBonusPlanet(Vector3 position)
    {
        Planet3DObject prefab = planetSettings.SpawnObject;
        Planet3DObject planetObject = Instantiate(prefab, position, Quaternion.identity);
        planetObject.transform.SetParent(spawnParent);

        planetObject.isBonus = true;
        planetObject.PreparePlanet(
            -1,
            planetSettings.GetBonusTexture(),
            planetSettings.GetBonusScale(),
            planetSettings.GetBonusRadius()
        );
    }
    private void ActivateBonusPlanet()
    {
        _isBonusActive = true;
        SetBonusPreview();
        gameUI.ActivateBonusTop();
    }

    private void OnClicked()
    {
        if (!_canSpawn3D)
        {
            return;
        }

        Vector3 spawnPosition = spawnPoint3D.position;
        spawnPosition.y -= 1;

        if (_isBonusActive)
        {
            SpawnBonusPlanet(spawnPosition);
            _isBonusActive = false;
        }
        else
        {
            SpawnPlanet3D(_next3DPlanetIndex, spawnPosition);
        }

        Destroy(_preview3DPlanet?.gameObject);

        _next3DPlanetIndex = Random.Range(0, planetSettings.MeshCount - 1);
        SetPreview(_next3DPlanetIndex);
        MovesLeft--;
    }

    public void Merge3D(Planet3DObject first, Planet3DObject second)
    {
        int nextPlanet = first.planetIndex + 1;
        if (nextPlanet >= planetSettings.MeshCount)
        {
            Destroy(first.gameObject);
            return;
        }

        Destroy(first.gameObject);

        second.isBonus = false;
        second.PreparePlanet(
            nextPlanet,
            planetSettings.GetTexture(nextPlanet),
            planetSettings.GetScale(nextPlanet),
            planetSettings.GetRadius(nextPlanet));
    }

    public void UpdateGameLevel()
    {
        _level++;
        _lastMoves += _movesLeft;
        if (_level >= 6)
        {
            GameWin();
            return;
        }

        gameUI.UpdateLevelText(_level);

        SetMovesForLevel();
        DestroyAllChildren(spawnParent);
    }

    private void SetMovesForLevel()
    {
        if (_level > _maxLevel)
        {
            GameOver();
        }
        else
        {
            MovesLeft = Mathf.Max(1, initialMoves - (_level - 1) * movesReductionPerLevel);
        }
    }

    private void AddMoves()
    {
        _movesLeft += 5;
        gameUI.AddMoves(_movesLeft);
    }

    private void DestroyAllChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }
    private void GameWin()
    {
        scoreController.SetHighScore(_lastMoves);
        GameOver();
    }

    public void GameOver()
    {
        _canSpawn3D = false;
        Time.timeScale = 0;
        gameUI.ActivateTryAgainPanel();
    }
}
