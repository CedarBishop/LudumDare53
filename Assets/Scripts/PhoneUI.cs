using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneUI : MonoBehaviour
{
    private Animator animator;
    private bool isMaximised;

    void Start()
    {
        isMaximised = false;
        animator = GetComponent<Animator>();
    }

    public void TogglePhoneSize()
    {
        if (isMaximised)
        {
            animator.Play("MinimisePhone", 0);
        }
        else
        {
            animator.Play("MaximisePhone", 0);
        }
        isMaximised = !isMaximised;
        
    }
}
