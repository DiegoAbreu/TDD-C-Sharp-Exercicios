using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.VisualStudio.TestAdapter;
using NuGet.VisualStudio;


namespace Caelum.Leilao
{
    [TestFixture]
    public class TesteDoAvaliador
    {
        private Avaliador leiloeiro;
        private Usuario joao;
        private Usuario jose;
        private Usuario maria;

        [SetUp]
        public void criaAvaliador()
        {
            this.leiloeiro = new Avaliador();
            this.joao = new Usuario("Joao");
            this.jose = new Usuario("Jose");
            this.maria = new Usuario("Maria");
        }

        [Test] //teste 1: Lances em ordem crescente
        public void DeveEntenderLancesEmOrdemCrescente()
        {
            //1a parte: cenario
            Leilao leilao = new CriadorDeLeilao().Para("Playstation 4 Novo")
            .Lance(maria, 250.0)
            .Lance(joao, 300.0)
            .Lance(jose, 400.0)
            .Constroi();

            //2a parte: acao
            leiloeiro.Avalia(leilao);

            //3a parte: validacao
            double maiorEsperado = 400;
            double menorEsperado = 250;

            Assert.AreEqual(maiorEsperado, leiloeiro.MaiorLance);
            Assert.AreEqual(menorEsperado, leiloeiro.MenorLance);

        }

        [Test] //teste 2: Lances em ordem crescente com outros valores
        public void DeveEntenderLancesEmOrdemCrescenteComOutrosValores()
        {
            //1a parte: cenario
            Leilao leilao = new CriadorDeLeilao().Para("Playstation 4 Novo")
            .Lance(maria, 1000.0)
            .Lance(joao, 2000.0)
            .Lance(jose, 3000.0)
            .Constroi();

            //2a parte: acao
            leiloeiro.Avalia(leilao);

            //3a parte: validacao
            Assert.AreEqual(3000, leiloeiro.MaiorLance);
            Assert.AreEqual(1000, leiloeiro.MenorLance);

        }
        [Test] //teste 3: Leilao com um unico lance
        public void DeveEntenderLeilaoComApenasUmLance()
        {
            //1a parte: cenario
            Leilao leilao = new CriadorDeLeilao().Para("Playstation 4 Novo")
                .Lance(joao, 1000.0)
                .Constroi();

            //2a parte: acao
            leiloeiro.Avalia(leilao);

            //3a parte: validacao
            Assert.AreEqual(1000, leiloeiro.MaiorLance, 0.0001);
            Assert.AreEqual(1000, leiloeiro.MenorLance, 0.0001);

        }
        [Test] //teste 4: Lances em ordem Decrescente
        public void DeveEntenderLancesEmOrdemDecrescente()
        {
            //1a parte: cenario
            Leilao leilao = new CriadorDeLeilao().Para("Playstation 4 Novo")
            .Lance(joao, 3000.0)
            .Lance(maria, 2000.0)
            .Lance(jose, 1000.0)
            .Constroi();

            //2a parte: acao
            leiloeiro.Avalia(leilao);

            //3a parte: validacao
            Assert.AreEqual(3000, leiloeiro.MaiorLance);
            Assert.AreEqual(1000, leiloeiro.MenorLance);
        }
        [Test] //teste 5: Lances em ordem Randomica
        public void DeveEntenderLancesEmOrdemRandomica()
        {
            //1a parte: cenario
            Leilao leilao = new CriadorDeLeilao().Para("Playstation 4 Novo")
                .Lance(joao, 500.0)
                .Lance(maria, 7000.0)
                .Lance(jose, 1000.0)
                .Constroi();

            //2a parte: acao
            leiloeiro.Avalia(leilao);

            //3a parte: validacao
            Assert.AreEqual(7000, leiloeiro.MaiorLance);
            Assert.AreEqual(500, leiloeiro.MenorLance);
        }
        [Test] //teste 6: Encontra os tres maiores lances
        public void DeveEncontrarOsTresMaioresLances()
        {
            //1a parte: cenario
            Leilao leilao = new CriadorDeLeilao().Para("Playstation 4 Novo")
                .Lance(joao, 500.0)
                .Lance(maria, 600.0)
                .Lance(joao, 800.0)
                .Lance(maria, 1200.0)
                .Lance(joao, 1500.0)
                .Lance(maria, 1700.0)
                .Constroi();

            //2a parte: acao
            leiloeiro.Avalia(leilao);
            var maiores = leiloeiro.TresMaiores;

            //3a parte: validacao
            Assert.AreEqual(3, maiores.Count);
            Assert.AreEqual(1700.0, maiores[0].Valor, 0.0001);
            Assert.AreEqual(1500.0, maiores[1].Valor, 0.0001);
            Assert.AreEqual(1200.0, maiores[2].Valor, 0.0001);
        }

        [Test]//leilao sem lances
        [ExpectedException( typeof( Exception))]
        public void NaoDeveAvaliarLeilaoSemLances()
        {
                Leilao leilao = new CriadorDeLeilao().Para("Geladeira")
                .Constroi();

                leiloeiro.Avalia(leilao);
        }
    }
}
   

