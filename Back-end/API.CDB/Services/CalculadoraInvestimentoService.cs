using System;

namespace API.CDB.Services
{
    public interface ICalculadoraInvestimentoService
    {
        (double ValorBruto, double ValorLiquido) CalcularInvestimento(double valorInicial, int prazo);
    }

    public class CalculadoraInvestimentoService : ICalculadoraInvestimentoService
    {
        public (double ValorBruto, double ValorLiquido) CalcularInvestimento(double valorInicial, int prazo)
        {
            double taxaCDI = 0.009; // Taxa CDI de 0,9%
            double taxaBanco = 1.08;   // Taxa do banco de 108%
            double valorBruto = valorInicial;
            double valorLiquido = valorInicial;

            for (int i = 0; i < prazo; i++)
            {
                valorBruto = CalcularProximoMes(valorBruto, taxaCDI, taxaBanco);
                double imposto = CalcularImposto(valorBruto, prazo);
                valorLiquido = valorBruto - imposto;

               // System.Diagnostics.Debug.WriteLine($"Mês {i + 1}: Valor Bruto = {valorBruto}");
            }
            valorBruto = Math.Round(valorBruto, 3);
            valorLiquido = Math.Round(valorLiquido, 3);
            // System.Diagnostics.Debug.WriteLine($"Valor Líquido = {valorLiquido}");
            return (valorBruto, valorLiquido);
        }

        private double CalcularProximoMes(double valorAtual, double taxaCDI, double taxaBanco)
        {
            return valorAtual * (1 + (taxaCDI * taxaBanco));
        }

        private double CalcularImposto(double valorBruto, int prazo)
        {
            double CalcularTaxa(int p)
            {
                if (p <= 6) return 0.225;  // 22,5% para até 6 meses
                if (p <= 12) return 0.20;  // 20% para até 12 meses
                if (p <= 24) return 0.175; // 17,5% para até 24 meses
                return 0.15;               // 15% para acima de 24 meses
            }

            double taxa = CalcularTaxa(prazo);
            return valorBruto * taxa;
        }
    }    
}
