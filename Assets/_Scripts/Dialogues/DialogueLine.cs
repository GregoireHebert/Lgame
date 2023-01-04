using System;
using UnityEngine;
using UnityEngine.Localization;

/// <summary>
/// DialogueLine is a Scriptable Object that represents one line of text dialogue.
/// </summary>
[CreateAssetMenu(fileName = "newLineOfDialogue", menuName = "Dialogues/Line of Dialogue")]
public class DialogueLine : ScriptableObject
{
	public LocalizedString Sentence { get => _sentence; }
	[SerializeField] private LocalizedString _sentence = default; 
}