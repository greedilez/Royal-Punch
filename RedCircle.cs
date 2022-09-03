using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCircle : MonoBehaviour
{
    private AdditionalBoxer _additionalBoxer;

    private Boxer _boxer;

    private Animator _animator;

    private void Awake(){
        _additionalBoxer = FindObjectOfType<AdditionalBoxer>();
        _boxer = FindObjectOfType<Boxer>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate(){
        FollowAdditionalBoxer();
    }

    private void FollowAdditionalBoxer() => transform.position = _additionalBoxer.transform.position;

    public void TriggerAnimation(string triggerName) => _animator.SetTrigger(triggerName);

    public void ThrowDamage() => _boxer.GetPunch(Random.Range(5, 10));
}
