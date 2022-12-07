using Cards;
using Cards.Container;
using Cards.Data;
using Core;
using Data;
using EffectSystem;
using SceneObjects;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "CardsController", menuName = "Controllers/Gameplay/CardsController")]
    public class CardsController : BaseController
    {
        [HideInInspector]
        public UnityAction EndUseFightCardEvent;

        private List<CardPresidentModel> playerPresidentCards = new List<CardPresidentModel>();
        private List<CardPresidentModel> enemyPresidentCards = new List<CardPresidentModel>();

        private List<CardFightModel> playerFightcards = new List<CardFightModel>();
        public List<CardFightModel> EnemyFightCards { get; private set; }

        private CardFightModel selectedFightCard, useFightCard;
        private bool canUseCard = false;

        public bool CanSelectedCard { get => canUseCard && useFightCard == null; }
        public bool SetCanUseCard { set => canUseCard = value; }
        public CardFightModel GetSelectedCard { get => selectedFightCard; }

        public void CreateCards()
        {
            List<CardPresidentData> cardsPresidentData = BoxController.GetController<GameStorageCardsController>().CardsPresidentData;
            List<CardFightData> cardsFightData = BoxController.GetController<GameStorageCardsController>().CardsFightData;

            CreatorController creator = BoxController.GetController<CreatorController>();
            ContainerPresidentCards containerPlayerPresidents = ObjectsOnScene.Instance.GetContainerPlayerPresidents;
            ContainerFightCards containerFightCards = ObjectsOnScene.Instance.GetContainerPlayerFights;

            containerFightCards.SetMaxCards = MainData.MAX_FIGHT_CARDS;
            containerPlayerPresidents.SetMaxCards = MainData.MAX_PRESIDENT_CARDS;

            foreach (var cardData in cardsPresidentData)
            {
                CardPresidentModel card = creator.CreateCardPresident(cardData);
                playerPresidentCards.Add(card);
            }

            foreach (var cardData in cardsFightData)
            {
                CardFightModel card = creator.CreateCardFight(cardData);
                playerFightcards.Add(card);
                card.SetIsPlayerCard = true;
            }

            for (int i = 0; i < playerPresidentCards.Count && i < MainData.MAX_PRESIDENT_CARDS; i++)
            {
                containerPlayerPresidents.AddCard(playerPresidentCards[i]);
            }

            for (int i = 0; i < playerFightcards.Count && i < MainData.MAX_FIGHT_CARDS; i++)
            {
                containerFightCards.AddCard(playerFightcards[i]);
            }


            // Create enemy AI cards
            List<CardPresidentData> enemyCardsPresidentData = BoxController.GetController<GameStorageCardsController>().CardsEnemyPresidentData;
            List<CardFightData> cardsEnemyFightData = BoxController.GetController<GameStorageCardsController>().CardsEnemyFightData;

            ContainerPresidentCards containerEnemyPresidentCards = ObjectsOnScene.Instance.GetContainerEnemyPresidents;
            ContainerFightCards containerEnemyFightCards = ObjectsOnScene.Instance.GetContainerEnemyFightCards;

            containerEnemyFightCards.SetMaxCards = MainData.MAX_FIGHT_CARDS;
            containerEnemyPresidentCards.SetMaxCards = MainData.MAX_PRESIDENT_CARDS;

            foreach (var cardData in enemyCardsPresidentData)
            {
                CardPresidentModel card = creator.CreateCardPresident(cardData);
                enemyPresidentCards.Add(card);
            }

            EnemyFightCards = new List<CardFightModel>();
            foreach (var cardData in cardsEnemyFightData)
            {
                CardFightModel card = creator.CreateCardFight(cardData);
                EnemyFightCards.Add(card);
                card.SetIsPlayerCard = false;
                card.SetBlockPosition = new Vector3(0, 180, 0);
                card.SetUnblockPosition = new Vector3(0, 0, 0);
            }

            for (int i = 0; i < enemyPresidentCards.Count && i < MainData.MAX_PRESIDENT_CARDS; i++)
            {
                containerEnemyPresidentCards.AddCard(enemyPresidentCards[i]);
            }

            for (int i = 0; i < EnemyFightCards.Count && i < MainData.MAX_FIGHT_CARDS; i++)
            {
                containerEnemyFightCards.AddCard(EnemyFightCards[i]);
                EnemyFightCards[i].transform.localScale = new Vector3(0.45f, 0.45f, 1f);
            }
        }

        public void HighlightPlayerPresidentCards(bool isPlayer)
        {
            foreach (var card in playerPresidentCards)
            {
                card.ChangeHighlight(isPlayer);
            }

            foreach (var card in enemyPresidentCards)
            {
                card.ChangeHighlight(!isPlayer);
            }
        }

        public void SelectFightCard(CardFightModel card)
        {
            selectedFightCard = card;
        }

        public void DeselectFightCard(CardFightModel card)
        {
            selectedFightCard = null;
        }

        public void UseFightCard(CardFightModel card)
        {
            if (card.CheckCanUseCard())
            {
                useFightCard = card;
                BoxController.GetController<EffectsController>().ClickFightCard(card);
            }
        }

        public void StopUseFightCard(CardFightModel card)
        {
            useFightCard = null;
            BoxController.GetController<EffectsController>().StopUseFightCard();
        }

        public void EndUseCard(CardFightModel card)
        {
            card.EndUseCard();

            selectedFightCard = null;
            useFightCard = null;

            CharacterData playerData = BoxController.GetController<CharactersDataController>().GetPlayerData;
            CharacterData enemyData = BoxController.GetController<CharactersDataController>().GetEnemyData;
            CheckCanUseCards(playerFightcards, playerData, enemyData);
            CheckCanUseCards(EnemyFightCards, enemyData, playerData);

            BoxController.GetController<FightSceneController>().AddCountUseCards();

            EndUseFightCardEvent?.Invoke();
        }

        public void DecreaseReloadingCharacterCards(bool isPlayer)
        {
            if (isPlayer)
            {
                foreach (var card in playerFightcards)
                {
                    card.DecreaseReloading();
                }
            }
            else
            {
                foreach (var card in EnemyFightCards)
                {
                    card.DecreaseReloading();
                }
            }
        }

        public void HighlightPlayerFightCards(bool highlight)
        {
            foreach (var card in playerFightcards)
            {
                card.ChangeHighlight(highlight);
            }
        }

        private void CheckCanUseCards(List<CardFightModel> cards, CharacterData dataMy, CharacterData dataEnemy)
        {
            foreach (var card in cards)
            {
                bool canPay = true;
                bool canUse = false;

                foreach (var typeCost in card.GetTypesCost)
                {
                    if (card.GetValueCost < dataMy.GetValueAttribute(typeCost))
                    {
                        canPay = false;
                    }
                }

                if (canPay)
                {
                    foreach (var effect in card.GetEffects)
                    {
                        if (effect.TypeSelectTarget == TypeSelectTarget.Game)
                        {
                            if (effect is AttackEffect)
                            {
                                CheckCanUse((effect as AttackEffect).TypeTargetObjects, dataEnemy);
                            }
                            else if (effect is BuffEffect)
                            {
                                CheckCanUse((effect as BuffEffect).TypesTargetObjects, dataMy);
                            }
                            else if (effect is DefendEffect)
                            {
                                CheckCanUse((effect as DefendEffect).TypeDefends, dataMy);
                            }
                            else if (effect is OtherEffect)
                            {
                                OtherEffect otherEffect = effect as OtherEffect;
                                if (otherEffect.TypeOtherEffect == TypeOtherEffect.Loan)
                                {
                                    if (dataMy.GetIsActiveAttribute(otherEffect.UpAttribute))
                                    {
                                        CheckCanUse((otherEffect).TypeAttributesAfterLoan, dataEnemy);
                                    }
                                }
                            }
                        }
                    }
                }

                bool CheckCanUse(TypeAttribute[] attributes, CharacterData data)
                {
                    foreach (var target in attributes)
                    {
                        if (data.GetIsActiveAttribute(target))
                        {
                            canUse = true;
                        }
                    }

                    return false;
                }

                if (!canUse)
                {
                    Debug.Log($"<color=red>Block card {card.GetId}</color>");
                }

                card.SetCanUseCard = canUse;
            }
        }

        // For tutorial
        public void BlockAllCardsExceptOne()
        {
            for (int i = 0; i < playerFightcards.Count; i++)
            {
                if (i > 0)
                {
                    playerFightcards[i].BlockCard(true);
                }
            }
        }

        // For tutorial
        public void UnblockAllCardsExceptOne()
        {
            for (int i = 0; i < playerFightcards.Count; i++)
            {
                if (i != 0)
                {
                    playerFightcards[i].UnBlockCard();
                }
            }
        }
    }
}