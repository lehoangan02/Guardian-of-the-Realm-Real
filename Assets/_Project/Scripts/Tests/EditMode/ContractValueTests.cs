using System;
using NUnit.Framework;
using UnityEngine;

namespace GuardianRealm.Hands.Contracts.Tests
{
    public sealed class ContractValueTests
    {
        [Test]
        public void InteractionId_RejectsZero()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new InteractionId(0));
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new InteractionContext(default, HandSide.Left, 0d));
        }

        [Test]
        public void ArrowRequest_NormalizesAimAndPreservesBoardLocalData()
        {
            var context = new InteractionContext(new InteractionId(7), HandSide.Right, 12.5d);
            var request = new ArrowFireRequest(
                context,
                new Vector3(1f, 2f, 3f),
                new Vector3(0f, 0f, 4f),
                0.75f,
                new Vector3(4f, 0f, 8f));

            Assert.That(request.BoardLocalAimDirection, Is.EqualTo(Vector3.forward));
            Assert.That(request.NormalizedPower, Is.EqualTo(0.75f));
            Assert.That(request.Context.Id, Is.EqualTo(new InteractionId(7)));
        }

        [TestCase(-0.01f)]
        [TestCase(1.01f)]
        public void ArrowRequest_RejectsPowerOutsideUnitInterval(float power)
        {
            var context = new InteractionContext(new InteractionId(1), HandSide.Left, 0d);

            Assert.Throws<ArgumentOutOfRangeException>(() => new ArrowFireRequest(
                context,
                Vector3.zero,
                Vector3.forward,
                power,
                Vector3.zero));
        }

        [Test]
        public void RejectedDecision_RequiresReason()
        {
            Assert.Throws<ArgumentException>(() => ActionDecision.Reject(ActionRejectionCode.None));
        }

        [Test]
        public void EntityId_UsesOrdinalEquality()
        {
            Assert.That(new GameEntityId("hero-01"), Is.EqualTo(new GameEntityId("hero-01")));
            Assert.That(new GameEntityId("hero-01"), Is.Not.EqualTo(new GameEntityId("HERO-01")));
        }

        [Test]
        public void Requests_RejectDefaultEntityIds()
        {
            var context = new InteractionContext(new InteractionId(1), HandSide.Left, 0d);

            Assert.Throws<ArgumentException>(() =>
                new HeroMoveRequest(context, default, Vector3.zero));
            Assert.Throws<ArgumentException>(() =>
                new BuildActionRequest(context, default, new GameEntityId("build")));
        }
    }
}
