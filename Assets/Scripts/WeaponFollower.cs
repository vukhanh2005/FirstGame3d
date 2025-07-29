using UnityEngine;

public class WeaponFollower : MonoBehaviour
{
    public enum WeaponArm
    {
        ArmLeft,
        ArmRight
    }
    public WeaponArm arm;
    public Transform boneTransform;
    public Vector3 offsetPosition;
    public Vector3 offsetRotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (arm == WeaponArm.ArmRight)
            boneTransform = GameObject.FindWithTag("BoneRightArm").GetComponent<Transform>();
        if (arm == WeaponArm.ArmLeft)
            boneTransform = GameObject.FindWithTag("BoneLeftArm").GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (boneTransform != null)
        {
            transform.position = boneTransform.position + boneTransform.rotation * offsetPosition;

            transform.rotation = boneTransform.rotation * Quaternion.Euler(offsetRotation);        
        }
    }
}
