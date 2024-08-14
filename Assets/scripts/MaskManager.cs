using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class NPValueManager : MonoBehaviour
{
    public Transform Newstransform;

    public NewsSpecial newsData;
    public List<PlayerMask> playerMasks; // ���ڹ������������
    public TextMeshProUGUI npText;
    public GameObject[] commentObjects; // �洢�������������

    private float accumulatedNP = 0f;
    private HashSet<Transform> playersInRange = new HashSet<Transform>(); // ��¼���ڷ�Χ�ڵ����
    private HashSet<Transform> playersThatTriggered = new HashSet<Transform>(); // ��¼�Ѵ����� NP Ч�������

    private float distance;

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

            distance = Vector3.Distance(Newstransform.position, playerMask.player.position);

            if (distance < 0.5f) // ���þ�����ֵ
            {
                if (!playersInRange.Contains(playerMask.player))
                {
                    // ����ҽ��뷶Χ
                    playersInRange.Add(playerMask.player);

                    // ֻ����һ��Ч��
                    if (!playersThatTriggered.Contains(playerMask.player))
                    {
                        ApplyMaskEffect(playerMask.maskIndex, playerMask.player); // Ӧ�ö�Ӧ������Ч��
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
        ShowComment(maskIndex - 1, comment); // maskIndex �� 1 ��ʼ��������������Ϊ maskIndex - 1
    }

    private void UpdateNPText()
    {
        if (npText != null)
        {
            npText.text = $"{accumulatedNP}"; // ֻ��ʾ��ֵ
        }
    }

    private void ShowComment(int index, string commentText)
    {
        if (index < 0 || index >= commentObjects.Length) return;

        GameObject commentObject = commentObjects[index];
        if (commentObject == null) return;

        // ������������
        commentObject.SetActive(true);

        // ���������������ı�����������ı�
        TextMeshProUGUI textComponent = commentObject.GetComponent<TextMeshProUGUI>();
        if (textComponent != null)
        {
            textComponent.text = commentText;
        }
        else
        {
            // ����������岻���ı����������������͵����
            // �������������Ӷ���Ĵ����߼�������Ϊ Image ����һЩ���Ե�
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