// ReSharper disable InconsistentNaming

using System;
using NUnit.Framework;
using Vanderbilt.Biostatistics.Wfccm2;

namespace ExpressionEvaluatorTests
{
    [TestFixture]
    public class UtcNowFunctionTests
    {
        private Expression func;

        [SetUp]
        public void init() { func = new Expression(""); }

        [TearDown]
        public void clear() { func.Clear(); }

        [Test]
        public void UtcNow_AllLower_NoException()
        {
            func.Function = "utcnow()";
        }

        [Test]
        public void UtcNow_HasCaps_NoException()
        {
            func.Function = "UtcNow()";
        }

        [Test]
        public void UtcNow_AllUpper_NoException()
        {
            func.Function = "UTCNOW()";
        }

        [Test]
        public void UtcNow_Evaluate_ReturnsDateTime()
        {
            func.Function = "utcnow()";
            var result = func.Evaluate<DateTime>();
            Assert.IsInstanceOf<DateTime>(result);
        }

        [Test]
        public void UtcNow_Evaluate_ReturnsUtcKind()
        {
            func.Function = "utcnow()";
            var result = func.Evaluate<DateTime>();
            Assert.AreEqual(DateTimeKind.Utc, result.Kind);
        }

        [Test]
        public void UtcNow_Evaluate_ReturnsCurrentUtcTime()
        {
            func.Function = "utcnow()";
            var before = DateTime.UtcNow;
            var result = func.Evaluate<DateTime>();
            var after = DateTime.UtcNow;

            Assert.GreaterOrEqual(result, before);
            Assert.LessOrEqual(result, after);
        }

        [Test]
        public void UtcNow_AddDays_IsCorrect()
        {
            func.Function = "utcnow() + days(1)";
            var before = DateTime.UtcNow;
            var result = func.Evaluate<DateTime>();
            var after = DateTime.UtcNow;

            Assert.GreaterOrEqual(result, before.AddDays(1));
            Assert.LessOrEqual(result, after.AddDays(1));
        }

        [Test]
        public void UtcNow_SubtractDays_IsCorrect()
        {
            func.Function = "utcnow() - days(1)";
            var before = DateTime.UtcNow;
            var result = func.Evaluate<DateTime>();
            var after = DateTime.UtcNow;

            Assert.GreaterOrEqual(result, before.AddDays(-1));
            Assert.LessOrEqual(result, after.AddDays(-1));
        }

        [Test]
        public void UtcNow_GreaterThanPastDate_IsTrue()
        {
            func.Function = "utcnow() > a";
            func.AddSetVariable("a", new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            Assert.IsTrue(func.EvaluateBoolean());
        }

        [Test]
        public void UtcNow_LessThanFutureDate_IsTrue()
        {
            func.Function = "utcnow() < a";
            func.AddSetVariable("a", new DateTime(2100, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            Assert.IsTrue(func.EvaluateBoolean());
        }

        [Test]
        public void UtcNow_GreaterOrEqualPastDate_IsTrue()
        {
            var testDate = DateTime.UtcNow.AddDays(-1);
            func.Function = "utcnow() >= a";
            func.AddSetVariable("a", testDate);
            Assert.IsTrue(func.EvaluateBoolean());
        }

        [Test]
        public void UtcNow_LessOrEqualFutureDate_IsTrue()
        {
            var testDate = DateTime.UtcNow.AddDays(1);
            func.Function = "utcnow() <= a";
            func.AddSetVariable("a", testDate);
            Assert.IsTrue(func.EvaluateBoolean());
        }

        [Test]
        public void UtcNow_SubtractVariable_ReturnsTimeSpan()
        {
            var pastDate = DateTime.UtcNow.AddDays(-1);
            func.Function = "totaldays(utcnow() - a)";
            func.AddSetVariable("a", pastDate);
            var result = func.EvaluateNumeric();

            Assert.GreaterOrEqual(result, 0.99);
            Assert.LessOrEqual(result, 1.01);
        }

        [Test]
        public void UtcNow_AddHours_IsCorrect()
        {
            func.Function = "utcnow() + hours(2)";
            var before = DateTime.UtcNow;
            var result = func.Evaluate<DateTime>();
            var after = DateTime.UtcNow;

            Assert.GreaterOrEqual(result, before.AddHours(2));
            Assert.LessOrEqual(result, after.AddHours(2));
        }

        [Test]
        public void UtcNow_AddMinutes_IsCorrect()
        {
            func.Function = "utcnow() + minutes(30)";
            var before = DateTime.UtcNow;
            var result = func.Evaluate<DateTime>();
            var after = DateTime.UtcNow;

            Assert.GreaterOrEqual(result, before.AddMinutes(30));
            Assert.LessOrEqual(result, after.AddMinutes(30));
        }
    }
}
