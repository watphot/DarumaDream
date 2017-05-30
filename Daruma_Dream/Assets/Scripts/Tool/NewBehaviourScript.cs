using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class NewBehaviourScript : MonoBehaviour
{

    #region Public Variables

    /// <summary>
    /// Permite obtener y actualizar el Gameobject que se esta seleccionando o ha sido seleccionado.
    /// </summary>
    public EventSystem eventSystem;

    /// <summary>
    /// El Gameobject que incialmente tiene el foco.
    /// </summary>
    public GameObject firstSelected;

    /// <summary>
    /// El Gameobject que se desea modificar su posición.
    /// </summary>
    public GameObject buttonSelector;

    /// <summary>
    /// Permite que el Gameobject buttonSelector coja la posición en X del Gameobject firstSelected.
    /// </summary>
    public bool positionX;

    /// <summary>
    /// Permite que el Gameobject buttonSelector coja la posición en Y del Gameobject firstSelected.
    /// </summary>
    public bool positionY;

    /// <summary>
    /// Permite que el Gameobject buttonSelector coja la posición en Z del Gameobject firstSelected.
    /// </summary>
    public bool positionZ;

    #endregion

    #region Private Variables

    /// <summary>
    /// Permite lanzar rayos para comprobar si se esta encima de un Gameobject.
    /// </summary>
    private GraphicRaycaster myGraphicRaycaster;

    /// <summary>
    /// Permite actualizar la posición del ratón.
    /// </summary>
    private PointerEventData eventData;

    /// <summary>
    /// Lista que almacena el Gameobject cuando el ratón está encima de él.
    /// </summary>
    private List<RaycastResult> raycastResults = new List<RaycastResult>();

    /// <summary>
    /// Posición que tendrá el Gameobject buttonSelector cuando se seleccione un boton.
    /// </summary>
    private Vector3 finalPosition;

    /// <summary>
    /// Transición de Tipo Colortint del elemento del canvas que actualmente tiene el foco.
    /// </summary>
    private ColorBlock _newBlock;

    /// <summary>
    /// Transición de Tipo SpriteSwap del elemento del canvas que actualmente tiene el foco.
    /// </summary>
    private SpriteState _spriteState;

    /// <summary>
    /// Transición de Tipo Animation del elemento del canvas que actualmente tiene el foco.
    /// </summary>
    private AnimationTriggers _animationTriggers;

    /// <summary>
    /// Acceso a todos los tipos de transiciónes.
    /// </summary>
    private Selectable.Transition _transition;

    #endregion

    #region Main Methods

    void Start()
    {

        myGraphicRaycaster = gameObject.GetComponent<GraphicRaycaster>();

        eventData = new PointerEventData(null);

        _transition = firstSelected.GetComponent<Selectable>().transition;

        switch (_transition)
        {

            case (Selectable.Transition.None):

                break;

            case (Selectable.Transition.ColorTint):

                firstSelected = eventSystem.firstSelectedGameObject;

                _newBlock = firstSelected.GetComponent<Selectable>().colors;

                break;

            case (Selectable.Transition.SpriteSwap):

                firstSelected = eventSystem.firstSelectedGameObject;

                _spriteState = firstSelected.GetComponent<Selectable>().spriteState;

                break;

            case (Selectable.Transition.Animation):

                firstSelected = eventSystem.firstSelectedGameObject;

                _animationTriggers = firstSelected.GetComponent<Selectable>().animationTriggers;

                break;

        }


    }

    void Update()
    {
        GetMouseOverObject();

        SetButtonSelectorPosition();

        Autofocus();

        RayCasting();

    }

    #endregion

    #region Utility Methods

    /// <summary>
    /// Devuelve el foco al último elemento del canvas con foco cuando se pierde el foco.
    /// </summary>
    public void Autofocus()
    {

        _transition = firstSelected.GetComponent<Selectable>().transition;

        switch (_transition)
        {

            case (Selectable.Transition.None):

                if (eventSystem.currentSelectedGameObject == null)
                {

                    eventSystem.SetSelectedGameObject(firstSelected);

                }

                else
                {

                    firstSelected = eventSystem.currentSelectedGameObject;

                }

                break;

            case (Selectable.Transition.ColorTint):

                if (eventSystem.currentSelectedGameObject == null)
                {

                    _newBlock.fadeDuration = 0;

                    eventSystem.SetSelectedGameObject(firstSelected);

                }

                else
                {

                    _newBlock = firstSelected.GetComponent<Selectable>().colors;

                    firstSelected = eventSystem.currentSelectedGameObject;

                }

                break;

            case (Selectable.Transition.SpriteSwap):

                if (eventSystem.currentSelectedGameObject == null)
                {

                    _spriteState.pressedSprite = _spriteState.highlightedSprite;

                    eventSystem.SetSelectedGameObject(firstSelected);

                }

                else
                {

                    _spriteState = firstSelected.GetComponent<Selectable>().spriteState;

                    firstSelected = eventSystem.currentSelectedGameObject;

                }

                break;

            case (Selectable.Transition.Animation):

                if (eventSystem.currentSelectedGameObject == null)
                {

                    _animationTriggers.highlightedTrigger = _animationTriggers.highlightedTrigger;

                    eventSystem.SetSelectedGameObject(firstSelected);

                }

                else
                {

                    _animationTriggers = firstSelected.GetComponent<Selectable>().animationTriggers;

                    firstSelected = eventSystem.currentSelectedGameObject;

                }

                break;

        }

    }

    /// <summary>
    /// Actualiza la posición del ratón y comprueba si esta encima de un Gameobject.
    /// </summary>
    public void GetMouseOverObject()
    {
        eventData.position = Input.mousePosition;

        myGraphicRaycaster.Raycast(eventData, raycastResults);

    }

    /// <summary>
    /// Restricciones de donde se va a posicionar el Gameobject buttonSelector.
    /// </summary>
    public void SetButtonSelectorPosition()
    {

        if (positionX)
        {

            finalPosition.x = firstSelected.transform.position.x;

        }

        else
        {

            finalPosition.x = buttonSelector.transform.position.x;

        }

        if (positionY)
        {

            finalPosition.y = firstSelected.transform.position.y;

        }

        else
        {

            finalPosition.y = buttonSelector.transform.position.y;

        }

        if (positionZ)
        {

            finalPosition.z = firstSelected.transform.position.z;

        }

        else
        {

            finalPosition.z = buttonSelector.transform.position.z;

        }

        buttonSelector.transform.position = new Vector3(finalPosition.x, finalPosition.y, finalPosition.z);

    }

    /// <summary>
    /// Comprueba si el ratón está encima de un Gameobject y lo almacena.
    /// </summary>
    public void RayCasting()
    {

        if (raycastResults.Count > 0)
        {

            firstSelected = raycastResults[raycastResults.Count - 1].gameObject;

            eventSystem.SetSelectedGameObject(firstSelected);

            raycastResults.Clear();

        }

    }

    #endregion

}
