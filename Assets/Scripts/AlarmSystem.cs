using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private float _volumeGrowth;

    private AudioSource _audioSource;
    private bool _isAlarm;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0f;
    }

    private void Update()
    {
        AlarmValidate();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Bandit bandit))
        {
            _audioSource.Play();
            _isAlarm = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Bandit bandit))
        {
            _isAlarm = false;
        }
    }

    private void AlarmValidate()
    {
        if(_isAlarm)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, 1, _volumeGrowth*Time.deltaTime);
        }
        else if(_isAlarm == false && _audioSource.volume > 0)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, 0, _volumeGrowth*Time.deltaTime);

            if(_audioSource.volume <= 0 ) 
            {
                _audioSource.Stop();
            }
        }
    }
}