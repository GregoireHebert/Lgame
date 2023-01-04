using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : Singleton<MenuManager>
{
    [SerializeField] private GameObject _forward;
    [SerializeField] private GameObject _rotateLeft;
    [SerializeField] private GameObject _rotateRight;
    [SerializeField] private GameObject _mirror;

    private Animator _forwardAnimator;
    private Animator _rotateLeftAnimator;
    private Animator _rotateRightAnimator;
    private Animator _mirrorAnimator;

    public void Start()
    {
        _forwardAnimator = _forward.GetComponent<Animator>();
        _rotateLeftAnimator = _rotateLeft.GetComponent<Animator>();
        _rotateRightAnimator = _rotateRight.GetComponent<Animator>();
        _mirrorAnimator = _mirror.GetComponent<Animator>();
    }

    public void ToggleShapeButtons()
    {
        _forwardAnimator.ResetTrigger("show");
        _forwardAnimator.SetTrigger("hide");

        _rotateLeftAnimator.SetTrigger("show");
        _rotateRightAnimator.SetTrigger("show");
        _mirrorAnimator.SetTrigger("show");
    }

    public void ToggleForwardButton()
    {
        _forwardAnimator.ResetTrigger("hide");
        _forwardAnimator.SetTrigger("show");

        _rotateLeftAnimator.SetTrigger("hide");
        _rotateRightAnimator.SetTrigger("hide");
        _mirrorAnimator.SetTrigger("hide");
    }
}

