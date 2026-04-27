using UnityEngine;

public class SFXManager : StateMachineBehaviour
{
    [SerializeField] private float _timer;
    private AudioSource _audioSource;
    private bool _isPlaying;
    
    public AudioClip _soundEffect;
    public float _delay;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!_audioSource)
            _audioSource = animator.GetComponent<AudioSource>();

        _timer = 0;
        _isPlaying = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer += Time.deltaTime;
        
        if(!_isPlaying && _timer >= _delay)
        {
            PlaySound();
            _isPlaying = true;
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer = 0;
        _isPlaying = false;
    }

    private void PlaySound()
    {
            if(_audioSource &&  _soundEffect )
                _audioSource.PlayOneShot(_soundEffect);
    }
}
