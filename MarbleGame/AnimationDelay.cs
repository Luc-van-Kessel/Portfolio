using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;


public class AnimationDelay : MonoBehaviour
{
    private Animator animator;
    public float delay = 2f;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
        Invoke("StartAnimation", delay);
    }

    void StartAnimation()
    {
        animator.enabled = true;
        animator.Play("");
    }
}