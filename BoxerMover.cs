using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoxerMover : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField] private float _moveSpeed = 10f;

    [SerializeField] private float _lookSpeed = 10f;

    [SerializeField] private Joystick _joystick;

    public Joystick CurrentJoystick{ get => _joystick; }

    [SerializeField] private Space _rotationSpace = Space.Self;

    private void Awake() => Init();

    private void Init(){
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate(){
        Move();
        Look();
    }

    private void Move(){
        MoveAlongAxis(_joystick.Vertical, transform.forward);
        MoveAlongAxis(_joystick.Horizontal, transform.right);
    }

    public void MoveAlongAxis(float axis, Vector3 force) => transform.Translate(force * (axis * _moveSpeed * Time.deltaTime), Space.World);

    private void Look() => transform.Rotate(new Vector3(0, (-_joystick.Horizontal * _lookSpeed), 0), _rotationSpace);
}
