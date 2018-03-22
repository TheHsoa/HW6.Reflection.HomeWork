using NUnit.Framework;

namespace Reflection_test.Tests
{
    [TestFixture]
    public sealed class CatTests
    {
        [Test]
        public void CatLikeToEatMeat_False()
        {
            Assert.That(new Cat().InvokeLikeToEatMethod(new Meat()), Is.False);
        }
    }
}
