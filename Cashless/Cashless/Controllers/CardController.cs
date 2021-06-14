using System;
using Cashless.Domain.Card;
using Cashless.Domain.Card.Class;
using Cashless.Domain.Card.Interface;
using Cashless.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace Cashless.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class CardController : Controller
    {
        private ICard _card;
        public CardController(ICard card)
        {
            _card = card;
        }
        /// <summary>
        /// Save the card token
        /// </summary>
        /// <returns></returns>
        [HttpPost("save")]
        public ActionResult<Result<SaveCardResponse>> Save([FromBody]SaveCardRequest req)
        {
            try
            {
                var result = _card.SaveCard(req);
                return result.Success ? Ok(result) : StatusCode(500, result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, new Result<SaveCardResponse>()
                {
                    Data = null,
                    Error = ex.Message,
                    Success = false
                });
            }
            
        }
        
        
        /// <summary>
        /// Validates a card token
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("validate")]
        public ActionResult<Result<bool>> Validate([FromBody]ValidateCardTokenRequest req)
        {
            try
            {
                var result = _card.ValidateToken(req);
                return result.Success ? Ok(result) : StatusCode(500, result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, new Result<bool>()
                {
                    Data = false,
                    Error = ex.Message,
                    Success = false
                });
            }
            
        }
    }
}