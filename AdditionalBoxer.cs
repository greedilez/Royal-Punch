using UnityEngine.Events;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class AdditionalBoxer : MonoBehaviour, IPunchable
{
    [SerializeField] private float _health = 100;

    private bool _isGameOver = false;

    public bool IsGameOver{ get => _isGameOver; }

    public UnityEvent OnGameOver;

    [SerializeField] private RagdollTuner _ragdoll;

    private Boxer _boxer;

    private Animator _animator;

    [SerializeField] private Image _healthBar;

    [SerializeField] private TMP_Text _healthText;

    private void Awake(){
        _boxer = FindObjectOfType<Boxer>();
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
            if(_boxer.OnGameWin != null) _boxer.OnGameWin.Invoke();
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
}
