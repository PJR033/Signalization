using UnityEngine;

[RequireComponent(typeof(AlarmSystem))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider2D))]
public class EnterValidator : MonoBehaviour
{
    private AlarmSystem _alarmSystem;
    private AudioSource _audioSource;
    private Coroutine _increaseSmoothly;
    private Coroutine _decreaseSmoothly;

    private void Awake()
    {
        _alarmSystem = GetComponent<AlarmSystem>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Bandit bandit))
        {
            if (_decreaseSmoothly != null)
            {
                StopCoroutine(_decreaseSmoothly);
            }

            _audioSource.Play();
            _increaseSmoothly = StartCoroutine(_alarmSystem.IncreaseSmoothly());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Bandit bandit))
        {
            if (_increaseSmoothly != null)
            {
                StopCoroutine(_increaseSmoothly);
            }
            
            _decreaseSmoothly = StartCoroutine(_alarmSystem.DecreaseSmoothly());

            if (_audioSource.volume == 0f)
            {
                _audioSource.Stop();
            }
        }
    }
}
