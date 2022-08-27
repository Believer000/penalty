using System;
using UnityEngine;
using TMPro;

public sealed class Game : Singleton<Game>
{
    private Player _player = null;

    public static Player Player => Instance._player;

    [Header("Ball Setup")]
    [SerializeField] private BallController _ballPrefab = null;
    [SerializeField] private Vector3 _ballSpawnPoint = Vector3.zero;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _scoreText = null;
    [SerializeField] private TextMeshProUGUI _hearthText = null;
    [SerializeField] private GameObject _deathPanel = null;

    private void Awake()
    {
        _player = new Player();
        UpdateText();
    }

    private void OnEnable()
    {
        _player.OnTakedDamage += UpdateText;
        _player.OnIncreasedScore += UpdateText;
        _player.OnPlayerDied += ShowDeathPanel;

    }

    private void OnDisable()
    {
        _player.OnTakedDamage -= UpdateText;
        _player.OnIncreasedScore -= UpdateText;
        _player.OnPlayerDied -= ShowDeathPanel;
    }

    public static void NextRound()
    {
        Goalkeeper.Instance.IncreaseMoveSpeed();
        Instantiate(Instance._ballPrefab, Instance._ballSpawnPoint, Quaternion.identity);
    }

    private void UpdateText()
    {
        _hearthText.text = $"∆изни: {_player.health}";
        _scoreText.text = $"—чет: {_player.Score}";
    }

    private void ShowDeathPanel()
    {
        _deathPanel.SetActive(true);

    }

}