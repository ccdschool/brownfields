using NUnit.Framework;

namespace Parrot.Tests
{
    [TestFixture]
    public class ParrotTest
    {
        [Test]
        public void GetSpeed_NorwegianBlueParrotNailed_Return0()
        {
            var parrot = new Parrot(ParrotTypeEnum.NORWEGIAN_BLUE, 0, 0, true);

            var speed = parrot.GetSpeed();

            Assert.That(speed, Is.EqualTo(0.0));
        }

        [Test]
        public void GetSpeed_NorwegianBlueParrotNailedWithVoltage_Return0()
        {
            var parrot = new Parrot(ParrotTypeEnum.NORWEGIAN_BLUE, 0, 1.5, true);

            var speed = parrot.GetSpeed();

            Assert.That(speed, Is.EqualTo(0.0));
        }

        [Test]
        public void GetSpeed_NorwegianBlueParrotNotNailedWithVoltage_Return18()
        {
            var parrot = new Parrot(ParrotTypeEnum.NORWEGIAN_BLUE, 0, 1.5, false);

            var speed = parrot.GetSpeed();

            Assert.That(speed, Is.EqualTo(18.0));
        }

        [Test]
        public void GetSpeed_NorwegianBlueParrotNotNailedWithHighVoltage_Return24()
        {
            var parrot = new Parrot(ParrotTypeEnum.NORWEGIAN_BLUE, 0, 4, false);
            
            var speed = parrot.GetSpeed();

            Assert.That(speed, Is.EqualTo(24.0));
        }

        [Test]
        public void GetSpeed_OfAfricanParrotWitNoCoconuts_Return12()
        {
            var parrot = new Parrot(ParrotTypeEnum.AFRICAN, 0, 0, false);

            var speed = parrot.GetSpeed();

            Assert.That(speed, Is.EqualTo(12.0));
        }

        [Test]
        public void GetSpeed_OfAfricanParrotWithOneCoconut_Return3()
        {
            var parrot = new Parrot(ParrotTypeEnum.AFRICAN, 1, 0, false);

            var speed = parrot.GetSpeed();

            Assert.That(speed, Is.EqualTo(3.0));
        }

        [Test]
        public void GetSpeed_OfAfricanParrotWithTwoCoconuts_Return0()
        {
            var parrot = new Parrot(ParrotTypeEnum.AFRICAN, 2, 0, false);

            var speed = parrot.GetSpeed();

            Assert.That(speed, Is.EqualTo(0.0));
        }

        [Test]
        public void GetSpeed_OfEuropeanParrot_Return12()
        {
            var parrot = new Parrot(ParrotTypeEnum.EUROPEAN, 0, 0, false);

            var speed = parrot.GetSpeed();

            Assert.That(speed, Is.EqualTo(12.0));
        }

        [Test]
        public void GetCry_OfEuropeanParrot_ReturnSqoork()
        {
            var parrot = new Parrot(ParrotTypeEnum.EUROPEAN, 0, 0, false);

            var cry = parrot.GetCry();

            Assert.That(cry, Is.EqualTo("Sqoork!"));
        }

        [Test]
        public void GetCry_OfAfricanParrot_ReturnSqaark()
        {
            var parrot = new Parrot(ParrotTypeEnum.AFRICAN, 2, 0, false);

            var cry = parrot.GetCry();

            Assert.That(cry, Is.EqualTo("Sqaark!"));
        }

        [Test]
        public void GetCry_NorwegianBlueParrotWithHighVoltage_ReturnBzzzzzz()
        {
            var parrot = new Parrot(ParrotTypeEnum.NORWEGIAN_BLUE, 0, 4, false);

            var cry = parrot.GetCry();

            Assert.That(cry, Is.EqualTo("Bzzzzzz"));
        }

        [Test]
        public void GetCry_NorwegianBlueParroNoVoltage_ReturnDots()
        {
            var parrot = new Parrot(ParrotTypeEnum.NORWEGIAN_BLUE, 0, 0, false);
            
            var cry = parrot.GetCry();

            Assert.That(cry, Is.EqualTo("..."));
        }
    }
}