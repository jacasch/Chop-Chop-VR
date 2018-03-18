﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class LogSpawner : MonoBehaviour {
    public GameObject normalLog;
    public float normalLogCommonness;
    //public GameObject BirdLog;
    //public float BirdLogCommonness;


    float minSpawnDistance = 0.5f;
    float spawnFrequency = 1.5f; //rate of logs per second
    int maxLogs = 5;
    private float lastSpawnTime = 0f;
    private int logCount = 0;

    private SteamVR_PlayArea playArea;
    private Vector2 playAreaDimensions = new Vector2(2, 2);

    void Start() {
        playArea = GameObject.Find("[CameraRig]").GetComponent<SteamVR_PlayArea>();
    }

    void Update() {
        logCount = GameObject.FindGameObjectsWithTag("Log").Length;
        if ((Time.time - lastSpawnTime > 1f / spawnFrequency) && (logCount < maxLogs))
        {
            SpawnLog();
            lastSpawnTime = Time.time;
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
}