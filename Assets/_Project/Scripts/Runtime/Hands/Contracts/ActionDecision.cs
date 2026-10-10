using System;

namespace GuardianRealm.Hands.Contracts
{
    public readonly struct ActionDecision
    {
        private ActionDecision(bool accepted, ActionRejectionCode rejectionCode, string message)
        {
            Accepted = accepted;
            RejectionCode = rejectionCode;
            Message = message ?? string.Empty;
        }

        public bool Accepted { get; }

        public ActionRejectionCode RejectionCode { get; }

        public string Message { get; }

        public static ActionDecision Accept() => new ActionDecision(true, ActionRejectionCode.None, string.Empty);

        public static ActionDecision Reject(ActionRejectionCode code, string message = "")
        {
            if (code == ActionRejectionCode.None)
            {
                throw new ArgumentException("A rejected action requires a rejection code.", nameof(code));
            }

            return new ActionDecision(false, code, message);
        }
    }
}
