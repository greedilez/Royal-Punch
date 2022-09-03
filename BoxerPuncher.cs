using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

[RequireComponent(typeof(Boxer))]
public class BoxerPuncher : MonoBehaviour
{
    [SerializeField] private float _punchForce = 10f;

    [SerializeField] private float _minBeatDistance = 0.95f;

    [SerializeField] private float _betweenPunchDelay = 1f;

    private AdditionalBoxer _additionalBoxer;

    private Boxer _boxer;

    [SerializeField] private Animator _animator;

    [SerializeField] private int _multiplier = 1;

    [SerializeField] private int _multiplierSmaller = 1;

    public int MultiplierSmaller{ get => _multiplierSmaller; }

    [SerializeField] private TMP_Text _multiplierText;

    private bool _isPunched = false;

    public UnityEvent OnFisrtPunch, OnSecondPunch;

    private int _punchIteration = 0;

    private void Awake() => Init();

    private void Init(){
        _additionalBoxer = FindObjectOfType<AdditionalBoxer>();
        _boxer = GetComponent<Boxer>();
    }

    private void FixedUpdate(){
        BeatAdditionalBoxerOnNearDistance();
        UpdateMultiplier();
    }

    private void BeatAdditionalBoxerOnNearDistance(){
        if(Vector3.Distance(transform.position, _additionalBoxer.transform.position) <= _minBeatDistance){
            if(!_isPunched){
                if(!_additionalBoxer.IsGameOver && !_boxer.IsGameOver){
                    BeatAdditionalBoxer(ref _isPunched, _punchForce, PunchDelay());
                    StartCoroutine(ArmPunchDelay());
                }
            }
        }
    }

    private IEnumerator ArmPunchDelay(){
        yield return new WaitForSeconds(0.01f);{
            switch(_punchIteration){
                case 0:
                OnFisrtPunch.Invoke();
                _punchIteration++;
                break;

                case 1:
                OnSecondPunch.Invoke();
                _punchIteration = 0;
                break;
            }
        }
    }

    public void BeatAdditionalBoxer(ref bool isPunchedState, float punchForce, IEnumerator punchDelay){
        _additionalBoxer.GetPunch((punchForce * (_multiplier + _multiplierSmaller / 10)) + Random.Range(3, 7));
        isPunchedState = true;
        _animator.SetTrigger("Punch");
        StartCoroutine(punchDelay);
        SmallerMultiplierUp(Random.Range(1, 3));
    }

    private IEnumerator PunchDelay(){
        yield return new WaitForSeconds(_betweenPunchDelay); if(_isPunched) _isPunched = false;
    }

    private void SmallerMultiplierUp(int upAmount){
        if((_multiplierSmaller + upAmount) < 10) _multiplierSmaller += upAmount;
        else{
            _multiplierSmaller = 0;
            _multiplier++;
        }
    }

    private void UpdateMultiplier() => _multiplierText.text = $"X{_multiplier}.{_multiplierSmaller}";
}
