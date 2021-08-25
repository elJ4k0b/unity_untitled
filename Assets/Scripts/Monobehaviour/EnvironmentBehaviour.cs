using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentBehaviour : MonoBehaviour
{
    [SerializeField] public Animator animator;

    public void Start()
    {
        this.gameObject.SetActive(true);
        animator.Play("idle");
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        this.gameObject.SetActive(true);
        this.animator.Play("wackeln");
        
    }
    private void Update()
    {
        

    }

}
