using System;
using Infrastructure.System;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.System
{
    public class ConverterTests
    {
        private Converter _converter;

        [SetUp]
        public void SetUp()
        {
            _converter = new Converter();
        }

        [Test]
        public void Convert_Success_DoesNotThrowException()
        {
            object value = "value";

            Assert.DoesNotThrow(() => { string _ = _converter.Convert<string>(value); });
        }

        [Test]
        public void Convert_SuccessButNull_ThrowsException()
        {
            const object value = null;

            InvalidOperationException invalidOperationException = Assert.Throws<InvalidOperationException>(() => { string _ = _converter.Convert<string>(value); });
            Assert.AreEqual("convertedValue cannot be null", invalidOperationException.Message);
        }

        [Test]
        public void Convert_Failure_ThrowsException()
        {
            object value = "value";

            InvalidOperationException invalidOperationException = Assert.Throws<InvalidOperationException>(() => { int _ = _converter.Convert<int>(value); });
            Assert.IsTrue(invalidOperationException.Message.StartsWith($"Cannot convert \"{value}\" to value of Type: {typeof(int)}. "));
        }
    }
}