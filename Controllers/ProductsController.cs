using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RefactorThis.Filters;
using RefactorThis.Models;
using RefactorThis.Services;

namespace RefactorThis.Controllers
{
    //// <summary>
    /// Products Controller to define API endpoints CRUD
    /// </summary>
    [Route("api/Products")]
    [ApiController]
    [FormatFilter]
    
    public class ProductsController : ControllerBase
    {
        #region Members
        private readonly IProductService _productService;
        private ILogger<ProductsController> Logger { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="productService">Inject IProductService</param>
        /// <param name="logger">Inject ProductsController logger</param>
        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(IProductService));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        #endregion
        #region Controller Methods
        /// <summary>
        /// Method to return the all the products
        /// </summary>        
        /// <returns>Products in json format</returns>         
        [HttpGet("GetAllProducts")]
        [ResultsFilter]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(BadRequestObjectResult))]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            try
            {
                var result = await _productService.GetAllProductsAsync();
                if (result != null && result.Count() != 0)
                    return Ok(result);
                return NotFound();
            }
            catch (AggregateException aggEx)
            {
                string errorMsg = String.Empty;
                Logger.LogError("Error in Get All Products. Request: " + " Error " + aggEx);
                aggEx.InnerExceptions.ToList().ForEach(ex => errorMsg += $" Error Message :{ex.Message} StackTace: {ex.StackTrace.ToString()} " + Environment.NewLine);
                return BadRequest(errorMsg);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetAllProductsAsync. Request: " + " Error " + ex);
                return BadRequest(ex);
            }
        }


        /// <summary>
        /// Method to return the selected product Id
        /// </summary>        
        /// <returns>Products in json format</returns>         
        [HttpGet("{id}")]
        [RequestValidate]
        [ResultsFilter]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(BadRequestObjectResult))]
        public async Task<IActionResult> GetProductByIdAsync(Guid id)
        {
            try
            { 
                var result = await _productService.GetProductByIdAsync(id);
                if (result != null)
                    return Ok(result);
                return NotFound();
            }
            catch (AggregateException aggEx)
            {
                string errorMsg = String.Empty;
                Logger.LogError("Error in Get Matching Product. Request: " + " Error " + aggEx);
                aggEx.InnerExceptions.ToList().ForEach(ex => errorMsg += $" Error Message :{ex.Message} StackTace: {ex.StackTrace.ToString()} " + Environment.NewLine);
                return BadRequest(errorMsg);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetProductByIdAsync. Request: " + " Error " + ex);
                return BadRequest(ex);
            }
        }

        [HttpPost("AddProductAsync")]
        [RequestValidate]        
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(BadRequestObjectResult))]
        public async Task<IActionResult> CreateProductAsync([FromBody]Product product)
        {
            try
            { 
                var result = await _productService.AddProductAsync(product);
                if (result != null)
                    return Ok(result);
                return NotFound();
            }
            catch (AggregateException aggEx)
            {
                string errorMsg = String.Empty;
                Logger.LogError("Error in Create Product. Request: " + JsonConvert.SerializeObject(product) + " Error " + aggEx);
                aggEx.InnerExceptions.ToList().ForEach(ex => errorMsg += $" Error Message :{ex.Message} StackTace: {ex.StackTrace.ToString()} " + Environment.NewLine);
                return BadRequest(errorMsg);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in CreateProductAsync. Request: " + JsonConvert.SerializeObject(product) + " Error " + ex);
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        [RequestValidate]
        [ResultsFilter]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(BadRequestObjectResult))]
        public async Task<IActionResult> UpdateProductAsync(Guid id, Product product)
        {
            try
            {
                var result = await _productService.UpdateProductAsync(id, product);
                if (result != null)
                    return Ok(result);
                return NotFound();
            }
            catch (AggregateException aggEx)
            {
                string errorMsg = String.Empty;
                Logger.LogError("Error in Update Product. Request: " + JsonConvert.SerializeObject(product) + " Error " + aggEx);
                aggEx.InnerExceptions.ToList().ForEach(ex => errorMsg += $" Error Message :{ex.Message} StackTace: {ex.StackTrace.ToString()} " + Environment.NewLine);
                return BadRequest(errorMsg);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in UpdateProductAsync. Request: " + JsonConvert.SerializeObject(product) + " Error " + ex);
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        [RequestValidate]
        [ResultsFilter]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(BadRequestObjectResult))]
        public async Task<IActionResult> DeleteProductAsync(Guid productId)
        {
            try
            {
                var response = await _productService.DeleteProductAsync(productId);
                if (response != null)
                    return Ok(response);
                return NotFound();
            }
            catch (AggregateException aggEx)
            {
                string errorMsg = String.Empty;
                Logger.LogError("Error in Delete Product. Request: " + JsonConvert.SerializeObject(productId) + " Error " + aggEx);
                aggEx.InnerExceptions.ToList().ForEach(ex => errorMsg += $" Error Message :{ex.Message} StackTace: {ex.StackTrace.ToString()} " + Environment.NewLine);
                return BadRequest(errorMsg);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in Get DeleteProductAsync. Request: " + JsonConvert.SerializeObject(productId) +  " Error " + ex);
                return BadRequest(ex);
            }
        }
        #endregion
    }
}