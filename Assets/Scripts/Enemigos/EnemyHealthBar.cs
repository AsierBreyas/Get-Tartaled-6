using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] Slider slider;
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void UpdateHealthbar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }

    private void Update()
    {
        transform.rotation = mainCamera.transform.rotation;
        transform.position = target.position + offset;
    }
}
