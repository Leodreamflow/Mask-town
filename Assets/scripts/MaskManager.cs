using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class NPValueManager : MonoBehaviour
{
    public NewsSpecial newsData;
    public List<PlayerMask> playerMasks; // 用于关联玩家与掩码
    public TextMeshProUGUI npText;
    public GameObject[] commentObjects; // 存储评论物体的数组

    private float accumulatedNP = 0f;
    private HashSet<Transform> playersInRange = new HashSet<Transform>(); // 记录处于范围内的玩家
    private HashSet<Transform> playersThatTriggered = new HashSet<Transform>(); // 记录已触发过 NP 效果的玩家

    private void Start()
    {
        if (newsData == null)
        {
            Debug.LogError("NewsSpecial data is not assigned!");
            return;
        }

        if (playerMasks == null || playerMasks.Count != 5)
        {
            Debug.LogError("PlayerMask list is not assigned or does not contain exactly 5 items!");
            return;
        }

        if (npText == null)
        {
            Debug.LogError("NP Text UI is not assigned!");
            return;
        }

        if (commentObjects == null || commentObjects.Length != 5)
        {
            Debug.LogError("Comment objects array is not assigned or does not contain exactly 5 items!");
            return;
        }

        // Ensure all comment objects are initially inactive
        foreach (GameObject commentObject in commentObjects)
        {
            commentObject.SetActive(false);
        }

        UpdateNPText();
    }

    private void Update()
    {
        foreach (PlayerMask playerMask in playerMasks)
        {
            if (playerMask == null || playerMask.player == null) continue;

            float distance = Vector3.Distance(transform.position, playerMask.player.position);

            if (distance < 10f) // 设置距离阈值
            {
                if (!playersInRange.Contains(playerMask.player))
                {
                    // 新玩家进入范围
                    playersInRange.Add(playerMask.player);

                    // 只触发一次效果
                    if (!playersThatTriggered.Contains(playerMask.player))
                    {
                        ApplyMaskEffect(playerMask.maskIndex, playerMask.player); // 应用对应的掩码效果
                        playersThatTriggered.Add(playerMask.player); // 记录玩家已触发效果
                    }
                }
            }
            else
            {
                if (playersInRange.Contains(playerMask.player))
                {
                    // 玩家离开范围
                    playersInRange.Remove(playerMask.player);
                    // 移除已触发玩家，以便再次触发
                    playersThatTriggered.Remove(playerMask.player);
                }
            }
        }
    }

    public void ApplyMaskEffect(int maskIndex, Transform player)
    {
        if (newsData == null)
        {
            Debug.LogWarning("NewsSpecial data is not assigned!");
            return;
        }

        float npValue = 0;
        string comment = null;

        switch (maskIndex)
        {
            case 1:
                npValue = newsData.NPMask01;
                comment = newsData.commentarMask01;
                break;
            case 2:
                npValue = newsData.NPMask02;
                comment = newsData.commentarMask02;
                break;
            case 3:
                npValue = newsData.NPMask03;
                comment = newsData.commentarMask03;
                break;
            case 4:
                npValue = newsData.NPMask04;
                comment = newsData.commentarMask04;
                break;
            case 5:
                npValue = newsData.NPMask05;
                comment = newsData.commentarMask05;
                break;
            default:
                Debug.LogWarning("Invalid mask index.");
                return;
        }

        accumulatedNP += npValue;
        UpdateNPText();
        ShowComment(maskIndex - 1, comment); // maskIndex 从 1 开始，所以数组索引为 maskIndex - 1
    }

    private void UpdateNPText()
    {
        if (npText != null)
        {
            npText.text = $"{accumulatedNP}"; // 只显示数值
        }
    }

    private void ShowComment(int index, string commentText)
    {
        if (index < 0 || index >= commentObjects.Length) return;

        GameObject commentObject = commentObjects[index];
        if (commentObject == null) return;

        // 激活评论物体
        commentObject.SetActive(true);

        // 如果评论物体包含文本组件，更新文本
        TextMeshProUGUI textComponent = commentObject.GetComponent<TextMeshProUGUI>();
        if (textComponent != null)
        {
            textComponent.text = commentText;
        }
        else
        {
            // 如果评论物体不是文本，可能是其他类型的组件
            // 你可以在这里添加额外的处理逻辑，比如为 Image 设置一些属性等
        }
    }
}

// 用于关联玩家与掩码的自定义类
[System.Serializable]
public class PlayerMask
{
    public Transform player;
    public int maskIndex; // 指定玩家对应的掩码索引 (1-5)
}