using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    private const int jumpIndex = 0, dashIndex = 1, attackIndex = 2, attackHitIndex = 3, chakraIndex = 4, saveIndex = 5, openDoorIndex = 6,
                            lockedDoorIndex = 7;

    [SerializeField] private int moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpDistance;
    [SerializeField] private float dashDistance;
    [SerializeField] private float attackCoolTime = 1.3f;
    [SerializeField] private float jumpGamma;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject chakra;

    [SerializeField] private GameObject chakraPoint;
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerInfo playerInfo;

    [SerializeField] private BoxCollider2D leftAttack;
    [SerializeField] private BoxCollider2D rightAttack;
    [SerializeField] private BoxCollider2D dashDetectCollider;
    [SerializeField] private BoxCollider2D jumpDetectCollider;
    [SerializeField] private CircleCollider2D groundDetectCollider;
    [SerializeField] private BoxCollider2D doorDetectCollider;

    [SerializeField] private float dashingCooldown;
    [SerializeField] private ParticleSystem dust;

    [SerializeField] private SceneController sceneController;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private Image dashGauge;
    //0 jump / 1 dash / 2 attack / 3 attack hit / 4 chakra

    [SerializeField] private CanSave[] saves;

    public event Action<Status> OnStatusChange;
    //public event Action<float> OnTakeDamage;

    private const int maxJumpCount = 3;

    private int isJumped, itemNum, time;
    private int currentJumpCount = 2;
    // 대시를 사용 가능한지, 대시 중인지, 땅 위에 서 있는지, 세이브를 할 수 있는지, 차크람을 던졌는지, 차크람을 멈췄는지, 차크람을 멈출 수 있는지
    private bool canDash, isDashed, isGround, isShootChakra, stopChakra, canStopChakra, canGenerateParticle, canContactDoor, canMove, canAttack, canPress;
    public bool STOPCHAKRA { get { return stopChakra; } set { stopChakra = value; } }
    public bool canSave { get; set; }
    public bool goalPoint { get; set; }
    private float direction, dashForce;
    public float DIRECTION { get { return direction; } set { direction = value; } }
    private float curTime; // 공격의 쿨타임을 재기 위한 변수

    private Vector2 boxSize; // 공격 박스의 크기
    private Vector2 savePoint, currentPoint; // 세이브를 위한 지점 / 현재 지점
    private GameObject howToAttack; // 공격 방향 정하기 위한 변수
    private ContactFilter2D contactFilter; // Trigger을 직접 처리하기 위한 필터
    private GameManager manager;

    #region Start
    void Start()
    {
        isJumped = 0;
        isGround = true;

        canMove = true;
        canAttack = true;

        isDashed = false;
        canDash = true;
        dashForce = 40f;
        dashingCooldown = 3f;

        animator.SetInteger("Direction", 0);
        direction = 1;
        
        isShootChakra = false;
        
        savePoint = transform.position;

        dashDetectCollider.enabled = false;
        jumpDetectCollider.enabled = false;

        canGenerateParticle = true;
        goalPoint = false;
        canContactDoor = false;

        contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(LayerMask.GetMask("Floor"));
        contactFilter.useTriggers = true;

        manager = GameManager.Instance;
        manager.canMove = true;
        sceneController = GameObject.Find("GameManager").GetComponent<SceneController>();
        time = manager.ReturnTimeOrDay("Time");
        if(dashGauge != null)
        {
            dashGauge.fillAmount = 1f;
        }

        canPress = true;
    }
    #endregion
    #region Update
    void Update()
    {
        if (!manager.canMove)
        {
            return;
        }

        if(playerInfo.status == Status.Die)
        {
            return;
        }

        #region move
        if (!canMove)
        {
            animator.SetBool("isWalking", false);
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            animator.SetInteger("Direction", 1);
            if (!isDashed)
            {
                direction = -1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            animator.SetInteger("Direction", 0);
            if (!isDashed)
            {
                direction = 1;
            }
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            animator.SetBool("isWalking", false);
        }
        else if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            dust.Stop();
        }


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (animator.GetInteger("Direction") == 0)
            {
                animator.SetInteger("Direction", 1);
            }
            Walk();
            PlayDust();
            if (!isDashed)
            {
                direction = -1;
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (animator.GetInteger("Direction") == 1)
            {
                animator.SetInteger("Direction", 0);
            }
            Walk();
            PlayDust();
            if (!isDashed)
            {
                direction = 1;
            }
        }
        #endregion

        #region jump

        if (isGround)
        {
            isJumped = 0;
            currentJumpCount = 2;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && isJumped < currentJumpCount)
        {
            dust.Stop();
            Jump();
        }
        #endregion

        #region attack
        if (Input.GetKeyDown(KeyCode.Z) && canAttack && time == 1)
        {
            canAttack = false;
            if (isJumped > 0)
            {
                animator.SetBool("isJump", false);
            }
            animator.SetBool("isAttack", true);
            if (direction == -1)
            {
                howToAttack = leftAttack.gameObject;
                boxSize = leftAttack.size;
            }
            else
            {
                howToAttack = rightAttack.gameObject;
                boxSize = rightAttack.size;
            }

            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(howToAttack.transform.position, boxSize, 0);
            foreach (Collider2D collider in collider2Ds)
            {
                if (collider.TryGetComponent<IDamageable>(out var monster))
                {
                    float damage = playerInfo.ReturnDamage();
                    //audioSource.PlayOneShot(audioClips[attackHitIndex]);
                    monster.TakeDamage(damage);
                }
                else
                {
                    audioSource.PlayOneShot(audioClips[attackIndex]);
                }
            }

            StartCoroutine(Wait(0.5f, 0.8f));
        }
        
        #endregion

        #region chakra

        if(time == 1)
        {
            if (Input.GetKeyDown(KeyCode.C) && !isShootChakra)
            {
                audioSource.PlayOneShot(audioClips[chakraIndex]);
                Instantiate(chakra, chakraPoint.transform.position, Quaternion.identity);
                canStopChakra = true;
            }
            if (Input.GetKeyUp(KeyCode.C) && !isShootChakra)
            {
                isShootChakra = true;
            }

            if (Input.GetKey(KeyCode.C) && !stopChakra && isShootChakra && canStopChakra)
            {
                stopChakra = true;
            }

            if (Input.GetKeyUp(KeyCode.C) && isShootChakra && stopChakra)
            {
                stopChakra = false;
                canStopChakra = false;
            }

            if (GameObject.FindWithTag("Chakra") == null)
            {
                isShootChakra = false;
                canStopChakra = true;
            }
        }
        
        #endregion

        #region Dash
        if (Input.GetKeyDown(KeyCode.X) && canDash && time == 1){
            Dash(direction);
            if(isJumped > 0){
                currentJumpCount = maxJumpCount;
            }
        }
        #endregion

        #region Save, Goal and Door
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (canSave)
            {
                if(saves.Length > 0)
                {
                    foreach (CanSave point in saves)
                    {
                        if (point.isOnPlayer)
                        {
                            point.GetComponent<Animator>().SetTrigger("Save");
                            point.isSaved = true;
                        }
                    }
                }
                
                currentPoint = transform.position;
                savePoint = currentPoint;
                audioSource.PlayOneShot(audioClips[saveIndex]);
            }
            else if (goalPoint || isPlayerOnDoor().Equals("ExitDoor"))
            {
                if (canPress)
                {
                    isPlayerOpenDoor();
                    manager.SkipToNextDay();
                    ChangeScene();
                }
            }
            else if (isPlayerOnDoor().Equals("NormalDoor") && !canContactDoor && manager.isHaveKey && time == 0)
            {
                manager.isHaveKey = false;
                isPlayerOpenDoor();
                itemNum = getItemNum();
                manager.itemNum = this.itemNum;
                audioSource.PlayOneShot(audioClips[openDoorIndex]);
                canContactDoor = true;
            }
            else if (canContactDoor || !manager.isHaveKey)
            {
                audioSource.PlayOneShot(audioClips[lockedDoorIndex]);
            }
        }
        #endregion
    }
    #endregion

    #region MoveDef

    private void Walk()
    {
        if (canMove)
        {
            animator.SetBool("isWalking", true);
            transform.Translate(new Vector2(direction, 0) * moveSpeed * Time.deltaTime);
        }
    }

    public void SetCanMove(bool boo)
    {
        canMove = boo;
    }

    #endregion
    #region JumpDef

    private void Jump()
    {
        StartCoroutine(JumpCoroutine());
    }

    private IEnumerator JumpCoroutine()
    {
        isGround = false;

        if (isJumped >= 1)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }

        animator.SetBool("isJump", true);
        isJumped += 1;

        int prevJumpCount = isJumped;
        float jumpSpeed = jumpForce;
        float startY = rb.position.y;

        audioSource.PlayOneShot(audioClips[jumpIndex]);

        while (true)
        {
            if (prevJumpCount != isJumped || IsWallUpside())
            {
                break;
            }

            if (Mathf.Abs(rb.position.y - startY) >= jumpDistance)
            {
                rb.velocity = new Vector2 (rb.velocity.x, rb.velocity.y);
                break;
            }

            transform.Translate(Vector2.up * jumpSpeed * Time.deltaTime);

            jumpSpeed *= jumpGamma;

            yield return null;
        }
    }

    private bool isPlayerOnGround()
    {
        Collider2D[] colliders = new Collider2D[10];
        int count = groundDetectCollider.OverlapCollider(contactFilter, colliders);
        for (int i = 0; i < count; i++)
        {
            Collider2D other = colliders[i];
            if (other.CompareTag("Floor"))
            {
                return true;
            }
            else if (other.CompareTag("Chakra") && stopChakra)
            {
                return true;
            }
        }

        return false;
    }

    private void OnGround()
    {
        isGround = true;
        animator.SetBool("isJump", false);
    }

    private bool IsWallUpside()
    {
        Collider2D[] colliders = new Collider2D [10];
        int count = jumpDetectCollider.OverlapCollider (contactFilter, colliders);
        for (int i = 0; i < count; i++)
        {
            Collider2D other = colliders[i];
            if (other.CompareTag("Floor"))
            {
                return true;
            }
            else if (other.CompareTag("Chakra") && stopChakra)
            {
                return true;
            }
        }
        return false;
    }
    #endregion
    #region DashDef
    private void Dash(float direction)
    {
        StartCoroutine(DashCoroutine(direction));
    }

    private IEnumerator DashCoroutine(float direction)
    {
        animator.SetBool("isDash", true);
        dashDetectCollider.enabled = true;
        if (isJumped > 0)
        {
            animator.SetBool("isJump", false);
        }
        if (!canDash || isDashed) {
            yield break;
        }
        canDash = false;
        isDashed = true;
        OnStatusChange?.Invoke(Status.Immune);

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        Vector2 dashStartPos = rb.position;
        Vector2 prevPos = rb.position;

        float dashSpeed = dashForce;

        audioSource.PlayOneShot(audioClips[dashIndex]);

        while(Mathf.Abs(rb.transform.position.x - dashStartPos.x) < dashDistance)
        {

            if (IsWallFront(direction))
            {
                break;
            }

            rb.velocity = new Vector2(direction * dashSpeed, 0f);
            dashSpeed *= 0.98f;
            yield return null;
        }
        
        rb.velocity = new Vector2(0, rb.velocity.y);
        rb.gravityScale = originalGravity;
        isDashed = false;
        OnStatusChange?.Invoke(Status.Standard);

        dashDetectCollider.enabled = false;
        animator.SetBool("isDash", false);
        if(isJumped > 0)
        {
            animator.SetBool("isJump", true);
        }
        StartCoroutine(DashCooldown());
    }

    private IEnumerator DashCooldown()
    {
        float timer = 0f;
        while (timer < dashingCooldown)
        {
            timer += Time.unscaledDeltaTime;
            dashGauge.fillAmount = timer / dashingCooldown;
            yield return null;
        }
        dashGauge.fillAmount = 1f;
        canDash = true;
    }

    private bool IsWallFront(float direction)
    {
        Collider2D[] colliders = new Collider2D[10];
        int count = dashDetectCollider.OverlapCollider(contactFilter, colliders);
        for(int i = 0; i < count; i++)
        {
            Collider2D other = colliders[i];
            if (other.CompareTag("Floor"))
            {
                return true;
            }
            else if (other.CompareTag("Chakra") && stopChakra)
            {
                return true;
            }
        }
        return false;
    }
    #endregion
    #region ChakraDef
    public void OnChakraReturn()
    {
        isShootChakra = false;
        stopChakra = false;
    }
    #endregion
    #region Trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isPlayerOnGround())
        {
            OnGround();
        }
        else if (other.CompareTag("SpawnPoint"))
        {
            canSave = true;
            Debug.Log("spawnPoint");
            
        }
        else if (other.CompareTag("DeadZone"))
        {
            transform.position = savePoint;
            GameObject chakra = GameObject.Find("Chakra(Clone)");
            Destroy(chakra);
            OnChakraReturn();
        }
    }
    #endregion
    #region DayTime
    private string isPlayerOnDoor()
    {
        Collider2D[] colliders = new Collider2D[10];
        int count = doorDetectCollider.OverlapCollider(contactFilter, colliders);
        for (int i = 0; i < count; i++)
        {
            Collider2D other = colliders[i];
            if (other.CompareTag("Door"))
            {
                return "NormalDoor";
            }
            else if (other.CompareTag("ExitDoor"))
            {
                return "ExitDoor";
            }
        }

        return null;
    }

    private void isPlayerOpenDoor()
    {
        Collider2D[] colliders = new Collider2D[10];
        int count = doorDetectCollider.OverlapCollider(contactFilter, colliders);
        for (int i = 0; i < count; i++)
        {
            Collider2D other = colliders[i];
            if (other.CompareTag("Door"))
            {
                other.GetComponent<Animator>().enabled = true;
            }
            else if (other.CompareTag("ExitDoor"))
            {
                other.GetComponent<Animator>().enabled = true;
            }
            else if (other.CompareTag("GoalPoint"))
            {
                other.GetComponent<Animator>().enabled = true;
            }
        }
    }

    private void ChangeScene()
    {
        canPress = false;
        StartCoroutine(ChangeSceneRoutine());
    }

    private IEnumerator ChangeSceneRoutine()
    {
        yield return new WaitForSeconds(0.25f);
        sceneController.ChangeScene("StoryScene");
    }

    private int getItemNum()
    {
        int rand = Random.Range(0, 100);
        int itemNum;
        if (rand < 30) // 0 ~ 29
        {
            itemNum = 0;
        }
        else if (rand >= 30 && rand < 60) // 30 ~ 59
        {
            itemNum = 1;
        }
        else if (rand >= 60 && rand < 90) // 60 ~ 89
        {
            itemNum = 2;
        }
        else if (rand >= 90 && rand < 94) // 90 ~ 93
        {
            itemNum = 3;
        }
        else if (rand >= 94 && rand < 98) // 94 ~ 97
        {
            itemNum = 4;
        }
        else if (rand == 98)
        {
            itemNum = 5;
        }
        else
        {
            itemNum = 6;
        }

        return itemNum;
    }
    #endregion
    #region Effect
    private void PlayDust()
    {
        if (isGround)
        {
            if(canGenerateParticle)
            {
                StartCoroutine(DustCoroutine());
            }   
        }
        else
        {
            dust.Stop();
        }
    }

    private IEnumerator DustCoroutine()
    {
        canGenerateParticle = false;

        dust.Play();
        yield return new WaitForSeconds(0.1f);

        canGenerateParticle = true;
    }

    #endregion
    #region Debug
    private void OnDrawGizmos() // for Debug
    {
        Gizmos.color = Color.red;
        if (howToAttack != null && boxSize != null)
        {
            Gizmos.DrawWireCube(howToAttack.transform.position, boxSize);
        }
    }

    private IEnumerator Wait(float delay1, float delay2)
    {
        manager.canMove = false;
        yield return new WaitForSeconds(delay1);
        manager.canMove = true;
        animator.SetBool("isAttack", false);
        yield return new WaitForSeconds(delay2);
        canAttack = true;
        if(isJumped > 0)
        {
            animator.SetBool("isJump", true);
        }
    }
    #endregion
}
