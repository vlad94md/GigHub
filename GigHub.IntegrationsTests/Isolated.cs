using NUnit.Framework;
using System;
using System.Transactions;

namespace GigHub.IntegrationsTests
{
    public class Isolated : Attribute, ITestAction
    {
        private TransactionScope _transactionScope;
        public void BeforeTest(TestDetails testDetails)
        {
            _transactionScope = new TransactionScope();
        }

        public void AfterTest(TestDetails testDetails)
        {
            _transactionScope.Dispose();
        }

        public ActionTargets Targets
        {
            get
            {
                return ActionTargets.Test;
            }
        }
    }
}
