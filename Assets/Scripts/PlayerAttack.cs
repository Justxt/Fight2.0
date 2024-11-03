using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ComboState
{
    None,
    punch,
    punch2,
    PUNCH3,
    PUNCH4,
    kick
}

public class PlayerAttack : MonoBehaviour
{

    private CharacterAnimation playerAnim;
    private bool activateTimerToReset;
    private float defaultComboTimer = 0.4f;
    private float currentComboTimer;
    private ComboState currentComboState;

    [SerializeField]
    private GameObject punch1AttackPoint, punch2AttackPoint, kickAttackPoint;

    private void Awake()
    {
        playerAnim = GetComponent<CharacterAnimation>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentComboTimer = defaultComboTimer;
        currentComboState = ComboState.None;
    }

    // Update is called once per frame
    void Update()
    {
        ComboAttack();
        ResetComboState();
    }

    void ComboAttack()
    {
        // Golpe
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (currentComboState == ComboState.PUNCH4 || currentComboState == ComboState.kick)
            {
                return;
            }

            currentComboState++;
            activateTimerToReset = true;
            currentComboTimer = defaultComboTimer;

            switch (currentComboState)
            {
                case ComboState.punch:
                    playerAnim.Punch();
                    break;
                case ComboState.punch2:
                    playerAnim.Punch2();
                    break;
                case ComboState.PUNCH3:
                    playerAnim.Punch();
                    break;
                case ComboState.PUNCH4:
                    playerAnim.Punch2();
                    break;
            }
        }

        // Patada
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (currentComboState == ComboState.PUNCH4 || currentComboState == ComboState.kick)
            {
                return;
            }

            currentComboState = ComboState.kick;
            activateTimerToReset = true;
            currentComboTimer = defaultComboTimer;
            playerAnim.Kick();
        }
    }


    void ResetComboState()
    {
        if (activateTimerToReset)
        {
            currentComboTimer -= Time.deltaTime;

            if (currentComboTimer <= 0f)
            {
                currentComboState = ComboState.None;
                activateTimerToReset = false;
                currentComboTimer = defaultComboTimer;
            }
        }
    }

    public void ActivatePunch1()
    {
        punch1AttackPoint.SetActive(true);
    }

    public void ActivatePunch2()
    {
        punch2AttackPoint.SetActive(true);
    }

    public void ActivateKick()
    {
        kickAttackPoint.SetActive(true);
    }

    public void DesactivePunch1()
    {
        punch1AttackPoint.SetActive(false);
    }

    public void DesactivePunch2()
    {
        punch2AttackPoint.SetActive(false);
    }

    public void DesactiveKick()
    {
        kickAttackPoint.SetActive(false);
    }

    public void DesactivateAllAttack()
    {
        punch1AttackPoint.SetActive(false);
        punch2AttackPoint.SetActive(false);
        kickAttackPoint.SetActive(false);
    }

}
