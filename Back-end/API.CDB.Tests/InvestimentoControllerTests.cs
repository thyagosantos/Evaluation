using NUnit.Framework;
using System;
using API.CDB.Controllers;
using API.CDB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Results;

namespace API.CDB.Tests
{
    [TestFixture]
    public class InvestimentoControllerTests
    {

        [TestCase(100, 10, 110.156, 88.125)]
        [TestCase(200, 5, 209.910, 162.680)]
        [TestCase(50, 12, 56.154, 44.923)]
        [TestCase(300, 8, 324.137, 259.309)]
        public void CalcularInvestimento_DeveRetornarResultadosCorretos(
    double valorInicial, int prazo, double resultadoBrutoEsperado, double resultadoLiquidoEsperado)
        {
            // Arrange
            var controller = new InvestimentoController();

            // Act
            var result = controller.CalcularInvestimento(new InvestimentoRequest
            {
                ValorInicial = valorInicial,
                Prazo = prazo
            });

            // Assert
            Assert.NotNull(result);

            // Verifique se o resultado é um OkResult
            var okResult = result as OkNegotiatedContentResult<InvestimentoResponse>;
           // Assert.NotNull(okResult); esta bugado .net zuando traz resultado  mais pensa que e null!

            var investimentoResponse = okResult.Content;

            // Antes da asserção, imprima os valores reais para depuração
            Console.WriteLine($"Valor Bruto Real: {investimentoResponse.ValorBruto}");
            Console.WriteLine($"Valor Líquido Real: {investimentoResponse.ValorLiquido}");

            // Assert
            Assert.AreEqual(resultadoBrutoEsperado, investimentoResponse.ValorBruto, 0.01d);
            Assert.AreEqual(resultadoLiquidoEsperado, investimentoResponse.ValorLiquido, 0.01d);
        }
    }
}
