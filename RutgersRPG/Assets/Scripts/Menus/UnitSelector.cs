using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelector : MonoBehaviour
{
    [SerializeField]
    private Color _selectableColor, _selectedColor;
    [SerializeField]
    private Image _clickable;
    [SerializeField]
    private CanvasGroup _canvasGroup;

    public Unit unit;

    public UnitSelectedEvent _unitSelectedEvent = default;

    private void Awake() {
        _unitSelectedEvent.AddListener(BattleSystem.Instance.RecordTarget);
    }

    public void PotentialTarget() {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        _clickable.color = _selectableColor;
    }

    public void SelectAsTarget() {
        if (_clickable.color == _selectedColor) _clickable.color = _selectableColor;
        else _clickable.color = _selectedColor;
        _unitSelectedEvent.Invoke(unit);
    }

    public void NoLongerTarget() {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }
}
