using UnityEngine;
using NaughtyAttributes;

namespace EffectSystem
{
    [CreateAssetMenu(fileName = "RandomGetProtectEffect", menuName = "Effects/RandomGetProtectEffect")]
    public class SCRO_RandomGetProtectEffect : SCRO_Effect
    {
        [BoxGroup("Target")]
        [Label("��� �������� ���� �������")]
        public TypeSelectTarget TypeSelectTarget;

        [BoxGroup("Target")]
        [ShowIf("TypeSelectTarget", TypeSelectTarget.Game)]
        [Label("����� ������� ��� �������")]
        public TypeAttribute[] ProtectAttributes;

        [BoxGroup("Random")]
        [Label("����� ������� ������������ ��� �������")]
        public TypeAttribute RandomAttribute;
    }
}