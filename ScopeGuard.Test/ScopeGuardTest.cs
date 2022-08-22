using System;
using NUnit.Framework;


namespace ScopeGuard.Test
{
    public class ScopeGuardTest
    {
        [Test]
        public void ShouldDisposeNormally()
        {
            var testObj = new TestObj();

            ScopeGuardSimpleReturn(testObj, shouldThrow: false);

            Assert.True(testObj.HasDeScoped);
        }

        [Test]
        public void ShouldDisposeOnException()
        {
            var testObj = new TestObj();

            try
            {
                ScopeGuardSimpleReturn(testObj, shouldThrow: true);
            }
            catch (Exception)
            {
                // ignored
            }

            Assert.True(testObj.HasDeScoped);
        }

        [Test]
        public void AnonymousScope()
        {
            var testObj = new TestObj();

            {
                using var sg = new ScopeGuard(() => { testObj.HasDeScoped = true; });
            }

            Assert.True(testObj.HasDeScoped);
        }

        [Test]
        public void CallbackNotNull()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var sg = new ScopeGuard(callback: null);
            });
        }

        private static void ScopeGuardSimpleReturn(TestObj testObj, bool shouldThrow)
        {
            using var sg = new ScopeGuard(() => { testObj.HasDeScoped = true; });
            Assert.False(testObj.HasDeScoped);

            if (shouldThrow)
                throw new Exception(message: "Oh no!");
        }
    }
}
