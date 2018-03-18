using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class LogSpawner : MonoBehaviour {
    public GameObject normalLog;
    public float normalLogCommonness;
    //public GameObject BirdLog;
    //public float BirdLogCommonness;


    float minSpawnDistance = 0.5f;
    float spawnFrequency = 0.5f; //rate of logs per second
    private float lastSpawnTime = 0f;

    private SteamVR_PlayArea playArea;
    private Vector2 playAreaDimensions = new Vector2(2, 2);

    void Start() {
        playArea = GameObject.Find("[CameraRig]").GetComponent<SteamVR_PlayArea>();
        FloatingText.Print("test", Vector3.zero);
    }

    void Update() {
        if (Time.time - lastSpawnTime > 1f / spawnFrequency)
        {
            SpawnLog();
            lastSpawnTime = Time.time;
            print("spawned log.");
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
        do {
            RandomSpawnPosProj = new Vector2(Random.Range(-playAreaDimensions.x / 2, playAreaDimensions.x / 2), Random.Range(-playAreaDimensions.y / 2, playAreaDimensions.y / 2));
        } while ((HeadsetPosProj - RandomSpawnPosProj).magnitude < minSpawnDistance);

        return new Vector3(RandomSpawnPosProj.x, 0, RandomSpawnPosProj.y);
    }
}
