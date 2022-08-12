using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKnockbackable
{
    Rigidbody2D body { get; }
    void Knockback(Vector2 dir, float force);
}
