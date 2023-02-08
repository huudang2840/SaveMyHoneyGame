using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CharacterController_2D : MonoBehaviour,IDataPersistence  {
    Rigidbody2D m_rigidbody;
    Animator m_Animator;
    Transform m_tran;

    Scene scene;
    //Xu ly HP cua nhan vat
    private float maxHealth = 30;
    private float health;
    public HealthBar healthBar;
    public int totalBullet;
    public GameObject BulletPrefab;

    public ParticleSystem particle;
    public int qtyItemHealth;
    public int qtyItemProtected;
    public int qtyItemBuffDame;
    public int indexScene;
    public ColliderEvent_Sender knifeAttack;
    
    //Xu ly save du lieu

    private float h = 0;
    private float v = 0;

    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;

    public float MoveSpeed = 10.0f;
    private float JumpForce = 50.0f;

    
    public float dameKnife = 2;
    public float dameGun = 3;

    public SpriteRenderer[] m_SpriteGroup;
    public bool Once_Attack = false;
    public bool isGrounded = false;

    private bool isProtected = false;
    // Sound
    [SerializeField] public AudioSource deadSound;
    [SerializeField] public AudioSource hurtSound;
    [SerializeField] public AudioSource attackSound;
    [SerializeField] public AudioSource buffSound;
    [SerializeField] public AudioSource useBuffSound;
    [SerializeField] public AudioSource gunSound;

    //Tải dữ liệu
    public void LoadData(GameData data) {
        this.health = data.health;
        // this.transform.position = data.playerPosition;
        this.totalBullet = data.totalBullet;
        this.qtyItemHealth = data.qtyItemHealth;
        this.qtyItemBuffDame = data.qtyItemBuffDame;
        this.qtyItemProtected = data.qtyItemProtected;
        this.indexScene = data.indexScene;
    }

    //Lưu dữ liệu
    public void SaveData(ref GameData data) {
        data.health = this.health;
        // data.playerPosition = this.transform.position;
        data.totalBullet = this.totalBullet;
        data.qtyItemBuffDame = this.qtyItemBuffDame;
        data.qtyItemHealth = this.qtyItemHealth;
        data.qtyItemProtected = this.qtyItemProtected;
        data.indexScene = this.indexScene;
    }


    void Start () {
        // DataPersistenceManagement.instance.LoadData();
        health = maxHealth;
        scene = SceneManager.GetActiveScene();
        this.particle.Stop();
        m_rigidbody = this.GetComponent<Rigidbody2D>();
        m_Animator = this.transform.Find("BURLY-MAN_1_swordsman_model").GetComponent<Animator>();
        m_tran = this.transform;
        m_SpriteGroup = this.transform.Find("BURLY-MAN_1_swordsman_model").GetComponentsInChildren<SpriteRenderer>(true);
        healthBar.SetMaxHealth(maxHealth);
        indexScene = scene.buildIndex;
        Debug.Log(indexScene);
    }
	

	void Update () {
        healthBar.SetHealth(health);

        //Ấn chuột trái để đánh
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Once_Attack = true;
            m_Animator.SetTrigger("Attack");
            attackSound.Play();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1)) {
            if(totalBullet > 0){
                gunSound.Play();
                m_Animator.Play("AttackWithGun");
                Shoot();
                totalBullet-=1;
            }
            else {
                Debug.Log("Don't have enough bullet");
                
            }
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        { 
            useBuffSound.Play();
           useItemHealth();
        }

           else if (Input.GetKeyDown(KeyCode.X))
        { 
            useBuffSound.Play();
            useItemProtected();  
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
             Debug.Log("Before: " + dameKnife);
            useBuffSound.Play();

            useItemBuffDame();
        }
           if(health <= 0 || transform.position.y < -10) {
            
            StartCoroutine(wait2GameOver());
        }

        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") || m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Die")||
            m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Hit")|| m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2")||
            m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Jump") || m_Animator.GetCurrentAnimatorStateInfo(0).IsName("AttackWithGun")||
            m_Animator.GetCurrentAnimatorStateInfo(0).IsName("JumpWithGun") || m_Animator.GetCurrentAnimatorStateInfo(0).IsName("IdleWithGun")||
            m_Animator.GetCurrentAnimatorStateInfo(0).IsName("DieWithGun") || m_Animator.GetCurrentAnimatorStateInfo(0).IsName("RunWithGun")||
            m_Animator.GetCurrentAnimatorStateInfo(0).IsName("HitWithGun")
            )
            return;

        if(m_tran.position.x < 0.7f){
                m_tran.position = new Vector2(0.7f,m_tran.position.y);
        }
        else if(m_tran.position.x > 100f){
                m_rigidbody.velocity = new Vector2(0, m_rigidbody.velocity.y);
        }
        Jump();
        HandleMover();

        h = Input.GetAxis("Horizontal");
        m_Animator.SetFloat("MoveSpeed", Mathf.Abs(h ));
 
    }

       // Xoay nhân vật
    bool B_Attack = false;
    bool B_FacingRight = true;



    public int sortingOrder = 0;
    public int sortingOrderOrigine = 0;

    private float Update_Tic = 0;
    private float Update_Time = 0.1f;

    //Điều khiển các thành phần của nhân vật
    void spriteOrder_Controller() {
        Update_Tic += Time.deltaTime;
        if (Update_Tic > 0.1f)
        {
            sortingOrder = Mathf.RoundToInt(this.transform.position.y * 100);
            for (int i = 0; i < m_SpriteGroup.Length; i++)
            {
                m_SpriteGroup[i].sortingOrder = sortingOrderOrigine - sortingOrder;
            }
            Update_Tic = 0;
        }
    }

     // Function di chuyển nhân vật
    void HandleMover() {
        // Di chuyển sang trái
        if (Input.GetKey(KeyCode.A))
        {
            m_rigidbody.velocity = new Vector2(-MoveSpeed, m_rigidbody.velocity.y);
            if (B_FacingRight)
                RotateCharacter();
        }
        //Di chuyển sang phải
        else if (Input.GetKey(KeyCode.D))
        {
            m_rigidbody.velocity = new Vector2(MoveSpeed, m_rigidbody.velocity.y);
            if (!B_FacingRight)
                RotateCharacter();
        }    
    }

    // Function hành động nhảy
    void Jump(){
         if (Input.GetKey(KeyCode.W) && isGrounded == true)
        {
            m_rigidbody.velocity = Vector2.up * JumpForce;
        }
    }




    //Shoot 
    private void Shoot() {
        Vector3 direction;
        if (B_FacingRight) direction = Vector2.right;
        else direction = Vector2.left;

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 1.7f,  Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
        bullet.GetComponent<BulletScript>().DestroyBullet();
        bullet.GetComponent<BulletScript>().dameGunn = dameGun;
        Debug.Log(totalBullet);
    }

   
    //gây dame lên nhân vật
    public void takeDameFromMonster(float dame) {
        if(!isProtected) {
            m_Animator.Play("Hit");
            hurtSound.Play();
            health -= dame;
            healthBar.SetHealth(health);
            Debug.Log("Take Dame From Monster: " + dame);
        }else{
            Debug.Log("You are protected");
        }
        
    }

    //sử dụng Item hồi máu
    public void useItemHealth() {
        if(qtyItemHealth > 0) {
            health += 10;
            if(health > 30) {
                health = 30;
            }
            qtyItemHealth-=1;
            healthBar.SetHealth(health);
        }else{
            Debug.Log("You don't have enough items");
        }
        healthBar.SetHealth(health);
    }

    //sử dụng Item bất tử
    public void useItemProtected() {
     
        if(qtyItemProtected > 0){
            qtyItemProtected -= 1;
            StartCoroutine(protectPlayer());
        }else {
            Debug.Log("You don't have enough items");
        }
        
    }

    public void useItemBuffDame() {
        if(qtyItemBuffDame > 0){
            qtyItemBuffDame -= 1;
            StartCoroutine(buffDame());
        }else {
            Debug.Log("You don't have enough items");

        }
        
    }
    //nhặt thêm đạn
    public void pickUpBullet() {
        if(totalBullet>20){
            totalBullet = 20;
        }
        totalBullet += 10;
    }

 
    //Xoay nhân vật theo hướng di chuyển
    void RotateCharacter() {
        B_FacingRight = !B_FacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        m_tran.localScale = theScale;
    }

    //Xử lý trigger
    void OnTriggerEnter2D(Collider2D other) {
        //Khi nhặt item hồi máu
        if(other.tag == "HealthBonus"  ) {
            buffSound.Play();
            qtyItemHealth += 1;
            if(qtyItemHealth > 10){
                qtyItemHealth = 10;
            }
        }
        //Khi nhặt item thêm đạn
        else if(other.tag =="BulletItems" ) {
            buffSound.Play();

            pickUpBullet();
        }
        else if(other.tag =="ProtectedItem" ) {
            buffSound.Play();

            qtyItemProtected += 1;
            if(qtyItemProtected > 10){
                qtyItemProtected = 10;
            }
        }
        else if(other.tag =="BuffDameItem" ) {
            buffSound.Play();

            qtyItemBuffDame += 1;
            if(qtyItemBuffDame > 10){
                qtyItemBuffDame = 10;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "PointPass"){
            indexScene +=2;
            SceneManager.LoadScene(scene.buildIndex + 1, LoadSceneMode.Single);
            DataPersistenceManagement.instance.SaveGame();

        }
    }
    //Nếu sử dụng Item buffDame sẽ được nhân đôi sát thương gây ra
    IEnumerator buffDame() {
        dameKnife *=2;
        dameGun *=2 ;
        this.particle.Play();
        Debug.Log("After: " + dameKnife);
        yield return new WaitForSeconds(10.0f);
        this.particle.Stop();
        dameKnife /= 2;
        dameGun /=2;
    }

    //Nếu sử dựng Item  bất tử người chơi sẽ không phải chịu bất kì sát thương nào 
    IEnumerator protectPlayer() {
        isProtected = true;
        Debug.Log("Protecting player");
        yield return new WaitForSeconds(10.0f);
        isProtected = false;
        Debug.Log("Don't protect player");
    }


    IEnumerator wait2GameOver() {
        deadSound.Play();
        m_Animator.Play("Die");
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }




}
