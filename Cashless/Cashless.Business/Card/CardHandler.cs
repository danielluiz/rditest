using System;
using System.Collections.Generic;
using System.Linq;
using Cashless.Domain.Card.Class;
using Cashless.Domain.Card.Interface;
using Cashless.Domain.Common;

namespace Cashless.Business.Card
{
    public class CardHandler : ICard
    {
        private static int _savedCardId = 1; 
        public Result<SaveCardResponse> SaveCard(SaveCardRequest req)
        {
            var last4 = GetLast4(req.CardNumber);

            var savedCardId = _savedCardId;
            var last4String = new string(last4.ToArray());
            Storage.Storage.Cards.Add(new SavedCard()
            {
                Id = savedCardId,
                Last4 = int.Parse(last4String),
                CustomerId = req.CustomerId
            });
            _savedCardId++;

            
            var token = GetToken(req.CVV, last4);
            return new Result<SaveCardResponse>()
            {
                Success = true,
                Data = new SaveCardResponse()
                {
                    Token = token,
                    CardId = savedCardId,
                    RegistrationDate = DateTime.UtcNow
                }
            };

        }

        private static List<char> GetLast4(long cardNumber)
        {
            var cardNumberStr = cardNumber.ToString().ToCharArray();
            var last4 = cardNumberStr.Skip(cardNumberStr.Length - 4).ToList();
            return last4;
        }

        private static int GetToken(int cvv, List<char> last4)
        {
            for (int i = 0; i < cvv; i++)
            {
                var lastIndex = last4.Count - 1;
                var first = last4[0];
                for (int j = 0; j < last4.Count; j++)
                {
                    if (j + 1 < last4.Count)
                        last4[j] = last4[j + 1];
                }

                last4[lastIndex] = first;
            }

            var token = int.Parse(new string(last4.ToArray()));
            return token;
        }

        public Result<bool> ValidateToken(ValidateCardTokenRequest req)
        {
            var card = Storage.Storage.Cards.FirstOrDefault(x => x.Id == req.CardId);
            if (card == null)
                return new Result<bool>()
                {
                    Success = false,
                    Error = "Card Id does not exist",
                    Data = false
                };
            
            if (card.CustomerId != req.CustomerId)
                return new Result<bool>()
                {
                    Success = false,
                    Error = "Card does not belong to customer",
                    Data = false
                };

            var last4 = card.Last4;
            var token = GetToken(req.CVV, last4.ToString().ToCharArray().ToList());
            if (req.Token != token) 
                return new Result<bool>()
                {
                    Success = true,
                    Error = "Token is not valid",
                    Data = false
                };

            return new Result<bool>()
            {
                Success = true,
                Error = null,
                Data = true
            };
        }
    }
}