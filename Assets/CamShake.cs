using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    [SerializeField] private Animator animator;
    void Start()
    {

    }
    public void ShakeCamera()
    {
        animator.Play("camera_shake");
    }

}
