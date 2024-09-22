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

    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private int initialMoves = 50;
    [SerializeField] private int movesReductionPerLevel = 5;

    private Planet3DObject _preview3DPlanet;
    private Camera _camera;

    private int _next3DPlanetIndex;

    private bool _canSpawn3D = true;
    private bool _isBonusActive = false;

    private int _movesLeft;
    private int _lastMoves;
    private int _level = 1;
    private int _maxLevel = 5;
    private int _maxSpawnedIndex = 2;
    private int _currentSpawnedIndex;

    private bool IsClick => Input.GetMouseButtonDown(0);

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
        _currentSpawnedIndex = _maxSpawnedIndex;

        _next3DPlanetIndex = Random.Range(0, _currentSpawnedIndex);

        SetPreview(_next3DPlanetIndex);
        SetMovesForLevel();

        gameUI.bonusButton.onClick.AddListener(ActivateBonusPlanet);
        gameUI.addMovesButton.onClick.AddListener(AddMoves);
    }

    private void Update()
    {
        if (IsClick)
        {
            if (MousePositionHit() != Vector3.zero)
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
                GameLoseOver();
            }
        }
    }

    public void SetPreview(int index)
    {
        if (_preview3DPlanet != null)
        {
            Destroy(_preview3DPlanet.gameObject);
        }

        GameObject prefab = planetSettings.GetPrefab(index);
        _preview3DPlanet = Instantiate(prefab, spawnPoint3D.position, prefab.transform.localRotation).GetComponent<Planet3DObject>();
        _preview3DPlanet.PreparePlanet(
            index,
            planetSettings.GetScale(index)
        );
        _preview3DPlanet.GetComponent<Rigidbody>().isKinematic = true;
        _preview3DPlanet.GetComponent<Collider>().isTrigger = true;
        _preview3DPlanet.gameObject.name = "Preview";
    }

    private void SpawnPlanet3D(int index, Vector3 position)
    {
        GameObject prefab = planetSettings.GetPrefab(index);

        Planet3DObject planetObject = Instantiate(prefab, position, prefab.transform.localRotation).GetComponent<Planet3DObject>();
        planetObject.transform.SetParent(spawnParent);

        float scale = planetSettings.GetScale(index);

        planetObject.PreparePlanet(index, scale);
    }

    private void SetBonusPreview()
    {
        if (_preview3DPlanet != null)
        {
            Destroy(_preview3DPlanet.gameObject);
        }

        GameObject prefab = planetSettings.GetBonusPrefab();
        _preview3DPlanet = Instantiate(prefab, spawnPoint3D.position, prefab.transform.localRotation).GetComponent<Planet3DObject>();
        _preview3DPlanet.PreparePlanet(
            -1,
            planetSettings.GetBonusScale()
        );
        _preview3DPlanet.GetComponent<Rigidbody>().isKinematic = true;
        _preview3DPlanet.GetComponent<Collider>().isTrigger = true;
    }

    private void SpawnBonusPlanet(Vector3 position)
    {
        GameObject prefab = planetSettings.GetBonusPrefab();
        Planet3DObject planetObject = Instantiate(prefab, position, prefab.transform.localRotation).GetComponent<Planet3DObject>();
        planetObject.transform.SetParent(spawnParent);

        planetObject.isBonus = true;
        planetObject.PreparePlanet(
            -1,
            planetSettings.GetBonusScale()
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
        spawnPosition.y -= 0.2f;

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

        _next3DPlanetIndex = Random.Range(0, _currentSpawnedIndex);
        SetPreview(_next3DPlanetIndex);
        MovesLeft--;
    }

    public void Merge3D(Planet3DObject first, Planet3DObject second)
    {
        int nextPlanet = first.planetIndex + 1;
        if (nextPlanet >= planetSettings.PrefabCount)
        {
            Destroy(first.gameObject);
            return;
        }

        _currentSpawnedIndex = nextPlanet > _currentSpawnedIndex ? nextPlanet : _currentSpawnedIndex;

        Destroy(first.gameObject);

        second.isBonus = false;
        second.PreparePlanet(
            nextPlanet,
            planetSettings.GetScale(nextPlanet));

        GameObject newObject = planetSettings.GetPrefab(nextPlanet);

        Planet3DObject newPlanet = Instantiate(newObject, second.transform.position, newObject.transform.localRotation).GetComponent<Planet3DObject>();
        newPlanet.PreparePlanet(index: nextPlanet, scale: planetSettings.GetScale(nextPlanet));
        newPlanet.transform.SetParent(spawnParent);

        Destroy(second.gameObject);
    }

    public void UpdateGameLevel()
    {
        _level++;
        _lastMoves += _movesLeft;
        _currentSpawnedIndex = _maxSpawnedIndex;
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
    public void GameLoseOver()
    {
        _canSpawn3D = false;
        Time.timeScale = 0;
        gameUI.ActivateGameLosePanel();
    }
    public Vector3 MousePositionHit()
    {
        Ray mouse = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mouse, out RaycastHit hit, Mathf.Infinity, _layerMask))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
}