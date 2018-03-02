﻿namespace Castanha.Domain.Customers
{
    using System;
    using Castanha.Domain.ValueObjects;
    using Castanha.Domain.Customers.Events;
    using Castanha.Domain.Accounts;

    public class Customer : AggregateRoot
    {
        public Name Name { get; private set; }
        public PIN PIN { get; private set; }
        public AccountCollection Accounts { get; private set; }
        
        public Customer()
        {
            Register<RegisteredDomainEvent>(When);
        }

        public Customer(PIN pin, Name name)
            : this()
        {
            PIN = pin;
            Name = name;
        }

        public virtual void Register(Guid accountId, Credit credit)
        {
            var domainEvent = new RegisteredDomainEvent(
                Id, Version, Name, PIN,
                accountId,
                credit.Id,
                credit.Amount,
                credit.TransactionDate);

            Raise(domainEvent); 
        }

        protected void When(RegisteredDomainEvent domainEvent)
        {
            Id = domainEvent.AggregateRootId;
            Version = domainEvent.Version;
            Name = domainEvent.CustomerName;
            PIN = domainEvent.CustomerPIN;

            Accounts = new AccountCollection();
            Accounts.Add(domainEvent.AccountId);
        }
    }
}
