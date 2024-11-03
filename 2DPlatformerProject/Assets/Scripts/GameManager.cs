using UnityEngine;

public abstract class GameManager : MonoBehaviour
{

    // �÷��̾� ü�°��� ������Ʈ
    protected PlayerHealthControl playerHpControl;
    // �÷��̾� �̵����� ������Ʈ
    protected PlayerInputMovement playerInputMovement;

    // �÷��̾� ü�� ������ ����
    protected static int playerHp = 0;
    // �÷��̾� ü�� ���� ������Ʈ���� ȣ�� ���� ������Ƽ
    public static int PlayerHp { get => playerHp; }
    // �÷��̾� ���� ����
    protected bool playerDetected = false;
    // �ִϸ����� ����
    [SerializeField] protected Animator animator;
    // �Ͻ����� ���ӸŴ��� ����
    protected PauseGameManager pauseGameManager;

    [SerializeField] protected int sceneIndex;

    protected virtual void Start()
    {
        pauseGameManager = GetComponent<PauseGameManager>();
        playerHpControl = GameObject.FindWithTag("Player").GetComponent<PlayerHealthControl>();
        playerInputMovement = GameObject.FindWithTag("Player").GetComponent<PlayerInputMovement>();
        StopAllCoroutines();
        //Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);

    }
}
