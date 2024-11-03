using UnityEngine;

public abstract class GameManager : MonoBehaviour
{

    // 플레이어 체력관리 컴포넌트
    protected PlayerHealthControl playerHpControl;
    // 플레이어 이동관리 컴포넌트
    protected PlayerInputMovement playerInputMovement;

    // 플레이어 체력 저장할 변수
    protected static int playerHp = 0;
    // 플레이어 체력 관리 컴포넌트에서 호출 받을 프로퍼티
    public static int PlayerHp { get => playerHp; }
    // 플레이어 감지 여부
    protected bool playerDetected = false;
    // 애니메이터 참조
    [SerializeField] protected Animator animator;
    // 일시정지 게임매니저 참조
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
