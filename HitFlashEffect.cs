using UnityEngine;

interface ITriggerable{ public void TriggerAnimation(string triggerName); }

interface IFollowable{ public void FollowTransform(Transform target); }

[RequireComponent(typeof(Animator))]
public class HitFlashEffect : MonoBehaviour, ITriggerable, IFollowable
{
    public Animator HitFlashAnimator{ get; private set; }

    [SerializeField] private Transform _targetHitFlash;

    private void Awake() => HitFlashAnimator = GetComponent<Animator>();

    private void FixedUpdate() => FollowTransform(_targetHitFlash);

    public void FollowTransform(Transform target){
        transform.position = target.position;
        transform.rotation = target.rotation;
    }

    public void TriggerAnimation(string triggerName) => HitFlashAnimator.SetTrigger(triggerName);
}
