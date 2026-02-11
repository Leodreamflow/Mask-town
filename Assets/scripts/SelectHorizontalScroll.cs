using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SelectHorizontalScroll : MonoBehaviour,
    IDragHandler,
    IPointerDownHandler,
    IPointerUpHandler
{
    #region ===== Item Data =====

    [Serializable]
    private struct ItemInfo
    {
        public string name;
        public Sprite sprite;
        public string description;

        public ItemInfo(string name, Sprite sprite, string description)
        {
            this.name = name;
            this.sprite = sprite;
            this.description = description;
        }
    }

    #endregion


    #region ===== Inspector Fields =====

    [Tooltip("选项预制体")]
    [SerializeField] private GameObject itemPrefab;

    [Tooltip("选项父物体")]
    [SerializeField] private RectTransform itemParent;

    [Tooltip("描述文字")]
    [SerializeField] private TMP_Text descriptionText;

    [Tooltip("选项信息")]
    [SerializeField] private ItemInfo[] itemInfos;

    [Tooltip("显示数量（尽量填奇数）")]
    [SerializeField] private int displayNumber;

    [Tooltip("选项间隔")]
    [SerializeField] private float itemSpace;

    [Tooltip("移动插值")]
    [SerializeField] private float moveSmooth;

    [Tooltip("拖动速度")]
    [SerializeField] private float dragSpeed;

    [Tooltip("缩放倍率")]
    [SerializeField] private float scaleMultiplying;

    [Tooltip("透明度倍率")]
    [SerializeField] private float alphaMultiplying;

    #endregion


    #region ===== Events =====

    public event Action<int> SelectAction;

    #endregion


    #region ===== Runtime Fields =====

    private SelectHorizontalScrollItem[] items;
    private float displayWidth;
    private int offsetTimes;
    private bool isDrag;

    // 注意：截图里这个变量名叫 currentItemIndex，但它存的是“居中 item 的 itemIndex”
    private int currentItemIndex;

    private float[] distances;

    // 点击非居中项时，用它来移动到居中
    private float selectItemX;

    // 是否处于“点击选择导致的移动”
    private bool isSelectMove;

    // 是否已经触发过一次选择（防止重复触发）
    private bool isSelected;

    #endregion


    #region ===== Unity Lifecycle =====

    private void Start()
    {
        Init();
        MoveItems(0);
    }

    private void Update()
    {
        if (!isDrag)
        {
            Adsorption();
        }

        int currentOffsetTimes =
            Mathf.FloorToInt(itemParent.localPosition.x / itemSpace);

        if (currentOffsetTimes != offsetTimes)
        {
            offsetTimes = currentOffsetTimes;
            MoveItems(offsetTimes);
        }

        ItemsControl();
    }

    #endregion


    #region ===== Init & Setup =====

    /// <summary>
    /// 初始化
    /// </summary>
    private void Init()
    {
        displayWidth = (displayNumber - 1) * itemSpace;
        items = new SelectHorizontalScrollItem[displayNumber];

        for (int i = 0; i < displayNumber; i++)
        {
            SelectHorizontalScrollItem item = Instantiate(itemPrefab, itemParent)
                .GetComponent<SelectHorizontalScrollItem>();

            item.itemIndex = i;
            items[i] = item;
        }
    }

    /// <summary>
    /// 设置选项信息
    /// </summary>
    public void SetItemsInfo(
        string[] names,
        Sprite[] sprites,
        string[] descriptions)
    {
        if (names.Length != sprites.Length || sprites.Length != descriptions.Length
            || names.Length != descriptions.Length)
        {
            Debug.Log("选择数据不完整");
            return;
        }

        itemInfos = new ItemInfo[names.Length];
        for (int i = 0; i < itemInfos.Length; i++)
        {
            itemInfos[i] = new ItemInfo(names[i], sprites[i], descriptions[i]);
        }

        SelectAction = null;
        isSelected = false;
    }

    #endregion


    #region ===== Selection =====

    /// <summary>
    /// 点击选择
    /// </summary>
    public void Select(
        int itemIndex,
        int infoIndex,
        RectTransform itemRectTransform)
    {
        if (!isSelected && itemIndex == currentItemIndex)
        {
            SelectAction?.Invoke(infoIndex);
            isSelected = true;
            Debug.Log("select " + (infoIndex + 1).ToString());
        }
        else
        {
            isSelectMove = true;
            selectItemX = itemRectTransform.localPosition.x;
        }
    }

    #endregion


    #region ===== Movement =====

    /// <summary>
    /// 移动列表
    /// </summary>
    private void MoveItems(int offsetTimes)
    {
        for (int i = 0; i < displayNumber; i++)
        {
            float x = itemSpace * (i - offsetTimes) - displayWidth / 2;
            items[i].rectTransform.localPosition = new Vector2(
                x,
                items[i].rectTransform.localPosition.y
            );
        }

        int middle;

        if (offsetTimes > 0)
        {
            middle = itemInfos.Length - offsetTimes % itemInfos.Length;
        }
        else
        {
            middle = -offsetTimes % itemInfos.Length;
        }

        int infoIndex = middle;

        // 从中间正向循环赋值
        for (int i = Mathf.FloorToInt(displayNumber / 2f); i < displayNumber; i++)
        {
            if (infoIndex >= itemInfos.Length)
            {
                infoIndex = 0;
            }

            items[i].SetInfo(
                itemInfos[infoIndex].sprite,
                itemInfos[infoIndex].name,
                itemInfos[infoIndex].description,
                infoIndex,
                this
            );

            infoIndex++;
        }

        // 从中间的上一个反向循环赋值
        infoIndex = middle - 1;
        for (int i = Mathf.FloorToInt(displayNumber / 2f) - 1; i >= 0; i--)
        {
            if (infoIndex <= -1)
            {
                infoIndex = itemInfos.Length - 1;
            }

            items[i].SetInfo(
                itemInfos[infoIndex].sprite,
                itemInfos[infoIndex].name,
                itemInfos[infoIndex].description,
                infoIndex,
                this
            );

            infoIndex--;
        }
    }

    /// <summary>
    /// 控制选项的透明度和缩放，获取中间的选项
    /// </summary>
    private void ItemsControl()
    {
        distances = new float[displayNumber];
        for (int i = 0; i < displayNumber; i++)
        {
            float distance = Mathf.Abs(items[i].rectTransform.position.x - transform.position.x);
            distances[i] = distance;

            float scale = 1 - distance * scaleMultiplying;
            items[i].rectTransform.localScale = new Vector3(scale, scale, 1);
            items[i].SetAlpha(1 - distance * alphaMultiplying);
        }

        float minDistance = itemSpace * displayNumber;
        int minIndex = 0;
        for (int i = 0; i < displayNumber; i++)
        {
            if (distances[i] < minDistance)
            {
                minDistance = distances[i];
                minIndex = i;
            }
        }

        descriptionText.text = items[minIndex].description;
        currentItemIndex = items[minIndex].itemIndex;
    }

    /// <summary>
    /// 自动吸附
    /// </summary>
    private void Adsorption()
    {
        float targetX;

        if (!isSelectMove)
        {
            float distance = itemParent.localPosition.x % itemSpace;
            int times = Mathf.FloorToInt(itemParent.localPosition.x / itemSpace);

            if (distance > 0)
            {
                if (distance < itemSpace / 2)
                {
                    targetX = times * itemSpace;
                }
                else
                {
                    targetX = (times + 1) * itemSpace;
                }
            }
            else
            {
                if (distance < -itemSpace / 2)
                {
                    targetX = times * itemSpace;
                }
                else
                {
                    targetX = (times + 1) * itemSpace;
                }
            }
        }
        else
        {
            targetX = -selectItemX;
        }

        itemParent.localPosition = new Vector2(
            Mathf.Lerp(itemParent.localPosition.x, targetX, moveSmooth / 10),
            itemParent.localPosition.y
        );
    }

    #endregion


    #region ===== Drag Events =====

    public void OnDrag(PointerEventData eventData)
    {
        isSelectMove = false;

        itemParent.localPosition = new Vector2(
            itemParent.localPosition.x + eventData.delta.x * dragSpeed,
            itemParent.localPosition.y
        );
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDrag = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDrag = false;
    }

    #endregion
}