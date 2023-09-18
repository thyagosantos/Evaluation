using System;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using API.CDB.Models;
using API.CDB.Services;
using System.Web.Http.Cors;

namespace API.CDB.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class InvestimentoController : ApiController
    {       

        public InvestimentoController()
        {
           
        }

        [HttpPost]
        [Route("api/cdb/calcularInvestimento")]
        [SwaggerResponse(200, "Resultado do investimento", typeof(InvestimentoResponse))]
        [SwaggerResponse(400, "Dados de entrada inválidos")]
        [SwaggerResponse(500, "Erro interno do servidor")]
        public IHttpActionResult CalcularInvestimento([FromBody] InvestimentoRequest request)
        {
            if (request == null)
            {
                return BadRequest("Dados de entrada inválidos.");
            }

            if (request.ValorInicial <= 0 || request.Prazo <= 0)
            {
                return BadRequest("Valor inicial e prazo devem ser maiores que zero.");
            }

            try
            {
                var calculadoraInvestimentoService = new CalculadoraInvestimentoService();

                
                (double valorBruto, double valorLiquido) = calculadoraInvestimentoService.CalcularInvestimento(request.ValorInicial, request.Prazo);

                var resultado = new InvestimentoResponse
                {
                    ValorBruto = valorBruto,
                    ValorLiquido = valorLiquido
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
