
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class LayoutItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI layoutText;
    [SerializeField] private Image checkedIcon;
    [SerializeField] private Toggle checkedToggle;
    public int Rows;
    public int Columns;

    public bool IsSelected = false;
    public int ItemIndex;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    public void Render(int index,int row, int column , ToggleGroup tGroup)
    {
        ItemIndex = index;
        checkedToggle.group = tGroup;   
        Rows = row; 
        Columns = column;
        checkedIcon.enabled = false;

        layoutText.text = string.Format("LAYOUT {0}x{1}",row,column);

        checkedToggle.onValueChanged.RemoveAllListeners();
        checkedToggle.onValueChanged.AddListener(OnLayoutSelected);


    }

    public void OnLayoutSelected(bool isSelected)
    {
        if (isSelected)
        {
            checkedIcon.enabled = true;
            IsSelected = true;
            MainMenuManager.LayoutSectedAction?.Invoke(new Vector2Int(Rows,Columns));
        }
        else
        {
            checkedIcon.enabled = false;
            IsSelected = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetEnable()
    {
        checkedToggle.isOn = true;
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetDisable()
    {
        checkedToggle.isOn = false;
    }
}
