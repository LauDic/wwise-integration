using UnityEngine;

public interface ICharacter
{
    public void Jump();
    public void Move(Vector3 movementDir);
    public void LaunchAttack();
    public void CheckRebound(Collision other);
    public void AddSpeed(Vector3 direction, float speedBoost);
}
