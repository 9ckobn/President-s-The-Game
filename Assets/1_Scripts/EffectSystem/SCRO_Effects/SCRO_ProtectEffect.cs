using UnityEngine;
using NaughtyAttributes;

namespace EffectSystem.SCRO
{
    [CreateAssetMenu(fileName = "ProtectEffect", menuName = "Effects/ProtectEffect")]
    public class SCRO_ProtectEffect : SCRO_Effect
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