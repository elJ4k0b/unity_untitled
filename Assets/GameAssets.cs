using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    [Header("Particles")]
    public GameObject featherParticles;
    public GameObject grenadeExplosionParticles;
    public GameObject walkingParticles;
    public GameObject trailParticles;
    public GameObject dustParticles;
    [Header("Prefabs")]
    public GameObject background;
    public GameObject chicken;
    public GameObject eiergeschoss;
    public GameObject granatengeschoss;
    public List<GameObject> environment = new List<GameObject>();
    public List<GameObject> craters = new List<GameObject>();


    private static GameAssets _instance;
    public static GameAssets instance
    {
        get 
        {
            if(_instance == null)
            {
                _instance = (Instantiate(Resources.Load("GameAssetManager")) as GameObject).GetComponent<GameAssets>();
            }
            return _instance;
        }
    }

}
