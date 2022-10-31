using UnityEngine;
using NaughtyAttributes;

namespace EffectSystem.SCRO
{
    [CreateAssetMenu(fileName = "DefendEffect", menuName = "Effects/DefendEffect")]
    public class SCRO_DefendEffect : SCRO_Effect
    {
        [BoxGroup("Protect")]
        [Label("����� ������ �������")]
        public TypeAttribute TypeProtect;

        [BoxGroup("Protect")]
        [Label("������ �� �������� �����?")]
        public bool Immortal = true;

        [BoxGroup("Protect")]
        [HideIf("Immortal")]
        [Label("������� % ������ ��������")]
        public int ValueProtect;

        [BoxGroup("Protect")]
        [Label("������� ���� ������ ������")]
        public int DurationProtect;
    }
}