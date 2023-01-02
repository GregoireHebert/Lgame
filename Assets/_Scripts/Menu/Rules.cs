using UnityEngine;

public class Rules: MonoBehaviour
{
    [SerializeField] private GameObject RulesCanvas;
    
    public void open() {
        RulesCanvas.SetActive(true);
    }   
    
    public void close() {
        RulesCanvas.SetActive(false);
    }   
}