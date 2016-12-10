using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class WaveManager : MonoBehaviour {

    public Wave[] Waves;
    public Transform[] Spawns;

    private int _currentWave;
    private float _currentWaveRemaining;

	// Use this for initialization
	void Start () {
        _currentWave = -1;
	}
	
	// Update is called once per frame
	void Update () {
        _currentWaveRemaining -= Time.deltaTime;

        if(_currentWaveRemaining < 0 || transform.childCount == 0)
        {
            _currentWave++;

            if(transform.childCount == 0 && _currentWave >= Waves.Length)
            {
                FindObjectOfType<WorldManager>().GameOver(true);
            } else
            {
                SpawnWave();
            }
        }
    }

    void SpawnWave()
    {
        for(var i = 0; i < Waves[_currentWave].Enemys.Length;i++)
        {
            for(var j = 0; j < Waves[_currentWave].Enemys[i].Count; j++)
            {
                var creep = Instantiate(Waves[_currentWave].Enemys[i].Enemy, transform.transform);
                creep.transform.position = Spawns[Random.Range(0, Spawns.Length - 1)].position;
            }
        }

        _currentWaveRemaining = Waves[_currentWave].Length;
    }

    public void ClearAndRespawn()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }

        _currentWave--;
        _currentWaveRemaining = 0;
    }
}
