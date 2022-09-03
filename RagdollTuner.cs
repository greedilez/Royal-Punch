using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollTuner : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [SerializeField] private bool _autoTuneAtStart = false;

    [SerializeField] private bool _autoDieAtStart = false;

    private void Start(){
        if(_autoTuneAtStart){
            SetColliderState(false);
            SetRigidbodyState(true);
        }

        if(_autoDieAtStart) Die();
    }

    public void Die(){
        TurnOffAnimator(_animator);
        SetRigidbodyState(false);
        SetColliderState(true);
    }

    public void SetRigidbodyState(bool state){
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody rigidbody in rigidbodies) rigidbody.isKinematic = state;
        GetComponent<Rigidbody>().isKinematic = !state;
    }

    public void SetColliderState(bool state){
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach(Collider collider in colliders) collider.enabled = state;
        GetComponent<Collider>().enabled  = !state;
    }

    public void TurnOffAnimator(Animator animator) => animator.enabled = false;
}
