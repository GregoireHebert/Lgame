using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulsing : MonoBehaviour
{
    [SerializeField] private bool _coroutineAllowed;
    [SerializeField] private BaseUnit _unit;

    void Start()
    {
        _coroutineAllowed = true;
    }

    private IEnumerator StartPulsing()
    {
        _coroutineAllowed = false;

        for (float i = 0f; i <= 2f; i += 0.1f) {
            transform.localScale = new UnityEngine.Vector3(
                (Mathf.Lerp(transform.localScale.x, transform.localScale.x + 0.001f, Mathf.SmoothStep(0f, 1f, i))),
                (Mathf.Lerp(transform.localScale.y, transform.localScale.y + 0.001f, Mathf.SmoothStep(0f, 1f, i))),
                (Mathf.Lerp(transform.localScale.z, transform.localScale.z + 0.001f, Mathf.SmoothStep(0f, 1f, i)))
            );
            yield return new WaitForSeconds(0.015f);
        }

        for (float i = 0f; i <= 2f; i += 0.1f) {
            transform.localScale = new UnityEngine.Vector3(
                (Mathf.Lerp(transform.localScale.x, transform.localScale.x - 0.001f, Mathf.SmoothStep(0f, 1f, i))),
                (Mathf.Lerp(transform.localScale.y, transform.localScale.y - 0.001f, Mathf.SmoothStep(0f, 1f, i))),
                (Mathf.Lerp(transform.localScale.z, transform.localScale.z - 0.001f, Mathf.SmoothStep(0f, 1f, i)))
            );
            yield return new WaitForSeconds(0.015f);
        }

        _coroutineAllowed = true;
    }

    public void Update()
    {
        if (_coroutineAllowed && _unit != null && UnitManager.Instance.SelectedUnit != null && _unit.Equals(UnitManager.Instance.SelectedUnit)) {
            StartCoroutine("StartPulsing");
        }
    }
}
