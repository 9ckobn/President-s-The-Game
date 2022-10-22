using UnityEngine;
using NaughtyAttributes;

namespace EffectSystem
{
    [CreateAssetMenu(fileName = "RandomEffect", menuName = "Effects/RandomEffect")]
    public class SCRO_RandomEffect : SCRO_Effect
    {
        [BoxGroup("Random")]
        [Label("Базовое значение рандома")]
        public int Value;

        [BoxGroup("Random")]
        [Label("Есть усиление от атрибута?")]
        public bool IsNeedAttribute;

        [BoxGroup("Random")]
        [ShowIf("IsNeedAttribute")]
        [Label("Добавляем значение атрибута")]
        public TypeAttribute TypeAttribute;

        [BoxGroup("Random")]
        [ShowIf("IsNeedAttribute")]
        [Label("Значение в % от аттрибута")]
        public int ValueAttribute;

        [BoxGroup("Random")]
        [Label("Какой показатель увеличивается при победе")]
        public TypeAttribute TypeWinAttribute;

        [BoxGroup("Random")]
        [Label("На сколько % увличиается")]
        public int WinProcent;

        [BoxGroup("Random")]
        [Label("Какой показатель уменьшается при проигрыше")]
        public TypeAttribute TypeLoseAttribute;

        [BoxGroup("Random")]
        [Label("На сколько % уменьшается")]
        public int LoseProcent;
    }
}