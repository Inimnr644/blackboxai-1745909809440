using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Manages the visual novel dialogue display and choices
public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    public GameObject choicesPanel;
    public Button choiceButtonPrefab;

    private Queue<string> sentences;
    private System.Action<int> onChoiceSelected;

    void Start()
    {
        sentences = new Queue<string>();
        choicesPanel.SetActive(false);
    }

    public void StartDialogue(List<string> dialogueSentences, System.Action<int> choiceCallback = null, List<string> choices = null)
    {
        sentences.Clear();
        foreach (string sentence in dialogueSentences)
        {
            sentences.Enqueue(sentence);
        }
        onChoiceSelected = choiceCallback;
        DisplayNextSentence();

        if (choices != null && choices.Count > 0)
        {
            ShowChoices(choices);
        }
        else
        {
            choicesPanel.SetActive(false);
        }
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void ShowChoices(List<string> choices)
    {
        choicesPanel.SetActive(true);
        foreach (Transform child in choicesPanel.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < choices.Count; i++)
        {
            int choiceIndex = i;
            Button choiceButton = Instantiate(choiceButtonPrefab, choicesPanel.transform);
            choiceButton.GetComponentInChildren<Text>().text = choices[i];
            choiceButton.onClick.AddListener(() => OnChoiceSelected(choiceIndex));
        }
    }

    void OnChoiceSelected(int choiceIndex)
    {
        choicesPanel.SetActive(false);
        onChoiceSelected?.Invoke(choiceIndex);
    }

    void EndDialogue()
    {
        dialogueText.text = "";
        choicesPanel.SetActive(false);
        // Could trigger next scene or event here
    }
}
