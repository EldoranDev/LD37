using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour {

    public GameObject PlayerPrefab;

    private WaveManager _waveManager;

    public int Points
    {
        get
        {
            return _points;
        }
        set
        {
            _points = value;
            PointsDisplay.text = _points.ToString();
        }
    }

    public int StartLives;

    public Text PointsDisplay;

    public GameObject UI;
    public GameObject GameOverUI;

    private int _points;
    private int _lives;

    GameObject _currentPlayer;

	// Use this for initialization
	void Start () {
        _lives = StartLives;
        _waveManager = GetComponent<WaveManager>();

        Initialize();	
	}

    void Initialize()
    {
        if(_currentPlayer != null)
        {
            Destroy(_currentPlayer);
        }

        _currentPlayer = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
        var player = _currentPlayer.GetComponent<Player>();

        player.AmmoDisplay = GameObject.Find("AmmoDisplay").GetComponent<Text>();
    }

    public void RemoveLife()
    {
        _lives--;

        if (_lives < 0)
        {
            GameOver(false);
        }
        else
        {
            Initialize();
            _waveManager.ClearAndRespawn();
        }
    }

    public void GameOver(bool won)
    {
        var ui = GameObject.Find("UI");

        UI.SetActive(false);
        GameOverUI.SetActive(true);

        Destroy(_currentPlayer);

        var points = GameObject.Find("GameOverPoints").GetComponent<Text>();
        var message = GameObject.Find("GameOverMessage").GetComponent<Text>();

        points.text = Points.ToString();
        message.text = (won) ? "You defeated all Zombies" : "You got killed by the Zombies";
    }
}
