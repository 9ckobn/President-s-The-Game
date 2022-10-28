using Cards.Data;
using Cards.View;
using Core;
using Gameplay;
using UnityEngine;

namespace Cards
{
    [RequireComponent(typeof(CardFightView))]
    public class CardFightModel : CardModel
    {
        private const float SCALE_ANIM_VALUE = 1.5f, POS_Z = 0.1f;

        public CardFightData SetCardData 
        {
            set
            {
                data = value;

                (view as CardFightView).SetData(data as CardFightData);
            }
        }

        protected override void MouseEnter()
        {
            BoxController.GetController<CardsController>().SelectFightCard(this);

            float scale = transform.localScale.x;
            Vector3 position = transform.position;

            transform.localScale = new Vector3(scale * SCALE_ANIM_VALUE, scale * SCALE_ANIM_VALUE, scale * SCALE_ANIM_VALUE);
            transform.position = new Vector3(position.x, position.y + POS_Z, position.z + POS_Z);
        }

        protected override void MouseExit()
        {
            BoxController.GetController<CardsController>().DeselectFightCard(this);

            float scale = transform.localScale.x;
            Vector3 position = transform.position;

            transform.localScale = new Vector3(scale / SCALE_ANIM_VALUE, scale / SCALE_ANIM_VALUE, scale / SCALE_ANIM_VALUE);
            transform.position = new Vector3(position.x, position.y - POS_Z, position.z - POS_Z);
        }

        protected override void MouseDown()
        {
            BoxController.GetController<CardsController>().ClickFightCard(this);
        }
    }
}