using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audiocourse : MonoBehaviour
{
    public AudioSource soundEffect;

    void Start() {
        // �� AudioSource �ҡ GameObject ���
        soundEffect = GetComponent<AudioSource>();
    }

    void Update() {
        // ��Ǩ�ͺ��á�����
        if (Input.GetKeyDown(KeyCode.Mouse0)) // ��駤���繻������س��ͧ���
        {
            // ������§
            soundEffect.Play();
        }
    }
}
