using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MoveMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _content = default;

    [SerializeField]
    private GameObject _menuItemTemplate = default;

    [SerializeField]
    private CanvasGroup _canvasGroup = default;

    public void Hide()
    {
        _canvasGroup.interactable = false;
        _canvasGroup.alpha = 0f;
        _canvasGroup.blocksRaycasts = false;
    }

    public void Show()
    {
        _canvasGroup.interactable = true;
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
    }

    public void AddMenuItems(List<MoveSpec> list)
    {
        foreach (MoveSpec move in list)
        {
            AddMenuItem(move);
        }
    }

    public void AddMenuItem(MoveSpec move)
    {
        string label = move.Name;
        GameObject menuItem = Instantiate(_menuItemTemplate, transform.position, transform.rotation);
        menuItem.name = label;
        menuItem.transform.SetParent(_content.transform, false);
        menuItem.SetActive(true);
        menuItem.GetComponentInChildren<Text>().text = label;
        MoveSelector moveChooser = menuItem.GetComponent<MoveSelector>();
        moveChooser.move = move;
    }
}
