using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : Singleton<MenuManager>
{
    public GameObject Forward;
    public GameObject RotateLeft;
    public GameObject RotateRight;
    public GameObject Mirror;

    private Animator ForwardAnimator;
    private Animator RotateLeftAnimator;
    private Animator RotateRightAnimator;
    private Animator MirrorAnimator;

    public void Start() {
        ForwardAnimator = Forward.GetComponent<Animator>();
        RotateLeftAnimator = RotateLeft.GetComponent<Animator>();
        RotateRightAnimator = RotateRight.GetComponent<Animator>();
        MirrorAnimator = Mirror.GetComponent<Animator>();
    }

    public void toggleShapeButtons() {
        ForwardAnimator.ResetTrigger("show");
        ForwardAnimator.SetTrigger("hide");

        RotateLeftAnimator.SetTrigger("show");
        RotateRightAnimator.SetTrigger("show");
        MirrorAnimator.SetTrigger("show");
    }

    public void toggleForwardButton() {
        ForwardAnimator.ResetTrigger("hide");
        ForwardAnimator.SetTrigger("show");

        RotateLeftAnimator.SetTrigger("hide");
        RotateRightAnimator.SetTrigger("hide");
        MirrorAnimator.SetTrigger("hide");
    }
}

