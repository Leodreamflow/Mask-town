using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text dialogueText;
    public Button clickNextButton;

    [Header("Choice")]
    public GameObject choicePanel;
    public Button choicePrefab;   // 一个选项按钮预制体
    public Transform choiceParent; // 按钮生成位置

    [Header("Data")]
    public DialogueNode[] nodes;

    [Header("Scene")]
    public string nextSceneName;

    private int index = 0;
    private bool waitingChoice = false;
    private bool ended = false;

    void Start()
    {
        ShowNode();
        clickNextButton.onClick.AddListener(OnClickNext);
    }

    void OnClickNext()
    {
        if (ended)
        {
            SceneManager.LoadScene(nextSceneName);
            return;
        }

        if (waitingChoice) return;

        Next();
    }

    void Next()
    {
        index++;

        if (index >= nodes.Length)
        {
            EndDialogue();
            return;
        }

        ShowNode();
    }

    void ShowNode()
    {
        ClearChoices();

        var node = nodes[index];
        dialogueText.text = node.text;

        if (node.choices != null && node.choices.Count > 0)
        {
            waitingChoice = true;
            clickNextButton.interactable = false;
            choicePanel.SetActive(true);

            foreach (var choice in node.choices)
            {
                var btn = Instantiate(choicePrefab, choiceParent);
                btn.GetComponentInChildren<TMP_Text>().text = choice.text;
                btn.onClick.AddListener(OnChoiceClicked);
            }
        }
        else
        {
            waitingChoice = false;
            clickNextButton.interactable = true;
            choicePanel.SetActive(false);
        }
    }

    void OnChoiceClicked()
    {
        waitingChoice = false;

        // 立刻隐藏并清空旧选项
        if (choicePanel != null) choicePanel.SetActive(false);
        ClearChoices();

        // 恢复“点击继续”可用（下一句是否需要选项，ShowNode 会再决定）
        if (clickNextButton != null) clickNextButton.interactable = true;

        Next();
    }

    void ClearChoices()
    {
        foreach (Transform child in choiceParent)
        {
            Destroy(child.gameObject);
        }
    }

    void EndDialogue()
    {
        ended = true;
    }
}