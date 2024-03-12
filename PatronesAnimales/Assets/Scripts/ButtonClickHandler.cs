using UnityEngine;
using UnityEngine.UI;

public class ButtonClickHandler : MonoBehaviour
{
    public enum ButtonType { Model, Trajectory }
    public ButtonType type;

    public int modelOrTrajectoryID; // 1, 2, 3 for model or trajectory

    private Button btn;

    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        GameController.Instance.ButtonClicked(this);
    }
}
