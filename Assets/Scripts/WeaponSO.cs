using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
	public GameObject WeaponPrefab;
	public bool IsAutomatic = false;
	public bool CanZoom = false;
	public float ZoomAmount = 10;
	public float ZoomRotationSpeed = 0.3f;
	public float FireRate = 0.5f;
    public uint Damage = 1;
	public int MagazineSize = 12;
	public GameObject HitVFXPrefab;
}
