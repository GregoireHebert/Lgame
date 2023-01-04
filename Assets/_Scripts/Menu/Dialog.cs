using UnityEngine;

public class Dialog : MonoBehaviour
{
    [SerializeField] private GameObject _dialogCanvas;

    public void Open()
    {
        _dialogCanvas.SetActive(true);
    }

    public void Close()
    {
        _dialogCanvas.SetActive(false);
    }
}