using UnityEngine.Events;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

interface IPunchable{ public void GetPunch(float punchForce); }

[RequireComponent(typeof(Animator))]
public class Boxer : MonoBehaviour, IPunchable
{
    [SerializeField] private float _health = 100;

    private bool _isGameOver = false;

    public bool IsGameOver{ get => _isGameOver; }

    public UnityEvent OnGameOver, OnGameWin;

    [SerializeField] private RagdollTuner _ragdoll;

    private Animator _animator;

    [SerializeField] private TMP_Text _healthText;

    [SerializeField] private Image _healthBar;

    private void Awake(){
        _animator = GetComponent<Animator>();
    }

    public void GetPunch(float punchForce){
        if((_health - punchForce) > 0){
            _health -= punchForce;
            Debug.Log($"{gameObject.name} health is {_health}");
        }
        else{
            Debug.Log($"{gameObject.name} has been killed!");
            GameOver();
        }
    }

    private void GameOver(){
        if(!_isGameOver){
            _health = 0;
            _animator.SetBool("GameOver", true);
            _isGameOver = true;
            if(OnGameOver != null) OnGameOver.Invoke();
        }
    }

    private void FixedUpdate(){
        UpdateHealthText();
        UpdateHealthBar();
    }

    private void UpdateHealthText() => _healthText.text = _health.ToString();

    private void UpdateHealthBar() => _healthBar.fillAmount = _health / 100;

    public void SetBoolTrue(string boolName) => _animator.SetBool(boolName, true);
}
