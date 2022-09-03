using UnityEngine;

public class BoxerAnimator : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] private BoxerMover _mover;

    [SerializeField] private Transform _armature;

    private void Awake() => Init();

    private void Init() => _animator = GetComponent<Animator>();

    private void FixedUpdate() => Animate();

    private void Animate(){
        int targetHorizontal = (int)(_mover.CurrentJoystick.Horizontal * 2);
        int targetVertical = (int)(_mover.CurrentJoystick.Vertical * 2);
        _animator.SetInteger("Horizontal", targetHorizontal);
        _animator.SetInteger("Vertical", targetVertical);
        TurnArmatureAlongHorizontal();
    }

    private void TurnArmatureAlongHorizontal() => _armature.localRotation = Quaternion.Euler(_armature.rotation.eulerAngles.x, (_mover.CurrentJoystick.Horizontal * 90f), _armature.rotation.eulerAngles.z);
}
