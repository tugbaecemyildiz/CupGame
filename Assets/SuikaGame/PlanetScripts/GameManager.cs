using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using SuikaGame;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private PlanetObjectSetting settings;
    [SerializeField] private SuikaGame.ScoreManager scoreManager;
    [SerializeField] private GameArea gameArea;
    [SerializeField] private SuikaGameUI suikaGameUI;

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform spawnParent;
    [SerializeField] private float previewHeight = 24f;
    [SerializeField] private LayerMask raycastLayerMask;
    [SerializeField] private Collider2D spawnAreaCollider;

    [SerializeField] private int initialMoves = 50;
    [SerializeField] private int movesReductionPerLevel = 5;

    private PlanetObject _previewPlanet;
    private Camera _mainCamera;

    private int _movesLeft;
    private int _lastMoves;
    private int _nextPlanetIndex;
    private int _level = 1;
    private int _maxLevel = 5;

    private int _maxSpawnedIndex = 2;
    private int _currentSpawnedIndex;

    private bool _isBonusActive = false;
    private bool _canSpawn = true;

    private bool IsClick => Input.GetMouseButtonDown(0);

    private int MovesLeft
    {
        get => _movesLeft;
        set
        {
            _movesLeft = value;
            suikaGameUI.UpdateTextMoves(_movesLeft);

            if (_movesLeft <= 0)
            {
                GameOverLose();
            }
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _mainCamera = Camera.main;
    }

    private void Start()
    {
        Time.timeScale = 1;
        _currentSpawnedIndex = _maxSpawnedIndex;
        _nextPlanetIndex = Random.Range(0, _currentSpawnedIndex);
        SetPreview(_nextPlanetIndex);
        SetMovesForLevel();

        suikaGameUI.bonusButton.onClick.AddListener(ActivateBonusTop);
        suikaGameUI.addMovesButton.onClick.AddListener(AddMoves);
    }
    private void Update()
    {
        if (IsClick)
        {
            OnClicked();
        }

        if (_previewPlanet != null)
        {
            _previewPlanet.transform.position = new Vector2(GetInputHorizontalPosition(), previewHeight);
        }
    }

    private void ActivateBonusTop()
    {
        _isBonusActive = true;
        SetBonusPreview();
        suikaGameUI.ActivateBonusTop();
    }

    private void SetBonusPreview()
    {
        if (_previewPlanet != null)
        {
            Destroy(_previewPlanet.gameObject);
        }

        PlanetObject prefab = settings.SpawnObject;
        _previewPlanet = Instantiate(prefab, new Vector2(GetInputHorizontalPosition(), previewHeight), Quaternion.identity).GetComponent<PlanetObject>();
        _previewPlanet.Prepare(
            settings.GetBonusSprite(),
            -1,
            settings.GetBonusScale(),
            settings.GetBonusRadius()
        );
        _previewPlanet.GetComponent<Rigidbody2D>().isKinematic = true;
        _previewPlanet.GetComponent<Collider2D>().isTrigger = true;
    }

    public void UpdateLevel()
    {
        _level++;
        _lastMoves += _movesLeft;
        _currentSpawnedIndex = _maxSpawnedIndex;
        if (_level >= 6)
        {
            GameWin();
            return;
        }

        suikaGameUI.UpdateLevelText(_level);

        SetMovesForLevel();
        DestroyAllChildren(spawnParent);

        _nextPlanetIndex = Random.Range(0, _currentSpawnedIndex);
        SetPreview(_nextPlanetIndex);
    }

    private void GameWin()
    {
        scoreManager.SetHighScore(_lastMoves);
        GameOverWin();
    }

    private void SetMovesForLevel()
    {
        if (_level > _maxLevel)
        {
            GameOverWin();
        }
        else
        {
            MovesLeft = Mathf.Max(1, initialMoves - (_level - 1) * movesReductionPerLevel);
        }
    }

    private void OnClicked()
    {
        if (GetSpawnPosition() == Vector2.zero) return;

        if (!_canSpawn) return;

        Vector2 spawnPosition = GetSpawnPosition();
        spawnPosition.y = previewHeight - 2;

        if (_isBonusActive)
        {
            SpawnBonusPlanet(spawnPosition);
            _isBonusActive = false;
        }
        else
        {
            SpawnPlanet(_nextPlanetIndex, spawnPosition);
        }

        Destroy(_previewPlanet?.gameObject);

        _nextPlanetIndex = Random.Range(0, _currentSpawnedIndex);
        SetPreview(_nextPlanetIndex);

        MovesLeft--;
    }

    private void SpawnBonusPlanet(Vector2 position)
    {
        PlanetObject prefab = settings.SpawnObject;
        PlanetObject planetObject = Instantiate(prefab, position, Quaternion.identity);
        planetObject.transform.SetParent(spawnParent);

        planetObject.isBonus = true;
        planetObject.Prepare(
            settings.GetBonusSprite(),
            -1,
            settings.GetBonusScale(),
            settings.GetBonusRadius()
        );
    }

    private void SpawnPlanet(int index, Vector2 position)
    {
        PlanetObject prefab = settings.SpawnObject;
        PlanetObject planetObject = Instantiate(prefab, position, Quaternion.identity);
        planetObject.transform.SetParent(spawnParent);

        Sprite sprite = settings.GetSprite(index);
        float scale = settings.GetScale(index);
        float radius = settings.GetRadius(index);

        planetObject.Prepare(sprite, index, scale, radius);
    }

    public void Merge(PlanetObject first, PlanetObject second)
    {
        int nextType = first.planetIndex + 1;
        if (nextType >= settings.SpritesCount)
        {
            Destroy(first.gameObject);
            return;
        }

        _currentSpawnedIndex = nextType > _currentSpawnedIndex ? nextType : _currentSpawnedIndex;

        Destroy(first.gameObject);

        second.isBonus = false;
        second.Prepare(
            settings.GetSprite(nextType),
            nextType,
            settings.GetScale(nextType),
            settings.GetRadius(nextType)
        );
    }

    public void SetPreview(int index)
    {
        if (_previewPlanet != null)
        {
            Destroy(_previewPlanet.gameObject);
        }

        PlanetObject prefab = settings.SpawnObject;
        _previewPlanet = Instantiate(prefab, new Vector2(GetInputHorizontalPosition(), previewHeight), Quaternion.identity).GetComponent<PlanetObject>();
        _previewPlanet.Prepare(
            settings.GetSprite(index),
            index,
            settings.GetScale(index),
            settings.GetRadius(index)
        );
        _previewPlanet.GetComponent<Rigidbody2D>().isKinematic = true;
        _previewPlanet.GetComponent<Collider2D>().isTrigger = true;
    }

    private void AddMoves()
    {
        _movesLeft += 5;
        suikaGameUI.AddMoves(_movesLeft);
    }

    public void GameOverLose()
    {
        _canSpawn = false;
        Time.timeScale = 0;
        suikaGameUI.ActivateTryAgainPanel();
    }

    public void GameOverWin()
    {
        _canSpawn = false;
        Time.timeScale = 0;
        suikaGameUI.ActivateWinGamePanel();
    }

    private void DestroyAllChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }

    private float GetInputHorizontalPosition()
    {
        Vector3 mouseWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        float inputPosition = mouseWorldPosition.x;
        Vector2 limit = gameArea.GetBorderPositionHorizontal();
        return Mathf.Clamp(inputPosition, limit.x, limit.y);
    }

    private Vector2 GetSpawnPosition()
    {
        Vector3 mouseWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPosition, Vector2.zero, Mathf.Infinity, raycastLayerMask);

        if (hit.collider != null && hit.collider == spawnAreaCollider)
        {
            return hit.point;
        }

        return Vector2.zero;
    }
}
