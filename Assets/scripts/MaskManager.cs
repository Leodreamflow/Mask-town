using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using TMPro; // 引用 TextMeshPro 命名空间
using System.Collections.Generic;

public class NPValueManager : MonoBehaviour
{
    public NewsSpecial newsData;
    public List<PlayerMask> playerMasks; // 用于关联玩家与掩码
    public TextMeshProUGUI npText;

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
                        ApplyMaskEffect(playerMask.maskIndex); // 应用对应的掩码效果
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

    public void ApplyMaskEffect(int maskIndex)
    {
        if (newsData == null)
        {
            Debug.LogWarning("NewsSpecial data is not assigned!");
            return;
        }

        float npValue = 0;

        switch (maskIndex)
        {
            case 1:
                npValue = newsData.NPMask01;
                break;
            case 2:
                npValue = newsData.NPMask02;
                break;
            case 3:
                npValue = newsData.NPMask03;
                break;
            case 4:
                npValue = newsData.NPMask04;
                break;
            case 5:
                npValue = newsData.NPMask05;
                break;
            default:
                Debug.LogWarning("Invalid mask index.");
                return;
        }

        accumulatedNP += npValue;
        UpdateNPText();
    }

    private void UpdateNPText()
    {
        if (npText != null)
        {
            npText.text = $"{accumulatedNP}"; // 只显示数值
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