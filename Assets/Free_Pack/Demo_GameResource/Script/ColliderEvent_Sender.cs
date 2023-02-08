using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEvent_Sender : MonoBehaviour {
    private CharacterController_2D m_parent;
    Collider2D col;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        m_parent = this.transform.root.transform.GetComponent<CharacterController_2D>();
    }

    private void Update() {
         if (m_parent.Once_Attack == true) {
            this.GetComponent<BoxCollider2D>().enabled = true;
        }else {
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
      

        if (m_parent.Once_Attack == true) 
        {
            
              if(other.gameObject.TryGetComponent<MoveGoblin_1>(out MoveGoblin_1 enemy)) {
                    Debug.Log("Dame từ knife to Golblin1:" + m_parent.dameKnife);
                    enemy.takeDameFromPlayer(m_parent.dameKnife);
                }
                else if(other.gameObject.TryGetComponent<MoveGoblin_2>(out MoveGoblin_2 enemy1)) {
                    Debug.Log("Dame từ knife to Golblin2:" + m_parent.dameKnife);
                    enemy1.takeDameFromPlayer(m_parent.dameKnife);
                }
                else if(other.gameObject.TryGetComponent<MoveGoblin_3>(out MoveGoblin_3 enemy2)) {
                    Debug.Log("Dame từ knife to Golblin3:" + m_parent.dameKnife);
                    enemy2.takeDameFromPlayer(m_parent.dameKnife);
                }
                else if(other.gameObject.TryGetComponent<MoveWraith_1>(out MoveWraith_1 enemy3)) {
                    Debug.Log("Dame từ knife to Golblin3:" + m_parent.dameKnife);
                    enemy3.takeDameFromPlayer(m_parent.dameKnife);
                }
                else if(other.gameObject.TryGetComponent<MoveGolem_1>(out MoveGolem_1 enemy4)) {
                    Debug.Log("Dame từ knife to Golblin3:" + m_parent.dameKnife);
                    enemy4.takeDameFromPlayer(m_parent.dameKnife);
                }
                else if(other.gameObject.TryGetComponent<MoveGolem_2>(out MoveGolem_2 enemy5)) {
                    Debug.Log("Dame từ knife to Golblin3:" + m_parent.dameKnife);
                    enemy5 .takeDameFromPlayer(m_parent.dameKnife);
                }
                else if(other.gameObject.TryGetComponent<MoveGolem_3>(out MoveGolem_3 enemy6)) {
                    Debug.Log("Dame từ knife to Golblin3:" + m_parent.dameKnife);
                    enemy6 .takeDameFromPlayer(m_parent.dameKnife);
                }
                else if(other.gameObject.TryGetComponent<MoveWraith_2>(out MoveWraith_2 enemy7)) {
                    Debug.Log("Dame từ knife to Golblin3:" + m_parent.dameKnife);
                    enemy7 .takeDameFromPlayer(m_parent.dameKnife);
                }
                else if(other.gameObject.TryGetComponent<MoveMino_1>(out MoveMino_1 enemy8)) {
                    Debug.Log("Dame từ knife to Golblin3:" + m_parent.dameKnife);
                    enemy8 .takeDameFromPlayer(m_parent.dameKnife);
                }
                else if(other.gameObject.TryGetComponent<MoveMino_2>(out MoveMino_2 enemy9)) {
                    Debug.Log("Dame từ knife to Golblin3:" + m_parent.dameKnife);
                    enemy9 .takeDameFromPlayer(m_parent.dameKnife);
                }
                else if(other.gameObject.TryGetComponent<MoveMino_3>(out MoveMino_3 enemy10)) {
                    Debug.Log("Dame từ knife to Golblin3:" + m_parent.dameKnife);
                    enemy10 .takeDameFromPlayer(m_parent.dameKnife);
                }
                else if(other.gameObject.TryGetComponent<MoveWraith_3>(out MoveWraith_3 enemy11)) {
                    Debug.Log("Dame từ knife to Golblin3:" + m_parent.dameKnife);
                    enemy11 .takeDameFromPlayer(m_parent.dameKnife);
                }
                else if(other.gameObject.TryGetComponent<ControBoss1>(out ControBoss1 enemy12)) {
                    Debug.Log("Dame từ knife to Golblin3:" + m_parent.dameKnife);
                    enemy12 .takeDameFromPlayer(m_parent.dameKnife);
                }
                else if(other.gameObject.TryGetComponent<ControBoss2>(out ControBoss2 enemy13)) {
                    Debug.Log("Dame từ knife to Golblin3:" + m_parent.dameKnife);
                    enemy13 .takeDameFromPlayer(m_parent.dameKnife);
                }
                else if(other.gameObject.TryGetComponent<ControBoss3>(out ControBoss3 enemy14)) {
                    Debug.Log("Dame từ knife to Golblin3:" + m_parent.dameKnife);
                    enemy14 .takeDameFromPlayer(m_parent.dameKnife);
                }
                m_parent.Once_Attack = false;
        }

    }


}
