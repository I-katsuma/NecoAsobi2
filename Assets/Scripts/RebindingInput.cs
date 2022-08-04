using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RebindingInput : MonoBehaviour
{
    [SerializeField] private PlayerInput _pInput;
    [SerializeField] private GameObject _ribindingMessage;
    [SerializeField] private GameObject _rebindingButton;
    [SerializeField] private Text _bindingName;

    [SerializeField] private InputActionReference _action;
    private string rebindingIndex;

    private InputActionRebindingExtensions.RebindingOperation _rebindingOperation;

    private void Start() 
    {
            
    }
}
