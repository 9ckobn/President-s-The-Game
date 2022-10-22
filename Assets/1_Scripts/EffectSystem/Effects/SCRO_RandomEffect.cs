using UnityEngine;
using NaughtyAttributes;

namespace EffectSystem
{
    [CreateAssetMenu(fileName = "RandomEffect", menuName = "Effects/RandomEffect")]
    public class SCRO_RandomEffect : SCRO_Effect
    {
        [BoxGroup("Random")]
        [Label("������� �������� �������")]
        public int Value;

        [BoxGroup("Random")]
        [Label("���� �������� �� ��������?")]
        public bool IsNeedAttribute;

        [BoxGroup("Random")]
        [ShowIf("IsNeedAttribute")]
        [Label("��������� �������� ��������")]
        public TypeAttribute TypeAttribute;

        [BoxGroup("Random")]
        [ShowIf("IsNeedAttribute")]
        [Label("�������� � % �� ���������")]
        public int ValueAttribute;

        [BoxGroup("Random")]
        [Label("����� ���������� ������������� ��� ������")]
        public TypeAttribute TypeWinAttribute;

        [BoxGroup("Random")]
        [Label("�� ������� % �����������")]
        public int WinProcent;

        [BoxGroup("Random")]
        [Label("����� ���������� ����������� ��� ���������")]
        public TypeAttribute TypeLoseAttribute;

        [BoxGroup("Random")]
        [Label("�� ������� % �����������")]
        public int LoseProcent;
    }
}