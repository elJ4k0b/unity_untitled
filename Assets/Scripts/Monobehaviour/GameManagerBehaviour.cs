using System.Collections.Generic;
using UnityEngine;

public class GameManagerBehaviour : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private Spawner enemySpawner;
    [SerializeField] private Spawner environmentSpawner;
    private GameObject background;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<PlayerBehaviour>().mainCam = this.mainCam.GetComponent<CameraMovement>();
        enemySpawner = new Spawner(1);
        environmentSpawner = new Spawner(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerBehaviour>().GetEntity().alive == false)
        {
            this.gameObject.GetComponent<SceneLoader>().LoadScene(0);
        }
        if(enemySpawner.Spawn(Time.deltaTime))
        {
            SpawnEnemy();
        }
        if(enemySpawner.Spawn(Time.deltaTime))
        {
            SpawnEnvrionment();
        }
        CalculateBackground();
        
    }
    private void SpawnEnemy()
    {
        Instantiate(GameAssets.instance.chicken, mainCam.ScreenToWorldPoint(enemySpawner.CalculateRandomPosition(mainCam.WorldToScreenPoint(mainCam.transform.position), mainCam.pixelHeight, mainCam.pixelWidth)), Quaternion.identity);
    }
    private void SpawnEnvrionment()
    {
        GameObject environmentToSpawn = GameAssets.instance.environment[Random.Range(0, GameAssets.instance.environment.Count)];
        Instantiate(environmentToSpawn, mainCam.ScreenToWorldPoint(environmentSpawner.CalculateRandomPosition(mainCam.WorldToScreenPoint(mainCam.transform.position), mainCam.pixelHeight, mainCam.pixelWidth)), Quaternion.identity);
    }
    private void CalculateBackground()
    {
        if(background == null)
        {
            background = Instantiate(GameAssets.instance.background, Vector2.zero, Quaternion.identity);
        }
        if (background.transform.position.y <= mainCam.transform.position.y)
        {
            Vector3 newBackgroundposition = mainCam.WorldToScreenPoint(mainCam.transform.position);
            newBackgroundposition.y += mainCam.pixelHeight;
            newBackgroundposition = mainCam.ScreenToWorldPoint(newBackgroundposition);
            newBackgroundposition.z = 1;
            background = Instantiate(GameAssets.instance.background, newBackgroundposition, Quaternion.identity);



        }
    }

}
