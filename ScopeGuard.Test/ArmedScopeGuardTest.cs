using NUnit.Framework;


namespace ScopeGuard.Test
{
    public class ArmedScopeGuardTest
    {
        [Test]
        public void ShouldCallbackWhenArmed()
        {
            var testObj = new TestObj();

            ArmedScopeGuardSimpleReturn(testObj, shouldDisarm: false);

            Assert.True(testObj.HasDeScoped);
        }

        [Test]
        public void ShouldNotCallbackWhenDisarmed()
        {
            var testObj = new TestObj();

            ArmedScopeGuardSimpleReturn(testObj, shouldDisarm: true);

            Assert.False(testObj.HasDeScoped);
        }

        private static void ArmedScopeGuardSimpleReturn(TestObj testObj, bool shouldDisarm)
        {
            using var asg = new ArmedScopeGuard(() => { testObj.HasDeScoped = true; });

            if (!shouldDisarm)
                return;

            asg.Disarm();
        }
    }
}
