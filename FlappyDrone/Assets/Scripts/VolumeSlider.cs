using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeSlider : MonoBehaviour {
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();

        // Устанавливаем значение по умолчанию 0.5, если SoundManager не найден
        float defaultValue = 0.5f;

        SoundManager soundManager = SoundManager.Instance;
        if (soundManager != null)
        {
            defaultValue = soundManager.musicVolume;
        }

        // Устанавливаем значение без вызова события
        slider.SetValueWithoutNotify(defaultValue);

        // Добавляем обработчик изменения значения
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        SoundManager soundManager = SoundManager.Instance;
        if (soundManager != null)
        {
            soundManager.SetMusicVolume(value);
        }
    }
}