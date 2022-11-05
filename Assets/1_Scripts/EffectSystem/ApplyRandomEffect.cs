using Buildings;
using Data;
using UnityEngine;

namespace EffectSystem
{
    public abstract class ApplyRandomEffect : ApplyEffect
    {
        protected CharacterData characterData;

        public override void SelectTargetBuilding(Building building)
        {
        }
    }
}