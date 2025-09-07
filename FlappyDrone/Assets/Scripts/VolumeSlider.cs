using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeSlider : MonoBehaviour {
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();

        // ������������� �������� �� ��������� 0.5, ���� SoundManager �� ������
        float defaultValue = 0.5f;

        SoundManager soundManager = SoundManager.Instance;
        if (soundManager != null)
        {
            defaultValue = soundManager.musicVolume;
        }

        // ������������� �������� ��� ������ �������
        slider.SetValueWithoutNotify(defaultValue);

        // ��������� ���������� ��������� ��������
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