using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class CharacterStatHealthModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        KnightScript knight = character.GetComponent<KnightScript>();
        if (knight != null)
            knight.AddHealth((int)val);
        }
}
