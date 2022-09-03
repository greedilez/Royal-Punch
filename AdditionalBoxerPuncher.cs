using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AdditionalBoxer))]
public class AdditionalBoxerPuncher : MonoBehaviour
{
    [SerializeField] private float _punchForce = 10f;

    [SerializeField] private float _minBeatDistance = 0.95f;

    [SerializeField] private float _betweenPunchDelay = 1f;

    private Boxer _boxer;

    private BoxerPuncher _boxerPuncher;

    private AdditionalBoxer _additionalBoxer;

    [SerializeField] private Animator _animator;

    private bool _isPunched = false;

    [SerializeField] private RedCircle _redCircle;

    private bool _isShowedRedCircle = false;

    private void Awake() => Init();

    private void Init(){
        _boxer = FindObjectOfType<Boxer>();
        _additionalBoxer = GetComponent<AdditionalBoxer>();
        _boxerPuncher = FindObjectOfType<BoxerPuncher>();
    }

    private void FixedUpdate(){
        BeatAdditionalBoxerOnNearDistance();
        TurnToBoxer();
        TurnRedCircleOnOneHalfMultiplier();
    }

    private void BeatAdditionalBoxerOnNearDistance(){
        if(Vector3.Distance(transform.position, _boxer.transform.position) <= _minBeatDistance){
            if(!_isPunched) BeatAdditionalBoxer(ref _isPunched, _punchForce, PunchDelay());
        }
    }

    public void BeatAdditionalBoxer(ref bool isPunchedState, float punchForce, IEnumerator punchDelay){
        if(!_boxer.IsGameOver && !_additionalBoxer.IsGameOver){
            _boxer.GetPunch(punchForce + Random.Range(5, 10));
            isPunchedState = true;
            _animator.SetTrigger("Punch");
            StartCoroutine(punchDelay);
        }
    }

    private IEnumerator PunchDelay(){
        yield return new WaitForSeconds(_betweenPunchDelay + Random.Range(0.5f, 1f)); if(_isPunched) _isPunched = false;
    }

    private void TurnToBoxer() => transform.LookAt(_boxer.transform);

    private void TurnRedCircleOnOneHalfMultiplier(){
        int multiplier = _boxerPuncher.MultiplierSmaller;
        int showCircleMultiplier = 5;
        if(multiplier == showCircleMultiplier || multiplier == showCircleMultiplier + 1) TurnRedCircle();
    }

    private void TurnRedCircle(){
        if(!_isShowedRedCircle){
            Debug.Log("Turn red cirlce");
            _redCircle.TriggerAnimation("ShowCircle");
            StartCoroutine(ThrowCircleDamage());
            _isShowedRedCircle = true;
        }
        else Debug.Log("Already showed red cirlce");
    }

    private IEnumerator ThrowCircleDamage(){
        yield return new WaitForSeconds(1f);{
            _redCircle.ThrowDamage();
            _redCircle.gameObject.SetActive(false);
        }
    }
}
