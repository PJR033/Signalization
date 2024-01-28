using UnityEngine;

[RequireComponent(typeof(Signalization))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider2D))]
public class SignalizationSwitcher : MonoBehaviour
{
    private Signalization _alarmSystem;
    private AudioSource _audioSource;
    private Coroutine _changeSmoothly;

    private void Awake()
    {
        _alarmSystem = GetComponent<Signalization>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        int volumeMaxValue = 1;

        if (other.gameObject.TryGetComponent(out Bandit bandit))
        {
            if (_changeSmoothly != null)
                StopCoroutine(_changeSmoothly);

            _audioSource.Play();
            _changeSmoothly = StartCoroutine(_alarmSystem.ChangeSmoothly(volumeMaxValue));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        int volumeMinValue = 0;

        if (other.gameObject.TryGetComponent(out Bandit bandit))
        {
            if (_changeSmoothly != null)
                StopCoroutine(_changeSmoothly);

            _changeSmoothly = StartCoroutine(_alarmSystem.ChangeSmoothly(volumeMinValue));

            if (_audioSource.volume == 0f)
                _audioSource.Stop();
        }
    }
}
