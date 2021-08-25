using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner
{
    public int level;
    float timeBetweenSpawn;
    float timeToNextLevel;
    private Timer levelTimer;
    private Timer spawnTimer;
    [SerializeField] private List <Entity> enemys;

    public Spawner (int level)
    {
        this.level = level;
        timeBetweenSpawn = 1 - 0.05f* this.level;
        timeToNextLevel = 15 + 10 * this.level;
        spawnTimer = new Timer(timeBetweenSpawn);
        levelTimer = new Timer(timeToNextLevel);
    }

    private void CalculateTimeToNextLevel(int level)
    {
        timeToNextLevel = 15 + 10 * level;

    }

    private void CalculateTimeBetweenSpawn(int level)
    {
        timeBetweenSpawn = 1 - 0.05f * level;

    }

    public bool Spawn(float deltaTime)
    {
        levelTimer.Tick(deltaTime);
        spawnTimer.Tick(deltaTime);
        if(levelTimer.TimerUp())
        {
            this.level += 1;
            CalculateTimeToNextLevel(this.level);
            CalculateTimeBetweenSpawn(this.level);
            levelTimer.StartTimer(timeToNextLevel);
            spawnTimer.StartTimer(timeBetweenSpawn);
            Debug.Log("LevelUp");
            return false;

        }
        else if(spawnTimer.TimerUp())
        {
            spawnTimer.StartTimer(timeBetweenSpawn);
            return true;
        }
        else
        {
            return false;
        }

    }
    public Vector3 CalculateRandomPosition(Vector2 cameraPosition,float cameraHeight, float cameraWidth)
    {
        Vector3 position = new Vector3();
        position.y = Random.Range(cameraPosition.y + cameraHeight / 2, cameraPosition.y + cameraHeight);
        position.x = Random.Range(cameraPosition.x - cameraWidth / 2, cameraPosition.x + cameraWidth / 2);
        position.z = 10;
        return position;
    }
}