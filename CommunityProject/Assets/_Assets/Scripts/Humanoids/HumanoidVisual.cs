using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidVisual : MonoBehaviour
{
    private Humanoid humanoid;
    [SerializeField] private GameObject questionMarkGameObject;

    private void Awake() {
        humanoid = GetComponentInParent<Humanoid>();
        questionMarkGameObject.SetActive(false);
    }

    public void SetQuestionMarkActive(bool active) {
        questionMarkGameObject.SetActive(active);
    }

}
