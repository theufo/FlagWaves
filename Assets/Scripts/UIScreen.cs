using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScreen : MonoBehaviour
{
    [SerializeField] private TMP_InputField _xInputField;
    [SerializeField] private TMP_InputField _yInputField;
    [SerializeField] private Button _updateButton;

    [SerializeField] private GridComponent _cpuSlideGridComponent;
    [SerializeField] private GridComponent _gpuSlideGridComponent;
    [SerializeField] private GridComponent _cpuWaveGridComponent;
    [SerializeField] private GridComponent _gpuWaveGridComponent;
    
    private void Start()
    {
        _updateButton.onClick.AddListener(OnUpdatePressed);
        _xInputField.text = _cpuSlideGridComponent.XVertices.ToString();
        _yInputField.text = _cpuSlideGridComponent.YVertices.ToString();
    }

    private void OnUpdatePressed()
    {
        _cpuSlideGridComponent.SetSize(int.Parse(_xInputField.text), int.Parse(_yInputField.text));
        _gpuSlideGridComponent.SetSize(int.Parse(_xInputField.text), int.Parse(_yInputField.text));
        _cpuWaveGridComponent.SetSize(int.Parse(_xInputField.text), int.Parse(_yInputField.text));
        _gpuWaveGridComponent.SetSize(int.Parse(_xInputField.text), int.Parse(_yInputField.text));
    }
}
