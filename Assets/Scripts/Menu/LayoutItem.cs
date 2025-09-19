
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class LayoutItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI layoutText;
    [SerializeField] private Button layoutButton;
    [SerializeField] private Image checkedIcon;
    public int Rows;
    public int Columns;

    public bool IsSelected = false;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    public void Render(int row, int column)
    {
        Rows = row; 
        Columns = column;
        checkedIcon.enabled = false;

        layoutText.text = string.Format("LAYOUT {0}x{1}",row,column);

        layoutButton.onClick.RemoveAllListeners();
        layoutButton.onClick.AddListener(OnLayoutSelected);


    }

    public void OnLayoutSelected()
    {
        checkedIcon.enabled = true;
        IsSelected = true;
    }
}
