using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Caelum.Leilao
{
    [TestFixture]
    public class LeilaoTest
    {
        [Test] //leilao com um lance
        public void DeveReceberUmLance()
        {
            Leilao leilao = new Leilao("MacBook Pro 15");
            Assert.AreEqual(0, leilao.Lances.Count);

            var usuario = new Usuario("Steve Jobs");
            leilao.Propoe(new Lance(usuario, 2000));

            Assert.AreEqual(1, leilao.Lances.Count);
            Assert.AreEqual(2000, leilao.Lances[0].Valor, 0.00001);
        }
        [Test] //Deve com varios lances
        public void DeveReceberVariosLances()
        {
            Leilao leilao = new Leilao("MacBook Pro 15");
            Assert.AreEqual(0, leilao.Lances.Count);

            var usuario = new Usuario("Steve Jobs");
            var usuario1 = new Usuario("Steve Wozniac");
            leilao.Propoe(new Lance(usuario, 2000));
            leilao.Propoe(new Lance(usuario1, 3000));

            Assert.AreEqual(2, leilao.Lances.Count);
            Assert.AreEqual(2000, leilao.Lances[0].Valor, 0.00001);
            Assert.AreEqual(3000, leilao.Lances[1].Valor, 0.00001);
        }
        [Test] //Nao Deve aceitar lances seguidos de um mesmo usuario
        public void NaoDeveAceitarDoisLancesSeguidosDoMesmoUsuario()
        {
            Leilao leilao = new Leilao("MacBook Pro 15");
            Assert.AreEqual(0, leilao.Lances.Count);

            var usuario = new Usuario("Steve Jobs");
            var usuario1 = new Usuario("Steve Wozniac");
            leilao.Propoe(new Lance(usuario, 2000));
            leilao.Propoe(new Lance(usuario, 2500));
            leilao.Propoe(new Lance(usuario1, 3000));

            Assert.AreEqual(2, leilao.Lances.Count);
            Assert.AreEqual(2000, leilao.Lances[0].Valor, 0.00001);
            Assert.AreEqual(3000, leilao.Lances[1].Valor, 0.00001);
        }

        [Test] //Nao Deve aceitar mais que 5 lances de um mesmo usuario
        public void NaoDeveAceitarMaisDoQueCincoLancesDeUmMesmoUsuario()
        {
            Leilao leilao = new Leilao("MacBook Pro 15");
            Assert.AreEqual(0, leilao.Lances.Count);

            var usuario = new Usuario("Steve Jobs");
            var usuario1 = new Usuario("Steve Wozniac");
            leilao.Propoe(new Lance(usuario, 2000));
            leilao.Propoe(new Lance(usuario1, 3000));

            leilao.Propoe(new Lance(usuario, 4000));
            leilao.Propoe(new Lance(usuario1, 5000));

            leilao.Propoe(new Lance(usuario, 5500));
            leilao.Propoe(new Lance(usuario1, 5600));

            leilao.Propoe(new Lance(usuario, 6000));
            leilao.Propoe(new Lance(usuario1, 6200));

            leilao.Propoe(new Lance(usuario, 6500));
            leilao.Propoe(new Lance(usuario1, 6800));

            //Esses lances devem ser ignorados
            leilao.Propoe(new Lance(usuario, 7000));
            leilao.Propoe(new Lance(usuario1, 7500));

            Assert.AreEqual(10, leilao.Lances.Count);
            var ultimo = leilao.Lances.Count - 1;
            Lance ultimoLance = leilao.Lances[ultimo];

            Assert.AreEqual(6800, ultimoLance.Valor, 0.00001);
         
        }
    }
}
  
