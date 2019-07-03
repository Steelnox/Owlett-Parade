using UnityEngine;
using System.Collections;

public interface IHealth
{
    int MaxHealth { get; set; }
    int CurrentHealth { get; set; }

    void GetDamaged(int amount);
    void GetHealed(int amount);
    void OutOfHealth();
}
