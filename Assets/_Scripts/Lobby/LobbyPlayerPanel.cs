using TMPro;
using UnityEngine;

public class LobbyPlayerPanel : MonoBehaviour {
    [SerializeField] private TMP_Text _nameText, _statusText;

    public ulong PlayerId { get; private set; }

    public void Init(ulong playerId) {
        PlayerId = playerId;
        _nameText.text = $"Anon. {PlayerId}";
    }

    public void SetReady() {
        _statusText.text = "ok";
        Color color = _statusText.color;
        color.a = 1.0f;

        _statusText.color = color;
    }
}