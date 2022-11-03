using UnityEngine;
using NaughtyAttributes;

namespace EffectSystem.SCRO
{
    [CreateAssetMenu(fileName = "RandomGetDefendEffect", menuName = "Effects/RandomGetDefendEffect")]
    public class SCRO_RandomGetDefendEffect : SCRO_Effect
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