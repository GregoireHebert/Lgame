using System.Linq;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : Singleton<Tutorial>
{
    [SerializeField] private Canvas _GUI;
    [SerializeField] private Image _selectorPrefab;
    [SerializeField] private Image _currentSelector;
    [SerializeField] private int _step = -1;

    private List<UnityEngine.Vector2> _steps;

    protected override void Awake()
    {
        base.Awake();

        _steps = new List<UnityEngine.Vector2>();
        _steps.Insert(0, new UnityEngine.Vector2(290, 220));
        _steps.Insert(1, new UnityEngine.Vector2(-200, 200));
        _steps.Insert(2, new UnityEngine.Vector2(-200, -100));
        _steps.Insert(3, new UnityEngine.Vector2(200, -500));
        _steps.Insert(4, new UnityEngine.Vector2(400, -500));
        _steps.Insert(5, new UnityEngine.Vector2(600, -500));
        _steps.Insert(6, new UnityEngine.Vector2(100, -300));
        _steps.Insert(7, new UnityEngine.Vector2(800, -500));
        _steps.Insert(8, new UnityEngine.Vector2(-800, -500));
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentSelector = Instantiate(_selectorPrefab, new UnityEngine.Vector3(0, 0), UnityEngine.Quaternion.identity, _GUI.transform);
    }

    public void NextStep() 
    {
        UnityEngine.Vector2 nextPosition = _steps.ElementAtOrDefault(++_step);

        if (nextPosition.x != 0 && nextPosition.y != 0) {
            RectTransform rectTransform = _currentSelector.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = _steps[_step];
        } else {
            Destroy(_currentSelector);
        }

    }
}
