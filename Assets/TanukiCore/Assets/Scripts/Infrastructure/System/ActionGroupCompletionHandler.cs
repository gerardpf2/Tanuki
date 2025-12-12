using System;
using ArgumentOutOfRangeException = Infrastructure.System.Exceptions.ArgumentOutOfRangeException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Infrastructure.System
{
    // TODO: Test
    public class ActionGroupCompletionHandler
    {
        private int _amount;
        private readonly Action _onComplete;

        public ActionGroupCompletionHandler(int amount, Action onComplete)
        {
            ArgumentOutOfRangeException.ThrowIfNot(amount, ComparisonOperator.GreaterThan, 0);

            _amount = amount;
            _onComplete = onComplete;
        }

        public void RegisterCompleted()
        {
            InvalidOperationException.ThrowIfNot(_amount, ComparisonOperator.GreaterThan, 0);

            --_amount;

            if (_amount == 0)
            {
                _onComplete?.Invoke();
            }
        }
    }
}