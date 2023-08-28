using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using UnityEngine.EventSystems;

public class TriggerCursor :
    MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler,
    IPointerClickHandler,
    IInitializePotentialDragHandler,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler,
    IDropHandler,
    IScrollHandler,
    IUpdateSelectedHandler,
    ISelectHandler,
    IDeselectHandler,
    IMoveHandler,
    ISubmitHandler,
    ICancelHandler
{
    [SerializeField] private CustomCursor cursor;
    [SerializeField] private TriggerEvent triggerTrueEvent = TriggerEvent.PointerEnter;
    [SerializeField] private TriggerEvent triggerFalseEvent = TriggerEvent.PointerExit;

    private Selectable _selectable;

    private enum TriggerEvent
    {
        None,
        PointerEnter,
        PointerExit,
        PointerDown,
        PointerUp,
        PointerClick,
        InitializePotentialDrag,
        BeginDrag,
        Drag,
        EndDrag,
        Drop,
        Scroll,
        UpdateSelected,
        Select,
        Deselect,
        Move,
        Submit,
        Cancel
    }

    private void Awake()
    {
        _selectable = GetComponent<Selectable>();
    }

    private void OnValidate()
    {
        if (triggerTrueEvent == triggerFalseEvent && triggerTrueEvent != TriggerEvent.None)
            triggerFalseEvent = TriggerEvent.None;
    }

    private void OnDisable()
    {
        ActivateCursor(false);
    }

    [Button("Reset events")]
    public void ResetEvents()
    {
        triggerTrueEvent = TriggerEvent.PointerEnter;
        triggerFalseEvent = TriggerEvent.PointerExit;
    }

    #region Event Interfaces

    public void OnPointerEnter(PointerEventData eventData) => Call(TriggerEvent.PointerEnter);
    public void OnPointerExit(PointerEventData eventData) => Call(TriggerEvent.PointerExit);
    public void OnPointerDown(PointerEventData eventData) => Call(TriggerEvent.PointerDown);
    public void OnPointerUp(PointerEventData eventData) => Call(TriggerEvent.PointerUp);
    public void OnPointerClick(PointerEventData eventData) => Call(TriggerEvent.PointerClick);
    public void OnInitializePotentialDrag(PointerEventData eventData) => Call(TriggerEvent.InitializePotentialDrag);
    public void OnBeginDrag(PointerEventData eventData) => Call(TriggerEvent.BeginDrag);
    public void OnDrag(PointerEventData eventData) => Call(TriggerEvent.Drag);
    public void OnEndDrag(PointerEventData eventData) => Call(TriggerEvent.EndDrag);
    public void OnDrop(PointerEventData eventData) => Call(TriggerEvent.Drop);
    public void OnScroll(PointerEventData eventData) => Call(TriggerEvent.Scroll);
    public void OnUpdateSelected(BaseEventData eventData) => Call(TriggerEvent.UpdateSelected);
    public void OnSelect(BaseEventData eventData) => Call(TriggerEvent.Select);
    public void OnDeselect(BaseEventData eventData) => Call(TriggerEvent.Deselect);
    public void OnMove(AxisEventData eventData) => Call(TriggerEvent.Move);
    public void OnSubmit(BaseEventData eventData) => Call(TriggerEvent.Submit);
    public void OnCancel(BaseEventData eventData) => Call(TriggerEvent.Cancel);

    #endregion

    private void Call(TriggerEvent evt)
    {
        if (_selectable && !_selectable.interactable)
            return;

        if (triggerTrueEvent == evt)
            ActivateCursor(true);
        else if (triggerFalseEvent == evt)
            ActivateCursor(false);
    }

    private void ActivateCursor(bool value) => CursorManager.instance.SetCursor(value, cursor);
}