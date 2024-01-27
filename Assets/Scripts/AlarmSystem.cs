using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private float _volumeChange;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0f;
    }

    public IEnumerator IncreaseSmoothly()
    {
        float volumMaxValue = 1;

        while (_audioSource.volume < volumMaxValue)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, volumMaxValue, _volumeChange * Time.deltaTime);
            yield return null;
        }
    }

    public IEnumerator DecreaseSmoothly()
    {
        float volumMinValue = 0f;

        while (_audioSource.volume > volumMinValue)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, volumMinValue, _volumeChange * Time.deltaTime);
            yield return null;
        }
    }
}