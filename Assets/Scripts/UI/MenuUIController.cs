using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

namespace UI
{
    public class MenuUIController : MonoBehaviour
    {
        [SerializeField] private InputActionReference _menuButtonReference;


        [SerializeField]
        private UIDocument _menuUIDocument;


        private bool _isMenuOpen = false;

        [SerializeField] private AudioMixer _audioMixer;

        [SerializeField]
        private UnityEvent _onOpenedSoundMenu;
        [SerializeField]
        private UnityEvent _onClosedSoundMenu;



        private Slider _sliderMaster;
        private Slider _sliderMusic;
        private Slider _sliderSFX;
        private void Awake()
        {
            _sliderMaster = _menuUIDocument.rootVisualElement.Q<Slider>("SliderMaster");
            _sliderSFX = _menuUIDocument.rootVisualElement.Q<Slider>("SliderSFX");
            _sliderMusic = _menuUIDocument.rootVisualElement.Q<Slider>("SliderMusic");
            _sliderMaster.RegisterValueChangedCallback(OnSliderValueChanged);
            _sliderSFX.RegisterValueChangedCallback(OnSliderValueChanged);
            _sliderMusic.RegisterValueChangedCallback(OnSliderValueChanged);
        }
        private void OnEnable()
        {
            _menuButtonReference.action.performed += ToggleMenu;
            _menuButtonReference.action.Enable();


            //_menuUIDocument.rootVisualElement.Q<Slider>("SliderMaster").value = _sliderMaster.value;
            //_menuUIDocument.rootVisualElement.Q<Slider>("SliderSFX").value = _sliderSFX.value;
            //_menuUIDocument.rootVisualElement.Q<Slider>("SliderMusic").value = _sliderMusic.value;



            Debug.Log("LLamandose");

        }
        private void Start()
        {
            InitializeMixerVolumes();


        }
        private void OnDisable()
        {
            _menuButtonReference.action.performed -= ToggleMenu;
            _menuButtonReference.action.Disable();
        }

        private void OnSliderValueChanged(ChangeEvent<float> changeEvent)
        {
            if (changeEvent.target == _sliderMaster)
            {

                _audioMixer.SetFloat("MasterVolume", GetDecibelsValue(_sliderMaster.value));
            }
            else if (changeEvent.target == _sliderSFX)
            {
                _audioMixer.SetFloat("SFXVolume", GetDecibelsValue(_sliderSFX.value));

            }
            else if (changeEvent.target == _sliderMusic)
            {
                _audioMixer.SetFloat("MusicVolume", GetDecibelsValue(_sliderMusic.value));

            }
        }
        private void InitializeMixerVolumes()
        {
            _audioMixer.SetFloat("MasterVolume", GetDecibelsValue(_sliderMaster.value));
            _audioMixer.SetFloat("SFXVolume", GetDecibelsValue(_sliderSFX.value));
            _audioMixer.SetFloat("MusicVolume", GetDecibelsValue(_sliderMusic.value));
        }
        private void ToggleMenu(InputAction.CallbackContext callbackContext)
        {
            _isMenuOpen = !_isMenuOpen;
            //_menuUIDocument.enabled = _isMenuOpen;

            if (!_isMenuOpen)
            {
                _onClosedSoundMenu?.Invoke();
                _menuUIDocument.panelSettings.targetDisplay = 1;

            }

            else
            {
                _onOpenedSoundMenu?.Invoke();

                _menuUIDocument.panelSettings.targetDisplay = 0;
            }
        }
        private float GetDecibelsValue(float inputValue)
        {
            float outputValue = Mathf.Lerp(-20, 5, inputValue / 100f);


            if (outputValue == -20) return -100f;

            return outputValue;
        }
    }

}
