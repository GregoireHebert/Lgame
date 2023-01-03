using UnityEngine;

public class Dialog: MonoBehaviour
{
    [SerializeField] private GameObject DialogCanvas;
    
    public void open() {
        DialogCanvas.SetActive(true);
    }   
    
    public void close() {
        DialogCanvas.SetActive(false);
    }   
}