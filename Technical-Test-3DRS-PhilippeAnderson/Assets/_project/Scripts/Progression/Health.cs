using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Stats")]
    public float maxHP = 100;
    public float currentHP;

    [Header("UI")]
    public Canvas worldCanvas;        // Canvas em modo World Space
    public Slider healthSlider;       // Slider acima da cabeça
    public bool hideWhenFull = true;

    Transform cam;

    public System.Action OnDie;
    
    public bool IsDead => currentHP <= 0;

    void Awake()
    {
        currentHP = maxHP;
    }

    void Start()
    {
        cam = Camera.main.transform;
        UpdateUI(true);
    }

    void Update()
    {
        if (worldCanvas != null && cam != null)
        {
            // barra sempre olhando para câmera
            worldCanvas.transform.LookAt(
                worldCanvas.transform.position + cam.forward,
                cam.up
            );
        }
    }

    public void TakeDamage(float amount)
    {
        currentHP -= amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        UpdateUI();

        if (currentHP == 0)
        {
            OnDie?.Invoke();  // evento para PlayerController, Enemy, GameManager etc.
            if (transform.tag == "Player")
            {
                GameManager.Instance.ShowGameOver();
            }
        }
    }

    public void Heal(float amount)
    {
        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UpdateUI();
    }

    void UpdateUI(bool forceShow = false)
    {
        if (healthSlider == null) return;

        healthSlider.maxValue = maxHP;
        healthSlider.value = currentHP;

        if (hideWhenFull)
        {
            bool isFull = currentHP >= maxHP;
            worldCanvas.gameObject.SetActive(forceShow || !isFull);
        }
    }
}