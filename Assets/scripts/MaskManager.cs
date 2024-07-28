using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using TMPro; // ���� TextMeshPro �����ռ�
using System.Collections.Generic;

public class NPValueManager : MonoBehaviour
{
    public NewsSpecial newsData;
    public List<PlayerMask> playerMasks; // ���ڹ������������
    public TextMeshProUGUI npText;

    private float accumulatedNP = 0f;
    private HashSet<Transform> playersInRange = new HashSet<Transform>(); // ��¼���ڷ�Χ�ڵ����
    private HashSet<Transform> playersThatTriggered = new HashSet<Transform>(); // ��¼�Ѵ����� NP Ч�������

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

            if (distance < 10f) // ���þ�����ֵ
            {
                if (!playersInRange.Contains(playerMask.player))
                {
                    // ����ҽ��뷶Χ
                    playersInRange.Add(playerMask.player);

                    // ֻ����һ��Ч��
                    if (!playersThatTriggered.Contains(playerMask.player))
                    {
                        ApplyMaskEffect(playerMask.maskIndex); // Ӧ�ö�Ӧ������Ч��
                        playersThatTriggered.Add(playerMask.player); // ��¼����Ѵ���Ч��
                    }
                }
            }
            else
            {
                if (playersInRange.Contains(playerMask.player))
                {
                    // ����뿪��Χ
                    playersInRange.Remove(playerMask.player);
                    // �Ƴ��Ѵ�����ң��Ա��ٴδ���
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
            npText.text = $"{accumulatedNP}"; // ֻ��ʾ��ֵ
        }
    }
}

// ���ڹ��������������Զ�����
[System.Serializable]
public class PlayerMask
{
    public Transform player;
    public int maskIndex; // ָ����Ҷ�Ӧ���������� (1-5)
}