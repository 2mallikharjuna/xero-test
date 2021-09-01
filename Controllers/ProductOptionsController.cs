using System;
using System.Linq;
using Newtonsoft.Json;
using RefactorThis.Models;
using RefactorThis.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RefactorThis.Filters;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace RefactorThis.Controllers
{
    /// <summary>
    /// ProductOptions Controller class
    /// </summary>    
    [ApiController]
    [Route("api/ProductOptions")]    
    [FormatFilter]
    public class ProductOptionsController : ControllerBase
    {
        #region Members
        private readonly IProductOptionsService _productOptionsService;
        private ILogger<ProductOptionsController> Logger { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productOptionsService">Inject IProductOptionsService</param>
        /// <param name="logger">Inject ILogger</param>
        public ProductOptionsController(IProductOptionsService productOptionsService, ILogger<ProductOptionsController> logger)
        {
            _productOptionsService = productOptionsService ?? throw new ArgumentNullException(nameof(IProductOptionsService));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        #endregion
        #region Controller Methods
        /// <summary>
        /// Method to return the all the product options
        /// </summary>        
        /// <returns>Product options in json format</returns> 
        [RequestValidate]
        [ResultsFilter]
        [HttpGet("{productId}/options")]
        [ProducesResponseType(typeof(IEnumerable<ProductOption>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(BadRequestObjectResult))]
        public async Task<IActionResult> GetAllProductOptions(Guid productId)
        {
            try
            {
                Logger.LogDebug("Received a  GetAllProductOptions Request" + JsonConvert.SerializeObject(productId));
                var response = await _productOptionsService.GetAllProductOptionsAsync(productId); 
                if (response != null && response.Count() != 0)
                    return Ok(response);
                return NotFound();
            }
            catch (AggregateException aggEx)
            {
                string errorMsg = String.Empty;
                Logger.LogError("Error in Get Product Options. Requst: " + JsonConvert.SerializeObject(productId) + " Error " + aggEx);                
                aggEx.InnerExceptions.ToList().ForEach(ex => errorMsg += $" Error Message :{ex.Message} StackTace: {ex.StackTrace.ToString()} " + Environment.NewLine);
                return BadRequest(errorMsg);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetProductOptions. Requst: " + JsonConvert.SerializeObject(productId) + " Error " + ex);                
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Method to return the all the product options
        /// </summary>        
        /// <returns>Product options in json format</returns> 
        [RequestValidate]
        [ResultsFilter]
        [HttpGet("{productId}/options/{id}")]
        [ProducesResponseType(typeof(IEnumerable<ProductOption>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(BadRequestObjectResult))]
        public async Task<IActionResult> GetProductOptionById(Guid productId, Guid id)
        {
            try
            {
                Logger.LogDebug("Received a  GetProductOptionById Request : " + JsonConvert.SerializeObject(productId) + JsonConvert.SerializeObject(id));
                var response = await _productOptionsService.GetProductOptionAsync(productId, id);
                if (response != null)
                    return Ok(response);
                return NotFound();
            }
            catch (AggregateException aggEx)
            {
                string errorMsg = String.Empty;
                Logger.LogError("Error in Get ProductOptions By Id. Request: " + JsonConvert.SerializeObject(productId) + JsonConvert.SerializeObject(id) + " Error " + aggEx);
                aggEx.InnerExceptions.ToList().ForEach(ex => errorMsg += $" Error Message :{ex.Message} StackTace: {ex.StackTrace.ToString()} " + Environment.NewLine);
                return BadRequest(errorMsg);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetProductOptionById. Request: " + JsonConvert.SerializeObject(productId) + JsonConvert.SerializeObject(id) + " Error " + ex);
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Http Method to create product option
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="productOption"></param>
        /// <returns></returns>
        [RequestValidate]
        [ResultsFilter]
        [HttpPost("{productId}/options")]
        [ProducesResponseType(typeof(IEnumerable<ProductOption>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(BadRequestObjectResult))]
        public async Task<IActionResult> CreateProductOptionAsync(Guid productId, ProductOption productOption)
        {
            try
            {
                Logger.LogDebug("Received a  CreateProductOption Request" + JsonConvert.SerializeObject(productId));
                var response = await _productOptionsService.AddProductOptionAsync(productId, productOption); 
                if (response != null)
                    return Ok(response);
                return NotFound();
            }
            catch (AggregateException aggEx)
            {
                string errorMsg = String.Empty;
                Logger.LogError("Error in Creating Product Options. Request: " + JsonConvert.SerializeObject(productId) + " Error " + aggEx);
                aggEx.InnerExceptions.ToList().ForEach(ex => errorMsg += $" Error Message :{ex.Message} StackTace: {ex.StackTrace.ToString()} " + Environment.NewLine);
                return BadRequest(errorMsg);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in CreateProductOption. Request: " + JsonConvert.SerializeObject(productId) + " Error " + ex);
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Http Method to update the product option
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="id"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        [RequestValidate]
        [ResultsFilter]
        [HttpPut("{productId}/options/{id}")]
        [ProducesResponseType(typeof(IEnumerable<ProductOption>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(BadRequestObjectResult))]
        public async Task<IActionResult> UpdateProductOptionAsync(Guid productId, Guid id, ProductOption option)
        {
            try
            {
                Logger.LogDebug("Received an UpdateProductOption Async Request" + JsonConvert.SerializeObject(option));
                var response = await _productOptionsService.UpdateProductOptionAsync(productId, id, option);
                if (response != null)
                    return Ok(response);
                return NotFound();
            }
            catch (AggregateException aggEx)
            {
                string errorMsg = String.Empty;
                Logger.LogError("Error in Updating ProductOption. Request: " + JsonConvert.SerializeObject(option) + " Error " + aggEx);
                aggEx.InnerExceptions.ToList().ForEach(ex => errorMsg += $" Error Message :{ex.Message} StackTace: {ex.StackTrace.ToString()} " + Environment.NewLine);
                return BadRequest(errorMsg);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in UpdateProductOptionAsync. Request: " + JsonConvert.SerializeObject(option) + " Error " + ex);
                return BadRequest(ex);
            }
            
        }
        /// <summary>
        /// Http Method to Delete the produt option
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [RequestValidate]
        [ResultsFilter]
        [HttpDelete("{productId}/options/{id}")]
        [ProducesResponseType(typeof(IEnumerable<ProductOption>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(BadRequestObjectResult))]
        public async Task<IActionResult> DeleteProductOptionAsync(Guid productId, Guid id)
        {
            try
            {
                Logger.LogDebug("Received an DeleteProductOptionAsync Async Request" + JsonConvert.SerializeObject(productId) + JsonConvert.SerializeObject(id));
                var response = await _productOptionsService.DeleteProductOptionAsync(productId, id);
                if (response != null)
                    return Ok(response);
                return NotFound();
            }
            catch (AggregateException aggEx)
            {
                string errorMsg = String.Empty;
                Logger.LogError("Error in Deleting ProductOption. Request: " + JsonConvert.SerializeObject(productId) + JsonConvert.SerializeObject(id) + " Error " + aggEx);
                aggEx.InnerExceptions.ToList().ForEach(ex => errorMsg += $" Error Message :{ex.Message} StackTace: {ex.StackTrace.ToString()} " + Environment.NewLine);
                return BadRequest(errorMsg);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in Get DeleteProductOption. Request: " + JsonConvert.SerializeObject(productId) + JsonConvert.SerializeObject(id) + " Error " + ex);
                return BadRequest(ex);
            }
        }
#endregion
    }
}
