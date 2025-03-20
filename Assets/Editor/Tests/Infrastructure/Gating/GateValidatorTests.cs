using Infrastructure.Configuring;
using Infrastructure.Gating;
using Infrastructure.System;
using Infrastructure.Unity;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.Gating
{
    public class GateValidatorTests
    {
        private IGateDefinitionGetter _gateDefinitionGetter;
        private IProjectVersionGetter _projectVersionGetter;
        private IConfigValueGetter _configValueGetter;
        private IVersionComparer _versionComparer;

        private GateValidator _gateValidator;

        [SetUp]
        public void SetUp()
        {
            _gateDefinitionGetter = Substitute.For<IGateDefinitionGetter>();
            _projectVersionGetter = Substitute.For<IProjectVersionGetter>();
            _configValueGetter = Substitute.For<IConfigValueGetter>();
            _versionComparer = Substitute.For<IVersionComparer>();
        }

        [Test]
        public void Validate_GateKeyNull_ReturnsTrue()
        {
            const string projectVersion = "1.0";
            _projectVersionGetter.Get().Returns(projectVersion);
            _gateValidator = new GateValidator(_gateDefinitionGetter, _configValueGetter, _projectVersionGetter, _versionComparer);

            bool result = _gateValidator.Validate(null);

            Assert.IsTrue(result);
        }
    }
}