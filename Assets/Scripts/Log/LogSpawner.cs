using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class LogSpawner : MonoBehaviour {
    public GameObject normalLog;
    public GameObject gameStartLog;

    private bool spawning = false;
    //public GameObject BirdLog;
    //public float BirdLogCommonness;


    float minSpawnDistance = 0.5f;
    float spawnFrequency = 1.5f; //rate of logs per second
    int maxLogs = 5;
    private float lastSpawnTime = 0f;
    private int logCount = 0;
    private float comboSpawnTimer = 0f;
    private float comboTime = 5f;

    private SteamVR_PlayArea playArea;
    public Vector2 playAreaDimensions = new Vector2(2, 2);

    void Start() {
        playArea = GameObject.Find("[CameraRig]").GetComponent<SteamVR_PlayArea>();
    }

    void Update() {
        logCount = GameObject.FindGameObjectsWithTag("Log").Length;
        if (spawning && (Time.time - lastSpawnTime > 1f / spawnFrequency) && (logCount < maxLogs))
        {
            SpawnLog();
            lastSpawnTime = Time.time;
        }

        if (comboSpawnTimer > 0)
        {
            comboSpawnTimer -= Time.deltaTime;
            if (spawning && (Time.time - lastSpawnTime > 0.2f) && (logCount < 100))
            {
                SpawnLog();
                lastSpawnTime = Time.time;
            }
        }
        else if (logCount > 5) {
            ClearLogs();
        }
    }

    public void SpawnLog() {
        GameObject.Instantiate(normalLog, RandomSpawnPosition(), normalLog.transform.rotation);
    }

    private Vector3 RandomSpawnPosition() {
        Vector3 HeadsetPos = playArea.GetComponentInChildren<Camera>().transform.position;
        HeadsetPos = HeadsetPos == null ? Vector3.zero : HeadsetPos;

        //generate random position that is at least minDist away from player head (projected)
        Vector2 HeadsetPosProj = new Vector2(HeadsetPos.x, HeadsetPos.z);
        Vector2 RandomSpawnPosProj;
        float closestOtherLogDistance;
        do {
            do
            {
                RandomSpawnPosProj = new Vector2(Random.Range(-playAreaDimensions.x / 2, playAreaDimensions.x / 2), Random.Range(-playAreaDimensions.y / 2, playAreaDimensions.y / 2));
                closestOtherLogDistance = int.MaxValue;
                foreach (GameObject log in GameObject.FindGameObjectsWithTag("Log"))
                {
                    float tempDist = (new Vector2(log.transform.position.x, log.transform.position.z) - RandomSpawnPosProj).magnitude;
                    if (tempDist < closestOtherLogDistance)
                        closestOtherLogDistance = tempDist;
                }
            } while (closestOtherLogDistance < 0.3);
        } while ((HeadsetPosProj - RandomSpawnPosProj).magnitude < minSpawnDistance);

        return new Vector3(RandomSpawnPosProj.x, 0, RandomSpawnPosProj.y);
    }

    public void StopSpawning() {
        ClearLogs();
        //stop spawning new logs
        spawning = false;
    }

    public void StartSpawning() {
        ClearLogs();
        spawning = true;
    }

    public void FurySpawning() {
        comboSpawnTimer = comboTime;
    }

    public void SpawnPlayerReadyLog() {
        ClearLogs();
        GameObject.Instantiate(gameStartLog, RandomSpawnPosition(), normalLog.transform.rotation);
    }

    public void ClearLogs() {
        //destroy all logs
        foreach (GameObject log in GameObject.FindGameObjectsWithTag("Log"))
        {
            Destroy(log);
        }
    }

    public void ClearChops()
    {
        //destroy all logs
        foreach (GameObject log in GameObject.FindGameObjectsWithTag("Choped"))
        {
            Destroy(log);
        }
    }
}
